namespace LazyEvo.LGrindEngine
{
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class SubProfile
    {
        private readonly Random _ran = new Random();
        public List<uint> Factions;
        public List<string> Ignore;
        private int _currentHotSpotIndex = -1;

        public SubProfile()
        {
            this.Name = "Unnamed";
            this.PlayerMinLevel = 0;
            this.PlayerMaxLevel = 0x63;
            this.MobMinLevel = 0;
            this.MobMaxLevel = 0x63;
            this.SpotRoamDistance = 40;
            this.Order = false;
            this.Spots = new List<Location>();
            this.Factions = new List<uint>();
            this.Ignore = new List<string>();
        }

        public Location GetNextHotSpot()
        {
            if (this.Spots.Count == 0)
            {
                LazyEvo.Public.LazyHelpers.StopAll("Subprofile: " + this.Name + " does not have any spots");
            }
            if (this.Spots.Count == 1)
            {
                return this.Spots[0];
            }
            if (!this.Order)
            {
                this._currentHotSpotIndex = this._ran.Next(this.Spots.Count);
            }
            else
            {
                this._currentHotSpotIndex++;
                if (this._currentHotSpotIndex >= this.Spots.Count)
                {
                    this._currentHotSpotIndex = 0;
                }
            }
            if (this._currentHotSpotIndex < this.Spots.Count)
            {
                return this.Spots[this._currentHotSpotIndex];
            }
            Logging.Write(LogType.Warning, "Could not find a valid spot - spot bot and load a valid profile", new object[0]);
            return new Location(0f, 0f, 0f);
        }

        public string Name { get; set; }

        public int PlayerMinLevel { get; set; }

        public int PlayerMaxLevel { get; set; }

        public int MobMinLevel { get; set; }

        public int MobMaxLevel { get; set; }

        public int SpotRoamDistance { get; set; }

        public List<Location> Spots { get; set; }

        public bool Order { get; set; }

        public Location CurrentSpot
        {
            get
            {
                if (this.Spots.Count == 0)
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Subprofile: " + this.Name + " does not have any spots");
                }
                return ((this._currentHotSpotIndex != -1) ? ((this.Spots.Count != 1) ? this.Spots[this._currentHotSpotIndex] : this.Spots[0]) : this.Spots[0]);
            }
        }

        public Location ClosestSpot
        {
            get
            {
                if (this.Spots.Count == 0)
                {
                    LazyEvo.Public.LazyHelpers.StopAll("Subprofile: " + this.Name + " does not have any spots");
                }
                double maxValue = double.MaxValue;
                Location location = null;
                foreach (Location location2 in this.Spots)
                {
                    if (location2.DistanceToSelf < maxValue)
                    {
                        maxValue = location2.DistanceToSelf;
                        location = location2;
                    }
                }
                return location;
            }
        }
    }
}

