namespace LazyEvo.PVEBehavior
{
    using LazyEvo.Public;
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.Combat;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;

    internal class PVEBehaviorCombat : CombatEngine
    {
        private const int Avoidaddsdistance = 30;
        private static readonly KeyWrapper PetAttackKey = new KeyWrapper("PetAttack", "Ctrl", "Indifferent", "1");
        private static readonly KeyWrapper PetFollow = new KeyWrapper("PetFollow", "Ctrl", "Indifferent", "2");
        internal static BehaviorController Behavior;
        internal static string OurDirectory;
        private readonly Ticker _addBackup = new Ticker(4000.0);

        public override void BotStarted()
        {
            if (File.Exists(OurDirectory + @"\Behaviors\" + PveBehaviorSettings.LoadedBeharvior + ".xml"))
            {
                Behavior = new BehaviorController();
                Behavior.Load(OurDirectory + @"\Behaviors\" + PveBehaviorSettings.LoadedBeharvior + ".xml");
            }
            this.CheckBuffAndKeys(Behavior.PullController.GetRules);
            this.CheckBuffAndKeys(Behavior.CombatController.GetRules);
            this.CheckBuffAndKeys(Behavior.BuffController.GetRules);
            this.CheckBuffAndKeys(Behavior.RestController.GetRules);
            Behavior.CombatController.GetRules.Sort();
            Behavior.BuffController.GetRules.Sort();
            Behavior.BuffController.GetRules.Sort();
            Behavior.PrePullController.GetRules.Sort();
            Behavior.PullController.GetRules.Sort();
        }

        private void Buff()
        {
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                foreach (Rule rule in from rule in Behavior.BuffController.GetRules
                    where rule.IsOk
                    select rule)
                {
                    rule.ExecuteAction(Behavior.GlobalCooldown);
                }
            }
        }

        private void CheckBuffAndKeys(IEnumerable<Rule> rules)
        {
            if ((rules != null) && (rules.Count<Rule>() != 0))
            {
                foreach (Rule rule in rules)
                {
                    try
                    {
                        if (!rule.IsScript)
                        {
                            rule.BotStarting();
                            if (!rule.Action.DoesKeyExist)
                            {
                                Logging.Write(LogType.Warning, "Key: " + rule.Action.Name + " does not exist on your bars", new object[0]);
                            }
                            foreach (AbstractCondition condition in rule.GetConditions)
                            {
                                if ((condition is BuffCondition) && (!string.IsNullOrEmpty(((BuffCondition) condition).GetBuffName()) && !BarMapper.DoesBuffExist(((BuffCondition) condition).GetBuffName())))
                                {
                                    Logging.Write(LogType.Warning, "Buff: " + ((BuffCondition) condition).GetBuffName() + " does not exist in HasWellKnownBuff will not detect it correctly", new object[0]);
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        Logging.Debug("Error checking rule: " + exception, new object[0]);
                    }
                }
            }
        }

        public override void Combat(PUnit target)
        {
            if (Behavior.UseAutoAttack)
            {
                target.InteractWithTarget();
            }
            if (Behavior.SendPet)
            {
                PetAttackKey.SendKey();
            }
            while (true)
            {
                try
                {
                    if (target.DistanceToSelf > Behavior.CombatDistance)
                    {
                        MoveHelper.MoveToUnit(target, (double) Behavior.CombatDistance);
                    }
                }
                catch
                {
                }
                if (!target.Location.IsFacing())
                {
                    Logging.Debug("We are facing the wrong way?", new object[0]);
                    target.Face();
                }
                if (!LazyLib.Wow.ObjectManager.MyPlayer.IsValid || !ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, target))
                {
                    target.TargetHostile();
                }
                if (PveBehaviorSettings.AvoidAddsCombat)
                {
                    this.ConsiderAvoidAdds(target);
                }
                foreach (Rule rule in from rule in Behavior.CombatController.GetRules
                    where rule.IsOk
                    select rule)
                {
                    if (target.IsValid && target.IsAlive)
                    {
                        if (!target.Location.IsFacing())
                        {
                            target.Face();
                        }
                        rule.ExecuteAction(Behavior.GlobalCooldown);
                        break;
                    }
                }
                Thread.Sleep(10);
                Application.DoEvents();
            }
        }

        internal void ConsiderAvoidAdds(PUnit target)
        {
            bool isValid = false;
            List<PUnit> list = LazyLib.Wow.ObjectManager.CheckForMobsAtLoc(target.Location, (float) (PveBehaviorSettings.SkipAddsDis + 5), false);
            if (list.Count != 0)
            {
                PUnit closestBesides = GetClosestBesides(list, target);
                if (((closestBesides != null) && (closestBesides.GUID != target.GUID)) && (closestBesides.DistanceToSelf < 30.0))
                {
                    Logging.Write(string.Concat(new object[] { "Possible add: ", closestBesides.Name, ": ", closestBesides.DistanceToSelf }), new object[0]);
                    this._addBackup.Reset();
                    Ticker ticker = new Ticker(3000.0);
                    closestBesides.Face();
                    MoveHelper.Backwards(true);
                    if (LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet)
                    {
                        isValid = LazyLib.Wow.ObjectManager.MyPlayer.Pet.Target.IsValid;
                        PetFollow.SendKey();
                    }
                    while (true)
                    {
                        if (!ticker.IsReady)
                        {
                            Thread.Sleep(10);
                            closestBesides.Face();
                            if (closestBesides.DistanceToSelf <= 36.0)
                            {
                                continue;
                            }
                        }
                        MoveHelper.Backwards(false);
                        if (LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet && isValid)
                        {
                            PetAttackKey.SendKey();
                        }
                        this._addBackup.Reset();
                        break;
                    }
                }
            }
        }

        private static PUnit GetClosestBesides(IEnumerable<PUnit> list, PUnit ignore)
        {
            double maxValue = double.MaxValue;
            PUnit unit = null;
            foreach (PUnit unit2 in list)
            {
                if ((unit2.DistanceToSelf < maxValue) && ((ignore.GUID != unit2.GUID) && (!ignore.IsPet && (!unit2.IsInCombat && (!unit2.IsTargetingMe && !unit2.IsTargetingMyPet)))))
                {
                    maxValue = unit2.DistanceToSelf;
                    unit = unit2;
                }
            }
            return unit;
        }

        public override void OnRess()
        {
            this.Buff();
        }

        public void PrePull(PUnit target)
        {
            if (target.DistanceToSelf > Behavior.PrePullDistance)
            {
                MoveHelper.MoveToUnit(target, (double) Behavior.PrePullDistance);
            }
            foreach (Rule rule in from rule in Behavior.PrePullController.GetRules
                where rule.IsOk
                select rule)
            {
                rule.ExecuteAction(Behavior.GlobalCooldown);
            }
        }

        public override PullResult Pull(PUnit target)
        {
            this.Buff();
            this.PrePull(target);
            if (Behavior.UseAutoAttack)
            {
                target.InteractWithTarget();
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.RedMessage.Contains("Target not in line"))
            {
                Logging.Debug("Target is not in line of sight", new object[0]);
            }
            if (!MoveHelper.MoveToUnit(target, (double) Behavior.PullDistance))
            {
                return PullResult.CouldNotPull;
            }
            if (Behavior.SendPet)
            {
                PetAttackKey.SendKey();
            }
            foreach (Rule rule in from rule in Behavior.PullController.GetRules
                where rule.IsOk
                select rule)
            {
                target.Face();
                rule.ExecuteAction(Behavior.GlobalCooldown);
            }
            return (!PPullBlackList.IsBlacklisted(target) ? PullResult.Success : PullResult.CouldNotPull);
        }

        public override void Rest()
        {
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                Behavior.RestController.GetRules.Sort();
                foreach (Rule rule in from rule in Behavior.RestController.GetRules
                    where rule.IsOk
                    select rule)
                {
                    rule.ExecuteAction(Behavior.GlobalCooldown);
                }
                this.Buff();
            }
        }

        public override void RunningAction()
        {
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                try
                {
                    if (Behavior.BuffController.GetRules != null)
                    {
                        CS$<>9__CachedAnonymousMethodDelegate3 ??= rule => !ReferenceEquals(rule, null);
                        using (IEnumerator<Rule> enumerator = Enumerable.Where<Rule>(from rule in Behavior.BuffController.GetRules
                            where rule.IsOk
                            select rule, CS$<>9__CachedAnonymousMethodDelegate3).GetEnumerator())
                        {
                            if (enumerator.MoveNext())
                            {
                                enumerator.Current.ExecuteAction(Behavior.GlobalCooldown);
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public override Form Settings()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            return new BehaviorForm(Behavior);
        }

        public override string Name =>
            "Behavior Engine";

        public override bool StartOk
        {
            get
            {
                OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
                PveBehaviorSettings.LoadSettings();
                if (Behavior == null)
                {
                    if (!File.Exists(OurDirectory + @"\Behaviors\" + PveBehaviorSettings.LoadedBeharvior + ".xml"))
                    {
                        Logging.Write("Could not load the behavior, please select a different one", new object[0]);
                        Behavior = null;
                        return false;
                    }
                    Behavior = new BehaviorController();
                    Behavior.Load(OurDirectory + @"\Behaviors\" + PveBehaviorSettings.LoadedBeharvior + ".xml");
                }
                return true;
            }
        }
    }
}

