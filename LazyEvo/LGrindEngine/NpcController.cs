namespace LazyEvo.LGrindEngine
{
    using LazyEvo.LGrindEngine.NpcClasses;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class NpcController
    {
        public NpcController()
        {
            this.Npc = new List<VendorsEx>();
        }

        public void AddNpc(VendorsEx npc)
        {
            this.Npc.Add(npc);
        }

        private static string GetCorrectString(string t) => 
            (Convert.ToString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator) == ".") ? t : t.Replace(".", CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);

        public VendorsEx GetNearestRepair()
        {
            double maxValue = double.MaxValue;
            VendorsEx ex = null;
            foreach (VendorsEx ex2 in this.Npc)
            {
                if ((ex2.VendorType == VendorType.Repair) && (ex2.Location.DistanceToSelf2D < maxValue))
                {
                    ex = ex2;
                    maxValue = ex2.Location.DistanceToSelf2D;
                }
            }
            return ex;
        }

        public VendorsEx GetTrainer(Constants.UnitClass unitClass)
        {
            double maxValue = double.MaxValue;
            VendorsEx ex = null;
            foreach (VendorsEx ex2 in this.Npc)
            {
                if (ex2.VendorType == VendorType.Train)
                {
                    bool flag = false;
                    switch (unitClass)
                    {
                        case Constants.UnitClass.UnitClass_Unknown:
                            if (ex2.TrainClass == TrainClass.Unknown)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Warrior:
                            if (ex2.TrainClass == TrainClass.Warrior)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Paladin:
                            if (ex2.TrainClass == TrainClass.Paladin)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Hunter:
                            if (ex2.TrainClass == TrainClass.Hunter)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Rogue:
                            if (ex2.TrainClass == TrainClass.Rogue)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Priest:
                            if (ex2.TrainClass == TrainClass.Priest)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_DeathKnight:
                            if (ex2.TrainClass == TrainClass.DeathKnight)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Shaman:
                            if (ex2.TrainClass == TrainClass.Shaman)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Mage:
                            if (ex2.TrainClass == TrainClass.Mage)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Warlock:
                            if (ex2.TrainClass == TrainClass.Warlock)
                            {
                                flag = true;
                            }
                            break;

                        case Constants.UnitClass.UnitClass_Druid:
                            if (ex2.TrainClass == TrainClass.Druid)
                            {
                                flag = true;
                            }
                            break;

                        default:
                            break;
                    }
                    if (flag && (ex2.Location.DistanceToSelf2D < maxValue))
                    {
                        ex = ex2;
                        maxValue = ex2.Location.DistanceToSelf2D;
                    }
                }
            }
            return ex;
        }

        public void LoadXml(XmlNode xmlNode)
        {
            if (xmlNode != null)
            {
                foreach (XmlNode node in xmlNode.ChildNodes)
                {
                    if (node.Name == "Vendor")
                    {
                        string innerText = string.Empty;
                        int entryId = -2147483648;
                        VendorType unknown = VendorType.Unknown;
                        string correctString = string.Empty;
                        string correctString = string.Empty;
                        string correctString = string.Empty;
                        TrainClass trainClass = TrainClass.Unknown;
                        foreach (XmlAttribute attribute in node.Attributes)
                        {
                            string name = attribute.Name;
                            if (name != null)
                            {
                                int num2;
                                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x6000177-1 == null)
                                {
                                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(7);
                                    dictionary1.Add("Name", 0);
                                    dictionary1.Add("EntryId", 1);
                                    dictionary1.Add("Type", 2);
                                    dictionary1.Add("TrainClass", 3);
                                    dictionary1.Add("X", 4);
                                    dictionary1.Add("Y", 5);
                                    dictionary1.Add("Z", 6);
                                    <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x6000177-1 = dictionary1;
                                }
                                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x6000177-1.TryGetValue(name, out num2))
                                {
                                    switch (num2)
                                    {
                                        case 0:
                                            innerText = attribute.InnerText;
                                            break;

                                        case 1:
                                            entryId = Convert.ToInt32(attribute.InnerText);
                                            break;

                                        case 2:
                                            unknown = (VendorType) Enum.Parse(typeof(VendorType), attribute.InnerText, true);
                                            break;

                                        case 3:
                                            trainClass = (TrainClass) Enum.Parse(typeof(TrainClass), attribute.InnerText, true);
                                            break;

                                        case 4:
                                            correctString = GetCorrectString(attribute.InnerText);
                                            break;

                                        case 5:
                                            correctString = GetCorrectString(attribute.InnerText);
                                            break;

                                        case 6:
                                            correctString = GetCorrectString(attribute.InnerText);
                                            break;

                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(innerText))
                        {
                            if (trainClass == TrainClass.Unknown)
                            {
                                this.Npc.Add(new VendorsEx(unknown, innerText, new Location((float) Convert.ToDouble(correctString), (float) Convert.ToDouble(correctString), (float) Convert.ToDouble(correctString)), entryId));
                            }
                            else
                            {
                                this.Npc.Add(new VendorsEx(unknown, innerText, new Location((float) Convert.ToDouble(correctString), (float) Convert.ToDouble(correctString), (float) Convert.ToDouble(correctString)), trainClass, entryId));
                            }
                        }
                    }
                }
            }
        }

        public List<VendorsEx> Npc { get; set; }
    }
}

