namespace LazyLib.Helpers
{
    using System;
    using System.Collections.Generic;

    public class ValidateKeys
    {
        public static bool Validate()
        {
            KeyBindings.LoadBindings();
            bool flag = true;
            if (!KeyBindings.CheckBind("ACTIONPAGE1", "SHIFT-1"))
            {
                flag = false;
            }
            if (!KeyBindings.CheckBind("ACTIONPAGE2", "SHIFT-2"))
            {
                flag = false;
            }
            if (!KeyBindings.CheckBind("ACTIONPAGE3", "SHIFT-3"))
            {
                flag = false;
            }
            if (!KeyBindings.CheckBind("ACTIONPAGE4", "SHIFT-4"))
            {
                flag = false;
            }
            if (!KeyBindings.CheckBind("ACTIONPAGE5", "SHIFT-5"))
            {
                flag = false;
            }
            if (!KeyBindings.CheckBind("ACTIONPAGE6", "SHIFT-6"))
            {
                flag = false;
            }
            foreach (KeyValuePair<string, KeyWrapper> pair in KeyHelper.KeysList)
            {
                string key = pair.Key;
                if (key != null)
                {
                    int num;
                    if (<PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x6000052-1 == null)
                    {
                        Dictionary<string, int> dictionary1 = new Dictionary<string, int>(9);
                        dictionary1.Add("InteractWithMouseOver", 0);
                        dictionary1.Add("TargetLastTarget", 1);
                        dictionary1.Add("InteractTarget", 2);
                        dictionary1.Add("X", 3);
                        dictionary1.Add("Up", 4);
                        dictionary1.Add("Down", 5);
                        dictionary1.Add("Left", 6);
                        dictionary1.Add("Right", 7);
                        dictionary1.Add("Space", 8);
                        <PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x6000052-1 = dictionary1;
                    }
                    if (<PrivateImplementationDetails>{0030317D-C02A-4718-8857-291094DB4569}.$$method0x6000052-1.TryGetValue(key, out num))
                    {
                        switch (num)
                        {
                            case 0:
                                if (!KeyBindings.CheckBind("INTERACTMOUSEOVER", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 1:
                                if (!KeyBindings.CheckBind("TARGETLASTTARGET", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 2:
                                if (!KeyBindings.CheckBind("INTERACTTARGET", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 3:
                                if (!KeyBindings.CheckBind("SITORSTAND", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 4:
                                if (!KeyBindings.CheckBind("MOVEFORWARD", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 5:
                                if (!KeyBindings.CheckBind("MOVEBACKWARD", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 6:
                                if (!KeyBindings.CheckBind("TURNLEFT", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 7:
                                if (!KeyBindings.CheckBind("TURNRIGHT", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            case 8:
                                if (!KeyBindings.CheckBind("JUMP", pair.Value.Key.ToUpper()))
                                {
                                    flag = false;
                                }
                                break;

                            default:
                                break;
                        }
                    }
                }
            }
            return flag;
        }

        public static bool AutoLoot
        {
            get
            {
                uint[] addresses = new uint[] { Memory.ReadRelative<uint>(new uint[] { 0x7d0914 }) + 0x30 };
                return (Memory.Read<uint>(addresses) == 1);
            }
        }

        public static bool ClickToMove
        {
            get
            {
                uint[] addresses = new uint[] { Memory.ReadRelative<uint>(new uint[] { 0x7d08f4 }) + 0x30 };
                return (Memory.Read<uint>(addresses) == 1);
            }
        }
    }
}

