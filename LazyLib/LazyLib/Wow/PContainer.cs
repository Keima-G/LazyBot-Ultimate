namespace LazyLib.Wow
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PContainer : PObject
    {
        public PContainer(uint baseAddress) : base(baseAddress)
        {
        }

        public int Slots =>
            base.GetStorageField<int>((uint) 0x40);
    }
}

