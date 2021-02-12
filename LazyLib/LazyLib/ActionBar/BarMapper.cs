namespace LazyLib.ActionBar
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true), Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class BarMapper
    {
        private static Dictionary<int, string> _spellDatabase;
        private static readonly List<WowKey> LoadedKeys = new List<WowKey>();
        private static readonly List<BarItem> BarItems = new List<BarItem>();
        private static readonly IDictionary<string, BarSpell> Spells = new Dictionary<string, BarSpell>();
        private static readonly Dictionary<int, string> SpellsUsed = new Dictionary<int, string>();

        public static void CastSpell(string spellName)
        {
            BarSpell spell = (from barSpell in Spells
                where barSpell.Key == spellName
                select barSpell.Value).FirstOrDefault<BarSpell>();
            if (spell != null)
            {
                Logging.Write("[Mapper]Casting " + spellName, new object[0]);
                spell.CastSpell();
            }
        }

        public static bool DoesBuffExist(string name) => 
            GetIdsFromName(name).Count != 0;

        public static int GetIdByName(string spellName)
        {
            List<int> idsFromName = GetIdsFromName(spellName);
            return ((idsFromName.Count == 0) ? 0 : idsFromName[0]);
        }
        public static int GetIdFromName(string name)
        {
            List<int> idsFromName = GetIdsFromName(name);
            return ((idsFromName.Count == 0) ? 0 : idsFromName[0]);
        }

        public static List<int> GetIdsFromName(string name)
        {
            Func<KeyValuePair<int, string>, bool> func = null;
            Func<KeyValuePair<int, string>, bool> func2 = null;
            List<int> list3;
            Load();
            try
            {
                if (SpellsUsed.ContainsValue(name))
                {
                    if (func == null)
                    {
                        func = spell => spell.Value == name;
                    }
                    CS$<>9__CachedAnonymousMethodDelegate28 ??= spell => spell.Key;
                    list3 = Enumerable.Select<KeyValuePair<int, string>, int>(Enumerable.Where<KeyValuePair<int, string>>(SpellsUsed, func), CS$<>9__CachedAnonymousMethodDelegate28).ToList<int>();
                }
                else
                {
                    if (func2 == null)
                    {
                        func2 = spell => spell.Value == name;
                    }
                    CS$<>9__CachedAnonymousMethodDelegate2a ??= spell => spell.Key;
                    List<int> list = Enumerable.Select<KeyValuePair<int, string>, int>(Enumerable.Where<KeyValuePair<int, string>>(_spellDatabase, func2), CS$<>9__CachedAnonymousMethodDelegate2a).ToList<int>();
                    foreach (int num in list)
                    {
                        SpellsUsed.Add(num, name);
                    }
                    list3 = list;
                }
            }
            catch (Exception)
            {
                list3 = new List<int> { 0 };
            }
            return list3;
        }

        public static BarItem GetItemById(int itemId) => 
            Enumerable.FirstOrDefault<BarItem>(BarItems, barItem => barItem.ItemId.Equals(itemId));

        public static string GetNameFromSpell(int spellId)
        {
            Load();
            try
            {
                return (!_spellDatabase.ContainsKey(spellId) ? string.Empty : _spellDatabase[spellId]);
            }
            catch (Exception)
            {
                Logging.Write("Error find name of spell: " + spellId, new object[0]);
                return string.Empty;
            }
        }

        public static BarSpell GetSpellById(int spellId) => 
            (from spell in Spells
                where spell.Value.SpellId.Equals(spellId)
                select spell.Value).FirstOrDefault<BarSpell>();

        public static BarSpell GetSpellByName(string spellName)
        {
            BarSpell local1 = (from barSpell in Spells
                where barSpell.Key == spellName
                select barSpell.Value).FirstOrDefault<BarSpell>();
            BarSpell local3 = local1;
            if (local1 == null)
            {
                BarSpell local2 = local1;
                local3 = new BarSpell(0, 0, 0, "Unknown Spell");
            }
            return local3;
        }

        public static bool HasBuff(PUnit check, string name)
        {
            List<int> idsFromName = GetIdsFromName(name);
            return (LazyLib.Wow.ObjectManager.Initialized && check.HasBuff(idsFromName));
        }

        public static bool HasItemById(int itemId) => 
            Enumerable.Any<BarItem>(BarItems, a => a.ItemId.Equals(itemId));

        public static bool HasSpellById(int spellId) => 
            Enumerable.Any<KeyValuePair<string, BarSpell>>(Spells, spell => spell.Value.SpellId.Equals(spellId));

        public static bool HasSpellByName(string spellName) => 
            (from barSpell in Spells
                where barSpell.Key == spellName
                select barSpell.Value).FirstOrDefault<BarSpell>() != null;

        private static bool IsSpellReady(int spellidToCheck)
        {
            long num;
            long num2;
            uint[] numArray6;
            QueryPerformanceFrequency(out num);
            QueryPerformanceCounter(out num2);
            long num3 = (num2 * 0x3e8L) / num;
            for (uint i = Memory.ReadRelative<uint>(new uint[] { 0x93f5b4 }); (i != 0) && ((i & 1) == 0); i = Memory.Read<uint>(numArray6))
            {
                uint[] addresses = new uint[] { i + 8 };
                if (((ulong) Memory.Read<uint>(addresses)) == spellidToCheck)
                {
                    uint[] numArray3 = new uint[] { i + 0x10 };
                    uint[] numArray4 = new uint[] { i + 20 };
                    uint[] numArray5 = new uint[] { i + 0x20 };
                    int num9 = Math.Max(Memory.Read<int>(numArray4), Memory.Read<int>(numArray5));
                    if ((((ulong) Memory.Read<uint>(numArray3)) + num9) > num3)
                    {
                        return false;
                    }
                }
                numArray6 = new uint[] { i + 4 };
            }
            return true;
        }

        public static bool IsSpellReadyById(int id) => 
            IsSpellReady(id);

        public static bool IsSpellReadyByName(string name) => 
            ((from barSpell in Spells
                where barSpell.Key == name
                select barSpell.Value).FirstOrDefault<BarSpell>() != null) && IsSpellReady(GetSpellByName(name).SpellId);

        private static void Load()
        {
            if (_spellDatabase == null)
            {
                try
                {
                    _spellDatabase = new Dictionary<int, string>();
                    string[] strArray2 = Resource.Spells.Split(new char[] { '\n' });
                    int index = 0;
                    while (true)
                    {
                        if (index >= strArray2.Length)
                        {
                            Logging.Debug("[Mapper] We loaded " + _spellDatabase.Count + " spells", new object[0]);
                            break;
                        }
                        string str = strArray2[index];
                        if (str.Contains("="))
                        {
                            int key = Convert.ToInt32(str.Split(new char[] { '=' })[0]);
                            string str2 = str.Split(new char[] { '=' })[1].Replace("\n", "").Replace("\r", "");
                            if (!_spellDatabase.ContainsKey(key))
                            {
                                _spellDatabase.Add(key, str2);
                            }
                        }
                        index++;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write(LogType.Error, "[Mapper] Spells could not be loaded, LazyBot is fubar :( " + exception, new object[0]);
                }
            }
        }

        public static void MapBars()
        {
            LoadedKeys.Clear();
            BarItems.Clear();
            Spells.Clear();
            int num = 60;
            Constants.UnitClass unitClass = LazyLib.Wow.ObjectManager.MyPlayer.UnitClass;
            switch (unitClass)
            {
                case Constants.UnitClass.UnitClass_Warrior:
                    num = 0x60;
                    break;

                case Constants.UnitClass.UnitClass_Paladin:
                case Constants.UnitClass.UnitClass_Hunter:
                    break;

                case Constants.UnitClass.UnitClass_Rogue:
                    num = 0x48;
                    break;

                case Constants.UnitClass.UnitClass_Priest:
                    num = 0x48;
                    break;

                default:
                    if (unitClass == Constants.UnitClass.UnitClass_Druid)
                    {
                        num = 0x60;
                    }
                    break;
            }
            int num2 = 1;
            int bar = 1;
            for (uint i = 0; i < num; i++)
            {
                if (num2 > 12)
                {
                    bar++;
                    num2 = 1;
                }
                uint[] addresses = new uint[] { (0x81e358 + (4 * i)) + 0x30 };
                uint id = Memory.ReadRelative<uint>(addresses);
                if (id != 0)
                {
                    LoadedKeys.Add(new WowKey(id, bar, num2));
                }
                num2++;
            }
            int num6 = Memory.ReadRelative<int>(new uint[] { 0x81e59c });
            if (num6 == 0)
            {
                for (uint j = 0; j < 12; j++)
                {
                    uint[] addresses = new uint[] { 0x81e358 + (4 * j) };
                    uint id = Memory.ReadRelative<uint>(addresses);
                    if (id != 0)
                    {
                        LoadedKeys.Add(new WowKey(id, 0, ((int) j) + 1));
                    }
                    num2++;
                }
            }
            else
            {
                for (uint j = 0; j < 12; j++)
                {
                    uint[] addresses = new uint[] { ((0x81e358 + (4 * j)) + 0x120) + ((num6 - 1) * 0x30) };
                    uint id = Memory.ReadRelative<uint>(addresses);
                    if (id != 0)
                    {
                        LoadedKeys.Add(new WowKey(id, 0, ((int) j) + 1));
                    }
                    num2++;
                }
            }
            Load();
            LoadedKeys.Reverse();
            foreach (WowKey key in LoadedKeys)
            {
                string nameFromSpell = string.Empty;
                if (key.Bar > 5)
                {
                    key.Bar = 0;
                }
                key.Bar++;
                if (key.Key == 10)
                {
                    key.Key = 0;
                }
                if (key.Key <= 10)
                {
                    if (key.Type.Equals(LazyLib.ActionBar.KeyType.Spell))
                    {
                        nameFromSpell = GetNameFromSpell(key.SpellId);
                        if (nameFromSpell != string.Empty)
                        {
                            if (Spells.ContainsKey(nameFromSpell))
                            {
                                Logging.Debug(string.Concat(new object[] { "Key: ", nameFromSpell, " : ", key.Bar, " : ", key.Key, " is a duplicate" }), new object[0]);
                            }
                            else
                            {
                                Logging.Debug(string.Concat(new object[] { "Found key: ", nameFromSpell, " : ", key.Bar, " : ", key.Key }), new object[0]);
                                Spells.Add(nameFromSpell, new BarSpell(key.SpellId, key.Bar, key.Key, nameFromSpell));
                            }
                        }
                    }
                    if (key.Type.Equals(LazyLib.ActionBar.KeyType.Item))
                    {
                        BarItems.Add(new BarItem(key.ItemId, key.Bar, key.Key));
                        Logging.Debug($"Found item: {ItemHelper.GetNameById((uint) key.ItemId)} : {key.Bar} : {key.Key}", new object[0]);
                    }
                }
            }
            LoadedKeys.Clear();
            GC.Collect();
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);

        public static int SpellsLoaded =>
            Spells.Count;
    }
}

