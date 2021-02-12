namespace LazyEvo.Debug
{
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class PPlayerUtils : PPlayer
    {
        public PPlayerUtils(uint baseAddress) : base(baseAddress)
        {
        }

        public List<NameValuePair> GetNameValuePairs()
        {
            bool flag = base.BaseAddress != 0;
            List<NameValuePair> list = new List<NameValuePair>();
            list.AddRange(new PUnitUtils(base.BaseAddress).GetNameValuePairs());
            list.Add(new NameValuePair("", ""));
            list.Add(new NameValuePair("PPLayer", ""));
            list.Add(new NameValuePair("Ghost", flag ? (base.IsGhost) : ""));
            list.Add(new NameValuePair("Combat", flag ? (base.IsInCombat) : ""));
            list.Add(new NameValuePair("Name", flag ? (this.Name ?? "") : ""));
            list.Add(new NameValuePair("Mounted", flag ? (base.IsMounted) : ""));
            return list;
        }
    }
}

