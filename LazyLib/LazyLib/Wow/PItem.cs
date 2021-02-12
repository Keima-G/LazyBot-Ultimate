namespace LazyLib.Wow
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PItem : PObject
    {
        public PItem(uint baseAddress) : base(baseAddress)
        {
        }

        public int Durability =>
            base.GetStorageField<int>((uint) 60);

        public ulong Info =>
            base.GetStorageField<ulong>((uint) 4);

        public uint EntryId
        {
            get
            {
                try
                {
                    return base.GetStorageField<uint>((uint) 3);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public float GetDurabilityPercentage
        {
            get
            {
                try
                {
                    return ((((float) this.Durability) / ((float) this.MaximumDurability)) * 100f);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public List<uint> TempEnchants =>
            new List<uint> { 
                base.GetStorageField<uint>((uint) 0x19),
                base.GetStorageField<uint>((uint) 0x1b)
            };

        public List<uint> Enchants =>
            new List<uint> { 
                base.GetStorageField<uint>((uint) 0x16),
                base.GetStorageField<uint>((uint) 0x18),
                base.GetStorageField<uint>((uint) 0x19),
                base.GetStorageField<uint>((uint) 0x1b),
                base.GetStorageField<uint>((uint) 0x1c),
                base.GetStorageField<uint>((uint) 30),
                base.GetStorageField<uint>((uint) 0x1f),
                base.GetStorageField<uint>((uint) 0x21),
                base.GetStorageField<uint>((uint) 0x22),
                base.GetStorageField<uint>((uint) 0x24),
                base.GetStorageField<uint>((uint) 0x25),
                base.GetStorageField<uint>((uint) 0x27),
                base.GetStorageField<uint>((uint) 40),
                base.GetStorageField<uint>((uint) 0x2a),
                base.GetStorageField<uint>((uint) 0x2b),
                base.GetStorageField<uint>((uint) 0x2d),
                base.GetStorageField<uint>((uint) 0x2e),
                base.GetStorageField<uint>((uint) 0x30),
                base.GetStorageField<uint>((uint) 0x31),
                base.GetStorageField<uint>((uint) 0x33),
                base.GetStorageField<uint>((uint) 0x34),
                base.GetStorageField<uint>((uint) 0x36),
                base.GetStorageField<uint>((uint) 0x37),
                base.GetStorageField<uint>((uint) 0x39)
            };

        public ulong Contained =>
            base.GetStorageField<ulong>((uint) 8);

        public int MaximumDurability =>
            base.GetStorageField<int>((uint) 0x3d);

        public int StackCount =>
            base.GetStorageField<int>((uint) 14);

        public int Charges =>
            base.GetStorageField<int>((uint) 0x10);

        public bool HasCharges =>
            this.Charges > 0;
    }
}

