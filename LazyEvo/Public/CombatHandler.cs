namespace LazyEvo.Public
{
    using LazyEvo.Classes;
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public static class CombatHandler
    {
        private static EventHandler<GCombatEventArgs> CombatStatusChanged;

        public static event EventHandler<GCombatEventArgs> CombatStatusChanged
        {
            add
            {
                EventHandler<GCombatEventArgs> combatStatusChanged = CombatStatusChanged;
                while (true)
                {
                    EventHandler<GCombatEventArgs> comparand = combatStatusChanged;
                    EventHandler<GCombatEventArgs> handler3 = comparand + value;
                    combatStatusChanged = Interlocked.CompareExchange<EventHandler<GCombatEventArgs>>(ref CombatStatusChanged, handler3, comparand);
                    if (ReferenceEquals(combatStatusChanged, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<GCombatEventArgs> combatStatusChanged = CombatStatusChanged;
                while (true)
                {
                    EventHandler<GCombatEventArgs> comparand = combatStatusChanged;
                    EventHandler<GCombatEventArgs> handler3 = comparand - value;
                    combatStatusChanged = Interlocked.CompareExchange<EventHandler<GCombatEventArgs>>(ref CombatStatusChanged, handler3, comparand);
                    if (ReferenceEquals(combatStatusChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static void InvokeCombatStatusChanged(GCombatEventArgs e)
        {
            EventHandler<GCombatEventArgs> combatStatusChanged = CombatStatusChanged;
            if (combatStatusChanged != null)
            {
                combatStatusChanged(null, e);
            }
        }

        public static void OnRess()
        {
            PrivCombatHandler.OnRess();
        }

        public static void Rest()
        {
            PrivCombatHandler.Rest();
        }

        public static void RunningAction()
        {
            PrivCombatHandler.RunningAction();
        }

        public static void StartCombat(PUnit u)
        {
            PrivCombatHandler.StartCombat(u);
        }

        public static void Stop()
        {
            PrivCombatHandler.Stop();
        }
    }
}

