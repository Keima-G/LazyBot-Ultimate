namespace LazyLib.Wow
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class PObject
    {
        private const int iRestore = 9;
        private const int iShow = 5;
        private static Point _oldPoint;

        public PObject(uint baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        private static bool DoSmallestSearch(ulong guid)
        {
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return true;
            }
            GamePosition.Findpos(Memory.WindowHandle);
            int num = -40;
            int num2 = -40;
            while (true)
            {
                if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
                {
                    MoveMouse(GamePosition.GetCenterX + num, GamePosition.GetCenterY + num2);
                    Thread.Sleep(10);
                    if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
                    {
                        num += 10;
                        if (num <= 50)
                        {
                            continue;
                        }
                        num2 += 10;
                        num = -40;
                        if (num2 <= 50)
                        {
                            continue;
                        }
                    }
                }
                return Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid);
            }
        }

        private void FindUsingWorldToScreen()
        {
            Point point = Camera.World2Screen.WorldToScreen(this.Location, true);
            MoveMouse(point.X, point.Y);
            Thread.Sleep(50);
            if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
            {
                MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y - 15);
                Thread.Sleep(50);
                if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                {
                    MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y + 15);
                    Thread.Sleep(50);
                    if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                    {
                        Thread.Sleep(50);
                        MoveMouse(MouseHelper.MousePosition.X - 15, MouseHelper.MousePosition.Y);
                        Thread.Sleep(50);
                        if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                        {
                            MoveMouse(MouseHelper.MousePosition.X + 15, MouseHelper.MousePosition.Y);
                            Thread.Sleep(50);
                            if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                            {
                                MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y + 0x23);
                                Thread.Sleep(50);
                                if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                                {
                                    MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y + 40);
                                    Thread.Sleep(50);
                                    if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID))
                                    {
                                        MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y + 0x2d);
                                        Thread.Sleep(50);
                                        Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(this.GUID);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        protected T GetStorageField<T>(Descriptors.eObjectFields field) where T: struct
        {
            try
            {
                return this.GetStorageField<T>((uint) field);
            }
            catch (Exception exception)
            {
                Console.WriteLine("DO NOT POST THIS WARNING ON THE FORUM! ONLY DEBUG!: " + exception);
                return default(T);
            }
        }

        protected T GetStorageField<T>(uint field) where T: struct
        {
            try
            {
                field *= 4;
                uint[] addresses = new uint[] { this.BaseAddress + 8 };
                return (T) Memory.ReadObject(Memory.Read<uint>(addresses) + field, typeof(T));
            }
            catch (Exception exception)
            {
                Console.WriteLine("DO NOT POST THIS WARNING ON THE FORUM! ONLY DEBUG!: " + exception);
                return default(T);
            }
        }

        public bool Interact(bool multiclick) => 
            this.InteractOrTarget(multiclick);

        public bool InteractOrTarget(bool multiclick)
        {
            if (LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID == this.GUID)
            {
                KeyHelper.SendKey("InteractWithMouseOver");
                return true;
            }
            if (LazySettings.BackgroundMode)
            {
                Memory.Write<ulong>(Memory.BaseAddress + 0x7d07a0, this.GUID);
                Thread.Sleep(50);
                KeyHelper.SendKey("InteractWithMouseOver");
                Thread.Sleep(500);
                return LazyLib.Wow.ObjectManager.MyPlayer.TargetGUID.Equals(this.GUID);
            }
            _oldPoint.X = Cursor.Position.X;
            _oldPoint.Y = Cursor.Position.Y;
            MouseHelper.Hook();
            if (!LazySettings.HookMouse)
            {
                SetForGround();
            }
            this.FindUsingWorldToScreen();
            bool flag = LetsSearch(this.GUID, multiclick, true);
            MoveMouse(_oldPoint.X, _oldPoint.Y);
            MouseHelper.ReleaseMouse();
            return flag;
        }

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr Hwnd);
        public void LeftClick()
        {
            if (!LazySettings.HookMouse)
            {
                SetForGround();
            }
            Point point = Camera.World2Screen.WorldToScreen(this.Location, true);
            MoveMouse(point.X, point.Y);
            Thread.Sleep(50);
            MouseHelper.LeftClick();
            Thread.Sleep(50);
        }

        private static bool LetsSearch(ulong guid, bool multiclick, bool click)
        {
            if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
            {
                GamePosition.Findpos(Memory.WindowHandle);
                if (!DoSmallestSearch(guid) && (!Search(guid, GamePosition.Width / 0x10) && (!Search(guid, GamePosition.Width / 12) && (!Search(guid, GamePosition.Width / 10) && !Search(guid, GamePosition.Width / 8)))))
                {
                    Search(guid, GamePosition.Width / 6);
                }
            }
            if ((LazyLib.Wow.ObjectManager.GetAttackers.Count != 0) && LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return false;
            }
            if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
            {
                return false;
            }
            if (click)
            {
                if (!LazySettings.HookMouse)
                {
                    SetForGround();
                }
                if (multiclick)
                {
                    MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y - 15);
                    Thread.Sleep(50);
                    KeyHelper.SendKey("InteractWithMouseOver");
                    Thread.Sleep(50);
                    MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y + 15);
                    Thread.Sleep(50);
                    KeyHelper.SendKey("InteractWithMouseOver");
                    Thread.Sleep(50);
                    MoveMouse(MouseHelper.MousePosition.X - 15, MouseHelper.MousePosition.Y);
                    Thread.Sleep(50);
                    KeyHelper.SendKey("InteractWithMouseOver");
                    MoveMouse(MouseHelper.MousePosition.X + 15, MouseHelper.MousePosition.Y);
                    Thread.Sleep(50);
                    KeyHelper.SendKey("InteractWithMouseOver");
                    Thread.Sleep(50);
                }
                MoveMouse(MouseHelper.MousePosition.X, MouseHelper.MousePosition.Y);
                Thread.Sleep(50);
                KeyHelper.SendKey("InteractWithMouseOver");
                Thread.Sleep(50);
            }
            return true;
        }

        public bool MouseOver()
        {
            this.FindUsingWorldToScreen();
            return LetsSearch(this.GUID, false, false);
        }

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

        private static bool Search(ulong guid, int yValue)
        {
            if (LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                return true;
            }
            int num = 0;
            int num2 = -yValue;
            bool flag = true;
            while (true)
            {
                if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
                {
                    MoveMouse(GamePosition.GetCenterX + num, GamePosition.GetCenterY + num2);
                    Thread.Sleep(10);
                    if (!Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid))
                    {
                        num = !flag ? ((num2 >= 0) ? (num + 15) : (num - 15)) : ((num2 >= 0) ? (num - 15) : (num + 15));
                        num2 += 8;
                        if (num2 <= yValue)
                        {
                            continue;
                        }
                        if (flag)
                        {
                            num2 = -yValue;
                            flag = false;
                            continue;
                        }
                    }
                }
                return Memory.ReadObject(Memory.BaseAddress + 0x7d07a0, typeof(ulong)).Equals(guid);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr Hwnd);
        private static void SetForGround()
        {
            IntPtr windowHandle = Memory.WindowHandle;
            if (windowHandle.ToInt32() > 0)
            {
                SetForegroundWindow(windowHandle);
                ShowWindow(windowHandle, IsIconic(windowHandle) ? 9 : 5);
            }
        }

        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr Hwnd, int iCmdShow);
        public PGameObject ToGameObject(PObject obj) => 
            new PGameObject(obj.BaseAddress);

        public PPlayer ToPlayer(PItem obj) => 
            new PPlayer(obj.BaseAddress);

        public PPlayer ToPlayer(PObject obj) => 
            new PPlayer(obj.BaseAddress);

        public PPlayer ToPlayer(PPlayer obj) => 
            new PPlayer(obj.BaseAddress);

        public PPlayer ToPlayer(PUnit obj) => 
            new PPlayer(obj.BaseAddress);

        public PUnit ToUnit(PItem obj) => 
            new PUnit(obj.BaseAddress);

        public PUnit ToUnit(PObject obj) => 
            new PUnit(obj.BaseAddress);

        internal void UpdateBaseAddress(uint address)
        {
            this.BaseAddress = address;
        }

        public uint BaseAddress { get; set; }

        public virtual ulong GUID =>
            !this.IsValid ? 0UL : this.GetStorageField<ulong>((uint) 0);

        public bool IsValid =>
            this.BaseAddress != 0;

        public int Type
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 20 };
                return Memory.Read<int>(addresses);
            }
        }

        public int Entry =>
            this.GetStorageField<int>((uint) 3);

        public int Level =>
            this.GetStorageField<int>((uint) 0x36);

        public virtual float X
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[] { this.BaseAddress + 0x798 };
                    return Memory.Read<float>(addresses);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public virtual float Y
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[] { this.BaseAddress + 0x79c };
                    return Memory.Read<float>(addresses);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public virtual float Z
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[] { this.BaseAddress + 0x7a0 };
                    return Memory.Read<float>(addresses);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public virtual float Facing
        {
            get
            {
                try
                {
                    uint[] addresses = new uint[] { this.BaseAddress + 0x7a8 };
                    return Memory.Read<float>(addresses);
                }
                catch
                {
                    return 0f;
                }
            }
        }

        public virtual LazyLib.Wow.Location Location =>
            new LazyLib.Wow.Location(this.X, this.Y, this.Z);

        public bool IsMe =>
            this.GUID == LazyLib.Wow.ObjectManager.MyPlayer.GUID;

        public uint StorageField
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 8 };
                return Memory.Read<uint>(addresses);
            }
        }
    }
}

