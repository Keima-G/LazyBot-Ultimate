namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Xml;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public static class KeyHelper
    {
        private const string KeyFile = @"\Settings\Keys.xml";
        internal const string InteractWithMouseover = "InteractWithMouseOver";
        internal const string InteractTarget = "InteractTarget";
        internal const string TargetLastTarget = "TargetLastTarget";
        internal static readonly IDictionary<string, KeyWrapper> KeysList = new Dictionary<string, KeyWrapper>();
        private static readonly Ticker Open = new Ticker(800.0);
        private static object _lock = new object();

        public static void AddKey(string name, string shiftState, string barState, string character)
        {
            lock (_lock)
            {
                if (KeysList.ContainsKey(name))
                {
                    KeysList.Remove(name);
                }
                KeysList.Add(name, new KeyWrapper(name, shiftState, barState, character));
            }
        }

        public static void ChatboxSendText(string text)
        {
            if (!IsChatboxOpened)
            {
                KeyLowHelper.SendEnter();
                Open.Reset();
                while (!IsChatboxOpened && !Open.IsReady)
                {
                    Thread.Sleep(2);
                }
            }
            else
            {
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.VK_LCONTROL);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.VK_LCONTROL);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.Delete);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Delete);
                Thread.Sleep(200);
            }
            SendTextNow(text);
            Thread.Sleep(0x3e8);
            KeyLowHelper.SendEnter();
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
        internal static extern short GetKeyState(int virtualKeyCode);
        public static bool HasKey(string name) => 
            KeysList.ContainsKey(name);

        public static void LoadKeys()
        {
            lock (_lock)
            {
                if (!Directory.Exists(LazySettings.OurDirectory + @"\Settings"))
                {
                    Directory.CreateDirectory(LazySettings.OurDirectory + @"\Settings");
                }
                if (!File.Exists(LazySettings.OurDirectory + @"\Settings\Keys.xml"))
                {
                    SaveKeys();
                }
                AddKey("Eat", "None", LazySettings.KeysEatBar, LazySettings.KeysEatKey);
                AddKey("Drink", "None", LazySettings.KeysDrinkBar, LazySettings.KeysDrinkKey);
                AddKey("GMount", "None", LazySettings.KeysGroundMountBar, LazySettings.KeysGroundMountKey);
                AddKey("Q", "None", "Indifferent", LazySettings.KeysStafeLeftKeyText);
                AddKey("E", "None", "Indifferent", LazySettings.KeysStafeRightKeyText);
                AddKey("Attack1", "None", LazySettings.KeysAttack1Bar, LazySettings.KeysAttack1Key);
                AddKey("MacroForMail", "None", LazySettings.KeysMailMacroBar, LazySettings.KeysMailMacroKey);
                AddKey("InteractWithMouseOver", "None", "Indifferent", LazySettings.KeysInteractKeyText);
                AddKey("InteractTarget", "None", "Indifferent", LazySettings.KeysInteractTargetText);
                AddKey("TargetLastTarget", "None", "Indifferent", LazySettings.KeysTargetLastTargetText);
                XmlDocument document = new XmlDocument();
                try
                {
                    document.Load(LazySettings.OurDirectory + @"\Settings\Keys.xml");
                }
                catch (Exception exception)
                {
                    Logging.Write(LogType.Error, "Could not load keys: " + exception, new object[0]);
                    goto TR_0001;
                }
                foreach (XmlNode node in document.GetElementsByTagName("KeyWrapper"))
                {
                    string innerText = string.Empty;
                    string shiftState = string.Empty;
                    string barState = string.Empty;
                    string character = string.Empty;
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        string name = node2.Name;
                        if (name != null)
                        {
                            if (name == "name")
                            {
                                innerText = node2.InnerText;
                                continue;
                            }
                            if (name == "shiftstate")
                            {
                                shiftState = node2.InnerText;
                                continue;
                            }
                            if (name == "bar")
                            {
                                barState = node2.InnerText;
                                continue;
                            }
                            if (name == "key")
                            {
                                character = node2.InnerText;
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(innerText))
                    {
                        AddKey(innerText, shiftState, barState, character);
                    }
                }
            TR_0001:;
            }
        }

        public static void PressKey(string name)
        {
            lock (_lock)
            {
                if (KeysList.ContainsKey(name))
                {
                    KeysList[name].PressKey();
                }
                else
                {
                    Logging.Write("The key " + name + " could not be send", new object[0]);
                }
            }
        }

        public static void ReleaseKey(string name)
        {
            lock (_lock)
            {
                if (KeysList.ContainsKey(name))
                {
                    KeysList[name].ReleaseKey();
                }
                else
                {
                    Logging.Write("The key " + name + " could not be send", new object[0]);
                }
            }
        }

        private static void SaveKeys()
        {
            Dictionary<string, KeyWrapper> dictionary = new Dictionary<string, KeyWrapper> {
                { 
                    "Up",
                    new KeyWrapper("Up", "None", "Indifferent", "Up")
                },
                { 
                    "Down",
                    new KeyWrapper("Down", "None", "Indifferent", "Down")
                },
                { 
                    "Right",
                    new KeyWrapper("Down", "None", "Indifferent", "Right")
                },
                { 
                    "Left",
                    new KeyWrapper("Down", "None", "Indifferent", "Left")
                },
                { 
                    "Space",
                    new KeyWrapper("Space", "None", "Indifferent", "Space")
                },
                { 
                    "X",
                    new KeyWrapper("X", "None", "Indifferent", "X")
                },
                { 
                    "PetAttack",
                    new KeyWrapper("PetAttack", "Ctrl", "Indifferent", "1")
                },
                { 
                    "PetFollow",
                    new KeyWrapper("PetFollow", "Ctrl", "Indifferent", "2")
                },
                { 
                    "F1",
                    new KeyWrapper("F1", "None", "Indifferent", "F1")
                },
                { 
                    "TargetEnemy",
                    new KeyWrapper("Tab", "None", "Indifferent", "Tab")
                },
                { 
                    "TargetFriend",
                    new KeyWrapper("TargetFriend", "Ctrl", "Indifferent", "Tab")
                },
                { 
                    "ESC",
                    new KeyWrapper("ESC", "None", "Indifferent", "Escape")
                },
                { 
                    "InventoryOpenAll",
                    new KeyWrapper("InventoryOpenAll", "None", "Indifferent", "B")
                }
            };
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<?xml version=\"1.0\"?>", new object[0]);
            builder.AppendFormat("<KeyList>", new object[0]);
            foreach (KeyValuePair<string, KeyWrapper> pair in dictionary)
            {
                builder.AppendFormat("<KeyWrapper>", new object[0]);
                builder.AppendFormat("<name>{0}</name>", pair.Key);
                builder.AppendFormat("<shiftstate>{0}</shiftstate>", pair.Value.Special);
                builder.AppendFormat("<bar>{0}</bar>", pair.Value.Bar);
                builder.AppendFormat("<key>{0}</key>", pair.Value.Key);
                builder.AppendFormat("</KeyWrapper>", new object[0]);
            }
            builder.AppendFormat("</KeyList>", new object[0]);
            try
            {
                XmlDocument document = new XmlDocument();
                document.LoadXml(builder.ToString());
                document.Save(LazySettings.OurDirectory + @"\Settings\Keys.xml");
            }
            catch (Exception exception)
            {
                Logging.Write("Could not save the keys: " + exception, new object[0]);
            }
        }

        public static void SendEnter()
        {
            KeyLowHelper.SendEnter();
        }

        public static void SendKey(string name)
        {
            lock (_lock)
            {
                if (KeysList.ContainsKey(name))
                {
                    KeysList[name].SendKey();
                }
                else
                {
                    Logging.Write("Unknown key: " + name, new object[0]);
                }
            }
        }

        public static void SendTextNow(string text)
        {
            foreach (char ch in text)
            {
                KeyLowHelper.SendMessage(Memory.WindowHandle, 0x102, (IntPtr) ch, IntPtr.Zero);
            }
        }

        public static bool IsChatboxOpened =>
            Memory.ReadRelative<uint>(new uint[] { 0x941660 }) == 1;
    }
}

