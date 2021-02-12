namespace LazyEvo.LGrindEngine
{
    using LazyEvo.LGrindEngine.Activity;
    using LazyEvo.LGrindEngine.Helpers;
    using LazyEvo.LGrindEngine.Radar;
    using LazyEvo.LGrindEngine.States;
    using LazyEvo.Public;
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.IEngine;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    internal class GrindingEngine : ILazyEngine
    {
        internal static string OurDirectory;
        internal static PathProfile CurrentProfile;
        internal static GrindingNavigation Navigation;
        internal static GrindingNavigator Navigator;
        internal static Mode CurrentMode;
        private static Form _settings;
        private static Form _profile;
        private static int loots;
        private static int _kills;
        private static int _death;
        private static int _disconnects;
        private static DateTime _startTime;
        private static int _xpCurrent;
        private static int _xpInitial;
        internal static bool ShouldTrain;

        public void Close()
        {
            Navigator.Stop();
            GrindingSettings.SaveSettings();
            CloseWindows();
        }

        private static void CloseWindows()
        {
            if ((_profile != null) && !_profile.IsDisposed)
            {
                _profile.Close();
            }
            if ((_settings != null) && !_settings.IsDisposed)
            {
                _settings.Close();
            }
        }

        private void CombatChanged(object o, GCombatEventArgs eventArgs)
        {
            if (eventArgs.CombatType.Equals(CombatType.CombatStarted))
            {
                Navigator.Stop();
            }
            if (eventArgs.CombatType.Equals(CombatType.CombatDone) && !LazyLib.Wow.ObjectManager.MyPlayer.IsDead)
            {
                LootAndSkin.DoLootAfterCombat(eventArgs.Unit);
            }
        }

        public bool EngineStart()
        {
            GrindingSettings.LoadSettings();
            if (!LazyLib.Wow.ObjectManager.InGame)
            {
                Logging.Write(LogType.Info, "Enter game before starting the bot", new object[0]);
                return false;
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsDead && LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                Logging.Write(LogType.Info, "Please ress before starting the bot", new object[0]);
                return false;
            }
            if (CurrentProfile == null)
            {
                Logging.Write(LogType.Info, "Please load a profile", new object[0]);
                return false;
            }
            Navigator = new GrindingNavigator();
            Navigation = new GrindingNavigation(CurrentProfile);
            GrindingSettings.LoadSettings();
            ToTown.SetToTown(false);
            if (CurrentMode == Mode.TestToTown)
            {
                Logging.Write(LogType.Warning, "Starting Grinding engine in TestToTown mode, next start will be in normal mode THIS IS TODO", new object[0]);
            }
            else
            {
                List<MainState> list = new List<MainState> {
                    new StatePull(),
                    new StateLoot(),
                    new StateMoving(),
                    new StateTrainer(),
                    new StateResting(),
                    new StateResurrect(),
                    new StateCombat(),
                    new StateToTown(),
                    new StateVendor()
                };
                _states = list;
            }
            Stuck.Run();
            CurrentMode = Mode.Normal;
            CombatHandler.CombatStatusChanged += new EventHandler<GCombatEventArgs>(this.CombatChanged);
            CloseWindows();
            loots = 0;
            _kills = 0;
            _death = 0;
            _xpInitial = LazyLib.Wow.ObjectManager.MyPlayer.Experience;
            _startTime = DateTime.Now;
            if (GrindingSettings.ShouldTrain)
            {
                ShouldTrain = GrindingShouldTrain.ShouldTrain();
            }
            PullController.Start();
            return true;
        }

        public void EngineStop()
        {
            PullController.Stop();
            Stuck.Stop();
            Navigator.Stop();
            Navigation = null;
            Navigator = null;
            _states = null;
            CombatHandler.CombatStatusChanged -= new EventHandler<GCombatEventArgs>(this.CombatChanged);
            GC.Collect();
        }

        public List<IMouseClick> GetRadarClick() => 
            new List<IMouseClick>();

        public List<IDrawItem> GetRadarDraw() => 
            new List<IDrawItem> { new DrawWaypoints() };

        public void Load()
        {
            GrindingSettings.LoadSettings();
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            if (!string.IsNullOrEmpty(GrindingSettings.Profile) && File.Exists(GrindingSettings.Profile))
            {
                CurrentProfile = new PathProfile();
                CurrentProfile.LoadNoDialog(GrindingSettings.Profile);
            }
            else
            {
                CurrentProfile = null;
                Logging.Write(LogType.Error, "Could not load a valid grinding profile", new object[0]);
            }
        }

        public bool LoadProfile(string path)
        {
            CurrentProfile.LoadNoDialog(path);
            return true;
        }

        public void Pause()
        {
            Navigator.Stop();
        }

        public void Resume()
        {
            Navigator.Start();
        }

        public static void UpdateStats(int loot, int kills, int death)
        {
            loots += loot;
            _kills += kills;
            _death += death;
            TimeSpan span = (TimeSpan) (DateTime.Now - _startTime);
            string str = string.Empty;
            double num = 0.0;
            _xpCurrent = LazyLib.Wow.ObjectManager.MyPlayer.Experience;
            if (_xpCurrent < _xpInitial)
            {
                Logging.Write("Ding!", new object[0]);
                _xpInitial = LazyLib.Wow.ObjectManager.MyPlayer.Experience;
                _xpCurrent = _xpInitial;
                _startTime = DateTime.Now;
                if (GrindingSettings.ShouldTrain)
                {
                    Navigator.Stop();
                    MoveHelper.ReleaseKeys();
                    ShouldTrain = false;
                    ShouldTrain = GrindingShouldTrain.ShouldTrain();
                }
            }
            else
            {
                double num2 = _xpCurrent - _xpInitial;
                if (span.Milliseconds != 0.0)
                {
                    try
                    {
                        double totalSeconds = span.TotalSeconds;
                        num = Math.Round((double) ((num2 / totalSeconds) * 3600.0), 0);
                        TimeSpan span2 = TimeSpan.FromSeconds(((LazyLib.Wow.ObjectManager.MyPlayer.NextLevel - LazyLib.Wow.ObjectManager.MyPlayer.Experience) * totalSeconds) / num2);
                        str = $"{span2.Hours:D2}h:{span2.Minutes:D2}m:{span2.Seconds:D2}s";
                    }
                    catch
                    {
                    }
                }
            }
            LazyForm.Loots = loots;
            LazyForm.Deaths = _death;
            LazyForm.Kills = _kills;
            LazyForm.TimeToLevel = str;
            LazyForm.UpdateStatsText($"Loots:{loots} -Kills:{_kills} -Deaths:{_death} -XP/H: {num}-TTL:{str} ");
        }

        private static List<MainState> _states { get; set; }

        public string Name =>
            "Grinding Engine";

        public List<MainState> States =>
            _states;

        public Form Settings
        {
            get
            {
                _settings = new LazyEvo.LGrindEngine.Settings();
                return _settings;
            }
        }

        public Form ProfileForm
        {
            get
            {
                CurrentProfile ??= new PathProfile();
                return new PathControl(CurrentProfile);
            }
        }
    }
}

