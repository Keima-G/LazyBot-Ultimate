namespace LazyEvo.LGatherEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public class OnlineProfile
    {
        public OnlineProfile(string id, string name, string creator, string zone, string comment, string downloads)
        {
            this.Name = name;
            this.Creator = creator;
            this.Zone = zone;
            this.Comment = comment;
            this.Downloads = downloads;
            this.Id = id;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public string Creator { get; private set; }

        public string Zone { get; private set; }

        public string Comment { get; private set; }

        public string Downloads { get; private set; }
    }
}

