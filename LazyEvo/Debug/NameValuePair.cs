namespace LazyEvo.Debug
{
    using System;

    internal class NameValuePair
    {
        private readonly string _mValue = "";
        public string MName = "";

        public NameValuePair(string name, string value)
        {
            this.MName = name;
            this._mValue = value;
        }

        public string Name =>
            this.MName;

        public string Value =>
            this._mValue;
    }
}

