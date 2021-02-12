namespace LazyLib.Wow
{
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PPlayerSelf : PPlayer
    {
        private readonly uint[] _healthStone;
        private readonly uint[] _mageFood;

        public PPlayerSelf(uint baseAddress) : base(baseAddress)
        {
            this._healthStone = new uint[] { 
                0x901c, 0x901e, 0x901d, 0x9019, 0x901b, 0x901a, 0x5659, 0x5657, 0x5658, 0x24cd, 0x4a45, 0x4a44, 0x4a43, 0x4a42, 0x1586, 0x1585,
                0x1587, 0x1588, 0x4a3d, 0x4a3c, 0x4a41, 0x4a40, 0x4a3f
            };
            this._mageFood = new uint[] { 0xffdb, 0xaa03, 0xa9fe, 0xffed, 0xffec, 0xffeb, 0xffdc };
        }

        public uint GetItemBySlot(int slot)
        {
            switch (slot)
            {
                case 1:
                    return base.GetStorageField<uint>((uint) 0x11b);

                case 2:
                    return base.GetStorageField<uint>((uint) 0x11d);

                case 3:
                    return base.GetStorageField<uint>((uint) 0x11f);

                case 4:
                    return base.GetStorageField<uint>((uint) 0x121);

                case 5:
                    return base.GetStorageField<uint>((uint) 0x123);

                case 6:
                    return base.GetStorageField<uint>((uint) 0x125);

                case 7:
                    return base.GetStorageField<uint>((uint) 0x127);

                case 8:
                    return base.GetStorageField<uint>((uint) 0x129);

                case 9:
                    return base.GetStorageField<uint>((uint) 0x12b);

                case 10:
                    return base.GetStorageField<uint>((uint) 0x12d);

                case 11:
                    return base.GetStorageField<uint>((uint) 0x12f);

                case 12:
                    return base.GetStorageField<uint>((uint) 0x131);

                case 13:
                    return base.GetStorageField<uint>((uint) 0x133);

                case 14:
                    return base.GetStorageField<uint>((uint) 0x135);

                case 15:
                    return base.GetStorageField<uint>((uint) 0x137);

                case 0x10:
                    return base.GetStorageField<uint>((uint) 0x139);

                case 0x11:
                    return base.GetStorageField<uint>((uint) 0x13b);

                case 0x12:
                    return base.GetStorageField<uint>((uint) 0x13d);

                case 0x13:
                    return base.GetStorageField<uint>((uint) 0x13f);
            }
            return 0;
        }

        private bool IsRuneReady(int runeIndex) => 
            ((1 << (runeIndex & 0x1f)) & Memory.ReadRelative<byte>(new uint[] { 0x824388 })) != 0;

        public void TargetSelf()
        {
            KeyHelper.SendKey("F1");
            Thread.Sleep(100);
        }

        public bool ShouldRepair =>
            (InterfaceHelper.GetFrameByName("DurabilityFrame") != null) ? InterfaceHelper.GetFrameByName("DurabilityFrame").IsVisible : false;

        public bool HasAttackers =>
            LazyLib.Wow.ObjectManager.GetAttackers.Count != 0;

        public int CoinAge =>
            base.GetStorageField<int>((uint) 0x492);

        public List<uint> GetItemsEquippedId =>
            new List<uint> { 
                base.GetStorageField<uint>((uint) 0x11b),
                base.GetStorageField<uint>((uint) 0x11d),
                base.GetStorageField<uint>((uint) 0x11f),
                base.GetStorageField<uint>((uint) 0x121),
                base.GetStorageField<uint>((uint) 0x123),
                base.GetStorageField<uint>((uint) 0x125),
                base.GetStorageField<uint>((uint) 0x127),
                base.GetStorageField<uint>((uint) 0x129),
                base.GetStorageField<uint>((uint) 0x12b),
                base.GetStorageField<uint>((uint) 0x12d),
                base.GetStorageField<uint>((uint) 0x12f),
                base.GetStorageField<uint>((uint) 0x131),
                base.GetStorageField<uint>((uint) 0x133),
                base.GetStorageField<uint>((uint) 0x135),
                base.GetStorageField<uint>((uint) 0x137),
                base.GetStorageField<uint>((uint) 0x139),
                base.GetStorageField<uint>((uint) 0x13b),
                base.GetStorageField<uint>((uint) 0x13d),
                base.GetStorageField<uint>((uint) 0x13f)
            };

        public bool LootWinOpen
        {
            get
            {
                uint[] addresses = new uint[] { Memory.BaseAddress + 0x7fa8d8 };
                return (Memory.Read<uint>(addresses) != 0);
            }
        }

        public bool MainHandHasTempEnchant
        {
            get
            {
                PItem mainHand = LazyLib.Wow.ObjectManager.MyPlayer.MainHand;
                return ((mainHand == null) || Enumerable.Any<uint>(mainHand.TempEnchants, oneEnchant => oneEnchant != 0));
            }
        }

        public bool OffHandHasTempEnchant
        {
            get
            {
                bool flag;
                PItem offHand = LazyLib.Wow.ObjectManager.MyPlayer.OffHand;
                if (offHand == null)
                {
                    return true;
                }
                using (List<uint>.Enumerator enumerator = offHand.TempEnchants.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            if (enumerator.Current == 0)
                            {
                                continue;
                            }
                            flag = true;
                        }
                        else
                        {
                            return false;
                        }
                        break;
                    }
                }
                return flag;
            }
        }

        internal List<ulong> GUIDOfItemsInBag
        {
            get
            {
                List<ulong> list = new List<ulong>();
                for (uint i = 0; i < 0x10; i++)
                {
                    list.Add(base.GetStorageField<ulong>((uint) (370 + (8 * i))));
                }
                return list;
            }
        }

        internal List<ulong> GUIDOfBags
        {
            get
            {
                ulong num;
                List<ulong> list = new List<ulong>();
                try
                {
                    num = Memory.ReadRelative<ulong>(new uint[] { 0x823540 });
                    list.Add(num);
                }
                catch
                {
                }
                try
                {
                    num = Memory.ReadRelative<ulong>(new uint[] { 0x823548 });
                    list.Add(num);
                }
                catch
                {
                }
                try
                {
                    num = Memory.ReadRelative<ulong>(new uint[] { 0x823550 });
                    list.Add(num);
                }
                catch
                {
                }
                try
                {
                    list.Add(Memory.ReadRelative<ulong>(new uint[] { 0x823558 }));
                }
                catch
                {
                }
                return list;
            }
        }

        public PItem MainHand
        {
            get
            {
                PItem item2;
                using (List<PItem>.Enumerator enumerator = LazyLib.Wow.ObjectManager.GetItems.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PItem current = enumerator.Current;
                            uint entryId = current.EntryId;
                            if (!entryId.Equals(base.GetStorageField<uint>((uint) 0x139)))
                            {
                                continue;
                            }
                            item2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return item2;
            }
        }

        public PItem OffHand
        {
            get
            {
                PItem item2;
                using (List<PItem>.Enumerator enumerator = LazyLib.Wow.ObjectManager.GetItems.GetEnumerator())
                {
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PItem current = enumerator.Current;
                            uint entryId = current.EntryId;
                            if (!entryId.Equals(base.GetStorageField<uint>((uint) 0x13b)))
                            {
                                continue;
                            }
                            item2 = current;
                        }
                        else
                        {
                            return null;
                        }
                        break;
                    }
                }
                return item2;
            }
        }

        public int MageRefreshment =>
            Enumerable.Count<PItem>(LazyLib.Wow.ObjectManager.GetItems, var => this._mageFood.Contains<uint>(var.EntryId));

        public int HealthStoneCount =>
            Enumerable.Count<PItem>(LazyLib.Wow.ObjectManager.GetItems, var => this._healthStone.Contains<uint>(var.EntryId));

        public int ComboPoints =>
            Memory.ReadRelative<byte>(new uint[] { 0x7d084d });

        public bool WinterGraspInProgress =>
            base.HasBuff(0x93a3) || (base.HasBuff(0x8200) || base.HasBuff(0xd94d));

        public bool BloodRune1Ready =>
            this.IsRuneReady(0);

        public bool BloodRune2Ready =>
            this.IsRuneReady(1);

        public bool UnholyRune1Ready =>
            this.IsRuneReady(2);

        public bool UnholyRune2Ready =>
            this.IsRuneReady(3);

        public bool FrostRune1Ready =>
            this.IsRuneReady(4);

        public bool FrostRune2Ready =>
            this.IsRuneReady(5);

        public uint ZoneId =>
            Memory.ReadRelative<uint>(new uint[] { 0x7d080c });

        public bool InVashjir =>
            (this.ZoneId == 0x1419) || ((this.ZoneId == 0x1418) || ((this.ZoneId == 0x141a) || (this.ZoneId == 0x12cf)));

        public string ZoneText =>
            Memory.ReadUtf8(Memory.ReadRelative<uint>(new uint[] { 0x7d0788 }), 40);

        public string WorldMap =>
            Memory.ReadUtf8(Memory.ReadRelative<uint>(new uint[] { 0x7d0788 }), 40);

        public int ExperiencePercentage
        {
            get
            {
                try
                {
                    return ((100 * this.Experience) / this.NextLevel);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int Experience =>
            base.GetStorageField<int>((uint) 0x27a);

        public int NextLevel =>
            base.GetStorageField<int>((uint) 0x27b);

        public string RedMessage =>
            Memory.ReadUtf8StringRelative(0x7cfb90, 0x100);
    }
}

