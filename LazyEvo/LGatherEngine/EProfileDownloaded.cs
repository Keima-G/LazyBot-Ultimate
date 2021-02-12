namespace LazyEvo.LGatherEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public class EProfileDownloaded : EventArgs
    {
        public EProfileDownloaded(string path)
        {
            this.Path = path;
        }

        public string Path { get; private set; }
    }
}

