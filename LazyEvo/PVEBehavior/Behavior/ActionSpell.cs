namespace LazyEvo.PVEBehavior.Behavior
{
    using LazyLib.ActionBar;
    using LazyLib.Helpers;
    using System;
    using System.Xml;

    internal class ActionSpell : LazyEvo.PVEBehavior.Behavior.Action
    {
        private bool Exist;
        private string _name;
        private BarSpell _spell;
        private bool chek;

        public ActionSpell()
        {
        }

        public ActionSpell(string name)
        {
            this._name = name;
        }

        public override void Execute(int globalcooldown)
        {
            if (this.DoesKeyExist)
            {
                if (!KeyHelper.HasKey(this._name))
                {
                    this._spell = null;
                }
                if (this._spell == null)
                {
                    this._spell = BarMapper.GetSpellByName(this._name);
                    this._spell.SetCooldown(globalcooldown);
                    KeyHelper.AddKey(this._name, "", this._spell.Bar.ToString(), this._spell.Key.ToString());
                }
                this._spell.CastSpell();
            }
        }

        public override string GetXml() => 
            "<Type>ActionSpell</Type>" + "<Name>" + this._name + "</Name>";

        public override void Load(XmlNode node)
        {
            foreach (XmlNode node2 in node)
            {
                if (node2.Name.Equals("Name"))
                {
                    this._name = node2.InnerText;
                }
            }
        }

        public override bool DoesKeyExist
        {
            get
            {
                if (!this.chek)
                {
                    this.chek = true;
                    this.Exist = BarMapper.HasSpellByName(this._name);
                }
                return this.Exist;
            }
        }

        public override string Name =>
            this._name;

        public override bool IsReady =>
            BarMapper.IsSpellReadyByName(this._name);
    }
}

