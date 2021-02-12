namespace LazyEvo.Forms.Helpers
{
    using LazyEvo.LGatherEngine;
    using LazyLib;
    using LazyLib.FSM;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Media;
    using System.Threading;

    internal class Followers
    {
        private const int Distance = 40;
        private static readonly Dictionary<ulong, int> FollowersList = new Dictionary<ulong, int>();
        private static readonly Ticker Timer = new Ticker(5000.0);
        private static readonly System.Media.SoundPlayer SoundPlayer = new System.Media.SoundPlayer();

        internal static void CheckFollow()
        {
            try
            {
                if (Engine.Running && Timer.IsReady)
                {
                    Timer.Reset();
                    foreach (PPlayer player in from player in LazyLib.Wow.ObjectManager.GetPlayers
                        where (player.GUID != LazyLib.Wow.ObjectManager.MyPlayer.GUID) && (player.Name != LazyLib.Wow.ObjectManager.MyPlayer.Name)
                        select player)
                    {
                        if ((player.GUID != LazyLib.Wow.ObjectManager.MyPlayer.GUID) && (player.Name != LazyLib.Wow.ObjectManager.MyPlayer.Name))
                        {
                            if ((player.Location.DistanceToSelf < 40.0) && !FollowersList.ContainsKey(player.GUID))
                            {
                                Logging.Write("New player around: " + player.Name, new object[0]);
                                if ((player.Name == "Killadro") || (player.Name == "Sturm"))
                                {
                                    GatherEngine.Navigator.Stop();
                                    ChatQueu.AddChat("/Y !T_A_C_O_S!");
                                    GatherEngine.Navigator.Start();
                                }
                                FollowersList.Add(player.GUID, Environment.TickCount);
                                continue;
                            }
                            if ((player.Location.DistanceToSelf >= 40.0) && FollowersList.ContainsKey(player.GUID))
                            {
                                Logging.Write("Removed player: " + player.Name, new object[0]);
                                FollowersList.Remove(player.GUID);
                                continue;
                            }
                            if ((player.Location.DistanceToSelf < 40.0) && (FollowersList.ContainsKey(player.GUID) && ((FollowersList[player.GUID] + ((Convert.ToInt32(LazySettings.LogOutOnFollowTime) * 60) * 0x3e8)) < Environment.TickCount)))
                            {
                                FollowersList[player.GUID] = Environment.TickCount;
                                Logging.Write(LogType.Warning, string.Format(player.Name + " has been following me for {0} minutes !", LazySettings.LogOutOnFollowTime), new object[0]);
                                if (LazySettings.SoundFollow)
                                {
                                    PlayerSound();
                                }
                                if (LazySettings.LogoutOnFollow)
                                {
                                    LazyForms.MainForm.StopBotting(true);
                                    Thread.Sleep(0xbb8);
                                    KeyHelper.ChatboxSendText("/logout");
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private static void PlayerSound()
        {
            if (File.Exists(LazySettings.OurDirectory + @"\palert.wav"))
            {
                SoundPlayer.SoundLocation = LazySettings.OurDirectory + @"\palert.wav";
                SoundPlayer.Play();
            }
        }
    }
}

