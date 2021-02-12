namespace LazyEvo.Debug
{
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class PPlayerSelfUtils : PPlayerSelf
    {
        public PPlayerSelfUtils(uint baseAddress) : base(baseAddress)
        {
        }

        public List<NameValuePair> GetNameValuePairs()
        {
            uint baseAddress = base.BaseAddress;
            List<NameValuePair> list = new List<NameValuePair>();
            list.AddRange(new PPlayerUtils(base.BaseAddress).GetNameValuePairs());
            list.Add(new NameValuePair("", ""));
            list.Add(new NameValuePair("PPlayerSelf", ""));
            list.Add(new NameValuePair("ZoneId", base.ZoneId.ToString()));
            list.Add(new NameValuePair("Blood1", base.BloodRune1Ready.ToString()));
            list.Add(new NameValuePair("Blood2", base.BloodRune2Ready.ToString()));
            list.Add(new NameValuePair("Unholy1", base.UnholyRune1Ready.ToString()));
            list.Add(new NameValuePair("Unholy2", base.UnholyRune2Ready.ToString()));
            list.Add(new NameValuePair("Frost1", base.FrostRune1Ready.ToString()));
            list.Add(new NameValuePair("Frost2", base.FrostRune2Ready.ToString()));
            list.Add(new NameValuePair("Bagspace", Inventory.FreeBagSlots.ToString()));
            return list;
        }
    }
}

