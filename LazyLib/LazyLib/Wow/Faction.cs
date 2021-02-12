namespace LazyLib.Wow
{
    using LazyLib.Helpers;
    using System;

    internal class Faction
    {
        private static LazyLib.Wow.Reaction CompareFactionHash(uint? hash1, uint? hash2)
        {
            if ((hash1 != null) && (hash2 != null))
            {
                byte[] buffer = Memory.ReadBytes(hash1.Value, 0x40);
                byte[] buffer2 = Memory.ReadBytes(hash2.Value, 0x40);
                BitConverter.ToInt32(buffer, 4);
                int mobHashCheck = BitConverter.ToInt32(buffer2, 4);
                if (TestBits((uint) (BitConverter.ToInt32(buffer, 0) + 20), (uint) (BitConverter.ToInt32(buffer2, 0) + 12)))
                {
                    return LazyLib.Wow.Reaction.Hostile;
                }
                if (HashCompare(0x18, buffer, mobHashCheck))
                {
                    return LazyLib.Wow.Reaction.Hostile;
                }
                if (TestBits((uint) (BitConverter.ToInt32(buffer, 0) + 0x10), (uint) (BitConverter.ToInt32(buffer2, 0) + 12)))
                {
                    return LazyLib.Wow.Reaction.Friendly;
                }
                if (HashCompare(40, buffer, mobHashCheck))
                {
                    return LazyLib.Wow.Reaction.Friendly;
                }
            }
            return LazyLib.Wow.Reaction.Neutral;
        }

        private static LazyLib.Wow.Reaction FindReactionFromFactions(uint localFaction, uint mobFaction)
        {
            uint num = Memory.Read<uint>(new uint[] { 0xad3894 });
            uint num2 = Memory.Read<uint>(new uint[] { 0xad3890 });
            uint num3 = Memory.Read<uint>(new uint[] { 0xad38a4 });
            uint? nullable = null;
            uint? nullable2 = null;
            if ((localFaction >= num) && ((localFaction <= num2) && ((mobFaction >= num) && (mobFaction < num2))))
            {
                nullable = new uint?(num3 + ((localFaction - num) * 4));
                nullable2 = new uint?(num3 + ((mobFaction - num) * 4));
            }
            return ((nullable == null) ? LazyLib.Wow.Reaction.Unknown : CompareFactionHash(nullable, nullable2));
        }

        public static LazyLib.Wow.Reaction GetReaction(PUnit localObj, PUnit mobObj)
        {
            try
            {
                return (((localObj.Faction < 1) || (mobObj.Faction < 1)) ? LazyLib.Wow.Reaction.Missing : FindReactionFromFactions(localObj.Faction, mobObj.Faction));
            }
            catch (Exception)
            {
                return LazyLib.Wow.Reaction.Missing;
            }
        }

        private static bool HashCompare(int hashIndex, byte[] localBitHash, int mobHashCheck)
        {
            int num = BitConverter.ToInt32(localBitHash, hashIndex);
            uint num2 = 0;
            while (true)
            {
                if (num2 < 4)
                {
                    if (num == mobHashCheck)
                    {
                        return true;
                    }
                    hashIndex += 4;
                    num = BitConverter.ToInt32(localBitHash, hashIndex);
                    if (num != 0)
                    {
                        num2++;
                        continue;
                    }
                }
                return false;
            }
        }

        private static bool TestBits(uint lBitAddr, uint rBitAddr)
        {
            uint[] addresses = new uint[] { lBitAddr };
            uint[] numArray2 = new uint[] { rBitAddr };
            return ((Memory.Read<uint>(addresses) & Memory.Read<uint>(numArray2)) != 0);
        }
    }
}

