namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class MouseHelper
    {
        private const int iRestore = 9;
        private const int iShow = 5;
        private static bool _locked;
        private static EventHandler<MouseMoveMessasge> MouseMoveMessage;
        private static EventHandler<MouseBlocMessasge> MouseBlockMessage;
        internal static FunctionToCall GetPosFromDelegate;
        private static readonly Ticker TimeOut = new Ticker(25.0);

        public static event EventHandler<MouseBlocMessasge> MouseBlockMessage
        {
            add
            {
                EventHandler<MouseBlocMessasge> mouseBlockMessage = MouseBlockMessage;
                while (true)
                {
                    EventHandler<MouseBlocMessasge> comparand = mouseBlockMessage;
                    EventHandler<MouseBlocMessasge> handler3 = comparand + value;
                    mouseBlockMessage = Interlocked.CompareExchange<EventHandler<MouseBlocMessasge>>(ref MouseBlockMessage, handler3, comparand);
                    if (ReferenceEquals(mouseBlockMessage, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<MouseBlocMessasge> mouseBlockMessage = MouseBlockMessage;
                while (true)
                {
                    EventHandler<MouseBlocMessasge> comparand = mouseBlockMessage;
                    EventHandler<MouseBlocMessasge> handler3 = comparand - value;
                    mouseBlockMessage = Interlocked.CompareExchange<EventHandler<MouseBlocMessasge>>(ref MouseBlockMessage, handler3, comparand);
                    if (ReferenceEquals(mouseBlockMessage, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event EventHandler<MouseMoveMessasge> MouseMoveMessage
        {
            add
            {
                EventHandler<MouseMoveMessasge> mouseMoveMessage = MouseMoveMessage;
                while (true)
                {
                    EventHandler<MouseMoveMessasge> comparand = mouseMoveMessage;
                    EventHandler<MouseMoveMessasge> handler3 = comparand + value;
                    mouseMoveMessage = Interlocked.CompareExchange<EventHandler<MouseMoveMessasge>>(ref MouseMoveMessage, handler3, comparand);
                    if (ReferenceEquals(mouseMoveMessage, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<MouseMoveMessasge> mouseMoveMessage = MouseMoveMessage;
                while (true)
                {
                    EventHandler<MouseMoveMessasge> comparand = mouseMoveMessage;
                    EventHandler<MouseMoveMessasge> handler3 = comparand - value;
                    mouseMoveMessage = Interlocked.CompareExchange<EventHandler<MouseMoveMessasge>>(ref MouseMoveMessage, handler3, comparand);
                    if (ReferenceEquals(mouseMoveMessage, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static void BlockInput(bool block)
        {
            if (MouseBlockMessage != null)
            {
                MouseBlocMessasge e = new MouseBlocMessasge {
                    Block = block
                };
                MouseBlockMessage(new object(), e);
            }
            else
            {
                IntPtr windowHandle = Memory.WindowHandle;
                if (windowHandle.ToInt32() > 0)
                {
                    SetForegroundWindow(windowHandle);
                    ShowWindow(windowHandle, IsIconic(windowHandle) ? 9 : 5);
                }
            }
        }

        public static void Hook()
        {
            BlockInput(true);
            _locked = true;
        }

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr Hwnd);
        public static void LeftClick()
        {
            LeftDown();
            LeftUp();
        }

        private static void LeftDown()
        {
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x201, IntPtr.Zero, IntPtr.Zero);
            WaitFrameReload();
        }

        private static void LeftUp()
        {
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x202, IntPtr.Zero, IntPtr.Zero);
            WaitFrameReload();
        }

        public static void MoveMouseToPos(Point p)
        {
            MoveMouseToPos(p.X, p.Y);
        }

        public static void MoveMouseToPos(int x, int y)
        {
            BlockInput(true);
            _locked = true;
            MoveTheCursor(x, y);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
            WaitFrameReload();
        }

        public static void MoveMouseToPosHooked(int x, int y)
        {
            MoveTheCursor(x, y);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
        }

        private static void MoveTheCursor(int x, int y)
        {
            if (MouseMoveMessage == null)
            {
                SetCursorPos(x, y);
            }
            else
            {
                MouseMoveMessasge e = new MouseMoveMessasge {
                    X = x,
                    Y = y
                };
                MouseMoveMessage(new object(), e);
            }
        }

        public static void ReleaseMouse()
        {
            if (_locked)
            {
                BlockInput(false);
                _locked = false;
            }
        }

        public static void RightClick()
        {
            RightDown();
            RightUp();
        }

        private static void RightDown()
        {
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x204, IntPtr.Zero, IntPtr.Zero);
            WaitFrameReload();
        }

        private static void RightUp()
        {
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x200, IntPtr.Zero, IntPtr.Zero);
            KeyLowHelper.SendMessage(Memory.WindowHandle, 0x205, IntPtr.Zero, IntPtr.Zero);
            WaitFrameReload();
        }

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int x, int y);
        public static void SetDelegate(FunctionToCall function)
        {
            GetPosFromDelegate = function;
        }

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr Hwnd);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr Hwnd, int iCmdShow);
        public static void WaitFrameReload()
        {
            TimeOut.Reset();
            while (!TimeOut.IsReady)
            {
                Thread.Sleep(1);
            }
        }

        public static Point MousePosition
        {
            get
            {
                if (!LazySettings.HookMouse)
                {
                    return Cursor.Position;
                }
                GetPosFromDelegate();
                return GetPosFromDelegate();
            }
        }

        public delegate Point FunctionToCall();
    }
}

