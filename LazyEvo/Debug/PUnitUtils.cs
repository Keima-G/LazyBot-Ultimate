namespace LazyEvo.Debug
{
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;

    internal class PUnitUtils : PUnit
    {
        public PUnitUtils(uint baseAddress) : base(baseAddress)
        {
        }

        public List<NameValuePair> GetNameValuePairs()
        {
            bool flag = base.BaseAddress != 0;
            List<NameValuePair> list = new List<NameValuePair>();
            list.AddRange(new PObjectUtils(base.BaseAddress).GetNameValuePairs());
            list.Add(new NameValuePair("", ""));
            list.Add(new NameValuePair("PUnit", ""));
            list.Add(new NameValuePair("Critter", flag ? (base.Critter) : ""));
            list.Add(new NameValuePair("IsPlayer", flag ? (base.IsPlayer) : ""));
            list.Add(new NameValuePair("CharmedBy", flag ? (base.CharmedBy) : ""));
            list.Add(new NameValuePair("SummonedBy", flag ? (base.SummonedBy) : ""));
            list.Add(new NameValuePair("CreatedBy", flag ? (base.CreatedBy) : ""));
            list.Add(new NameValuePair("BaseHealth", flag ? (base.BaseHealth) : ""));
            list.Add(new NameValuePair("BaseMana", flag ? (base.BaseMana) : ""));
            list.Add(new NameValuePair("Health", flag ? (base.Health) : ""));
            list.Add(new NameValuePair("Mana", flag ? (base.Mana) : ""));
            list.Add(new NameValuePair("Maximum Health", flag ? (base.MaximumHealthPoints) : ""));
            list.Add(new NameValuePair("Maximum Mana", flag ? (base.MaximumManaPoints) : ""));
            list.Add(new NameValuePair("Rage", flag ? (base.Rage) : ""));
            list.Add(new NameValuePair("Energy", flag ? (base.Energy) : ""));
            list.Add(new NameValuePair("RunicPower", flag ? (base.RunicPower) : ""));
            list.Add(new NameValuePair("MaximumRage", flag ? (base.MaximumRage) : ""));
            list.Add(new NameValuePair("MaximumEnergy", flag ? (base.MaximumEnergy) : ""));
            list.Add(new NameValuePair("MaximumRunicPower", flag ? (base.MaximumRunicPower) : ""));
            list.Add(new NameValuePair("Level", flag ? (base.Level) : ""));
            list.Add(new NameValuePair("Class", flag ? (base.Class ?? "") : ""));
            list.Add(new NameValuePair("CreatureType", flag ? (base.CreatureType) : ""));
            list.Add(new NameValuePair("Classification", flag ? (base.Classification) : ""));
            list.Add(new NameValuePair("Speed", flag ? (base.Speed) : ""));
            list.Add(new NameValuePair("IsMoving", flag ? (base.IsMoving) : ""));
            list.Add(new NameValuePair("ShapeshiftForm", flag ? (base.ShapeshiftForm) : ""));
            list.Add(new NameValuePair("IsFlying", flag ? (base.IsFlying) : ""));
            list.Add(new NameValuePair("Swimming", flag ? (base.IsSwimming) : ""));
            list.Add(new NameValuePair("Reaction", flag ? (base.Reaction) : ""));
            list.Add(new NameValuePair("Casting", flag ? (base.IsCasting) : ""));
            return list;
        }
    }
}

