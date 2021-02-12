namespace LazyLib.Dialogs.UnhandledExceptionDlg
{
    using System;
    using System.Diagnostics;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class UnhandledExceptionDlg
    {
        private bool _dorestart = true;
        private SendExceptionClickHandler OnSendExceptionClick;

        public event SendExceptionClickHandler OnSendExceptionClick
        {
            add
            {
                SendExceptionClickHandler onSendExceptionClick = this.OnSendExceptionClick;
                while (true)
                {
                    SendExceptionClickHandler comparand = onSendExceptionClick;
                    SendExceptionClickHandler handler3 = comparand + value;
                    onSendExceptionClick = Interlocked.CompareExchange<SendExceptionClickHandler>(ref this.OnSendExceptionClick, handler3, comparand);
                    if (ReferenceEquals(onSendExceptionClick, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                SendExceptionClickHandler onSendExceptionClick = this.OnSendExceptionClick;
                while (true)
                {
                    SendExceptionClickHandler comparand = onSendExceptionClick;
                    SendExceptionClickHandler handler3 = comparand - value;
                    onSendExceptionClick = Interlocked.CompareExchange<SendExceptionClickHandler>(ref this.OnSendExceptionClick, handler3, comparand);
                    if (ReferenceEquals(onSendExceptionClick, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public UnhandledExceptionDlg()
        {
            Application.ThreadException += new ThreadExceptionEventHandler(this.ThreadExceptionFunction);
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(this.UnhandledExceptionFunction);
        }

        private static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < size; i++)
            {
                char ch = Convert.ToChar(Convert.ToInt32(Math.Floor((double) ((26.0 * random.NextDouble()) + 65.0))));
                builder.Append(ch);
            }
            return (!lowerCase ? builder.ToString() : builder.ToString().ToLower());
        }

        private void ShowUnhandledExceptionDlg(Exception e)
        {
            EventHandler handler = null;
            Exception exceptionArg = e;
            exceptionArg = new Exception("Unknown unhandled Exception was occurred!");
            using (UnhandledExDlgForm exDlgForm = new UnhandledExDlgForm())
            {
                string processName = Process.GetCurrentProcess().ProcessName;
                exDlgForm.Text = RandomString(8, true);
                exDlgForm.labelTitle.Text = string.Format(exDlgForm.labelTitle.Text, processName);
                exDlgForm.checkBoxRestart.Text = string.Format(exDlgForm.checkBoxRestart.Text, processName);
                exDlgForm.checkBoxRestart.Checked = this.RestartApp;
                exDlgForm.buttonSend.Enabled = !ReferenceEquals(this.OnSendExceptionClick, null);
                if (handler == null)
                {
                    handler = (param0, param1) => this._dorestart = exDlgForm.checkBoxRestart.Checked;
                }
                exDlgForm.checkBoxRestart.CheckedChanged += handler;
                exDlgForm.textBox1.AppendText("Message: " + exceptionArg.Message + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Inner exception: " + exceptionArg.InnerException + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Source: " + exceptionArg.Source + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Stack trace: " + exceptionArg.StackTrace + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Target site: " + exceptionArg.TargetSite + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Data: " + exceptionArg.Data + Environment.NewLine);
                exDlgForm.textBox1.AppendText("Link: " + exceptionArg.HelpLink + Environment.NewLine);
                bool sendDetailsArg = exDlgForm.ShowDialog() == DialogResult.Yes;
                if (this.OnSendExceptionClick != null)
                {
                    SendExceptionClickEventArgs args = new SendExceptionClickEventArgs(sendDetailsArg, exceptionArg, this._dorestart);
                    this.OnSendExceptionClick(this, args);
                }
            }
            Environment.Exit(0);
        }

        private void ThreadExceptionFunction(object sender, ThreadExceptionEventArgs e)
        {
            this.ShowUnhandledExceptionDlg(e.Exception);
        }

        private void UnhandledExceptionFunction(object sender, UnhandledExceptionEventArgs args)
        {
            this.ShowUnhandledExceptionDlg((Exception) args.ExceptionObject);
        }

        public bool RestartApp
        {
            get => 
                this._dorestart;
            set => 
                this._dorestart = value;
        }

        public delegate void SendExceptionClickHandler(object sender, SendExceptionClickEventArgs args);
    }
}

