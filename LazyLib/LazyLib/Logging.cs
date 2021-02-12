namespace LazyLib
{
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public static class Logging
    {
        private static readonly Thread QueueThread;
        private static readonly Queue<string> LogQueue = new Queue<string>();
        private static string _logSpam;
        private static WriteDelegate OnWrite;
        private static DebugDelegate OnDebug;

        public static event DebugDelegate OnDebug
        {
            add
            {
                DebugDelegate onDebug = OnDebug;
                while (true)
                {
                    DebugDelegate comparand = onDebug;
                    DebugDelegate delegate4 = comparand + value;
                    onDebug = Interlocked.CompareExchange<DebugDelegate>(ref OnDebug, delegate4, comparand);
                    if (ReferenceEquals(onDebug, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                DebugDelegate onDebug = OnDebug;
                while (true)
                {
                    DebugDelegate comparand = onDebug;
                    DebugDelegate delegate4 = comparand - value;
                    onDebug = Interlocked.CompareExchange<DebugDelegate>(ref OnDebug, delegate4, comparand);
                    if (ReferenceEquals(onDebug, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static event WriteDelegate OnWrite
        {
            add
            {
                WriteDelegate onWrite = OnWrite;
                while (true)
                {
                    WriteDelegate comparand = onWrite;
                    WriteDelegate delegate4 = comparand + value;
                    onWrite = Interlocked.CompareExchange<WriteDelegate>(ref OnWrite, delegate4, comparand);
                    if (ReferenceEquals(onWrite, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                WriteDelegate onWrite = OnWrite;
                while (true)
                {
                    WriteDelegate comparand = onWrite;
                    WriteDelegate delegate4 = comparand - value;
                    onWrite = Interlocked.CompareExchange<WriteDelegate>(ref OnWrite, delegate4, comparand);
                    if (ReferenceEquals(onWrite, comparand))
                    {
                        return;
                    }
                }
            }
        }

        static Logging()
        {
            LogOnWrite = true;
            Thread thread = new Thread(new ParameterizedThreadStart(Logging.WriteQueue)) {
                IsBackground = true
            };
            QueueThread = thread;
            QueueThread.Name = "Logging";
            QueueThread.Start(true);
        }

        public static void Debug(string format, params object[] args)
        {
            Debug(LogType.Warning, format, args);
        }

        public static void Debug(LogType color, string format, params object[] args)
        {
            string message = TimeStamp + string.Format(format, args);
            InvokeOnDebug(message, color);
            if (LogOnWrite)
            {
                Console.WriteLine(message);
                LogQueue.Enqueue(message);
            }
        }

        public static void ExtendedDebug(string format, params object[] args)
        {
            if (LazySettings.DebugMode)
            {
                Debug(format, args);
            }
        }

        private static void InvokeOnDebug(string message, LogType col)
        {
            DebugDelegate onDebug = OnDebug;
            if (onDebug != null)
            {
                onDebug(message, col);
            }
        }

        private static void InvokeOnWrite(string message, LogType col)
        {
            WriteDelegate onWrite = OnWrite;
            if (onWrite != null)
            {
                onWrite(message, col);
            }
        }

        public static void Write(string format, params object[] args)
        {
            Write(LogType.Normal, format, args);
        }

        public static void Write(LogType color, string format, params object[] args)
        {
            string message = TimeStamp + string.Format(format, args);
            if (message != _logSpam)
            {
                InvokeOnWrite(message, color);
                if (LogOnWrite)
                {
                    LogQueue.Enqueue(message);
                }
            }
            _logSpam = message;
        }

        private static void WriteQueue(object blocking)
        {
            if (!Directory.Exists($"{Utilities.ApplicationPath}\Logs"))
            {
                Directory.CreateDirectory($"{Utilities.ApplicationPath}\Logs");
            }
            try
            {
            TR_000B:
                while (true)
                {
                    using (TextWriter writer = new StreamWriter($"{Utilities.ApplicationPath}\Logs\LogFile.txt", true))
                    {
                        while (true)
                        {
                            if (LogQueue.Count != 0)
                            {
                                writer.WriteLine(LogQueue.Dequeue());
                                continue;
                            }
                            if (!((bool) blocking))
                            {
                                break;
                            }
                            goto TR_0004;
                        }
                    }
                    break;
                }
                return;
            TR_0004:
                Thread.Sleep(500);
                goto TR_000B;
            }
            catch
            {
            }
        }

        public static bool LogOnWrite { get; set; }

        private static string TimeStamp =>
            $"[{DateTime.Now.ToLongTimeString()}] ";

        public delegate void DebugDelegate(string message, LogType logType);

        public delegate void WriteDelegate(string message, LogType logType);
    }
}

