namespace LazyLib.Wow
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;

    public class PGameObject : PObject
    {
        public PGameObject(uint baseAddress) : base(baseAddress)
        {
        }

        public string Name
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[1];
                    uint[] numArray2 = new uint[] { base.BaseAddress + 420 };
                    addresses[0] = Memory.Read<uint>(numArray2) + 0x90;
                    return Memory.ReadUtf8(Memory.Read<uint>(addresses), 100);
                }
                catch
                {
                    return "Failed";
                }
            }
        }

        public int DisplayId =>
            base.GetStorageField<int>((uint) 8);

        public int Faction =>
            base.GetStorageField<int>((uint) 15);

        public bool IsInSchool
        {
            get
            {
                if ((Fishing.bobberX <= (LazyLib.Wow.Globals._schoolLocX + 4f)) && ((Fishing.bobberX >= (LazyLib.Wow.Globals._schoolLocX - 4f)) && ((Fishing.bobberY <= (LazyLib.Wow.Globals._schoolLocY + 4f)) && (Fishing.bobberY >= (LazyLib.Wow.Globals._schoolLocY - 4f)))))
                {
                    return true;
                }
                Logging.Debug("Bobber is not in a School", new object[0]);
                return false;
            }
        }

        public bool IsBobbing
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0xbc };
                return (Memory.Read<byte>(addresses) != 0);
            }
        }

        public int GameObjectType =>
            (base.GetStorageField<int>((uint) 0x11) >> 8) & 0xff;

        public int Level =>
            base.GetStorageField<int>((uint) 0x10);

        public override float X
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0xe8 };
                return Memory.Read<float>(addresses);
            }
        }

        public override float Y
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0xec };
                return Memory.Read<float>(addresses);
            }
        }

        public override float Z
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 240 };
                return Memory.Read<float>(addresses);
            }
        }

        public override float Facing
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[1];
                    uint[] numArray2 = new uint[1];
                    uint[] numArray3 = new uint[] { base.BaseAddress + 0x1c8 };
                    numArray2[0] = Memory.Read<uint>(numArray3) + 4;
                    addresses[0] = (Memory.Read<uint>(numArray2) + 0x20) + 0x100;
                    long num = Memory.Read<long>(addresses);
                    double num2 = (num >> 0x2a) * 4.768372E-07f;
                    double num3 = (((num << 0x16) >> 0x20) >> 11) * 9.536743E-07f;
                    double num4 = ((num << 0x2b) >> 0x2b) * 9.536743E-07f;
                    double num5 = ((num2 * num2) + (num3 * num3)) + (num4 * num4);
                    num5 = (Math.Abs((double) (num5 - 1.0)) < 9.5367431640625E-07) ? 0.0 : ((double) ((float) Math.Sqrt(1.0 - num5)));
                    double num8 = Math.Atan2(((2.0 * num4) * num5) + ((2.0 * num2) * num3), (1.0 - ((2.0 * num3) * num3)) - ((2.0 * num4) * num4));
                    if (num8 < 0.0)
                    {
                        num8 = (float) (6.28 - (-1.0 * num8));
                    }
                    return (float) num8;
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public override LazyLib.Wow.Location Location =>
            new LazyLib.Wow.Location(this.X, this.Y, this.Z);
    }
}

