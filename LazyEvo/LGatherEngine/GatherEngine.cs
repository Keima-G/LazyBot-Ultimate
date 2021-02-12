namespace LazyEvo.LGatherEngine
{
    using LazyEvo.LGatherEngine.Activity;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyEvo.LGatherEngine.Radar;
    using LazyEvo.LGatherEngine.States;
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

    internal class GatherEngine : ILazyEngine
    {
        internal static string OurDirectory;
        internal static GatherProfile CurrentProfile;
        internal static FlyingNavigation Navigation;
        internal static FlyingNavigator Navigator;
        internal static Mode CurrentMode;
        private static Form _settings;
        private static Form _profile;
        private static DateTime _startTime;
        private static int _harvest;
        private static int _kills;
        private static int _death;
        private static int _disconnects;
        private bool _naviRunning;

        public void Close()
        {
            GatherSettings.SaveSettings();
            Navigator.Stop();
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

        public bool EngineStart()
        {
            FindNode.LoadHarvest();
            GatherSettings.LoadSettings();
            KeyHelper.AddKey("FMount", "None", GatherSettings.FlyingMountBar, GatherSettings.FlyingMountKey);
            KeyHelper.AddKey("Lure", "None", GatherSettings.LureBar, GatherSettings.LureKey);
            KeyHelper.AddKey("Waterwalk", "None", GatherSettings.WaterwalkBar, GatherSettings.WaterwalkKey);
            KeyHelper.AddKey("CombatStart", "None", GatherSettings.ExtraBar, GatherSettings.ExtraKey);
            if (!LazyLib.Wow.ObjectManager.InGame)
            {
                Logging.Write(LogType.Info, "Enter game before starting the bot", new object[0]);
                return false;
            }
            if (LazyLib.Wow.ObjectManager.MyPlayer.IsGhost)
            {
                Logging.Write(LogType.Info, "Please ress before starting the bot", new object[0]);
                return false;
            }
            if (CurrentProfile == null)
            {
                Logging.Write(LogType.Info, "Please load a profile", new object[0]);
                return false;
            }
            if (CurrentProfile.WaypointsNormal.Count < 2)
            {
                Logging.Write(LogType.Info, "Profile should have more than 2 waypoints", new object[0]);
                return false;
            }
            Navigation = new FlyingNavigation(CurrentProfile.WaypointsNormal, true, FlyingWaypointsType.Normal);
            Navigator = new FlyingNavigator();
            ToTown.SetToTown(false);
            switch (CurrentMode)
            {
                case Mode.Normal:
                {
                    List<MainState> list = new List<MainState> {
                        new StateMount(),
                        new StateMoving(),
                        new StateGather(),
                        new StateCombat(),
                        new StateRess(),
                        new StateResting(),
                        new StateMailbox(),
                        new StateToTown(),
                        new StateVendor(),
                        new StateFullBags()
                    };
                    FlyingStates = list;
                    break;
                }
                case Mode.TestNormal:
                {
                    Logging.Write(LogType.Warning, "Starting gathering engine in TestNormal mode, next start will be in normal mode", new object[0]);
                    List<MainState> list2 = new List<MainState> {
                        new StateMount(),
                        new StateMoving(),
                        new StateFullBags()
                    };
                    FlyingStates = list2;
                    break;
                }
                case Mode.TestToTown:
                {
                    Logging.Write(LogType.Warning, "Starting gathering engine in TestToTown mode, next start will be in normal mode", new object[0]);
                    List<MainState> list3 = new List<MainState> {
                        new StateMount(),
                        new StateMoving(),
                        new StateCombat(),
                        new StateMailbox(),
                        new StateToTown(),
                        new StateVendor(),
                        new StateFullBags()
                    };
                    FlyingStates = list3;
                    ToTown.SetToTown(true);
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Stuck.Run();
            GatherBlackList.Load();
            CloseWindows();
            CurrentMode = Mode.Normal;
            _harvest = 0;
            _kills = 0;
            _death = 0;
            _disconnects = 0;
            _startTime = DateTime.Now;
            UpdateStats(0, 0, 0);
            return true;
        }

        public void EngineStop()
        {
            Stuck.Stop();
            Navigator.Stop();
        }

        public List<IMouseClick> GetRadarClick() => 
            new List<IMouseClick> { new MouseHandler() };

        public List<IDrawItem> GetRadarDraw() => 
            new List<IDrawItem> { 
                new DrawNodes(),
                new DrawWaypoints()
            };

        public void Load()
        {
            OurDirectory = new FileInfo(Application.ExecutablePath).DirectoryName;
            GatherSettings.LoadSettings();
            CurrentMode = Mode.Normal;
            if (!string.IsNullOrEmpty(GatherSettings.Profile) && File.Exists(GatherSettings.Profile))
            {
                CurrentProfile = new GatherProfile();
                CurrentProfile.LoadFile(GatherSettings.Profile);
            }
            else
            {
                CurrentProfile = null;
                Logging.Write("Could not load a valid flying profile", new object[0]);
            }
        }

        public bool LoadProfile(string path) => 
            CurrentProfile.LoadFile(path);

        public void Pause()
        {
            if (Navigator.IsRunning)
            {
                this._naviRunning = true;
                Navigator.Stop();
            }
        }

        public void Resume()
        {
            if (this._naviRunning)
            {
                Navigator.Start();
                this._naviRunning = false;
            }
        }

        public void UpdateState(string text)
        {
            LazyForm.UpdateStatsText(text);
        }

        public static void UpdateStats(int harvest, int kills, int death)
        {
            _harvest += harvest;
            _kills += kills;
            _death += death;
            TimeSpan span = (TimeSpan) (DateTime.Now - _startTime);
            string str = string.Empty;
            if (span.Milliseconds != 0.0)
            {
                double totalSeconds = span.TotalSeconds;
                str = Math.Round((double) ((((double) _harvest) / totalSeconds) * 3600.0), 2).ToString();
            }
            LazyForm.Deaths = _death;
            LazyForm.Kills = _kills;
            LazyForm.Harvests = _harvest;
            LazyForm.LPH = str;
            LazyForm.UpdateStatsText($"Loots: {_harvest} - Kills: {_kills} - Deaths: {_death} - Harvests/Hour: {str}");
        }

        private static List<MainState> FlyingStates { get; set; }

        public string Name =>
            "Gathering Engine";

        public List<MainState> States =>
            FlyingStates;

        public Form Settings
        {
            get
            {
                _settings = new LazyEvo.LGatherEngine.Settings();
                return _settings;
            }
        }

        public Form ProfileForm
        {
            get
            {
                _profile = new GatherProfileForm();
                return _profile;
            }
        }
    }
}

