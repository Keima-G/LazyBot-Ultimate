namespace LazyEvo.PluginSystem
{
    using LazyEvo.Classes;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class PluginManager
    {
        private static Thread _pulseThread = new Thread(new ThreadStart(PluginManager.Pulse));
        private static bool _keepPulsing;

        public static void BotStart()
        {
            foreach (KeyValuePair<string, ILazyPlugin> pair in from lazyPlugin in PluginCompiler.Assemblys
                where PluginCompiler.LoadedPlugins.Contains(lazyPlugin.Key)
                select lazyPlugin)
            {
                pair.Value.BotStart();
            }
        }

        public static void BotStop()
        {
            foreach (KeyValuePair<string, ILazyPlugin> pair in from lazyPlugin in PluginCompiler.Assemblys
                where PluginCompiler.LoadedPlugins.Contains(lazyPlugin.Key)
                select lazyPlugin)
            {
                pair.Value.BotStop();
            }
        }

        private static void Pulse()
        {
            while (_keepPulsing)
            {
                foreach (KeyValuePair<string, ILazyPlugin> pair in from lazyPlugin in PluginCompiler.Assemblys
                    where PluginCompiler.LoadedPlugins.Contains(lazyPlugin.Key)
                    select lazyPlugin)
                {
                    pair.Value.Pulse();
                }
                Thread.Sleep(200);
            }
        }

        public static void StartPulseThread(bool restartIfRunning)
        {
            if (restartIfRunning)
            {
                TerminatePulseThread();
            }
            if (!_pulseThread.IsAlive)
            {
                _keepPulsing = true;
                _pulseThread = new Thread(new ThreadStart(PluginManager.Pulse));
                _pulseThread.IsBackground = true;
                _pulseThread.Start();
            }
        }

        public static void StopPulseThread()
        {
            try
            {
                _keepPulsing = false;
                if ((_pulseThread != null) && _pulseThread.IsAlive)
                {
                    _pulseThread.Join();
                    GC.Collect();
                }
            }
            catch (ThreadStateException)
            {
            }
        }

        public static void TerminatePulseThread()
        {
            try
            {
                _keepPulsing = false;
                if (_pulseThread != null)
                {
                    _pulseThread.Abort();
                    _pulseThread.Join();
                    GC.Collect();
                }
            }
            catch (ThreadStateException)
            {
            }
        }
    }
}

