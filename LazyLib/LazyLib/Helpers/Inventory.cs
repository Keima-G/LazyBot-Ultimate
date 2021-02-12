namespace LazyLib.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Inventory
    {
        public static void CloseAllBags()
        {
            for (int i = 1; i <= 5; i++)
            {
                Frame frameByName = InterfaceHelper.GetFrameByName("ContainerFrame" + i);
                Frame frame2 = InterfaceHelper.GetFrameByName("ContainerFrame" + i + "CloseButton");
                if ((((frameByName != null) || (frame2 != null)) && (frameByName != null)) && frameByName.IsVisible)
                {
                    frame2.LeftClick();
                    Thread.Sleep(0x5dc);
                }
            }
        }

        public static void OpenBagByNumber(int index)
        {
            Frame frameByName;
            switch (index)
            {
                case 0:
                    frameByName = InterfaceHelper.GetFrameByName("MainMenuBarBackpackButton");
                    break;

                case 1:
                    frameByName = InterfaceHelper.GetFrameByName("CharacterBag0Slot");
                    break;

                case 2:
                    frameByName = InterfaceHelper.GetFrameByName("CharacterBag1Slot");
                    break;

                case 3:
                    frameByName = InterfaceHelper.GetFrameByName("CharacterBag2Slot");
                    break;

                case 4:
                    frameByName = InterfaceHelper.GetFrameByName("CharacterBag3Slot");
                    break;

                default:
                    throw new ArgumentException("Number outside bounds");
            }
            if (frameByName == null)
            {
                Logging.Write("Could not find bag " + index, new object[0]);
            }
            else
            {
                frameByName.LeftClick();
                Thread.Sleep(500);
            }
        }

        public static List<ulong> GUIDOfItemsInBag =>
            LazyLib.Wow.ObjectManager.MyPlayer.GUIDOfItemsInBag;

        private static List<ulong> GUIDOfBags
        {
            get
            {
                List<ulong> list = new List<ulong>();
                try
                {
                    list.Add(Bag1GUID);
                }
                catch
                {
                }
                try
                {
                    list.Add(Bag2GUID);
                }
                catch
                {
                }
                try
                {
                    list.Add(Bag3GUID);
                }
                catch
                {
                }
                try
                {
                    list.Add(Bag4GUID);
                }
                catch
                {
                }
                return list;
            }
        }

        private static ulong Bag1GUID =>
            Memory.ReadRelative<ulong>(new uint[] { 0x823540 });

        private static ulong Bag2GUID =>
            Memory.ReadRelative<ulong>(new uint[] { 0x823548 });

        private static ulong Bag3GUID =>
            Memory.ReadRelative<ulong>(new uint[] { 0x823550 });

        private static ulong Bag4GUID =>
            Memory.ReadRelative<ulong>(new uint[] { 0x823558 });

        public static PContainer Bag1 =>
            Enumerable.FirstOrDefault<PContainer>(LazyLib.Wow.ObjectManager.GetContainers, container => Bag1GUID == container.GUID);

        public static PContainer Bag2 =>
            Enumerable.FirstOrDefault<PContainer>(LazyLib.Wow.ObjectManager.GetContainers, container => Bag2GUID == container.GUID);

        public static PContainer Bag3 =>
            Enumerable.FirstOrDefault<PContainer>(LazyLib.Wow.ObjectManager.GetContainers, container => Bag3GUID == container.GUID);

        public static PContainer Bag4 =>
            Enumerable.FirstOrDefault<PContainer>(LazyLib.Wow.ObjectManager.GetContainers, container => Bag4GUID == container.GUID);

        public static List<PItem> GetItemsInBags
        {
            get
            {
                try
                {
                    List<ulong> gUIDOfBags = new List<ulong>();
                    try
                    {
                        gUIDOfBags = GUIDOfBags;
                    }
                    catch
                    {
                    }
                    List<PItem> list2 = new List<PItem>();
                    List<ulong> gUIDOfItemsInBag = GUIDOfItemsInBag;
                    foreach (PItem item in LazyLib.Wow.ObjectManager.GetItems)
                    {
                        if (item != null)
                        {
                            try
                            {
                                if (gUIDOfBags.Contains(item.Contained) || gUIDOfItemsInBag.Contains(item.GUID))
                                {
                                    list2.Add(item);
                                }
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    return list2;
                }
                catch (Exception exception1)
                {
                    Logging.Write("Exception in GetItemsInBags  (Cannot complete vendoring :( ) {0}", new object[] { exception1 });
                    return new List<PItem>();
                }
            }
        }

        public static int FreeBagSlots
        {
            get
            {
                try
                {
                    return Convert.ToInt32(InterfaceHelper.GetFrameByName("MainMenuBarBackpackButton").GetChildObject("MainMenuBarBackpackButtonCount").GetText.Split(new char[] { '(' })[1].Split(new char[] { ')' })[0]);
                }
                catch
                {
                    return 0x7fffffff;
                }
            }
        }
    }
}

