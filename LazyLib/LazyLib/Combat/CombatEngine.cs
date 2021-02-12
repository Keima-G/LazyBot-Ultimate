namespace LazyLib.Combat
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public abstract class CombatEngine
    {
        public const int MeleeDistance = 3;
        private readonly List<PAction> _queuedBuffs = new List<PAction>();
        public List<PAction> DamageActions;
        public List<PAction> SelfBuffActions;
        public List<PAction> SelfHealActions;

        protected CombatEngine()
        {
        }

        public virtual void BotStarted()
        {
        }

        public abstract void Combat(PUnit target);
        public virtual void CombatDone()
        {
        }

        public static void Debug(string message)
        {
            Logging.Debug(message, new object[0]);
        }

        public static void Log(string message)
        {
            Logging.Write(message, new object[0]);
        }

        public static void Log(string message, LogType type)
        {
            Logging.Write(type, message, new object[0]);
        }

        public virtual void LogicAttack(PUnit target)
        {
            try
            {
                this.DamageActions.Sort();
                PAction action = (from a in this.DamageActions
                    where a.IsWanted && (a.IsReady || a.WaitUntilReady)
                    select a).FirstOrDefault<PAction>();
                if (action != null)
                {
                    action.Execute();
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                Log("Error in LogicAttack please check class code: " + exception);
            }
        }

        public virtual void LogicSelfBuff()
        {
            try
            {
                this._queuedBuffs.Clear();
                this.SelfBuffActions.Sort();
                foreach (PAction action in from selfBuffAction in this.SelfBuffActions
                    where selfBuffAction.IsWanted && (selfBuffAction.IsReady || selfBuffAction.WaitUntilReady)
                    select selfBuffAction)
                {
                    this._queuedBuffs.Add(action);
                }
                if (this._queuedBuffs.Count != 0)
                {
                    MoveHelper.ReleaseKeys();
                }
                foreach (PAction action2 in this._queuedBuffs)
                {
                    action2.Execute();
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                Log("Error in LogicSelfBuff please check class code: " + exception);
            }
        }

        public virtual void LogicSelfHeal()
        {
            try
            {
                this.SelfHealActions.Sort();
                PAction action = (from a in this.SelfHealActions
                    where a.IsWanted && (a.IsReady || a.WaitUntilReady)
                    select a).FirstOrDefault<PAction>();
                if (action != null)
                {
                    action.Execute();
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception exception)
            {
                Log("Error in LogicSelfHeal please check class code: " + exception);
            }
        }

        public virtual void OnRess()
        {
        }

        public abstract PullResult Pull(PUnit target);
        public virtual void Rest()
        {
        }

        public virtual void RunningAction()
        {
        }

        public abstract Form Settings();
        public virtual void SettingsClosed()
        {
        }

        public abstract string Name { get; }

        public virtual bool StartOk =>
            true;
    }
}

