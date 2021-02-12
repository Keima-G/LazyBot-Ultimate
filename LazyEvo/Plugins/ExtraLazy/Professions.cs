namespace LazyEvo.Plugins.ExtraLazy
{
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Professions
    {
        private List<Profession> _pProfessions;
        private Action<Professions> _callback;

        public Professions(Action<Professions> callbackMethod)
        {
            this.IsMiner = false;
            this.IsSkinner = false;
            this.IsAlchemist = false;
            this.IsBlacksmith = false;
            this.IsEnchanter = false;
            this.IsEngineer = false;
            this.IsInscriptor = false;
            this.IsJewelCrafter = false;
            this.IsLeatherWorker = false;
            this.IsTailor = false;
            this.IsHerbalist = false;
            this._pProfessions = new List<Profession>();
            this._callback = callbackMethod;
            new Thread(new ParameterizedThreadStart(Professions.ReloadFrames)).Start(this);
        }

        internal void addPProfession(Profession profession)
        {
            this._pProfessions.Add(profession);
        }

        internal void addProfession(Profession profession, Professions professions)
        {
            string str;
            if ((profession.Name != "") && ((str = profession.Name) != null))
            {
                int num;
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60003bc-1 == null)
                {
                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(15);
                    dictionary1.Add("Mining", 0);
                    dictionary1.Add("Skinning", 1);
                    dictionary1.Add("Herbalism", 2);
                    dictionary1.Add("Alchemy", 3);
                    dictionary1.Add("Blacksmithing", 4);
                    dictionary1.Add("Engineering", 5);
                    dictionary1.Add("Enchanting", 6);
                    dictionary1.Add("Inscription", 7);
                    dictionary1.Add("Jewelcrafting", 8);
                    dictionary1.Add("Leatherworking", 9);
                    dictionary1.Add("Tailoring", 10);
                    dictionary1.Add("Cooking", 11);
                    dictionary1.Add("Archaeology", 12);
                    dictionary1.Add("First Aid", 13);
                    dictionary1.Add("Fishing", 14);
                    <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60003bc-1 = dictionary1;
                }
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60003bc-1.TryGetValue(str, out num))
                {
                    switch (num)
                    {
                        case 0:
                            professions.addPProfession(profession);
                            return;

                        case 1:
                            professions.addPProfession(profession);
                            return;

                        case 2:
                            professions.addPProfession(profession);
                            return;

                        case 3:
                            professions.addPProfession(profession);
                            return;

                        case 4:
                            professions.addPProfession(profession);
                            return;

                        case 5:
                            professions.addPProfession(profession);
                            return;

                        case 6:
                            professions.addPProfession(profession);
                            return;

                        case 7:
                            professions.addPProfession(profession);
                            return;

                        case 8:
                            professions.addPProfession(profession);
                            return;

                        case 9:
                            professions.addPProfession(profession);
                            return;

                        case 10:
                            professions.addPProfession(profession);
                            return;

                        case 11:
                            professions.Secondary3 = profession;
                            return;

                        case 12:
                            professions.Secondary1 = profession;
                            return;

                        case 13:
                            professions.Secondary4 = profession;
                            return;

                        case 14:
                            professions.Secondary2 = profession;
                            break;

                        default:
                            return;
                    }
                }
            }
        }

        internal void callback()
        {
            this._callback(this);
        }

        private static Frame ClickHooked(string frameName)
        {
            Frame frameByName = null;
            try
            {
                MouseHelper.Hook();
                MouseHelper.WaitFrameReload();
                frameByName = InterfaceHelper.GetFrameByName(frameName);
                InterfaceHelper.GetFrameByName(frameName).LeftClickHooked();
                MouseHelper.WaitFrameReload();
                MouseHelper.ReleaseMouse();
            }
            catch (Exception)
            {
            }
            return frameByName;
        }

        private static void CloseFrames()
        {
            while (!InterfaceHelper.GetFrameByName("GameMenuButtonContinue").IsVisible)
            {
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.Escape);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Escape);
                Thread.Sleep(800);
            }
            if (InterfaceHelper.GetFrameByName("GameMenuButtonContinue").IsVisible)
            {
                ClickHooked("GameMenuButtonContinue");
            }
        }

        public bool MsgUpdate(string msg)
        {
            string str;
            if (msg.Contains("You have u"))
            {
                str = msg.Substring(msg.IndexOf("[") + 1, (msg.IndexOf("]") - 1) - msg.IndexOf("["));
                this.parseProfession();
                return this.removeProfession(str);
            }
            if (msg.Contains("You have g"))
            {
                Profession profession = new Profession {
                    Name = msg.Substring(msg.IndexOf("he ") + 3, (msg.IndexOf(" s") - msg.IndexOf("he ")) - 3),
                    Level = 1,
                    Rank = RankFactory.getRank(0)
                };
                this.addProfession(profession, this);
                this.parseProfession();
            }
            else
            {
                if (msg.Contains("Your s"))
                {
                    string str2;
                    str = msg.Substring(msg.IndexOf("in ") + 3, (msg.IndexOf(" has") - msg.IndexOf("in ")) - 3);
                    int startIndex = msg.IndexOf("to ") + 3;
                    int num4 = int.Parse(msg.Substring(startIndex, msg.IndexOf(".") - startIndex));
                    bool flag = false;
                    foreach (Profession profession2 in this._pProfessions)
                    {
                        if (profession2.Name == str)
                        {
                            profession2.Level = num4;
                            flag = true;
                        }
                    }
                    if (!flag && ((str2 = str) != null))
                    {
                        if (str2 == "Cooking")
                        {
                            this.Secondary3.Level = num4;
                            flag = true;
                        }
                        else if (str2 == "Archaeology")
                        {
                            this.Secondary1.Level = num4;
                            flag = true;
                        }
                        else if (str2 == "First Aid")
                        {
                            this.Secondary4.Level = num4;
                            flag = true;
                        }
                        else if (str2 == "Fishing")
                        {
                            this.Secondary2.Level = num4;
                            flag = true;
                        }
                    }
                    return flag;
                }
                if (!msg.Contains("You have learned a new ability"))
                {
                    return false;
                }
                str = msg.Substring(msg.IndexOf("[") + 1, (msg.IndexOf("]") - 1) - msg.IndexOf("["));
                foreach (Profession profession3 in this._pProfessions)
                {
                    if (profession3.Name == str)
                    {
                        profession3.Rank += 1;
                    }
                }
                string str3 = str;
                if (str3 != null)
                {
                    if (str3 == "Cooking")
                    {
                        Profession profession1 = this.Secondary2;
                        profession1.Rank += 1;
                    }
                    else if (str3 == "First Aid")
                    {
                        Profession profession4 = this.Secondary3;
                        profession4.Rank += 1;
                    }
                    else if (str3 == "Fishing")
                    {
                        Profession profession5 = this.Secondary1;
                        profession5.Rank += 1;
                    }
                }
            }
            return true;
        }

        private void parseProfession()
        {
            this.Primary1 = (this._pProfessions.Count <= 0) ? null : this._pProfessions.ElementAt<Profession>(0);
            if (this._pProfessions.Count > 1)
            {
                this.Primary2 = this._pProfessions.ElementAt<Profession>(1);
            }
            else
            {
                this.Primary2 = null;
            }
        }

        internal static void ReloadFrames(object proObj)
        {
            Professions professions = (Professions) proObj;
            InterfaceHelper.ReloadFrames();
            Thread.Sleep(0x7d0);
            ClickHooked("SpellbookMicroButton");
            ClickHooked("SpellBookFrameTabButton2");
            List<Frame> list = new List<Frame>();
            List<Frame> source = new List<Frame>();
            list.Add(ClickHooked("PrimaryProfession1"));
            list.Add(ClickHooked("PrimaryProfession2"));
            list.Add(ClickHooked("SecondaryProfession1"));
            list.Add(ClickHooked("SecondaryProfession2"));
            list.Add(ClickHooked("SecondaryProfession3"));
            list.Add(ClickHooked("SecondaryProfession4"));
            source.Add(ClickHooked("PrimaryProfession1StatusBar"));
            source.Add(ClickHooked("PrimaryProfession2StatusBar"));
            source.Add(ClickHooked("SecondaryProfession1StatusBar"));
            source.Add(ClickHooked("SecondaryProfession2StatusBar"));
            source.Add(ClickHooked("SecondaryProfession3StatusBar"));
            source.Add(ClickHooked("SecondaryProfession4StatusBar"));
            int index = 0;
            foreach (Frame frame in list)
            {
                List<Frame> getChilds = frame.GetChilds;
                Profession profession = new Profession {
                    Name = getChilds.ElementAt<Frame>(0).GetText,
                    Rank = RankFactory.getRank(getChilds.ElementAt<Frame>(((index > 1) ? 1 : 3)).GetText),
                    Level = int.Parse(source.ElementAt<Frame>(index).GetChilds.ElementAt<Frame>(0).GetText.Split(new char[] { '/' })[0])
                };
                professions.addProfession(profession, professions);
                index++;
            }
            professions.parseProfession();
            Thread.Sleep(0x3e8);
            CloseFrames();
            Thread.Sleep(0x3e8);
            professions.callback();
        }

        internal bool removeProfession(string name)
        {
            bool flag = false;
            Profession item = null;
            foreach (Profession profession2 in this._pProfessions)
            {
                if (profession2.Name == name)
                {
                    item = profession2;
                    flag = true;
                }
            }
            if (flag)
            {
                this._pProfessions.Remove(item);
            }
            else
            {
                string str = name;
                if (str != null)
                {
                    if (str == "Cooking")
                    {
                        this.Secondary3 = null;
                        flag = true;
                    }
                    else if (str == "Archaeology")
                    {
                        this.Secondary1 = null;
                        flag = true;
                    }
                    else if (str == "First Aid")
                    {
                        this.Secondary4 = null;
                        flag = true;
                    }
                    else if (str == "Fishing")
                    {
                        this.Secondary2 = null;
                        flag = true;
                    }
                }
            }
            this.parseProfession();
            return flag;
        }

        internal void setPrimaryProfession1(Profession profession)
        {
            this.Primary1 = profession;
            this.addPProfession(profession);
        }

        internal void setPrimaryProfession2(Profession profession)
        {
            this.Primary2 = profession;
            this.addPProfession(profession);
        }

        internal void setSecondaryProfession1(Profession profession)
        {
            this.Secondary1 = profession;
        }

        internal void setSecondaryProfession2(Profession profession)
        {
            this.Secondary2 = profession;
        }

        internal void setSecondaryProfession3(Profession profession)
        {
            this.Secondary3 = profession;
        }

        internal void setSecondaryProfession4(Profession profession)
        {
            this.Secondary4 = profession;
        }

        public bool IsMiner { get; internal set; }

        public bool IsSkinner { get; internal set; }

        public bool IsAlchemist { get; internal set; }

        public bool IsBlacksmith { get; internal set; }

        public bool IsEnchanter { get; internal set; }

        public bool IsEngineer { get; internal set; }

        public bool IsInscriptor { get; internal set; }

        public bool IsJewelCrafter { get; internal set; }

        public bool IsLeatherWorker { get; internal set; }

        public bool IsTailor { get; internal set; }

        public bool IsHerbalist { get; internal set; }

        public bool IsCook { get; internal set; }

        public bool IsFirstAider { get; internal set; }

        public bool IsArchaeologist { get; internal set; }

        public bool IsFisherPerson { get; internal set; }

        public Profession Primary1 { get; private set; }

        public Profession Primary2 { get; private set; }

        public Profession Secondary1 { get; private set; }

        public Profession Secondary2 { get; private set; }

        public Profession Secondary3 { get; private set; }

        public Profession Secondary4 { get; private set; }
    }
}

