namespace LazyEvo.Forms
{
    using System;
    using System.Runtime.CompilerServices;

    internal class Plugin
    {
        public Plugin(string name, string fileName)
        {
            this.FileName = fileName;
            this.Name = name;
        }

        public override string ToString() => 
            this.FileName + "(" + this.Name + ")";

        public string FileName { get; set; }

        public string Name { get; set; }
    }
}

