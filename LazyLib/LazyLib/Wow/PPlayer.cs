namespace LazyLib.Wow
{
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PPlayer : PUnit
    {
        public PPlayer(uint baseAddress) : base(baseAddress)
        {
        }

        public string PlayerRace
        {
            get
            {
                long faction = base.Faction;
                return (!faction.Equals((long) 1L) ? (!faction.Equals((long) 0x64aL) ? (!faction.Equals((long) 3L) ? (!faction.Equals((long) 0x73) ? (!faction.Equals((long) 4L) ? (!faction.Equals((long) 2L) ? (!faction.Equals((long) 6L) ? (!faction.Equals((long) 0x74) ? (!faction.Equals((long) 5L) ? (!faction.Equals((long) 0x65dL) ? (!faction.Equals((long) 0x89bL) ? (!faction.Equals((long) 0x89cL) ? "Unknown" : "Goblin") : "Worgen") : "Draenei") : "Undead") : "Troll") : "Tauren") : "Orc") : "Night Elf") : "Gnome") : "Dwarf") : "Blood Elf") : "Human");
            }
        }

        public string PlayerFaction
        {
            get
            {
                string playerRace = this.PlayerRace;
                if (playerRace != null)
                {
                    int num;
                    if (<PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x600034a-1 == null)
                    {
                        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(12);
                        dictionary1.Add("Human", 0);
                        dictionary1.Add("Dwarf", 1);
                        dictionary1.Add("Gnome", 2);
                        dictionary1.Add("Night Elf", 3);
                        dictionary1.Add("Draenei", 4);
                        dictionary1.Add("Worgen", 5);
                        dictionary1.Add("Orc", 6);
                        dictionary1.Add("Undead", 7);
                        dictionary1.Add("Tauren", 8);
                        dictionary1.Add("Troll", 9);
                        dictionary1.Add("Blood Elf", 10);
                        dictionary1.Add("Goblin", 11);
                        <PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x600034a-1 = dictionary1;
                    }
                    if (<PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x600034a-1.TryGetValue(playerRace, out num))
                    {
                        switch (num)
                        {
                            case 0:
                            case 1:
                            case 2:
                            case 3:
                            case 4:
                            case 5:
                                return "Alliance";

                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 10:
                            case 11:
                                return "Horde";

                            default:
                                break;
                        }
                    }
                }
                return "Unknown";
            }
        }

        public override string Name
        {
            get
            {
                string str;
                try
                {
                    int num = Memory.ReadRelative<int>(new uint[] { 0x85d964 });
                    if (num == -1)
                    {
                        str = "Unknown Player";
                    }
                    else
                    {
                        num &= (int) this.GUID;
                        uint[] addresses = new uint[] { ((Memory.ReadRelative<int>(new uint[] { 0x85d95c }) + ((num + (num * 2)) * 4)) + 4) + 4 };
                        num = Memory.Read<int>(addresses);
                        while (true)
                        {
                            uint[] numArray8 = new uint[] { num };
                            if (Memory.Read<int>(numArray8) == ((int) this.GUID))
                            {
                                str = Memory.ReadUtf8((uint) (num + 0x20), 40);
                                break;
                            }
                            uint[] numArray4 = new uint[] { 0x85d95c };
                            int num3 = Memory.ReadRelative<int>(numArray4);
                            uint[] numArray5 = new uint[] { 0x85d964 };
                            int num2 = ((int) this.GUID) & Memory.ReadRelative<int>(numArray5);
                            uint[] numArray6 = new uint[] { num3 + ((num2 + (num2 * 2)) * 4) };
                            uint[] numArray7 = new uint[] { (Memory.Read<int>(numArray6) + num) + 4 };
                            num = Memory.Read<int>(numArray7);
                        }
                    }
                }
                catch
                {
                    str = "Error when reading player name";
                }
                return str;
            }
        }
    }
}

