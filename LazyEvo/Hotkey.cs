namespace LazyEvo
{
    using System;
    using System.ComponentModel;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml.Serialization;

    internal class Hotkey : IMessageFilter
    {
        private const uint WM_HOTKEY = 0x312;
        private const uint MOD_ALT = 1;
        private const uint MOD_CONTROL = 2;
        private const uint MOD_SHIFT = 4;
        private const uint MOD_WIN = 8;
        private const uint ERROR_HOTKEY_ALREADY_REGISTERED = 0x581;
        private const int maximumID = 0xbfff;
        private static int currentID;
        private bool alt;
        private bool control;
        [XmlIgnore]
        private int id;
        private Keys keyCode;
        [XmlIgnore]
        private bool registered;
        private bool shift;
        [XmlIgnore]
        private System.Windows.Forms.Control windowControl;
        private bool windows;
        private HandledEventHandler Pressed;

        public event HandledEventHandler Pressed
        {
            add
            {
                HandledEventHandler pressed = this.Pressed;
                while (true)
                {
                    HandledEventHandler comparand = pressed;
                    HandledEventHandler handler3 = comparand + value;
                    pressed = Interlocked.CompareExchange<HandledEventHandler>(ref this.Pressed, handler3, comparand);
                    if (ReferenceEquals(pressed, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                HandledEventHandler pressed = this.Pressed;
                while (true)
                {
                    HandledEventHandler comparand = pressed;
                    HandledEventHandler handler3 = comparand - value;
                    pressed = Interlocked.CompareExchange<HandledEventHandler>(ref this.Pressed, handler3, comparand);
                    if (ReferenceEquals(pressed, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public Hotkey() : this(Keys.None, false, false, false, false)
        {
        }

        public Hotkey(Keys keyCode, bool shift, bool control, bool alt, bool windows)
        {
            this.KeyCode = keyCode;
            this.Shift = shift;
            this.Control = control;
            this.Alt = alt;
            this.Windows = windows;
            Application.AddMessageFilter(this);
        }

        public Hotkey Clone() => 
            new Hotkey(this.keyCode, this.shift, this.control, this.alt, this.windows);

        ~Hotkey()
        {
            if (this.Registered)
            {
                this.Unregister();
            }
        }

        public bool GetCanRegister(System.Windows.Forms.Control windowControl)
        {
            bool flag;
            try
            {
                if (!this.Register(windowControl))
                {
                    flag = false;
                }
                else
                {
                    this.Unregister();
                    flag = true;
                }
            }
            catch (Win32Exception)
            {
                flag = false;
            }
            catch (NotSupportedException)
            {
                flag = false;
            }
            return flag;
        }

        private bool OnPressed()
        {
            HandledEventArgs e = new HandledEventArgs(false);
            if (this.Pressed != null)
            {
                this.Pressed(this, e);
            }
            return e.Handled;
        }

        public bool PreFilterMessage(ref Message message) => 
            (message.Msg == 0x312L) ? (this.registered && ((message.WParam.ToInt32() == this.id) && this.OnPressed())) : false;

        public bool Register(System.Windows.Forms.Control windowControl)
        {
            if (this.registered)
            {
                throw new NotSupportedException("You cannot register a hotkey that is already registered");
            }
            if (this.Empty)
            {
                throw new NotSupportedException("You cannot register an empty hotkey");
            }
            this.id = currentID;
            currentID++;
            uint fsModifiers = (uint) ((((this.Alt ? 1 : 0) | (this.Control ? 2 : 0)) | (this.Shift ? 4 : 0)) | (this.Windows ? 8 : 0));
            if (RegisterHotKey(windowControl.Handle, this.id, fsModifiers, this.keyCode) != 0)
            {
                this.registered = true;
                this.windowControl = windowControl;
                return true;
            }
            if (Marshal.GetLastWin32Error() != 0x581L)
            {
                throw new Win32Exception();
            }
            return false;
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, Keys vk);
        private void Reregister()
        {
            if (this.registered)
            {
                System.Windows.Forms.Control windowControl = this.windowControl;
                this.Unregister();
                this.Register(windowControl);
            }
        }

        public override string ToString()
        {
            if (this.Empty)
            {
                return "(none)";
            }
            string name = Enum.GetName(typeof(Keys), this.keyCode);
            switch (this.keyCode)
            {
                case Keys.D0:
                case Keys.D1:
                case Keys.D2:
                case Keys.D3:
                case Keys.D4:
                case Keys.D5:
                case Keys.D6:
                case Keys.D7:
                case Keys.D8:
                case Keys.D9:
                    name = name.Substring(1);
                    break;

                default:
                    break;
            }
            string str2 = "";
            if (this.shift)
            {
                str2 = str2 + "Shift+";
            }
            if (this.control)
            {
                str2 = str2 + "Control+";
            }
            if (this.alt)
            {
                str2 = str2 + "Alt+";
            }
            if (this.windows)
            {
                str2 = str2 + "Windows+";
            }
            return (str2 + name);
        }

        public void Unregister()
        {
            try
            {
                if (!this.registered)
                {
                    throw new NotSupportedException("You cannot unregister a hotkey that is not registered");
                }
                if (!this.windowControl.IsDisposed && (UnregisterHotKey(this.windowControl.Handle, this.id) == 0))
                {
                    throw new Win32Exception();
                }
                this.registered = false;
                this.windowControl = null;
            }
            catch
            {
            }
        }

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int UnregisterHotKey(IntPtr hWnd, int id);

        public bool Empty =>
            this.keyCode == Keys.None;

        public bool Registered =>
            this.registered;

        public Keys KeyCode
        {
            get => 
                this.keyCode;
            set
            {
                this.keyCode = value;
                this.Reregister();
            }
        }

        public bool Shift
        {
            get => 
                this.shift;
            set
            {
                this.shift = value;
                this.Reregister();
            }
        }

        public bool Control
        {
            get => 
                this.control;
            set
            {
                this.control = value;
                this.Reregister();
            }
        }

        public bool Alt
        {
            get => 
                this.alt;
            set
            {
                this.alt = value;
                this.Reregister();
            }
        }

        public bool Windows
        {
            get => 
                this.windows;
            set
            {
                this.windows = value;
                this.Reregister();
            }
        }
    }
}

