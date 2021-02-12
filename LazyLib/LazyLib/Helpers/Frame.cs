namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Frame
    {
        public uint BaseAddress;

        public Frame(uint baseAddress)
        {
            this.BaseAddress = baseAddress;
        }

        public Frame GetChildObject(string name) => 
            Enumerable.FirstOrDefault<Frame>(this.GetChilds, f => f.GetName == name);

        public void HoverHooked()
        {
            MouseHelper.MoveMouseToPosHooked(this.CenterX, this.CenterY);
        }

        public void LeftClick()
        {
            MouseHelper.MoveMouseToPos(this.CenterX, this.CenterY);
            MouseHelper.LeftClick();
            MouseHelper.ReleaseMouse();
        }

        public void LeftClickHooked()
        {
            MouseHelper.MoveMouseToPosHooked(this.CenterX, this.CenterY);
            MouseHelper.LeftClick();
        }

        public void RightClick()
        {
            MouseHelper.MoveMouseToPos(this.CenterX, this.CenterY);
            MouseHelper.RightClick();
            MouseHelper.ReleaseMouse();
        }

        public void RightClickHooked()
        {
            MouseHelper.MoveMouseToPosHooked(this.CenterX, this.CenterY);
            MouseHelper.RightClick();
        }

        [DllImport("user32")]
        internal static extern bool ScreenToClient(IntPtr hWnd, ref Point lpPoint);
        public void SetEditBoxText(string text)
        {
            this.LeftClick();
            if (this.GetEditBoxText.Length > 0)
            {
                Thread.Sleep(250);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.VK_LCONTROL);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.VK_LCONTROL);
                Thread.Sleep(200);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.Delete);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Delete);
                int num = 0;
                while (true)
                {
                    if (num >= 8)
                    {
                        KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Back);
                        Thread.Sleep(550);
                        break;
                    }
                    KeyLowHelper.PressKey(MicrosoftVirtualKeys.Back);
                    Thread.Sleep(100);
                    num++;
                }
            }
            KeyHelper.SendTextNow(text);
            Thread.Sleep(500);
        }

        public void SetEditBoxTextHooked(string text)
        {
            this.LeftClickHooked();
            if (this.GetEditBoxText.Length > 0)
            {
                Thread.Sleep(250);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.VK_LCONTROL);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.A);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.VK_LCONTROL);
                Thread.Sleep(200);
                KeyLowHelper.PressKey(MicrosoftVirtualKeys.Delete);
                KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Delete);
                int num = 0;
                while (true)
                {
                    if (num >= 8)
                    {
                        KeyLowHelper.ReleaseKey(MicrosoftVirtualKeys.Back);
                        Thread.Sleep(550);
                        break;
                    }
                    KeyLowHelper.PressKey(MicrosoftVirtualKeys.Back);
                    Thread.Sleep(100);
                    num++;
                }
            }
            KeyHelper.SendTextNow(text);
            Thread.Sleep(500);
        }

        public bool IsVisible
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 220 };
                return (Memory.Read<int>(addresses) == 1);
            }
        }

        public bool IsButtonChecked
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x2f5 };
                return (Memory.Read<uint>(addresses) != 0);
            }
        }

        public bool IsEnabled
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0xac };
                Logging.Debug("IsEnabled: " + Memory.Read<byte>(addresses), new object[0]);
                uint[] numArray2 = new uint[] { this.BaseAddress + 0xac };
                return (Memory.Read<byte>(numArray2) == 0);
            }
        }

        public bool Enabled
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x228 };
                Logging.Debug("Enabled reads: " + Memory.Read<byte>(addresses), new object[0]);
                uint[] numArray2 = new uint[] { this.BaseAddress + 0x228 };
                return (Memory.Read<byte>(numArray2) == 40);
            }
        }

        public string GetName
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x1c };
                return Memory.ReadUtf8(Memory.Read<uint>(addresses), 0x80);
            }
        }

        public string GetText
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0xf4 };
                return Memory.ReadUtf8(Memory.Read<uint>(addresses), 0xff);
            }
        }

        public string GetEditBoxText
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x2b4 };
                return Memory.ReadUtf8(Memory.Read<uint>(addresses), 0xff);
            }
        }

        internal float Left
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x68 };
                return ((Memory.Read<float>(addresses) * InterfaceHelper.WindowWidth) / Memory.ReadRelative<float>(new uint[] { 0x6c0cb4 }));
            }
        }

        internal float Right
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x70 };
                return ((Memory.Read<float>(addresses) * InterfaceHelper.WindowWidth) / Memory.ReadRelative<float>(new uint[] { 0x6c0cb4 }));
            }
        }

        internal float Top
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 0x6c };
                return ((Memory.Read<float>(addresses) * InterfaceHelper.WindowHeight) / Memory.ReadRelative<float>(new uint[] { 0x6c0cb8 }));
            }
        }

        internal float Bottom
        {
            get
            {
                uint[] addresses = new uint[] { this.BaseAddress + 100 };
                return ((Memory.Read<float>(addresses) * InterfaceHelper.WindowHeight) / Memory.ReadRelative<float>(new uint[] { 0x6c0cb8 }));
            }
        }

        internal float Width =>
            this.Right - this.Left;

        internal float Height =>
            this.Top - this.Bottom;

        public int CenterX
        {
            get
            {
                Point lpPoint = new Point();
                ScreenToClient(Memory.WindowHandle, ref lpPoint);
                return ((Math.Abs(lpPoint.X) + ((int) this.Left)) + ((int) (this.Width / 2f)));
            }
        }

        public int CenterY
        {
            get
            {
                Point lpPoint = new Point();
                ScreenToClient(Memory.WindowHandle, ref lpPoint);
                return (((Math.Abs(lpPoint.Y) + InterfaceHelper.WindowHeight) - ((int) this.Top)) + ((int) (this.Height / 2f)));
            }
        }

        public List<Frame> GetChilds
        {
            get
            {
                uint[] numArray3;
                List<Frame> list = new List<Frame>();
                uint[] addresses = new uint[] { this.BaseAddress + 0x214 };
                for (uint i = Memory.Read<uint>(addresses); (i != 0) && ((i & 1) == 0); i = Memory.Read<uint>(numArray3))
                {
                    uint[] numArray2 = new uint[] { i + 0x1c };
                    if (Memory.ReadUtf8(Memory.Read<uint>(numArray2), 0x63).Length > 0)
                    {
                        list.Add(new Frame(i));
                    }
                    numArray3 = new uint[1];
                    uint[] numArray4 = new uint[] { this.BaseAddress + 0x20c };
                    numArray3[0] = (i + Memory.Read<uint>(numArray4)) + 4;
                }
                return list;
            }
        }
    }
}

