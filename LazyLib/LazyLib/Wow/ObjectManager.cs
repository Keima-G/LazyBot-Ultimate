namespace LazyLib.Wow
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public static class ObjectManager
    {
        private static Process[] _wowProc = Process.GetProcessesByName("Wow");
        private static int _processPid;
        private static Thread _refresher;
        private static Thread _monitor;
        private static bool _alearted;
        private static readonly object Locker = new object();
        private static EventHandler<NotifyEventAttach> Attach;
        private static EventHandler<NotifyEventNoAttach> NoAttach;

        public static event EventHandler<NotifyEventAttach> Attach
        {
            add
            {
                EventHandler<NotifyEventAttach> attach = Attach;
                while (true)
                {
                    EventHandler<NotifyEventAttach> comparand = attach;
                    EventHandler<NotifyEventAttach> handler3 = comparand + value;
                    attach = Interlocked.CompareExchange<EventHandler<NotifyEventAttach>>(ref Attach, handler3, comparand);
                    if (ReferenceEquals(attach, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<NotifyEventAttach> attach = Attach;
                while (true)
                {
                    EventHandler<NotifyEventAttach> comparand = attach;
                    EventHandler<NotifyEventAttach> handler3 = comparand - value;
                    attach = Interlocked.CompareExchange<EventHandler<NotifyEventAttach>>(ref Attach, handler3, comparand);
                    if (ReferenceEquals(attach, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event EventHandler<NotifyEventNoAttach> NoAttach
        {
            add
            {
                EventHandler<NotifyEventNoAttach> noAttach = NoAttach;
                while (true)
                {
                    EventHandler<NotifyEventNoAttach> comparand = noAttach;
                    EventHandler<NotifyEventNoAttach> handler3 = comparand + value;
                    noAttach = Interlocked.CompareExchange<EventHandler<NotifyEventNoAttach>>(ref NoAttach, handler3, comparand);
                    if (ReferenceEquals(noAttach, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<NotifyEventNoAttach> noAttach = NoAttach;
                while (true)
                {
                    EventHandler<NotifyEventNoAttach> comparand = noAttach;
                    EventHandler<NotifyEventNoAttach> handler3 = comparand - value;
                    noAttach = Interlocked.CompareExchange<EventHandler<NotifyEventNoAttach>>(ref NoAttach, handler3, comparand);
                    if (ReferenceEquals(noAttach, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static int AttackersInRange(double pRangeToCheck) => 
            Enumerable.Count<PUnit>(GetAttackers, attacker => attacker.Location.DistanceToSelf < pRangeToCheck);

        public static bool AttackingMeOrPet(PUnit u) => 
            TargetingMeOrPet(u) && u.IsInCombat;

        public static List<PUnit> CheckForMobsAtLoc(Location l, float radius, bool includeFriendly)
        {
            var func = null;
            var func2 = null;
            var func3 = null;
            var func4 = null;
            List<PUnit> list = new List<PUnit>();
            List<PUnit> getUnits = GetUnits;
            if ((l != null) && (getUnits.Count > 0))
            {
                if (includeFriendly)
                {
                    if (func == null)
                    {
                        func = mob => new { 
                            mob = mob,
                            mdt = mob.Location.GetDistanceTo(l)
                        };
                    }
                    func2 ??= <>h__TransparentIdentifier1e => ((<>h__TransparentIdentifier1e.mdt <= radius) && (!<>h__TransparentIdentifier1e.mob.IsDead && (!<>h__TransparentIdentifier1e.mob.IsTagged && ((<>h__TransparentIdentifier1e.mob.Level > 1) && !<>h__TransparentIdentifier1e.mob.IsTargetingMe))));
                    CS$<>9__CachedAnonymousMethodDelegate28 ??= <>h__TransparentIdentifier1e => <>h__TransparentIdentifier1e.mob;
                    list.AddRange(Enumerable.Select(Enumerable.Where(Enumerable.Select(getUnits, func), func2), CS$<>9__CachedAnonymousMethodDelegate28));
                }
                else
                {
                    if (func3 == null)
                    {
                        func3 = mob => new { 
                            mob = mob,
                            mdt = mob.Location.GetDistanceTo(l)
                        };
                    }
                    func4 ??= <>h__TransparentIdentifier1f => ((<>h__TransparentIdentifier1f.mdt <= radius) && (!<>h__TransparentIdentifier1f.mob.IsDead && (!<>h__TransparentIdentifier1f.mob.IsTagged && ((<>h__TransparentIdentifier1f.mob.Level > 1) && (!<>h__TransparentIdentifier1f.mob.IsTargetingMe && <>h__TransparentIdentifier1f.mob.Reaction.Equals(LazyLib.Wow.Reaction.Hostile))))));
                    CS$<>9__CachedAnonymousMethodDelegate2b ??= <>h__TransparentIdentifier1f => <>h__TransparentIdentifier1f.mob;
                    list.AddRange(Enumerable.Select(Enumerable.Where(Enumerable.Select(getUnits, func3), func4), CS$<>9__CachedAnonymousMethodDelegate2b));
                }
            }
            return list;
        }

        public static void Close()
        {
            if (_monitor != null)
            {
                _monitor.Abort();
                _monitor = null;
            }
            if (_refresher != null)
            {
                _refresher.Abort();
                _refresher = null;
            }
            if (ObjectList != null)
            {
                ObjectList.Clear();
                ObjectList = null;
            }
            if (ObjectDictionary != null)
            {
                ObjectDictionary.Clear();
                ObjectDictionary = null;
            }
        }

        private static bool DoesProcessExsist(int pid)
        {
            lock (_wowProc)
            {
                _wowProc = Process.GetProcessesByName("Wow");
            }
            return Enumerable.Any<Process>(_wowProc, proc => proc.Id.Equals(pid));
        }

        public static PUnit GetClosestAttackerExclude(PUnit exclude)
        {
            PUnit unit = null;
            try
            {
                foreach (PUnit unit2 in GetAttackers)
                {
                    if (unit2.GUID != exclude.GUID)
                    {
                        if (unit == null)
                        {
                            unit = unit2;
                            continue;
                        }
                        if (unit2.DistanceToSelf < unit.DistanceToSelf)
                        {
                            unit = unit2;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return unit;
        }

        public static PUnit GetClosestUnit(List<PUnit> units)
        {
            PUnit unit = null;
            double maxValue = double.MaxValue;
            foreach (PUnit unit2 in units)
            {
                if (unit2.Reaction.Equals(LazyLib.Wow.Reaction.Hostile) && (unit2.Location.DistanceToSelf < maxValue))
                {
                    unit = unit2;
                    maxValue = unit2.Location.DistanceToSelf;
                }
            }
            return unit;
        }

        public static List<PUnit> GetLikelyAdds(int distance) => 
            (from monster in GetUnits
                where (monster.Health == 1) && (!monster.IsInCombat && (monster.Location.DistanceToSelf < distance))
                select monster).ToList<PUnit>();

        public static int GetNumAdds() => 
            GetAttackers.Count - 1;

        public static int GetNumAttackers() => 
            GetAttackers.Count;

        public static PObject GetObjectByGuid(ulong guid)
        {
            Func<PObject, bool> func = null;
            lock (Locker)
            {
                if (func == null)
                {
                    func = wowObject => wowObject.GUID.Equals(guid);
                }
                return Enumerable.Where<PObject>(ObjectList.OfType<PObject>(), func).FirstOrDefault<PObject>();
            }
        }

        public static bool HasAdds() => 
            GetNumAdds() > 1;

        public static bool HasAttackers() => 
            GetAttackers.Count != 0;

        public static void Initialize(int pid)
        {
            ObjectList = new List<PObject>();
            if (!DoesProcessExsist(pid))
            {
                Logging.Write("Instance does not exist: " + pid + " please select another process", new object[0]);
            }
            else
            {
                Memory.OpenProcess(pid);
                _processPid = pid;
                InterfaceHelper.StartUpdate();
                if (!InGame)
                {
                    Logging.Write(LogType.Warning, "Not ingame, could not attach", new object[0]);
                }
                else
                {
                    try
                    {
                        uint[] addresses = new uint[] { Memory.ReadRelative<uint>(new uint[] { 0x879ce0 }) + 0x2ed0 };
                        CurrentManager = Memory.Read<uint>(addresses);
                        uint[] numArray3 = new uint[] { CurrentManager + 0xc0 };
                        LocalGUID = Memory.Read<ulong>(numArray3);
                        if ((CurrentManager != 0) && (CurrentManager != uint.MaxValue))
                        {
                            Initialized = true;
                            WowHandle = Memory.ProcessHandle;
                        }
                        if ((_refresher != null) && _refresher.IsAlive)
                        {
                            _refresher.Abort();
                            _refresher = null;
                        }
                        Thread thread = new Thread(new ThreadStart(LazyLib.Wow.ObjectManager.Pulse)) {
                            IsBackground = true
                        };
                        _refresher = thread;
                        _refresher.Name = "Pulse";
                        _refresher.Start();
                        if (Attach != null)
                        {
                            Attach(new object(), new NotifyEventAttach(pid));
                        }
                        Logging.Write(LogType.Info, "Attached", new object[0]);
                        _alearted = false;
                    }
                    catch (Exception exception1)
                    {
                        Logging.Write(exception1.Message, new object[0]);
                    }
                }
            }
        }

        public static bool IsItSafeAt(ulong ignore, Location l) => 
            Enumerable.All<PUnit>(from mob in CheckForMobsAtLoc(l, 15f, false)
                where !mob.GUID.Equals(ignore)
                select mob, mob => mob.IsDead || (mob.TargetGUID == 0L));

        public static bool IsItSafeAt(ulong ignore, PUnit u) => 
            IsItSafeAt(ignore, u.Location);

        public static void MakeReady()
        {
            ObjectList = new List<PObject>();
            ObjectDictionary = new Dictionary<ulong, PObject>();
            MyPlayer = new PPlayerSelf(0);
            Thread thread = new Thread(new ThreadStart(LazyLib.Wow.ObjectManager.Monitor)) {
                IsBackground = true
            };
            _monitor = thread;
            _monitor.Name = "ObjectManager";
            _monitor.Start();
        }

        private static void Monitor()
        {
            while (!Closing)
            {
                if (InGame && (_processPid != -1))
                {
                    Thread.Sleep(500);
                    continue;
                }
                if (!DoesProcessExsist(_processPid) && !_alearted)
                {
                    InterfaceHelper.StopUpdate();
                    Logging.Write(LogType.Info, "No wow process, cannot attach", new object[0]);
                    ObjectList.Clear();
                    ObjectDictionary.Clear();
                    if (NoAttach != null)
                    {
                        NoAttach(new object(), new NotifyEventNoAttach("Not attached"));
                    }
                    _alearted = true;
                }
                Initialized = false;
                if ((_refresher != null) && _refresher.IsAlive)
                {
                    _refresher.Abort();
                    _refresher = null;
                }
                if (!DoesProcessExsist(_processPid))
                {
                    _processPid = -1;
                }
                else
                {
                    _alearted = false;
                    ObjectList.Clear();
                    ObjectDictionary.Clear();
                    if (NoAttach != null)
                    {
                        NoAttach(new object(), new NotifyEventNoAttach("Not attached"));
                    }
                    Logging.Write(LogType.Info, "Not ingame", new object[0]);
                    Thread.Sleep(0x5dc);
                    while (true)
                    {
                        if (!DoesProcessExsist(_processPid) || InGame)
                        {
                            if (InGame)
                            {
                                Initialize(_processPid);
                            }
                            else
                            {
                                _processPid = -1;
                            }
                            break;
                        }
                        Thread.Sleep(0x3e8);
                    }
                }
                Thread.Sleep(0x7d0);
            }
        }

        internal static void Pulse()
        {
            while (!Closing)
            {
                lock (Locker)
                {
                    foreach (KeyValuePair<ulong, PObject> pair in ObjectDictionary)
                    {
                        pair.Value.UpdateBaseAddress(0);
                    }
                    ReadObjectList();
                    CS$<>9__CachedAnonymousMethodDelegatef ??= o => o.Key;
                    foreach (ulong num in Enumerable.Select<KeyValuePair<ulong, PObject>, ulong>(from o in ObjectDictionary
                        where !o.Value.IsValid
                        select o, CS$<>9__CachedAnonymousMethodDelegatef).ToList<ulong>())
                    {
                        ObjectDictionary.Remove(num);
                    }
                    CS$<>9__CachedAnonymousMethodDelegate11 ??= o => o.Value;
                    ObjectList = Enumerable.Select<KeyValuePair<ulong, PObject>, PObject>(from o in ObjectDictionary
                        where o.Value.IsValid
                        select o, CS$<>9__CachedAnonymousMethodDelegate11).ToList<PObject>();
                }
                Thread.Sleep(700);
            }
        }

        private static void ReadObjectList()
        {
            uint[] addresses = new uint[] { CurrentManager + 0xac };
            PObject obj2 = new PObject(Memory.Read<uint>(addresses));
            uint[] numArray2 = new uint[] { CurrentManager + 0xc0 };
            LocalGUID = Memory.Read<ulong>(numArray2);
            while ((obj2.BaseAddress != 0) && ((obj2.BaseAddress % 2) == 0))
            {
                if (obj2.GUID == LocalGUID)
                {
                    MyPlayer.UpdateBaseAddress(obj2.BaseAddress);
                }
                if (ObjectDictionary.ContainsKey(obj2.GUID))
                {
                    ObjectDictionary[obj2.GUID].UpdateBaseAddress(obj2.BaseAddress);
                }
                else
                {
                    PObject obj3 = null;
                    switch (obj2.Type)
                    {
                        case 0:
                            obj3 = new PObject(obj2.BaseAddress);
                            break;

                        case 1:
                            obj3 = new PItem(obj2.BaseAddress);
                            break;

                        case 2:
                            obj3 = new PContainer(obj2.BaseAddress);
                            break;

                        case 3:
                            obj3 = new PUnit(obj2.BaseAddress);
                            break;

                        case 4:
                            obj3 = new PPlayer(obj2.BaseAddress);
                            break;

                        case 5:
                            obj3 = new PGameObject(obj2.BaseAddress);
                            break;

                        default:
                            break;
                    }
                    if (obj3 != null)
                    {
                        ObjectDictionary.Add(obj2.GUID, obj3);
                    }
                }
                uint[] numArray3 = new uint[] { obj2.BaseAddress + 60 };
                obj2.BaseAddress = Memory.Read<uint>(numArray3);
            }
        }

        public static void Refresh()
        {
            foreach (KeyValuePair<ulong, PObject> pair in ObjectDictionary)
            {
                pair.Value.UpdateBaseAddress(0);
            }
            ReadObjectList();
            CS$<>9__CachedAnonymousMethodDelegate33 ??= o => o.Key;
            foreach (ulong num in Enumerable.Select<KeyValuePair<ulong, PObject>, ulong>(from o in ObjectDictionary
                where !o.Value.IsValid
                select o, CS$<>9__CachedAnonymousMethodDelegate33).ToList<ulong>())
            {
                ObjectDictionary.Remove(num);
            }
            CS$<>9__CachedAnonymousMethodDelegate35 ??= o => o.Value;
            ObjectList = Enumerable.Select<KeyValuePair<ulong, PObject>, PObject>(from o in ObjectDictionary
                where o.Value.IsValid
                select o, CS$<>9__CachedAnonymousMethodDelegate35).ToList<PObject>();
        }

        public static bool TargetingMeOrPet(PUnit u) => 
            (u != null) && ((MyPlayer != null) && ((u.TargetGUID != MyPlayer.GUID) ? (MyPlayer.HasLivePet ? (u.TargetGUID == MyPlayer.PetGUID) : false) : true));

        private static List<PObject> ObjectList { get; set; }

        private static Dictionary<ulong, PObject> ObjectDictionary { get; set; }

        public static IntPtr WowHandle { get; set; }

        public static PPlayerSelf MyPlayer { get; private set; }

        public static bool Initialized { get; private set; }

        public static bool Closing { get; set; }

        public static bool ForceIngame { get; set; }

        public static bool InGame
        {
            get
            {
                try
                {
                    return (!ForceIngame ? (Memory.ReadRelative<byte>(new uint[] { 0x7d0792 }) == 1) : true);
                }
                catch
                {
                    return false;
                }
            }
        }

        public static bool ShouldDefend =>
            MyPlayer.IsInCombat ? (HasAttackers() && ((MyPlayer.IsInCombat || (MyPlayer.HasLivePet && MyPlayer.Pet.IsInCombat)) && !MyPlayer.IsDead)) : false;

        public static PUnit GetClosestAttacker
        {
            get
            {
                PUnit unit = null;
                try
                {
                    foreach (PUnit unit2 in GetAttackers)
                    {
                        if (unit == null)
                        {
                            unit = unit2;
                            continue;
                        }
                        if (unit2.DistanceToSelf < unit.DistanceToSelf)
                        {
                            unit = unit2;
                        }
                    }
                }
                catch (Exception)
                {
                }
                return unit;
            }
        }

        public static List<PUnit> GetAttackers
        {
            get
            {
                List<PUnit> list = new List<PUnit>();
                try
                {
                    list.AddRange(Enumerable.Where<PUnit>(GetUnits, new Func<PUnit, bool>(LazyLib.Wow.ObjectManager.AttackingMeOrPet)));
                }
                catch (Exception)
                {
                }
                return list;
            }
        }

        public static List<PObject> GetObjects
        {
            get
            {
                lock (Locker)
                {
                    return ObjectList.OfType<PObject>().ToList<PObject>();
                }
            }
        }

        public static List<PContainer> GetContainers
        {
            get
            {
                lock (Locker)
                {
                    return ObjectList.OfType<PContainer>().ToList<PContainer>();
                }
            }
        }

        public static List<PItem> GetItems
        {
            get
            {
                lock (Locker)
                {
                    return ObjectList.OfType<PItem>().ToList<PItem>();
                }
            }
        }

        public static List<PPlayer> GetPlayers
        {
            get
            {
                lock (Locker)
                {
                    return ObjectList.OfType<PPlayer>().ToList<PPlayer>();
                }
            }
        }

        public static List<PUnit> GetUnits
        {
            get
            {
                lock (Locker)
                {
                    return (from wowObject in ObjectList.OfType<PUnit>().ToList<PUnit>()
                        where !wowObject.GUID.Equals(MyPlayer.GUID)
                        select wowObject).ToList<PUnit>();
                }
            }
        }

        public static List<PGameObject> GetGameObject =>
            GetObjects.OfType<PGameObject>().ToList<PGameObject>();

        public static uint CurrentManager { get; set; }

        public static ulong LocalGUID { get; set; }
    }
}

