namespace LazyLib.Wow
{
    using LazyLib;
    using LazyLib.ActionBar;
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PUnit : PObject
    {
        public PUnit(uint baseAddress) : base(baseAddress)
        {
        }

        public int BuffStacks(int spellId)
        {
            Func<WoWAura, bool> func = null;
            try
            {
                if (func == null)
                {
                    func = woWAura => woWAura.SpellId == spellId;
                }
                CS$<>9__CachedAnonymousMethodDelegate1f ??= woWAura => woWAura.Stack;
                return Enumerable.Select<WoWAura, short>(Enumerable.Where<WoWAura>(this.GetAuras, func), CS$<>9__CachedAnonymousMethodDelegate1f).FirstOrDefault<short>();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public uint BuffTimeLeft(int spellId)
        {
            Func<WoWAura, bool> func = null;
            try
            {
                if (func == null)
                {
                    func = woWAura => woWAura.SpellId == spellId;
                }
                CS$<>9__CachedAnonymousMethodDelegate16 ??= woWAura => woWAura.SecondsLeft;
                return Enumerable.Select<WoWAura, uint>(Enumerable.Where<WoWAura>(this.GetAuras, func), CS$<>9__CachedAnonymousMethodDelegate16).FirstOrDefault<uint>();
            }
            catch
            {
                return 0;
            }
        }

        public void Face()
        {
            if (!this.Location.IsFacing())
            {
                this.Location.Face();
            }
        }

        public List<int> GetBuffs() => 
            (from woWAura in this.GetAuras select woWAura.SpellId).ToList<int>();

        public bool HasBuff(List<int> buffs)
        {
            try
            {
                if (Enumerable.Any<int>(buffs, new Func<int, bool>(this.HasBuff)))
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public bool HasBuff(int[] buff)
        {
            Func<int, bool> func = null;
            List<int> auras = this.GetBuffs();
            try
            {
                if (func == null)
                {
                    func = u => auras.Contains(u);
                }
                if (Enumerable.Any<int>(buff, func))
                {
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public bool HasBuff(int buff) => 
            this.GetBuffs().Contains(buff);

        public bool HasBuff(string buff) => 
            Enumerable.Any<int>(this.GetBuffs(), new Func<int, bool>(BarMapper.GetIdsFromName(buff).Contains));

        public bool HasBuff(int buff, bool playerShouldBeOwner)
        {
            Func<WoWAura, bool> func = null;
            if (!playerShouldBeOwner)
            {
                return this.HasBuff(buff);
            }
            try
            {
                if (func == null)
                {
                    func = woWAura => ((buff != woWAura.SpellId) || (woWAura.OwnerGUID != LazyLib.Wow.ObjectManager.MyPlayer.GUID)) ? (woWAura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.PetGUID) : true;
                }
                return Enumerable.Any<WoWAura>(this.GetAuras, func);
            }
            catch
            {
                return false;
            }
        }

        public bool HasBuff(string buff, bool playerShouldBeOwner)
        {
            if (!playerShouldBeOwner)
            {
                return this.HasBuff(buff);
            }
            IEnumerable<WoWAura> getAuras = this.GetAuras;
            List<int> buf = BarMapper.GetIdsFromName(buff);
            return Enumerable.Any<WoWAura>(getAuras, woWAura => (!buf.Contains(woWAura.SpellId) || (woWAura.OwnerGUID != LazyLib.Wow.ObjectManager.MyPlayer.GUID)) ? (woWAura.OwnerGUID == LazyLib.Wow.ObjectManager.MyPlayer.PetGUID) : true);
        }

        public bool HasWellKnownBuff(string buffName) => 
            this.HasBuff(BarMapper.GetIdByName(buffName));

        public bool HasWellKnownBuff(string buffName, bool playerIsOwner) => 
            this.HasBuff(BarMapper.GetIdByName(buffName), playerIsOwner);

        private bool HostileBackgroundTargetting()
        {
            Ticker ticker = new Ticker(4000.0);
            while (!ticker.IsReady)
            {
                ulong targetGUID = LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID;
                if (targetGUID.Equals(this.GUID))
                {
                    return true;
                }
                if (!this.Location.IsFacing())
                {
                    this.Location.Face();
                }
                Memory.Write<ulong>(Memory.BaseAddress + 0x7d07a0, this.GUID);
                Thread.Sleep(50);
                KeyHelper.SendKey("InteractWithMouseOver");
                Thread.Sleep(500);
            }
            return false;
        }

        private bool HostileTabTargetting()
        {
            Ticker ticker = new Ticker(4000.0);
            while (!ticker.IsReady)
            {
                ulong targetGUID = LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID;
                if (targetGUID.Equals(this.GUID))
                {
                    return true;
                }
                if (!this.Location.IsFacing())
                {
                    this.Location.Face();
                }
                KeyHelper.SendKey("TargetEnemy");
                Thread.Sleep(700);
            }
            return false;
        }

        public void Interact()
        {
            this.InteractWithTarget();
        }

        public void InteractWithTarget()
        {
            KeyHelper.SendKey("InteractTarget");
        }

        [DllImport("KERNEL32")]
        private static extern bool QueryPerformanceCounter(out long lpPerformanceCount);
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(out long lpFrequency);
        public void TargetChatTarget(string chatGUID)
        {
            Memory.Write<string>(Memory.BaseAddress + 0x7d07a0, chatGUID);
            Thread.Sleep(50);
            KeyHelper.SendKey("InteractWithMouseOver");
            Thread.Sleep(500);
            Logging.Write("TargetChatTarget: " + this.Name, new object[0]);
        }

        public bool TargetChatTargetNoMemWrite(string chatGUID)
        {
            Logging.Write("Target GUID Ulong: " + chatGUID, new object[0]);
            if (LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(chatGUID))
            {
                return true;
            }
            if (this.IsDead)
            {
                return this.TargetDead();
            }
            this.Target.Face();
            Ticker ticker = new Ticker(600.0);
            Thread.Sleep(500);
            while (true)
            {
                ulong targetGUID = LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID;
                if (targetGUID.Equals(chatGUID) || ticker.IsReady)
                {
                    if (!LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(chatGUID))
                    {
                        return false;
                    }
                    KeyHelper.SendKey("InteractTarget");
                    this.Target.Face();
                    Logging.Debug("TargetChatTargetNoMemWrite: " + LazyLib.Wow.ObjectManager.MyPlayer.Target.Name, new object[0]);
                    Thread.Sleep(0x1388);
                    return true;
                }
                KeyHelper.SendKey("TargetFriend");
                Logging.Debug("TargetChatTargetNoMemWrite: " + LazyLib.Wow.ObjectManager.MyPlayer.Target.Name, new object[0]);
                Thread.Sleep(0x3e8);
            }
        }

        private bool TargetDead() => 
            base.Interact(false);

        public bool TargetFriend()
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(this.GUID))
            {
                return true;
            }
            Logging.Write("[Unit]TargetingF: " + this.Name, new object[0]);
            if (this.IsDead)
            {
                return this.TargetDead();
            }
            this.Face();
            Ticker ticker = new Ticker(600.0);
            Thread.Sleep(500);
            while (true)
            {
                ulong targetGUID = LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID;
                if (targetGUID.Equals(this.GUID) || ticker.IsReady)
                {
                    if (LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(this.GUID))
                    {
                        this.Face();
                        return true;
                    }
                    Logging.Write("[Unit]Could not targetF: " + this.Name, new object[0]);
                    return false;
                }
                KeyHelper.SendKey("TargetFriend");
                Thread.Sleep(0x3e8);
            }
        }

        public bool TargetHostile()
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(this.GUID))
            {
                return true;
            }
            Logging.Debug("[Unit]TargetingH: " + this.Name, new object[0]);
            return (!this.IsDead ? (!LazySettings.BackgroundMode ? this.HostileTabTargetting() : this.HostileBackgroundTargetting()) : this.TargetDead());
        }

        public bool IsFacingAway =>
            (MoveHelper.NegativeAngle(this.Facing - LazyLib.Wow.ObjectManager.MyPlayer.Facing) > 5.5) || (MoveHelper.NegativeAngle(this.Facing - LazyLib.Wow.ObjectManager.MyPlayer.Facing) < 0.6);

        public double FacingPI =>
            (double) MoveHelper.NegativeAngle((float) Math.Atan2((double) (this.Y - LazyLib.Wow.ObjectManager.MyPlayer.Y), (double) (this.X - LazyLib.Wow.ObjectManager.MyPlayer.X)));

        public string PowerType
        {
            get
            {
                switch (this.PowerTypeId)
                {
                    case 0:
                        return "Mana";

                    case 1:
                        return "Rage";

                    case 2:
                        return "Focus";

                    case 3:
                        return "Energy";

                    case 6:
                        return "Runic Power";
                }
                return "";
            }
        }

        public string UnitRace
        {
            get
            {
                string str;
                switch (this.RaceId)
                {
                    case 1:
                        str = "Human";
                        break;

                    case 2:
                        str = "Orc";
                        break;

                    case 3:
                        str = "Dwarf";
                        break;

                    case 4:
                        str = "Night Elf";
                        break;

                    case 5:
                        str = "Undead";
                        break;

                    case 6:
                        str = "Tauren";
                        break;

                    case 7:
                        str = "Gnome";
                        break;

                    case 8:
                        str = "Troll";
                        break;

                    case 9:
                        str = "Goblin";
                        break;

                    case 10:
                        str = "Blood Elf";
                        break;

                    case 11:
                        str = "Draenei";
                        break;

                    case 12:
                        str = "Fel Orc";
                        break;

                    case 13:
                        str = "Naga";
                        break;

                    case 14:
                        str = "Broken";
                        break;

                    case 15:
                        str = "Skeleton";
                        break;

                    default:
                        str = "Unknown";
                        break;
                }
                return str;
            }
        }

        public float Speed
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0x814 };
                return Memory.Read<float>(addresses);
            }
        }

        public bool IsMoving =>
            this.Speed > 0f;

        public string Gender
        {
            get
            {
                string str;
                switch (this.GenderId)
                {
                    case 0:
                        str = "Male";
                        break;

                    case 1:
                        str = "Female";
                        break;

                    default:
                        str = "Unknown";
                        break;
                }
                return str;
            }
        }

        private uint InfoFlags
        {
            get
            {
                try
                {
                    return base.GetStorageField<uint>((uint) 0x17);
                }
                catch
                {
                    return 0;
                }
            }
        }

        private uint InfoFlagsTrainer
        {
            get
            {
                try
                {
                    return base.GetStorageField<uint>((uint) 0x52);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public uint RaceId =>
            this.InfoFlags & 0xff;

        public uint UnitClassId =>
            (this.InfoFlags >> 8) & 0xff;

        public uint UnitClassTrainer =>
            (this.InfoFlags >> 20) & 0xff;

        public uint GenderId =>
            (this.InfoFlags >> 0x10) & 0xff;

        public uint PowerTypeId =>
            (this.InfoFlags >> 0x18) & 0xff;

        public string Class
        {
            get
            {
                string str;
                switch (this.UnitClassId)
                {
                    case 1:
                        str = "Warrior";
                        break;

                    case 2:
                        str = "Paladin";
                        break;

                    case 3:
                        str = "Hunter";
                        break;

                    case 4:
                        str = "Rogue";
                        break;

                    case 5:
                        str = "Priest";
                        break;

                    case 6:
                        str = "Death Knight";
                        break;

                    case 7:
                        str = "Shaman";
                        break;

                    case 8:
                        str = "Mage";
                        break;

                    case 9:
                        str = "Warlock";
                        break;

                    case 11:
                        str = "Druid";
                        break;

                    default:
                        str = "Unknown";
                        break;
                }
                return str;
            }
        }

        public LazyLib.Wow.Constants.UnitClass UnitClass
        {
            get
            {
                switch (this.UnitClassId)
                {
                    case 1:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Warrior;

                    case 2:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Paladin;

                    case 3:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Hunter;

                    case 4:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Rogue;

                    case 5:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Priest;

                    case 6:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_DeathKnight;

                    case 7:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Shaman;

                    case 8:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Mage;

                    case 9:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Warlock;

                    case 11:
                        return LazyLib.Wow.Constants.UnitClass.UnitClass_Druid;
                }
                throw new Exception("Unknown class");
            }
        }

        public LazyLib.Wow.Constants.Classification Classification
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0x964 };
                Memory.Read<uint>(addresses);
                return LazyLib.Wow.Constants.Classification.Normal;
            }
        }

        public bool IsElite =>
            this.Classification.Equals(LazyLib.Wow.Constants.Classification.Elite);

        public LazyLib.Wow.Constants.CreatureType CreatureType
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0x964 };
                uint[] numArray2 = new uint[] { Memory.Read<uint>(addresses) + 0x10 };
                return Memory.Read<uint>(numArray2);
            }
        }

        public LazyLib.Wow.Reaction Reaction =>
            LazyLib.Wow.Faction.GetReaction(LazyLib.Wow.ObjectManager.MyPlayer, this);

        public bool IsPlayer =>
            Enumerable.Any<PPlayer>(LazyLib.Wow.ObjectManager.GetPlayers, player => player.GUID.Equals(this.GUID));

        public LazyLib.Wow.Constants.ShapeshiftForm ShapeshiftForm
        {
            get
            {
                uint[] addresses = new uint[1];
                uint[] numArray2 = new uint[] { base.BaseAddress + 0xd0 };
                addresses[0] = Memory.Read<uint>(numArray2) + 0x1d3;
                return Memory.Read<byte>(addresses);
            }
        }

        public bool IsPet
        {
            get
            {
                Func<PPlayer, bool> func = null;
                try
                {
                    func ??= cur => (cur.PetGUID == this.GUID);
                    return Enumerable.Any<PPlayer>(from cur in LazyLib.Wow.ObjectManager.GetPlayers
                        where cur.HasLivePet
                        select cur, func);
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsTotem =>
            this.CreatureType == LazyLib.Wow.Constants.CreatureType.Totem;

        public virtual PUnit Target
        {
            get
            {
                try
                {
                    if (!this.TargetGUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID))
                    {
                        using (List<PUnit>.Enumerator enumerator = LazyLib.Wow.ObjectManager.GetUnits.GetEnumerator())
                        {
                            while (true)
                            {
                                if (!enumerator.MoveNext())
                                {
                                    break;
                                }
                                PUnit current = enumerator.Current;
                                try
                                {
                                    if (current.GUID.Equals(this.TargetGUID))
                                    {
                                        return current;
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        return LazyLib.Wow.ObjectManager.MyPlayer;
                    }
                }
                catch (Exception)
                {
                }
                return new PUnit(0);
            }
        }

        public bool HasTarget
        {
            get
            {
                Func<PUnit, bool> func = null;
                try
                {
                    bool flag;
                    if (!this.TargetGUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID))
                    {
                        if (func == null)
                        {
                            func = u => u.GUID.Equals(this.TargetGUID);
                        }
                        if (!Enumerable.Any<PUnit>(LazyLib.Wow.ObjectManager.GetUnits, func))
                        {
                            goto TR_0000;
                        }
                        else
                        {
                            flag = true;
                        }
                    }
                    else
                    {
                        flag = true;
                    }
                    return flag;
                }
                catch (Exception)
                {
                }
            TR_0000:
                return false;
            }
        }

        public bool IsInFlightForm =>
            this.HasBuff(0x8497) || (this.HasBuff(0x9cb8) || this.AquaticForm);

        public bool TravelForm =>
            this.HasBuff(0x30f) || this.HasBuff(0xa55);

        public bool AquaticForm =>
            this.HasBuff(0x42a) || this.ShapeshiftForm.Equals(LazyLib.Wow.Constants.ShapeshiftForm.Aqua);

        public bool IsMounted
        {
            get
            {
                try
                {
                    return (!this.IsInFlightForm ? (!this.TravelForm ? (!this.AquaticForm ? (base.GetStorageField<int>((uint) 0x45) != 0) : true) : true) : true);
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public bool IsLootable
        {
            get
            {
                try
                {
                    int getDynFlags = this.GetDynFlags;
                    return ((getDynFlags == 1) || (getDynFlags == 13));
                }
                catch
                {
                    return false;
                }
            }
        }

        public bool IsTagged =>
            this.GetDynFlags == 4;

        public bool IsTaggedByMe =>
            (this.GetDynFlags == 8) || ((this.GetDynFlags == 13) || ((this.GetDynFlags == 1) || (this.GetDynFlags == 12)));

        public int GetDynFlags
        {
            get
            {
                try
                {
                    return base.GetStorageField<int>((uint) 0x4f);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public bool IsInCombat =>
            Convert.ToBoolean((long) (this.Flags & 0x80000L));

        public bool IsFleeing =>
            Convert.ToBoolean((long) (this.Flags & 0x800000L));

        public bool IsStunned =>
            Convert.ToBoolean((long) (this.Flags & 0x40000L));

        public bool IsAutoAttacking
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0x9e8 };
                uint[] numArray2 = new uint[] { base.BaseAddress + 0x9ec };
                return ((Memory.Read<int>(addresses) | Memory.Read<int>(numArray2)) != 0);
            }
        }

        public bool IsSwimming
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0xa30 };
                return ((Memory.Read<uint>(addresses) & 0x200000) != 0);
            }
        }

        public bool IsFlying
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0xd8 };
                uint[] numArray2 = new uint[] { Memory.Read<uint>(addresses) + 0x44 };
                return ((Memory.Read<uint>(numArray2) & 0x2000000) != 0);
            }
        }

        public bool IsSkinnable =>
            Convert.ToBoolean((long) (this.Flags & 0x4000000L));

        private long Flags
        {
            get
            {
                try
                {
                    return (long) base.GetStorageField<int>((uint) 0x3b);
                }
                catch
                {
                    return 0L;
                }
            }
        }

        public uint Faction
        {
            get
            {
                try
                {
                    return base.GetStorageField<uint>((uint) 0x37);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public bool IsGhost =>
            this.HealthPoints == 1;

        public virtual string Name
        {
            get
            {
                string str;
                try
                {
                    uint[] addresses = new uint[1];
                    uint[] numArray2 = new uint[] { base.BaseAddress + 0x964 };
                    addresses[0] = Memory.Read<uint>(numArray2) + 0x5c;
                    str = Memory.ReadUtf8(Memory.Read<uint>(addresses), 0x100);
                }
                catch (Exception)
                {
                    return "Read failed";
                }
                return str;
            }
        }

        public int CastingId
        {
            get
            {
                int num2;
                try
                {
                    num2 = Memory.Read<int>(new uint[] { base.BaseAddress + 0xa6c });
                }
                catch (Exception)
                {
                    return 0;
                }
                return num2;
            }
        }

        public bool IsCasting =>
            (this.CastingId != 0) || (this.ChanneledCastingId != 0);

        public int ChanneledCastingId =>
            Memory.Read<int>(new uint[] { base.BaseAddress + 0xa80 });

        public bool Critter
        {
            get
            {
                uint[] addresses = new uint[] { base.BaseAddress + 0x964 };
                uint[] numArray2 = new uint[] { Memory.Read<uint>(addresses) + 0x10 };
                return (Memory.Read<uint>(numArray2) == 8);
            }
        }

        public ulong CharmedBy =>
            base.GetStorageField<ulong>((uint) 12);

        public ulong SummonedBy =>
            base.GetStorageField<ulong>((uint) 14);

        public ulong CreatedBy =>
            base.GetStorageField<ulong>((uint) 0x10);

        public int HealthPoints =>
            base.GetStorageField<int>((uint) 0x18);

        public int MaximumHealthPoints =>
            base.GetStorageField<int>((uint) 0x20);

        public bool IsDead =>
            !this.IsAlive;

        public bool IsAlive
        {
            get
            {
                if (this.HealthPoints == 0)
                {
                    return false;
                }
                List<int> buffs = new List<int> { 
                    0x2086,
                    0x234c,
                    0x5068
                };
                return !this.HasBuff(buffs);
            }
        }

        public int Health
        {
            get
            {
                try
                {
                    return ((100 * this.HealthPoints) / this.MaximumHealthPoints);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int Mana
        {
            get
            {
                try
                {
                    return ((100 * this.ManaPoints) / this.MaximumManaPoints);
                }
                catch
                {
                    return 0;
                }
            }
        }

        public int BaseHealth =>
            base.GetStorageField<int>((uint) 0x79);

        public int BaseMana =>
            base.GetStorageField<int>((uint) 120);

        public int ManaPoints =>
            base.GetStorageField<int>((uint) 0x19);

        public int Rage
        {
            get
            {
                try
                {
                    if (this.UnitClass == LazyLib.Wow.Constants.UnitClass.UnitClass_Druid)
                    {
                        return this.DruidRage;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write("Something went wrong with Druid Rage:  " + exception, new object[0]);
                }
                return (base.GetStorageField<int>((uint) 0x19) / 10);
            }
        }

        public int Focus =>
            base.GetStorageField<int>((uint) 0x19);

        public int SoulShard =>
            base.GetStorageField<int>((uint) 0x1a);

        public int MaximumSoulShard =>
            base.GetStorageField<int>((uint) 0x22);

        public int HolyPower =>
            base.GetStorageField<int>((uint) 0x1a);

        public int MaximumHolyPower =>
            base.GetStorageField<int>((uint) 0x22);

        public int Energy
        {
            get
            {
                try
                {
                    if (this.UnitClass == LazyLib.Wow.Constants.UnitClass.UnitClass_Druid)
                    {
                        return this.DruidEnergy;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write("Druid Energy Failed: " + exception, new object[0]);
                }
                return base.GetStorageField<int>((uint) 0x19);
            }
        }

        private int DruidEnergy =>
            base.GetStorageField<int>((uint) 0x1b);

        private int DruidEnergyMax =>
            base.GetStorageField<int>((uint) 0x23);

        private int DruidRage =>
            base.GetStorageField<int>((uint) 0x1a) / 10;

        private int DruidRageMax =>
            base.GetStorageField<int>((uint) 0x25);

        public int Happinnes =>
            base.GetStorageField<int>((uint) 0x1c);

        public int RunicPower =>
            base.GetStorageField<int>((uint) 0x19) / 10;

        public int MaximumManaPoints =>
            base.GetStorageField<int>((uint) 0x21);

        public int MaximumRage
        {
            get
            {
                try
                {
                    if (this.UnitClass == LazyLib.Wow.Constants.UnitClass.UnitClass_Druid)
                    {
                        return this.DruidRageMax;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write("Druid Rage Max Failed: " + exception, new object[0]);
                }
                return base.GetStorageField<int>((uint) 0x21);
            }
        }

        public int MaximumEnergy
        {
            get
            {
                try
                {
                    if (this.UnitClass == LazyLib.Wow.Constants.UnitClass.UnitClass_Druid)
                    {
                        return this.DruidEnergyMax;
                    }
                }
                catch (Exception exception)
                {
                    Logging.Write("Druid Energy Max Failed: " + exception, new object[0]);
                }
                return base.GetStorageField<int>((uint) 0x21);
            }
        }

        public int MaximumRunicPower =>
            base.GetStorageField<int>((uint) 0x21);

        public int Level =>
            base.GetStorageField<int>((uint) 0x36);

        public int DisplayId =>
            base.GetStorageField<int>((uint) 0x43);

        public int MountDisplayId =>
            base.GetStorageField<int>((uint) 0x45);

        public ulong TargetGUID =>
            base.GetStorageField<ulong>((uint) 0x12);

        public bool IsTargetingMe =>
            (this.Target != null) && this.Target.TargetGUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID);

        public bool IsTargetingMyPet =>
            LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet ? ((this.Target != null) && this.Target.TargetGUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID)) : false;

        public virtual ulong PetGUID
        {
            get
            {
                try
                {
                    if (this.HasLivePet)
                    {
                        return this.Pet.GUID;
                    }
                }
                catch
                {
                }
                return 0UL;
            }
        }

        public bool HasLivePet
        {
            get
            {
                try
                {
                    return (this.Pet != null);
                }
                catch
                {
                    return false;
                }
            }
        }

        public PUnit Pet
        {
            get
            {
                try
                {
                    using (IEnumerator<PUnit> enumerator = LazyLib.Wow.ObjectManager.GetObjects.OfType<PUnit>().GetEnumerator())
                    {
                        while (true)
                        {
                            if (!enumerator.MoveNext())
                            {
                                break;
                            }
                            PUnit current = enumerator.Current;
                            ulong summonedBy = current.SummonedBy;
                            if (summonedBy.Equals(this.GUID))
                            {
                                return current;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                }
                return null;
            }
        }

        public double DistanceToSelf =>
            this.Location.DistanceToSelf;

        public IEnumerable<WoWAura> GetAuras
        {
            get
            {
                long num2;
                long num3;
                uint[] addresses = new uint[] { base.BaseAddress + 0xdd0 };
                int num = Memory.Read<int>(addresses);
                if (num == -1)
                {
                    uint[] numArray2 = new uint[] { base.BaseAddress + 0xc54 };
                    num = Memory.Read<int>(numArray2);
                }
                List<WoWAura> list = new List<WoWAura>();
                QueryPerformanceFrequency(out num2);
                QueryPerformanceCounter(out num3);
                long num4 = (num3 * 0x3e8L) / num2;
                for (uint i = 0; i < num; i++)
                {
                    int num6;
                    byte num7;
                    uint num8;
                    ulong num9;
                    uint[] numArray3 = new uint[] { base.BaseAddress + 0xdd0 };
                    if (Memory.Read<int>(numArray3) != -1)
                    {
                        uint[] numArray9 = new uint[] { ((base.BaseAddress + 0xc50) + (0x18 * i)) + 8 };
                        num6 = Memory.Read<int>(numArray9);
                        uint[] numArray10 = new uint[] { ((base.BaseAddress + 0xc50) + (0x18 * i)) + 15 };
                        num7 = Memory.Read<byte>(numArray10);
                        uint[] numArray11 = new uint[] { ((base.BaseAddress + 0xc50) + (0x18 * i)) + 20 };
                        num8 = Memory.Read<uint>(numArray11);
                        uint[] numArray12 = new uint[] { (base.BaseAddress + 0xc50) + (0x18 * i) };
                        num9 = Memory.Read<ulong>(numArray12);
                    }
                    else
                    {
                        uint[] numArray4 = new uint[] { base.BaseAddress + 0xc58 };
                        uint num10 = Memory.Read<uint>(numArray4);
                        uint[] numArray5 = new uint[] { (num10 + (0x18 * i)) + 8 };
                        num6 = Memory.Read<int>(numArray5);
                        uint[] numArray6 = new uint[] { (num10 + (0x18 * i)) + 15 };
                        num7 = Memory.Read<byte>(numArray6);
                        uint[] numArray7 = new uint[] { (num10 + (0x18 * i)) + 20 };
                        num8 = Memory.Read<uint>(numArray7);
                        uint[] numArray8 = new uint[] { num10 + (0x18 * i) };
                        num9 = Memory.Read<ulong>(numArray8);
                    }
                    if (num6 != 0)
                    {
                        WoWAura item = new WoWAura {
                            SpellId = num6,
                            Stack = num7,
                            SecondsLeft = (num8 - ((uint) num4)) / 0x3e8,
                            OwnerGUID = num9
                        };
                        list.Add(item);
                    }
                }
                return list;
            }
        }

        public bool IsSpiritHealer =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x4000) != 0;

        public bool IsInnkeeper =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x10000) != 0;

        public bool IsFlightmaster =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x2000) != 0;

        public bool IsTrainerMyClass
        {
            get
            {
                object[] args = new object[] { base.GetStorageField<uint>((uint) 0x52) & 0x20 };
                Logging.Debug("Target Trainer Class: {0}", args);
                return ((base.GetStorageField<uint>((uint) 0x52) & 0x20) != 0);
            }
        }

        public bool CanRepair =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x1000) != 0;

        public bool IsVendorReagent =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x800) != 0;

        public bool IsVendorFood =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x200) != 0;

        public bool IsVendor =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x80) != 0;

        public bool IsBanker =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x20000) != 0;

        public bool IsAuctioneer =>
            (base.GetStorageField<uint>((uint) 0x52) & 0x200000) != 0;

        internal enum UnitNPCFlags
        {
            UNIT_NPC_FLAG_AUCTIONEER = 0x200000,
            UNIT_NPC_FLAG_BANKER = 0x20000,
            UNIT_NPC_FLAG_BATTLEMASTER = 0x100000,
            UNIT_NPC_FLAG_FLIGHTMASTER = 0x2000,
            UNIT_NPC_FLAG_GOSSIP = 1,
            UNIT_NPC_FLAG_INNKEEPER = 0x10000,
            UNIT_NPC_FLAG_NONE = 0,
            UNIT_NPC_FLAG_PETITIONER = 0x40000,
            UNIT_NPC_FLAG_QUESTGIVER = 2,
            UNIT_NPC_FLAG_REPAIR = 0x1000,
            UNIT_NPC_FLAG_SPIRITGUIDE = 0x8000,
            UNIT_NPC_FLAG_SPIRITHEALER = 0x4000,
            UNIT_NPC_FLAG_STABLEMASTER = 0x400000,
            UNIT_NPC_FLAG_TABARDDESIGNER = 0x80000,
            UNIT_NPC_FLAG_TRAINER = 0x10,
            UNIT_NPC_FLAG_TRAINER_CLASS = 0x20,
            UNIT_NPC_FLAG_TRAINER_PROFESSION = 0x40,
            UNIT_NPC_FLAG_UNK1 = 4,
            UNIT_NPC_FLAG_UNK2 = 8,
            UNIT_NPC_FLAG_VENDOR = 0x80,
            UNIT_NPC_FLAG_VENDOR_AMMO = 0x100,
            UNIT_NPC_FLAG_VENDOR_FOOD = 0x200,
            UNIT_NPC_FLAG_VENDOR_POISON = 0x400,
            UNIT_NPC_FLAG_VENDOR_REAGENT = 0x800
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WoWAura
        {
            public int SpellId;
            public short Stack;
            public uint SecondsLeft;
            public ulong OwnerGUID;
        }
    }
}

