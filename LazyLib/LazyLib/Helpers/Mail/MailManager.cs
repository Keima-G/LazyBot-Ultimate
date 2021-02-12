namespace LazyLib.Helpers.Mail
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class MailManager
    {
        private static int _containerIndex;
        private static int _positionInBag;

        public static AddedItemsStatus ClickItems(bool reset, int howMany)
        {
            Logging.Debug("Called addedToMail", new object[0]);
            if (reset)
            {
                _containerIndex = 1;
                _positionInBag = 1;
            }
            int num = 0;
            while (_containerIndex != 6)
            {
                Frame frameByName = InterfaceHelper.GetFrameByName("ContainerFrame" + _containerIndex);
                if (frameByName == null)
                {
                    _containerIndex++;
                    return AddedItemsStatus.Error;
                }
                int slotCount = GetSlotCount(_containerIndex);
                Logging.Debug("Found ContainerFrame with Slot count: " + slotCount, new object[0]);
                while (true)
                {
                    if ((_positionInBag == (slotCount + 1)) || (num == howMany))
                    {
                        if (_positionInBag == (slotCount + 1))
                        {
                            _positionInBag = 1;
                            _containerIndex++;
                        }
                        if (num != howMany)
                        {
                            break;
                        }
                        return AddedItemsStatus.ClickedAll;
                    }
                    Frame frame2 = InterfaceHelper.GetFrameByName(string.Concat(new object[] { "ContainerFrame", _containerIndex, "Item", _positionInBag }));
                    if (frame2 != null)
                    {
                        frame2.HoverHooked();
                        Thread.Sleep(150);
                        try
                        {
                            Frame frame3 = InterfaceHelper.GetFrameByName("GameTooltip");
                            if ((frame3 != null) && MailList.ShouldMail(frame3.GetChildObject("GameTooltipTextLeft1").GetText))
                            {
                                Logging.Write("Adding: " + frame3.GetChildObject("GameTooltipTextLeft1").GetText, new object[0]);
                                Thread.Sleep(150);
                                frame2.RightClickHooked();
                                Thread.Sleep(150);
                                num++;
                            }
                        }
                        catch (Exception exception)
                        {
                            Logging.Write("Exception when parsing gametooltip: " + exception, new object[0]);
                        }
                    }
                    _positionInBag++;
                }
            }
            return ((num == 0) ? AddedItemsStatus.ClickedNothing : AddedItemsStatus.ClickedSomething);
        }

        private static bool ClickSendMailTab()
        {
            MailFrame.ClickMailFrame();
            Thread.Sleep(0x3e8);
            MailFrame.ClickSendMailTabHooked();
            Thread.Sleep(500);
            if (!MailFrame.CurrentTabIsSendMail)
            {
                Thread.Sleep(500);
                MailFrame.ClickSendMailTabHooked();
                Thread.Sleep(500);
                if (!MailFrame.CurrentTabIsSendMail)
                {
                    Logging.Write(LogType.Error, "Could not find mail frame", new object[0]);
                    return false;
                }
            }
            return true;
        }

        public static void CloseAllBags()
        {
            try
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame1").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("MainMenuBarBackpackButton").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame2").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag0Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame3").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag1Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame4").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag2Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame5").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag3Slot").LeftClick();
                }
            }
            catch
            {
            }
        }

        public static void DoMail()
        {
            MailList.Load();
            Thread.Sleep(0x3e8);
            MouseHelper.Hook();
            if (!MakeMailReady())
            {
                MouseHelper.ReleaseMouse();
            }
            else
            {
                AddedItemsStatus status = ClickItems(true, 12);
                bool flag = false;
                while (true)
                {
                    AddedItemsStatus status2 = status;
                    switch (status2)
                    {
                        case AddedItemsStatus.Error:
                            break;

                        case AddedItemsStatus.ClickedAll:
                            Logging.Write("Mail is full, sending", new object[0]);
                            MailFrame.ClickSend();
                            Thread.Sleep(0xdac);
                            if (LazySettings.MacroForMail)
                            {
                                SetMailNameUsingMacro();
                            }
                            else
                            {
                                MailFrame.SetReceiverHooked(LazySettings.MailTo);
                            }
                            Thread.Sleep(500);
                            break;

                        case AddedItemsStatus.ClickedSomething:
                            Logging.Write("Mailing Done", new object[0]);
                            MailFrame.ClickSend();
                            Thread.Sleep(500);
                            flag = true;
                            break;

                        default:
                            flag = true;
                            break;
                    }
                    if (flag)
                    {
                        KeyHelper.SendKey("ESC");
                        InterfaceHelper.CloseMainMenuFrame();
                        MailFrame.Close();
                        MouseHelper.ReleaseMouse();
                        CloseAllBags();
                        Logging.Debug("Mail Routine Result: " + status, new object[0]);
                        return;
                    }
                    Thread.Sleep(0x3e8);
                    Application.DoEvents();
                    status = ClickItems(false, 12);
                }
            }
        }

        private static int GetSlotCount(int item)
        {
            try
            {
                int slots;
                if (item != 1)
                {
                    if (item != 2)
                    {
                        if (item != 3)
                        {
                            if (item != 4)
                            {
                                if (item != 5)
                                {
                                    goto TR_0000;
                                }
                                else
                                {
                                    slots = Inventory.Bag4.Slots;
                                }
                            }
                            else
                            {
                                slots = Inventory.Bag3.Slots;
                            }
                        }
                        else
                        {
                            slots = Inventory.Bag2.Slots;
                        }
                    }
                    else
                    {
                        slots = Inventory.Bag1.Slots;
                    }
                }
                else
                {
                    slots = 0x10;
                }
                return slots;
            }
            catch
            {
            }
        TR_0000:
            return 0;
        }

        private static bool MakeMailReady()
        {
            bool flag;
            try
            {
                TargetMailBox();
                Thread.Sleep(0x7d0);
                OpenAllBags();
                Thread.Sleep(500);
                if (LazySettings.MacroForMail)
                {
                    flag = SetMailNameUsingMacro();
                }
                else if (!ClickSendMailTab())
                {
                    flag = false;
                }
                else
                {
                    Thread.Sleep(500);
                    MailFrame.SetReceiverHooked(LazySettings.MailTo);
                    Thread.Sleep(500);
                    flag = true;
                }
            }
            catch (Exception exception)
            {
                Logging.Write("Exception MakeMailReady: " + exception, new object[0]);
                flag = false;
            }
            return flag;
        }

        public static void OpenAllBags()
        {
            try
            {
                if (!InterfaceHelper.GetFrameByName("ContainerFrame1").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("MainMenuBarBackpackButton").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (!InterfaceHelper.GetFrameByName("ContainerFrame2").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag0Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (!InterfaceHelper.GetFrameByName("ContainerFrame3").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag1Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (!InterfaceHelper.GetFrameByName("ContainerFrame4").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag2Slot").LeftClick();
                }
            }
            catch
            {
            }
            try
            {
                if (!InterfaceHelper.GetFrameByName("ContainerFrame5").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("CharacterBag3Slot").LeftClick();
                }
            }
            catch
            {
            }
        }

        private static void RetryMailOpen(PGameObject node)
        {
            MoveHelper.StrafeLeft(true);
            Thread.Sleep(500);
            MoveHelper.StrafeLeft(false);
            node.Location.Face();
            Thread.Sleep(100);
            node.Interact(false);
            Thread.Sleep(0x5dc);
        }

        private static bool SetMailNameUsingMacro()
        {
            MailFrame.ClickMailFrame();
            Thread.Sleep(0x3e8);
            MailFrame.ClickInboxTab();
            Thread.Sleep(500);
            KeyHelper.SendKey("MacroForMail");
            Thread.Sleep(500);
            if (!ClickSendMailTab())
            {
                return false;
            }
            Thread.Sleep(500);
            return true;
        }

        public static bool TargetMailBox()
        {
            bool flag;
            using (List<PGameObject>.Enumerator enumerator = LazyLib.Wow.ObjectManager.GetGameObject.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PGameObject current = enumerator.Current;
                        if ((current.GameObjectType != 0x13) || (current.Location.DistanceToSelf >= 6.0))
                        {
                            continue;
                        }
                        current.Location.Face();
                        Thread.Sleep(100);
                        current.Interact(false);
                        if (!MailFrame.Open)
                        {
                            current.Interact(false);
                            if (!MailFrame.Open)
                            {
                                RetryMailOpen(current);
                                if (!MailFrame.Open)
                                {
                                    RetryMailOpen(current);
                                    if (!MailFrame.Open)
                                    {
                                        RetryMailOpen(current);
                                    }
                                }
                            }
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
}

