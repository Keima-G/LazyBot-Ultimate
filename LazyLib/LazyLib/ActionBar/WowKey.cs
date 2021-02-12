namespace LazyLib.ActionBar
{
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class WowKey
    {
        private readonly uint Id;

        public WowKey(uint id, int bar, int key)
        {
            this.Id = id;
            this.Bar = bar;
            this.Key = key;
        }

        public int Bar { get; set; }

        public int Key { get; set; }

        public int SpellId =>
            (int) this.Id;

        public KeyType Type =>
            ((this.Id & 0x80000000) == 0) ? KeyType.Spell : KeyType.Item;

        public int ItemId
        {
            get
            {
                long num1 = this.Id & 0x7fffffff;
                return (int) num1;
            }
        }

        private enum KeyTypeFlag : uint
        {
            ITEM = 0x80000000
        }
    }
}

