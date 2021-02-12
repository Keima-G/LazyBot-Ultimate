namespace LazyLib.Wow
{
    using System;

    internal class Pointers
    {
        public enum ActionBar
        {
            ActionBarFirstSlot = 0x81e358,
            ActionBarBonus = 0x81e59c
        }

        internal enum AutoAttack
        {
            AutoAttackFlag = 0x9e8,
            AutoAttackMask = 0x9ec
        }

        public enum AutoLoot
        {
            Pointer = 0x7d0914,
            Offset = 0x30
        }

        internal enum CastingInfo
        {
            IsCasting = 0xa6c,
            ChanneledCasting = 0xa80
        }

        public enum CgUnitCGetCreatureRank
        {
            GetCreatureRank = 0x718de0,
            Offset1 = 0x964,
            Offset2 = 0x10
        }

        public enum CgUnitCGetCreatureType
        {
            GetCreatureType = 0x71f300,
            Offset1 = 0x964,
            Offset2 = 0x10
        }

        public enum CgWorldFrameGetActiveCamera
        {
            CameraPointer = 0x77436c,
            CameraOffset = 0x7e20,
            CameraX = 8,
            CameraY = 12,
            CameraZ = 0x10,
            CameraMatrix = 20
        }

        internal enum Chat : uint
        {
            ChatStart = 0x775a9c,
            OffsetToNextMsg = 0x17c0
        }

        public enum ClickToMove
        {
            Pointer = 0x7d08f4,
            Offset = 0x30
        }

        internal enum ComboPoints
        {
            ComboPoints = 0x7d084d
        }

        internal enum Container
        {
            EquippedBagGUID = 0x823540
        }

        internal enum Globals
        {
            RedMessage = 0x7cfb90,
            MouseOverGUID = 0x7d07a0,
            LootWindow = 0x7fa8d8,
            IsBobbing = 0xbc,
            ArchFacing = 0x1c8,
            ChatboxIsOpen = 0x941660,
            CursorType = 0x93d0e0
        }

        public enum IsFlying
        {
            Pointer = 0x100,
            Offset1 = 0xd8,
            Offset2 = 0x44,
            Mask = 0x2000000
        }

        internal enum Items : uint
        {
            Offset = 0x708c20
        }

        internal enum KeyBinding
        {
            NumKeyBindings = 0x7eadd8,
            First = 0xb8,
            Next = 0xb0,
            Key = 20,
            Command = 40
        }

        internal enum Messages
        {
            EventMessage = 0xa98068
        }

        internal enum ObjectManager
        {
            CurMgrPointer = 0x879ce0,
            CurMgrOffset = 0x2ed0,
            NextObject = 60,
            FirstObject = 0xac,
            LocalGUID = 0xc0
        }

        public enum Quests
        {
            ActiveQuests = 0x274,
            SelectedQuestId = 0xb436f0,
            TitleText = 0xb434d0,
            GossipQuests = 0xb70f08,
            GossipQuestNext = 0x214
        }

        internal enum Reaction : uint
        {
            FactionStartIndex = 0xad3894,
            FactionPointer = 0xad38a4,
            FactionTotal = 0xad3890,
            HostileOffset1 = 20,
            HostileOffset2 = 12,
            FriendlyOffset1 = 0x10,
            FriendlyOffset2 = 12
        }

        internal enum Runes
        {
            RunesOffset = 0x824388,
            RuneState = 0x824388,
            RuneType = 0x824304,
            RuneCooldown = 0x824364
        }

        internal enum ShapeshiftForm
        {
            BaseAddressOffset1 = 0xd0,
            BaseAddressOffset2 = 0x1d3
        }

        internal enum SpellCooldown : uint
        {
            CooldPown = 0x93f5ac
        }

        internal enum Swimming
        {
            Pointer = 0x100,
            Offset = 0xa30,
            Mask = 0x200000
        }

        internal enum UiFrame
        {
            CurrentFramePtr = 0x7499a8,
            CurrentFrameOffset = 120,
            FrameBase = 0x7499a8,
            FirstFrame = 0xcd4,
            NextFrame = 0xccc,
            ScrHeight = 0x6c0cb8,
            ScrWidth = 0x6c0cb4,
            FrameLeft = 0x68,
            FrameRight = 0x70,
            FrameBottom = 100,
            FrameTop = 0x6c,
            ParentPtr = 0x94,
            EffectiveScale = 0x7c,
            Name = 0x1c,
            Visible = 220,
            RegionsFirst = 0x214,
            RegionsNext = 0x20c,
            LabelText = 0xf4,
            Visible1 = 0x16,
            Visible2 = 1,
            ButtonEnabledPointer = 0xac,
            ButtonEnabledMask = 0x800,
            ButtonChecked = 0x2f5,
            EditBoxText = 0x2b4,
            TrainButtonPtr = 0xb4796c,
            TrainButtonOff1 = 0x48c,
            TrainButtonOff2 = 0x40,
            TrainButtonOff3 = 440,
            TrainButtonOff4 = 0x2a0,
            TrainButtonOff5 = 0xe8,
            TrainButtonOff6 = 0x6e4
        }

        internal enum UnitAuras : uint
        {
            AuraCount1 = 0xdd0,
            AuraCount2 = 0xc54,
            AuraTable1 = 0xc50,
            AuraTable2 = 0xc58,
            AuraSize = 0x18,
            AuraSpellId = 8,
            AuraStack = 15,
            TimeLeft = 20
        }

        internal enum UnitName : uint
        {
            ObjectName1 = 420,
            ObjectName2 = 0x90,
            UnitName1 = 0x964,
            UnitName2 = 0x5c,
            PlayerNameCachePointer = 0x85d940,
            PlayerNameMaskOffset = 0x24,
            PlayerNameBaseOffset = 0x1c,
            PlayerNameStringOffset = 0x20
        }

        internal enum UnitSpeed
        {
            Pointer = 0x6f14a8,
            Pointer1 = 0x814,
            Pointer2 = 8
        }

        internal enum WowObject
        {
            X = 0x798,
            Y = 0x79c,
            Z = 0x7a0,
            RotationOffset = 0x7a8,
            GameObjectX = 0xe8,
            GameObjectY = 0xec,
            GameObjectZ = 240
        }

        internal enum Zone : uint
        {
            ZoneText = 0x7d0788,
            ZoneID = 0x7d080c
        }
    }
}

