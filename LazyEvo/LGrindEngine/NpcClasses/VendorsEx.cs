namespace LazyEvo.LGrindEngine.NpcClasses
{
    using LazyLib.Wow;
    using System;
    using System.Runtime.CompilerServices;

    internal class VendorsEx
    {
        public VendorsEx(LazyEvo.LGrindEngine.NpcClasses.VendorType vendorType, string name, LazyLib.Wow.Location location, int entryId)
        {
            this.VendorType = vendorType;
            this.Name = name;
            this.Location = location;
            this.TrainClass = LazyEvo.LGrindEngine.NpcClasses.TrainClass.Unknown;
            this.EntryId = entryId;
        }

        public VendorsEx(LazyEvo.LGrindEngine.NpcClasses.VendorType vendorType, string name, LazyLib.Wow.Location location, LazyEvo.LGrindEngine.NpcClasses.TrainClass trainClass, int entryId)
        {
            this.VendorType = vendorType;
            this.Name = name;
            this.Location = location;
            this.TrainClass = trainClass;
            this.EntryId = entryId;
        }

        public LazyEvo.LGrindEngine.NpcClasses.VendorType VendorType { get; private set; }

        public string Name { get; private set; }

        public LazyLib.Wow.Location Location { get; private set; }

        public LazyEvo.LGrindEngine.NpcClasses.TrainClass TrainClass { get; private set; }

        public int EntryId { get; set; }
    }
}

