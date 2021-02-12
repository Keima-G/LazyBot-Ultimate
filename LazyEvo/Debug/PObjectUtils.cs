namespace LazyEvo.Debug
{
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class PObjectUtils : PObject
    {
        public PObjectUtils(uint baseAddress) : base(baseAddress)
        {
        }

        public List<NameValuePair> GetNameValuePairs()
        {
            bool flag = base.BaseAddress != 0;
            return new List<NameValuePair> { 
                new NameValuePair("PObject", ""),
                new NameValuePair("GUID", flag ? (this.GUID) : ""),
                new NameValuePair("Type", flag ? (base.Type) : ""),
                new NameValuePair("X", flag ? (this.X) : ""),
                new NameValuePair("Y", flag ? (this.Y) : ""),
                new NameValuePair("Z", flag ? (this.Z) : ""),
                new NameValuePair("Facing", flag ? (this.Facing) : ""),
                new NameValuePair("IsMe", flag ? (base.IsMe) : "")
            };
        }
    }
}

