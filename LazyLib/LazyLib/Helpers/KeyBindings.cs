namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class KeyBindings
    {
        private static List<KeyBinding> _bindings = new List<KeyBinding>();

        public static bool CheckBind(string command, string key)
        {
            if (GetKeysForCommand(command).Contains(key))
            {
                return true;
            }
            Logging.Write(LogType.Error, "Key: " + command.ToLower() + " potentially bound incorrectly, should be: " + key.ToLower(), new object[0]);
            return false;
        }

        internal static List<KeyBinding> GetBindings() => 
            _bindings;

        internal static List<string> GetCommandsForKey(string key) => 
            (from binding in GetBindings()
                where binding.Key == key
                select binding.Command).ToList<string>();

        internal static List<string> GetKeysForCommand(string action) => 
            (from binding in GetBindings()
                where binding.Command == action
                select binding.Key).ToList<string>();

        internal static void LoadBindings()
        {
            uint[] numArray5;
            List<KeyBinding> list = new List<KeyBinding>();
            uint num = Memory.ReadRelative<uint>(new uint[] { 0x7eadd8 });
            uint[] addresses = new uint[] { num + 0xb8 };
            for (uint i = Memory.Read<uint>(addresses); i != 0; i = Memory.Read<uint>(numArray5))
            {
                uint[] numArray3 = new uint[] { i + 20 };
                string str = Memory.ReadUtf8(Memory.Read<uint>(numArray3), 100);
                uint[] numArray4 = new uint[] { i + 40 };
                string str2 = Memory.ReadUtf8(Memory.Read<uint>(numArray4), 100);
                if ((str.Length > 0) && (str2.Length > 0))
                {
                    KeyBinding item = new KeyBinding {
                        Command = str2,
                        Key = str
                    };
                    list.Add(item);
                }
                numArray5 = new uint[1];
                uint[] numArray6 = new uint[] { num + 0xb0 };
                numArray5[0] = (i + Memory.Read<uint>(numArray6)) + 4;
            }
            _bindings = list;
        }
    }
}

