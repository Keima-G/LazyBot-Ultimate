namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class HelperFunctions
    {
        public static void Clamp(ref int inVal, int min, int max)
        {
            inVal = Math.Min(max, inVal);
            inVal = Math.Max(min, inVal);
        }

        public static void Move3D(Location targetCoord, int zMaxAvoidiance)
        {
            Location location = new Location(targetCoord.X, targetCoord.Y, targetCoord.Z);
            if (!targetCoord.IsFacing(0.3f))
            {
                location.Face();
            }
            if ((targetCoord.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z) > zMaxAvoidiance)
            {
                KeyHelper.ReleaseKey("X");
                KeyHelper.PressKey("Space");
            }
            else if ((targetCoord.Z - LazyLib.Wow.ObjectManager.MyPlayer.Location.Z) < -zMaxAvoidiance)
            {
                KeyHelper.ReleaseKey("Space");
                KeyHelper.PressKey("X");
            }
            else
            {
                KeyHelper.ReleaseKey("Space");
                KeyHelper.ReleaseKey("X");
            }
        }

        public static int Pause(int minMs, int maxMs) => 
            new Random().Next(minMs, maxMs);

        public static void ResetRedMessage()
        {
            Logging.Write("Resetting red message", new object[0]);
            KeyHelper.SendKey("F1");
            Thread.Sleep(100);
            KeyHelper.SendKey("Attack1");
        }
    }
}

