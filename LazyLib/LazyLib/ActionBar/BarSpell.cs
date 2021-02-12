namespace LazyLib.ActionBar
{
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class BarSpell
    {
        private Ticker _globalCooldown;

        public BarSpell(int id, int bar, int key, string name)
        {
            this.SpellId = id;
            this.Bar = bar;
            this._globalCooldown = new Ticker(1600.0);
            this.Key = key;
            this.Name = name;
            KeyHelper.AddKey(name, "", this.Bar.ToString(), this.Key.ToString());
        }

        public void CastSpell()
        {
            KeyHelper.SendKey(this.Name);
            this._globalCooldown.Reset();
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsCasting || !this._globalCooldown.IsReady)
            {
                Thread.Sleep(10);
            }
        }

        public void SetCooldown(int cooldown)
        {
            this._globalCooldown = new Ticker((double) cooldown);
            this.Cooldown = cooldown;
        }

        public int SpellId { get; private set; }

        public int Bar { get; set; }

        public int Key { get; set; }

        public string Name { get; private set; }

        public int Cooldown { get; private set; }

        public bool DoesKeyExist =>
            BarMapper.HasSpellByName(this.Name);

        public bool IsReady =>
            BarMapper.IsSpellReadyById(this.SpellId);
    }
}

