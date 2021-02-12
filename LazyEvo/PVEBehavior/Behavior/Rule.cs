namespace LazyEvo.PVEBehavior.Behavior
{
    using LazyEvo.Other;
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class Rule : IComparable<Rule>, IComparer<Rule>
    {
        private readonly List<AbstractCondition> _conditions;
        public int GlobalCooldown;

        public Rule()
        {
            this._conditions = new List<AbstractCondition>();
            this.MatchAll = true;
            this.ShouldTarget = Target.Unchanged;
        }

        public Rule(string name, string script)
        {
            this._conditions = new List<AbstractCondition>();
            this.Name = name;
            this.Script = script;
        }

        public Rule(string name, LazyEvo.PVEBehavior.Behavior.Action action, int priority)
        {
            this._conditions = new List<AbstractCondition>();
            this.Name = name;
            this.Action = action;
            this.Priority = priority;
            this.MatchAll = true;
            this.ShouldTarget = Target.Unchanged;
        }

        public Rule(string name, LazyEvo.PVEBehavior.Behavior.Action action, int priority, List<AbstractCondition> conditions)
        {
            this._conditions = new List<AbstractCondition>();
            this.Name = name;
            this.Action = action;
            this._conditions = conditions;
            this.Priority = priority;
            this.MatchAll = true;
            this.ShouldTarget = Target.Enemy;
        }

        public Rule(string name, LazyEvo.PVEBehavior.Behavior.Action action, int priority, List<AbstractCondition> conditions, Target target)
        {
            this._conditions = new List<AbstractCondition>();
            this.Name = name;
            this.Action = action;
            this._conditions = conditions;
            this.Priority = priority;
            this.MatchAll = true;
            this.ShouldTarget = target;
        }

        public void AddCondition(AbstractCondition condition)
        {
            lock (this._conditions)
            {
                this._conditions.Add(condition);
            }
        }

        public void BotStarting()
        {
            foreach (TickerCondition condition in this._conditions.OfType<TickerCondition>())
            {
                condition.ForceReady();
            }
        }

        public void ClearConditions()
        {
            lock (this._conditions)
            {
                this._conditions.Clear();
            }
        }

        public int Compare(Rule x, Rule y) => 
            x.Priority.CompareTo(y.Priority);

        public int CompareTo(Rule other)
        {
            if (other != null)
            {
                return this.Priority.CompareTo(other.Priority);
            }
            Logging.Write("Tried to compare null Rule to another - check class code!", new object[0]);
            return 0;
        }

        public void ExecuteAction(int globalCooldown)
        {
            try
            {
                if (this.IsScript)
                {
                    this.RunScriptAction();
                }
                if (this.Action != null)
                {
                    switch (this.ShouldTarget)
                    {
                        case Target.None:
                            if (LazyLib.Wow.ObjectManager.MyPlayer.HasTarget)
                            {
                                KeyHelper.SendKey("ESC");
                            }
                            InterfaceHelper.CloseMainMenuFrame();
                            break;

                        case Target.Self:
                            LazyLib.Wow.ObjectManager.MyPlayer.TargetSelf();
                            break;

                        case Target.Pet:
                            if (LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet)
                            {
                                LazyLib.Wow.ObjectManager.MyPlayer.Pet.TargetFriend();
                            }
                            break;

                        case Target.Enemy:
                            if (!LazyLib.Wow.ObjectManager.MyPlayer.Target.IsValid)
                            {
                                LazyLib.Wow.ObjectManager.GetClosestAttacker.TargetHostile();
                            }
                            break;

                        default:
                            break;
                    }
                    this.Action.Execute(globalCooldown);
                    foreach (TickerCondition condition in this._conditions.OfType<TickerCondition>())
                    {
                        condition.Reset();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoadAction(XmlNode node)
        {
            foreach (XmlNode node2 in node.ChildNodes)
            {
                string str;
                if (((str = node2.Name) != null) && (str == "Type"))
                {
                    if (node2.InnerText.Equals("ActionSpell"))
                    {
                        this.Action = new ActionSpell();
                        this.Action.Load(node);
                    }
                    if (node2.InnerText.Equals("ActionKey"))
                    {
                        this.Action = new ActionKey();
                        this.Action.Load(node);
                    }
                }
            }
        }

        private void RunScriptAction()
        {
            ScriptRunner.RunCode(this.Name, this.Script);
        }

        public string SaveAction() => 
            (this.Action == null) ? "" : ("<Action>" + this.Action.GetXml() + "</Action>");

        public bool IsScript =>
            !string.IsNullOrEmpty(this.Script);

        public string Name { get; set; }

        public string Script { get; set; }

        public bool MatchAll { get; set; }

        public LazyEvo.PVEBehavior.Behavior.Action Action { get; set; }

        public Target ShouldTarget { get; set; }

        public int Priority { get; set; }

        public List<AbstractCondition> GetConditions
        {
            get
            {
                lock (this._conditions)
                {
                    return this._conditions;
                }
            }
        }

        public bool IsOk
        {
            get
            {
                if (this.IsScript)
                {
                    return (PveBehaviorSettings.AllowScripts && ScriptRunner.ShouldRun(this.Name, this.Script));
                }
                if (this.Action == null)
                {
                    return false;
                }
                if (!this.Action.IsReady)
                {
                    return false;
                }
                if (this.GetConditions.Count == 0)
                {
                    return true;
                }
                if (!this.MatchAll)
                {
                    return Enumerable.Any<AbstractCondition>(this.GetConditions, condition => condition.IsOk);
                }
                return Enumerable.All<AbstractCondition>(this.GetConditions, condition => condition.IsOk);
            }
        }
    }
}

