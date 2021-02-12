namespace LazyLib.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Threading;

    public class Fishing
    {
        private static bool _triedWorldToScreen;
        private static bool _tryingSearch;
        private static bool _saidSomethingManager;
        private static readonly Ticker TimeOut = new Ticker(2000.0);
        public static int _bites = 0;
        public static float bobberX = 0f;
        public static float bobberY = 0f;

        private static PGameObject Bobber() => 
            Enumerable.FirstOrDefault<PGameObject>(LazyLib.Wow.ObjectManager.GetGameObject, pGameObject => pGameObject.DisplayId == 0x29c);

        public static bool FindBobberAndClick(bool waitForLoot)
        {
            PGameObject obj2 = null;
            Thread.Sleep(550);
            _triedWorldToScreen = false;
            _saidSomethingManager = false;
            _tryingSearch = false;
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsCasting)
            {
                if (obj2 == null)
                {
                    obj2 = Bobber();
                }
                else
                {
                    bobberX = obj2.X;
                    bobberY = obj2.Y;
                    if (!LazySettings.JustFishing && !obj2.IsInSchool)
                    {
                        break;
                    }
                    if (!_saidSomethingManager)
                    {
                        if (!LazySettings.JustFishing)
                        {
                            Logging.Write("Bobber in school", new object[0]);
                        }
                        else
                        {
                            Logging.Write("Fishing", new object[0]);
                        }
                        _saidSomethingManager = true;
                    }
                    if (LazySettings.BackgroundMode)
                    {
                        if (obj2.IsBobbing)
                        {
                            _bites++;
                            object[] args = new object[] { _bites };
                            Logging.Write("Bite ( Total Bites: {0} )", args);
                            obj2.Interact(false);
                            Thread.Sleep(450);
                            if (waitForLoot)
                            {
                                while (true)
                                {
                                    if (!LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen || TimeOut.IsReady)
                                    {
                                        Thread.Sleep(850);
                                        break;
                                    }
                                    Thread.Sleep(100);
                                }
                            }
                            return true;
                        }
                    }
                    else if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(obj2.GUID))
                    {
                        if (!_triedWorldToScreen)
                        {
                            Logging.Write("Trying world to screen", new object[0]);
                            FindTheBobber(obj2.Location, obj2.GUID);
                            _triedWorldToScreen = true;
                        }
                        else
                        {
                            if (!_tryingSearch)
                            {
                                Logging.Write("Trying search", new object[0]);
                                _tryingSearch = true;
                            }
                            FindBobberSearch();
                        }
                        Thread.Sleep(100);
                    }
                    else if (obj2.IsBobbing)
                    {
                        KeyHelper.SendKey("InteractWithMouseOver");
                        Thread.Sleep(0x5dc);
                        if (waitForLoot)
                        {
                            while (true)
                            {
                                if (!LazyLib.Wow.ObjectManager.MyPlayer.LootWinOpen || TimeOut.IsReady)
                                {
                                    Thread.Sleep(850);
                                    break;
                                }
                                Thread.Sleep(100);
                            }
                        }
                        return true;
                    }
                    Thread.Sleep(850);
                }
                Thread.Sleep(100);
            }
            return false;
        }

        private static void FindBobberSearch()
        {
            GamePosition.Findpos(Memory.WindowHandle);
            int num = -200;
            int num2 = -200;
            while (!IsMouseOverBobber())
            {
                MoveMouse(GamePosition.GetCenterX + num, GamePosition.GetCenterY + num2);
                Thread.Sleep(10);
                if (IsMouseOverBobber())
                {
                    return;
                }
                num += 10;
                if (num >= 200)
                {
                    num2 += 10;
                    num = -200;
                    if (num2 > 200)
                    {
                        return;
                    }
                }
            }
        }

        private static void FindTheBobber(Location loc, ulong guid)
        {
            Point point = Camera.World2Screen.WorldToScreen(loc, true);
            MoveMouse(point.X, point.Y);
            Thread.Sleep(50);
            if (!IsMouseOverBobber())
            {
                MoveMouse(point.X, point.Y - 15);
                Thread.Sleep(50);
                if (!IsMouseOverBobber())
                {
                    MoveMouse(point.X, point.Y + 15);
                    Thread.Sleep(50);
                    if (!IsMouseOverBobber())
                    {
                        Thread.Sleep(50);
                        MoveMouse(point.X - 15, point.Y);
                        Thread.Sleep(50);
                        if (!IsMouseOverBobber())
                        {
                            MoveMouse(point.X + 15, point.Y);
                            Thread.Sleep(50);
                            if (!IsMouseOverBobber())
                            {
                                MoveMouse(point.X, point.Y + 0x23);
                                Thread.Sleep(50);
                                if (!IsMouseOverBobber())
                                {
                                    MoveMouse(point.X, point.Y + 40);
                                    Thread.Sleep(50);
                                    if (!IsMouseOverBobber())
                                    {
                                        MoveMouse(point.X, point.Y + 0x2d);
                                        Thread.Sleep(50);
                                        if (!IsMouseOverBobber())
                                        {
                                            MoveMouse(point.X + 0x23, point.Y);
                                            Thread.Sleep(50);
                                            if (!IsMouseOverBobber())
                                            {
                                                MoveMouse(point.X + 40, point.Y);
                                                Thread.Sleep(50);
                                                if (!IsMouseOverBobber())
                                                {
                                                    MoveMouse(point.X + 0x2d, point.Y);
                                                    Thread.Sleep(50);
                                                    if (!IsMouseOverBobber())
                                                    {
                                                        int num = -60;
                                                        int num2 = -60;
                                                        MoveMouse(point.X, point.Y);
                                                        while (!IsMouseOverBobber())
                                                        {
                                                            MoveMouse(point.X + num, point.Y + num2);
                                                            Thread.Sleep(10);
                                                            if (IsMouseOverBobber())
                                                            {
                                                                return;
                                                            }
                                                            num += 10;
                                                            if (num >= 60)
                                                            {
                                                                num2 += 10;
                                                                num = -60;
                                                                if (num2 > 60)
                                                                {
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static bool IsMouseOverBobber() => 
            Memory.ReadRelative<uint>(new uint[] { 0x93d0e0 }) == 0x35;

        private static void MoveMouse(int x, int y)
        {
            if (LazySettings.HookMouse)
            {
                MouseHelper.MoveMouseToPosHooked(x, y);
            }
            else
            {
                SetCursorPos(x, y);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
    }
}

