namespace LazyLib.Wow
{
    using System;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Constants
    {
        public enum ChatType : byte
        {
            Addon = 0,
            Say = 1,
            Party = 2,
            Raid = 3,
            Guild = 4,
            Officer = 5,
            Yell = 6,
            Whisper = 7,
            WhisperMob = 8,
            WhisperInform = 9,
            Emote = 10,
            TextEmote = 11,
            MonsterSay = 12,
            MonsterParty = 13,
            MonsterYell = 14,
            MonsterWhisper = 15,
            MonsterEmote = 0x10,
            Channel = 0x11,
            ChannelJoin = 0x12,
            ChannelLeave = 0x13,
            ChannelList = 20,
            ChannelNotice = 0x15,
            ChannelNoticeUser = 0x16,
            Afk = 0x17,
            Dnd = 0x18,
            Ignored = 0x19,
            Skill = 0x1a,
            Loot = 0x1b,
            BgEventNeutral = 0x23,
            BgEventAlliance = 0x24,
            BgEventHorde = 0x25,
            CombatFactionChange = 0x26,
            RaidLeader = 0x27,
            RaidWarning = 40,
            RaidWarningWidescreen = 0x29,
            Filtered = 0x2b,
            Battleground = 0x2c,
            BattlegroundLeader = 0x2d,
            Restricted = 0x2e,
            RealId = 0x35
        }

        public enum Classification
        {
            Normal = 1,
            Elite = 2,
            RareElite = 3,
            WorldBoss = 4,
            Rare = 5
        }

        public enum CreatureType
        {
            Player,
            Beast,
            Dragon,
            Demon,
            Elemental,
            Giant,
            Undead,
            Humanoid,
            Critter,
            Mechanical,
            NotSpecified,
            Totem,
            NonCombatPet,
            GasCloud
        }

        public enum KeyType : uint
        {
            Spell = 0,
            GeneralMacro = 0x40,
            ToonSpecificMacro = 0x41,
            Item = 0x80
        }

        public enum ObjectType : uint
        {
            Object = 0,
            Item = 1,
            Container = 2,
            Unit = 3,
            Player = 4,
            GameObject = 5,
            DynamicObject = 6,
            Corpse = 7,
            AiGroup = 8,
            AreaTrigger = 9
        }

        public enum ObjType : uint
        {
            OT_NONE = 0,
            OT_ITEM = 1,
            OT_CONTAINER = 2,
            OT_UNIT = 3,
            OT_PLAYER = 4,
            OT_GAMEOBJ = 5,
            OT_DYNOBJ = 6,
            OT_CORPSE = 7,
            OT_FORCEDWORD = 0xffffffff
        }

        public enum PlayerFactions : uint
        {
            Human = 1,
            Orc = 2,
            Dwarf = 3,
            NightElf = 4,
            Undead = 5,
            Tauren = 6,
            Gnome = 0x73,
            Troll = 0x74,
            BloodElf = 0x64a,
            Draenei = 0x65d,
            Worgen = 0x89b,
            Goblin = 0x89c
        }

        public enum ShapeshiftForm
        {
            Normal = 0,
            Cat = 1,
            TreeOfLife = 2,
            Travel = 3,
            Aqua = 4,
            Bear = 5,
            Ambient = 6,
            Ghoul = 7,
            DireBear = 8,
            CreatureBear = 14,
            CreatureCat = 15,
            GhostWolf = 0x10,
            BattleStance = 0x11,
            DefensiveStance = 0x12,
            BerserkerStance = 0x13,
            EpicFlightForm = 0x1b,
            Shadow = 0x1c,
            Stealth = 30,
            Moonkin = 0x1f
        }

        public enum UnitClass
        {
            UnitClass_Unknown = 0,
            UnitClass_Warrior = 1,
            UnitClass_Paladin = 2,
            UnitClass_Hunter = 3,
            UnitClass_Rogue = 4,
            UnitClass_Priest = 5,
            UnitClass_DeathKnight = 6,
            UnitClass_Shaman = 7,
            UnitClass_Mage = 8,
            UnitClass_Warlock = 9,
            UnitClass_Druid = 11
        }

        public enum UnitDynamicFlags
        {
            None = 0,
            Lootable = 1,
            TrackUnit = 2,
            TaggedByOther = 4,
            TaggedByMe = 8,
            SpecialInfo = 0x10,
            Dead = 0x20,
            ReferAFriendLinked = 0x40,
            IsTappedByAllThreatList = 0x80
        }

        public enum UnitFlags : uint
        {
            None = 0,
            Sitting = 1,
            Influenced = 4,
            PlayerControlled = 8,
            Totem = 0x10,
            Preparation = 0x20,
            PlusMob = 0x40,
            NotAttackable = 0x100,
            Looting = 0x400,
            PetInCombat = 0x800,
            PvPFlagged = 0x1000,
            Silenced = 0x2000,
            Pacified = 0x20000,
            Stunned = 0x40000,
            CanPerformAction_Mask1 = 0x60000,
            Combat = 0x80000,
            TaxiFlight = 0x100000,
            Disarmed = 0x200000,
            Confused = 0x400000,
            Fleeing = 0x800000,
            Possessed = 0x1000000,
            NotSelectable = 0x2000000,
            Skinnable = 0x4000000,
            Mounted = 0x8000000,
            Dazed = 0x20000000,
            Sheathe = 0x40000000
        }

        public enum UnitGender
        {
            UnitGender_Male,
            UnitGender_Female,
            UnitGender_Unknown
        }

        public enum UnitPower
        {
            UnitPower_Mana = 0,
            UnitPower_Rage = 1,
            UnitPower_Focus = 2,
            UnitPower_Energy = 3,
            UnitPower_Runes = 5,
            UnitPower_RunicPower = 6,
            UnitPower_SoulShard = 8,
            UnitPower_Eclipse = 9,
            UnitPower_HolyPower = 10,
            UnitPower_Max = 7
        }

        public enum UnitRace
        {
            UnitRace_Human = 1,
            UnitRace_Orc = 2,
            UnitRace_Dwarf = 3,
            UnitRace_NightElf = 4,
            UnitRace_Undead = 5,
            UnitRace_Tauren = 6,
            UnitRace_Gnome = 7,
            UnitRace_Troll = 8,
            UnitRace_Goblin = 9,
            UnitRace_BloodElf = 10,
            UnitRace_Draenei = 11,
            UnitRace_FelOrc = 12,
            UnitRace_Naga = 13,
            UnitRace_Broken = 14,
            UnitRace_Skeleton = 15
        }

        public enum UnitTrainerClass
        {
            UnitClass_Unknown = 0,
            UnitClass_Warrior = 1,
            UnitClass_Paladin = 2,
            UnitClass_Hunter = 3,
            UnitClass_Rogue = 4,
            UnitClass_Priest = 5,
            UnitClass_DeathKnight = 6,
            UnitClass_Shaman = 7,
            UnitClass_Mage = 8,
            UnitClass_Warlock = 9,
            UnitClass_Druid = 11
        }
    }
}

