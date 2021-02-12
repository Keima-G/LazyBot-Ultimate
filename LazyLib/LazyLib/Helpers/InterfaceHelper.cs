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
    public class InterfaceHelper
    {
        private static Dictionary<string, Frame> _allFrames = new Dictionary<string, Frame>();
        private static Thread _updateThread;

        public static void CloseMainMenuFrame()
        {
            try
            {
                if (GetFrameByName("GameMenuFrame").IsVisible)
                {
                    Logging.Debug("Hmm, the game menu seems to be open, lets close it", new object[0]);
                    Thread.Sleep(250);
                    KeyHelper.SendKey("ESC");
                }
            }
            catch
            {
                Logging.Debug("Check of MainMenuFrame returned false, wich is good :)", new object[0]);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);
        public static Frame GetFrameByName(string name)
        {
            try
            {
                return ((_allFrames[name] == null) ? null : _allFrames[name]);
            }
            catch
            {
                return null;
            }
        }

        public static void ReloadFrames()
        {
            Dictionary<string, Frame> dictionary = new Dictionary<string, Frame>();
            uint num = Memory.ReadRelative<uint>(new uint[] { 0x7499a8 });
            uint[] addresses = new uint[] { num + 0xcd4 };
            uint baseAddress = Memory.Read<uint>(addresses);
            while (baseAddress != 0)
            {
                Frame frame = new Frame(baseAddress);
                if (!dictionary.ContainsKey(frame.GetName))
                {
                    dictionary.Add(frame.GetName, frame);
                }
                uint[] numArray3 = new uint[1];
                uint[] numArray4 = new uint[] { num + 0xccc };
                numArray3[0] = (baseAddress + Memory.Read<uint>(numArray4)) + 4;
                baseAddress = Memory.Read<uint>(numArray3);
                Thread.Sleep(1);
            }
            _allFrames = dictionary;
        }

        internal static void StartUpdate()
        {
            if (_updateThread != null)
            {
                _updateThread.Abort();
                _updateThread = null;
            }
            Thread thread = new Thread(new ThreadStart(InterfaceHelper.UpdateThread)) {
                IsBackground = true,
                Name = "InterfaceUpdater"
            };
            _updateThread = thread;
            _updateThread.Start();
        }

        internal static void StopUpdate()
        {
            if ((_updateThread != null) && _updateThread.IsAlive)
            {
                _updateThread.Abort();
                _updateThread = null;
            }
        }

        internal static void UpdateThread()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        ReloadFrames();
                        Thread.Sleep(0x1f40);
                        break;
                    }
                }
                catch (ThreadAbortException)
                {
                    break;
                }
                catch (Exception)
                {
                    break;
                }
            }
        }

        public static int WindowWidth
        {
            get
            {
                Rectangle rectangle;
                GetClientRect(Memory.WindowHandle, out rectangle);
                return Math.Abs((int) (rectangle.Right - rectangle.Left));
            }
        }

        public static int WindowHeight
        {
            get
            {
                Rectangle rectangle;
                GetClientRect(Memory.WindowHandle, out rectangle);
                return Math.Abs((int) (rectangle.Bottom - rectangle.Top));
            }
        }

        public static List<Frame> GetFrames =>
            _allFrames.Values.ToList<Frame>();

        public static Frame GetMouseFocus
        {
            get
            {
                uint[] addresses = new uint[] { Memory.ReadRelative<uint>(new uint[] { 0x7499a8 }) + 120 };
                return new Frame(Memory.Read<uint>(addresses));
            }
        }
    }
}

