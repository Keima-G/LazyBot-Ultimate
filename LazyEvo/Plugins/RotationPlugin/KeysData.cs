namespace LazyEvo.Plugins.RotationPlugin
{
    using System;

    internal class KeysData
    {
        internal int Code;
        internal string Text;

        public KeysData(string text, int code)
        {
            this.Text = text;
            this.Code = code;
        }
    }
}

