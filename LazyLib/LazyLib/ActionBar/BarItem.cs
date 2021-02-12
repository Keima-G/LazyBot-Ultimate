namespace LazyLib.ActionBar
{
    using LazyLib.Helpers;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class BarItem
    {
        private readonly KeyWrapper _wrap;

        public BarItem(int id, int bar, int key)
        {
            this.ItemId = id;
            this.Bar = bar;
            this.Key = key;
            this._wrap = new KeyWrapper("Unkown item", "none", bar.ToString(), key.ToString());
        }

        public void SendItem()
        {
            this._wrap.SendKey();
        }

        public int ItemId { get; private set; }

        public int Bar { get; set; }

        public int Key { get; set; }
    }
}

