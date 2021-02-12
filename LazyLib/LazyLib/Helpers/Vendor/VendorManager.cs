namespace LazyLib.Helpers.Vendor
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Helpers.Mail;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class VendorManager
    {
        private static readonly List<string> Sold = new List<string>();
        private static EventHandler SellFinished;

        public static event EventHandler SellFinished
        {
            add
            {
                EventHandler sellFinished = SellFinished;
                while (true)
                {
                    EventHandler comparand = sellFinished;
                    EventHandler handler3 = comparand + value;
                    sellFinished = Interlocked.CompareExchange<EventHandler>(ref SellFinished, handler3, comparand);
                    if (ReferenceEquals(sellFinished, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler sellFinished = SellFinished;
                while (true)
                {
                    EventHandler comparand = sellFinished;
                    EventHandler handler3 = comparand - value;
                    sellFinished = Interlocked.CompareExchange<EventHandler>(ref SellFinished, handler3, comparand);
                    if (ReferenceEquals(sellFinished, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static void DoSell(PUnit vendor)
        {
            try
            {
                ProtectedList.Load();
                MailList.Load();
                MoveHelper.MoveToUnit(vendor, 3.0);
                vendor.Location.Face();
                InterfaceHelper.CloseMainMenuFrame();
                int num = 1;
                while (true)
                {
                    vendor.Interact(false);
                    Thread.Sleep(0x3e8);
                    if (InterfaceHelper.GetFrameByName("GossipFrameCloseButton").IsVisible)
                    {
                        if (!InterfaceHelper.GetFrameByName("GossipTitleButton" + num).IsVisible)
                        {
                            KeyHelper.SendKey("ESC");
                            InterfaceHelper.CloseMainMenuFrame();
                            num++;
                            continue;
                        }
                        Thread.Sleep(0x5dc);
                        InterfaceHelper.GetFrameByName("GossipTitleButton" + num).LeftClick();
                        Thread.Sleep(0x5dc);
                        if (!InterfaceHelper.GetFrameByName("MerchantFrame").IsVisible && (num < 6))
                        {
                            KeyHelper.SendKey("ESC");
                            InterfaceHelper.CloseMainMenuFrame();
                            num++;
                            continue;
                        }
                    }
                    if (!ReferenceEquals(LazyLib.Wow.ObjectManager.MyPlayer.Target, vendor))
                    {
                        vendor.Location.Face();
                        vendor.Interact(false);
                        Thread.Sleep(0x3e8);
                    }
                    MouseHelper.Hook();
                    MailManager.OpenAllBags();
                    if (LazySettings.ShouldVendor)
                    {
                        Logging.Write("[Vendor]Going to sell items", new object[0]);
                        Sell();
                    }
                    if (LazySettings.ShouldRepair)
                    {
                        Repair();
                    }
                    break;
                }
            }
            finally
            {
                MailManager.CloseAllBags();
                MouseHelper.ReleaseMouse();
                if (SellFinished != null)
                {
                    SellFinished("VendorEngine", new EventArgs());
                }
            }
        }

        public static void DoSell(string unit_name)
        {
            try
            {
                ProtectedList.Load();
                MailList.Load();
                KeyHelper.ChatboxSendText("/target " + unit_name);
                Thread.Sleep(0xbb8);
                if (LazyLib.Wow.ObjectManager.MyPlayer.Target.Name != unit_name)
                {
                    Logging.Write("Could not target vendor: " + unit_name, new object[0]);
                }
                else
                {
                    KeyHelper.SendKey("InteractTarget");
                    MouseHelper.Hook();
                    MailManager.OpenAllBags();
                    if (LazySettings.ShouldVendor)
                    {
                        Logging.Write("[Vendor]Going to sell items", new object[0]);
                        Sell();
                    }
                    if (LazySettings.ShouldRepair)
                    {
                        Repair();
                    }
                }
            }
            finally
            {
                MailManager.CloseAllBags();
                MouseHelper.ReleaseMouse();
                if (SellFinished != null)
                {
                    SellFinished("VendorEngine", new EventArgs());
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

        private static void LoadWowHead()
        {
            foreach (PItem item in Inventory.GetItemsInBags)
            {
                uint entryId = item.EntryId;
                if (ItemDatabase.GetItem(entryId.ToString()) == null)
                {
                    Dictionary<string, string> wowHeadItem = WowHeadData.GetWowHeadItem((double) item.EntryId);
                    if (wowHeadItem != null)
                    {
                        string str = wowHeadItem["name"];
                        string str2 = wowHeadItem["quality"];
                        if (!string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(str2))
                        {
                            ItemDatabase.PutItem(item.EntryId.ToString(), str, str2);
                        }
                    }
                }
            }
        }

        private static void Repair()
        {
            Frame frameByName = InterfaceHelper.GetFrameByName("MerchantRepairAllButton");
            if (frameByName != null)
            {
                frameByName.LeftClick();
            }
        }

        private static void Sell()
        {
            Sold.Clear();
            LoadWowHead();
            SellLoop();
        }

        private static void SellLoop()
        {
            int item = 1;
            int num2 = 1;
            while (item != 6)
            {
                if (InterfaceHelper.GetFrameByName("ContainerFrame" + item) == null)
                {
                    item++;
                    continue;
                }
                int slotCount = GetSlotCount(item);
                Logging.Write("Found ContainerFrame with Slot count: " + slotCount, new object[0]);
                while (true)
                {
                    if (num2 == (slotCount + 1))
                    {
                        if (num2 == (slotCount + 1))
                        {
                            num2 = 1;
                            item++;
                        }
                        break;
                    }
                    Frame frameByName = InterfaceHelper.GetFrameByName(string.Concat(new object[] { "ContainerFrame", item, "Item", num2 }));
                    if (frameByName != null)
                    {
                        frameByName.HoverHooked();
                        Thread.Sleep(170);
                        try
                        {
                            Frame frame2 = InterfaceHelper.GetFrameByName("GameTooltip");
                            if (frame2 != null)
                            {
                                Frame childObject = frame2.GetChildObject("GameTooltipTextLeft1");
                                if ((childObject != null) && ShouldSell(childObject.GetText))
                                {
                                    Logging.Write("Selling: " + childObject.GetText, new object[0]);
                                    Thread.Sleep(150);
                                    frameByName.RightClickHooked();
                                    Thread.Sleep(150);
                                }
                            }
                        }
                        catch (Exception exception)
                        {
                            Logging.Write("Exception when pasing gametooltip: " + exception, new object[0]);
                        }
                    }
                    num2++;
                }
            }
        }

        private static bool ShouldSell(string sellName)
        {
            try
            {
                using (List<PItem>.Enumerator enumerator = Inventory.GetItemsInBags.GetEnumerator())
                {
                    bool flag;
                    while (true)
                    {
                        if (enumerator.MoveNext())
                        {
                            PItem current = enumerator.Current;
                            try
                            {
                                string str;
                                if (ItemDatabase.GetItem(current.EntryId.ToString()) == null)
                                {
                                    continue;
                                }
                                string str2 = ItemDatabase.GetItem(current.EntryId.ToString())["item_name"].ToString();
                                string str3 = ItemDatabase.GetItem(current.EntryId.ToString())["item_quality"].ToString();
                                if (string.IsNullOrEmpty(str2) || string.IsNullOrEmpty(str3))
                                {
                                    Logging.Write($"[Vendor]Could not detect the name of: {current.EntryId} is wowhead down?", new object[0]);
                                    continue;
                                }
                                if ((((str2 == sellName) || (sellName.Replace(str2, "").Length != sellName.Length)) && (!MailList.ShouldMail(str2) && ProtectedList.ShouldVendor(str2))) && ((str = str3) != null))
                                {
                                    if (str == "Poor")
                                    {
                                        if (LazySettings.SellPoor)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (str == "Common")
                                    {
                                        if (LazySettings.SellCommon)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (str == "Uncommon")
                                    {
                                        if (LazySettings.SellUncommon)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (str == "Rare")
                                    {
                                        if (LazySettings.SellRare)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (str != "Epic")
                                    {
                                        if ((str == "Legendary") && LazySettings.SellLegendary)
                                        {
                                            flag = true;
                                            break;
                                        }
                                    }
                                    else if (LazySettings.SellEpic)
                                    {
                                        flag = true;
                                        break;
                                    }
                                }
                                continue;
                            }
                            catch (Exception exception1)
                            {
                                Logging.Debug("Exception in ShouldSell (Loop): {0}", new object[] { exception1 });
                                continue;
                            }
                        }
                        else
                        {
                            goto TR_0000;
                        }
                        break;
                    }
                    return flag;
                }
            }
            catch (Exception exception3)
            {
                Logging.Debug("Exception in ShouldSell: {0}", new object[] { exception3 });
            }
        TR_0000:
            return false;
        }
    }
}

