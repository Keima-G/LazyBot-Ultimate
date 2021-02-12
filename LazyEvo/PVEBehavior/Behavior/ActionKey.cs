namespace LazyEvo.PVEBehavior.Behavior
{
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Xml;

    internal class ActionKey : LazyEvo.PVEBehavior.Behavior.Action
    {
        private Ticker _globalCooldown;
        private KeyWrapper _keyWrapper;
        private string _name;
        private int _times;

        public ActionKey()
        {
        }

        public ActionKey(string name, string bar, string key, string special, int times)
        {
            this._times = times;
            this.Bar = bar;
            this.Key = key;
            this.Special = special;
            this._name = name;
        }

        public override void Execute(int globalCooldown)
        {
            this._globalCooldown ??= new Ticker((double) globalCooldown);
            if (this._times <= 0)
            {
                if (LazyLib.Wow.ObjectManager.MyPlayer.IsValid && !LazyLib.Wow.ObjectManager.MyPlayer.IsMe)
                {
                    LazyLib.Wow.ObjectManager.MyPlayer.Target.Face();
                }
                this.GetKey.SendKey();
                while (LazyLib.Wow.ObjectManager.MyPlayer.IsCasting || !this._globalCooldown.IsReady)
                {
                    Thread.Sleep(10);
                }
            }
            else
            {
                int num = this._times;
                while (num > 0)
                {
                    this.GetKey.SendKey();
                    this._globalCooldown.Reset();
                    while (true)
                    {
                        if (!LazyLib.Wow.ObjectManager.MyPlayer.IsCasting && this._globalCooldown.IsReady)
                        {
                            num--;
                            break;
                        }
                        Thread.Sleep(10);
                    }
                }
            }
        }

        public override string GetXml()
        {
            object[] objArray = new object[] { ((("<Type>ActionKey</Type>" + "<Name>" + this._name + "</Name>") + "<Bar>" + this.Bar + "</Bar>") + "<Key>" + this.Key + "</Key>") + "<Special>" + this.Special + "</Special>", "<Times>", this._times, "</Times>" };
            return string.Concat(objArray);
        }

        public override void Load(XmlNode node)
        {
            foreach (XmlNode node2 in node)
            {
                if (node2.Name.Equals("Name"))
                {
                    this._name = node2.InnerText;
                }
                if (node2.Name.Equals("Bar"))
                {
                    this.Bar = node2.InnerText;
                }
                if (node2.Name.Equals("Key"))
                {
                    this.Key = node2.InnerText;
                }
                if (node2.Name.Equals("Special"))
                {
                    this.Special = node2.InnerText;
                }
                if (node2.Name.Equals("Times"))
                {
                    this._times = Convert.ToInt32(node2.InnerText);
                }
            }
        }

        public string Bar { get; private set; }

        public string Key { get; private set; }

        public string Special { get; private set; }

        public KeyWrapper GetKey
        {
            get
            {
                this._keyWrapper ??= new KeyWrapper(this._name, this.Special, this.Bar, this.Key);
                return this._keyWrapper;
            }
        }

        public int Times =>
            this._times;

        public override bool IsReady =>
            true;

        public override bool DoesKeyExist =>
            true;

        public override string Name =>
            this._name;
    }
}

