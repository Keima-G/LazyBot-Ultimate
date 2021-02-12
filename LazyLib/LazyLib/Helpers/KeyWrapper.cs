namespace LazyLib.Helpers
{
    using LazyLib;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class KeyWrapper
    {
        private const uint PressKeyCode = 0x100;
        private const uint ReleaseKeyCode = 0x101;
        private readonly MicrosoftVirtualKeys _bar;
        private readonly bool _shift;
        private readonly MicrosoftVirtualKeys _wParam;
        private readonly MicrosoftVirtualKeys _wParam2;

        public KeyWrapper(string keyName, string shiftState, string barState, string character)
        {
            this.Name = keyName;
            this.Key = character;
            this.Bar = barState;
            this.Special = shiftState;
            if (shiftState.Equals("Ctrl"))
            {
                this._shift = true;
                this._wParam = MicrosoftVirtualKeys.VK_LCONTROL;
            }
            else if (shiftState.Equals("Shift"))
            {
                this._shift = true;
                this._wParam = MicrosoftVirtualKeys.VK_SHIFT;
            }
            else if (!shiftState.Equals("Alt"))
            {
                this._shift = false;
            }
            else
            {
                this._shift = true;
                this._wParam = MicrosoftVirtualKeys.Alt;
            }
            this._bar = (barState.Equals("1") || barState.Equals("Bar1")) ? MicrosoftVirtualKeys.key1 : ((barState.Equals("2") || barState.Equals("Bar2")) ? MicrosoftVirtualKeys.key2 : ((barState.Equals("3") || barState.Equals("Bar3")) ? MicrosoftVirtualKeys.key3 : ((barState.Equals("4") || barState.Equals("Bar4")) ? MicrosoftVirtualKeys.key4 : ((barState.Equals("5") || barState.Equals("Bar5")) ? MicrosoftVirtualKeys.key5 : ((barState.Equals("6") || barState.Equals("Bar6")) ? MicrosoftVirtualKeys.key6 : MicrosoftVirtualKeys.Indifferent)))));
            if (character.Equals("0") || character.Equals("10"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key0;
            }
            else if (character.Equals("1"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key1;
            }
            else if (character.Equals("2"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key2;
            }
            else if (character.Equals("3"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key3;
            }
            else if (character.Equals("4"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key4;
            }
            else if (character.Equals("5"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key5;
            }
            else if (character.Equals("6"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key6;
            }
            else if (character.Equals("7"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key7;
            }
            else if (character.Equals("8"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key8;
            }
            else if (character.Equals("9"))
            {
                this._wParam2 = MicrosoftVirtualKeys.key9;
            }
            else if (character.Equals("-"))
            {
                this._wParam2 = MicrosoftVirtualKeys.OEMMinus;
            }
            else if (character.Equals("="))
            {
                this._wParam2 = MicrosoftVirtualKeys.OEMPlus;
            }
            else if (character != "")
            {
                try
                {
                    this._wParam2 = (MicrosoftVirtualKeys) Enum.Parse(typeof(MicrosoftVirtualKeys), character, true);
                }
                catch (Exception)
                {
                    Logging.Write(LogType.Warning, "[KeyWrapper] Unsupported key: " + character + " : " + keyName, new object[0]);
                }
            }
            if (!Enum.IsDefined(typeof(MicrosoftVirtualKeys), this._wParam2))
            {
                Logging.Write(LogType.Warning, string.Concat(new object[] { "[KeyWrapper] Unsupported key: ", this._wParam2, " : ", keyName }), new object[0]);
            }
        }

        private void ChangeBar()
        {
            if (this._bar != MicrosoftVirtualKeys.Indifferent)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) 0x10, IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._bar), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) 0x10, IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._bar), IntPtr.Zero);
                Thread.Sleep(350);
            }
        }

        public void PressKey()
        {
            this.ChangeBar();
            if (!this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
            if (this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
        }

        public void ReleaseKey()
        {
            this.ChangeBar();
            if (!this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
            if (this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
        }

        public void SendKey()
        {
            Logging.Debug("SendKey: " + this.Name + " Bar: " + this.Bar + " Key: " + this.Key, new object[0]);
            this.ChangeBar();
            if (!this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
            if (this._shift)
            {
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x100, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam), IntPtr.Zero);
                KeyLowHelper.PostMessage(Memory.WindowHandle, 0x101, (IntPtr) ((long) this._wParam2), IntPtr.Zero);
            }
        }

        public string Key { get; private set; }

        public string Special { get; private set; }

        public string Name { get; private set; }

        public string Bar { get; private set; }
    }
}

