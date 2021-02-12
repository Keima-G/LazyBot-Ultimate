namespace LazyLib.Helpers
{
    using LazyLib.Wow;
    using System;

    internal class LazyMath
    {
        public static float CalculateFace(Location to) => 
            CalculateFace(to.X, to.Y, LazyLib.Wow.ObjectManager.MyPlayer.X, LazyLib.Wow.ObjectManager.MyPlayer.Y);

        public static float CalculateFace(Location from, Location to) => 
            CalculateFace(to.X, to.Y, from.X, from.Y);

        public static float CalculateFace(float x, float y) => 
            CalculateFace(x, y, LazyLib.Wow.ObjectManager.MyPlayer.X, LazyLib.Wow.ObjectManager.MyPlayer.Y);

        public static float CalculateFace(float toX, float toY, float fromX, float fromY) => 
            NegativeAngle(Convert.ToSingle(Math.Atan2(Convert.ToDouble(toY) - Convert.ToDouble(fromY), Convert.ToDouble(toX) - Convert.ToDouble(fromX))));

        public static double Distance2D(Location pos) => 
            Distance2D(pos.X, pos.Y, LazyLib.Wow.ObjectManager.MyPlayer.X, LazyLib.Wow.ObjectManager.MyPlayer.Y);

        public static double Distance2D(Location from, Location to) => 
            Distance2D(from.X, from.Y, to.X, to.Y);

        public static double Distance2D(float x, float y)
        {
            float num = x - LazyLib.Wow.ObjectManager.MyPlayer.X;
            float num2 = y - LazyLib.Wow.ObjectManager.MyPlayer.Y;
            return Math.Sqrt((double) ((num * num) + (num2 * num2)));
        }

        public static double Distance2D(float x1, float y1, float x2, float y2)
        {
            float num = x1 - x2;
            float num2 = y1 - y2;
            return Math.Sqrt((double) ((num * num) + (num2 * num2)));
        }

        public static double Distance3D(Location pos) => 
            Distance3D(pos.X, pos.Y, pos.Z, LazyLib.Wow.ObjectManager.MyPlayer.X, LazyLib.Wow.ObjectManager.MyPlayer.Y, LazyLib.Wow.ObjectManager.MyPlayer.Z);

        public static double Distance3D(Location from, Location to) => 
            Distance3D(from.X, from.Y, from.Z, to.X, to.Y, to.Z);

        public static double Distance3D(float x, float y, float z)
        {
            float num = x - LazyLib.Wow.ObjectManager.MyPlayer.X;
            float num2 = y - LazyLib.Wow.ObjectManager.MyPlayer.Y;
            float num3 = z - LazyLib.Wow.ObjectManager.MyPlayer.Z;
            return Math.Sqrt((double) (((num * num) + (num2 * num2)) + (num3 * num3)));
        }

        public static double Distance3D(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            float num = x1 - x2;
            float num2 = y1 - y2;
            float num3 = z1 - z2;
            return Math.Sqrt((double) (((num * num) + (num2 * num2)) + (num3 * num3)));
        }

        public static bool IsFacingH(float angle, float errorMarge) => 
            (NegativeAngle(angle - LazyLib.Wow.ObjectManager.MyPlayer.Facing) >= 3.1415926535897931) ? (NegativeAngle(LazyLib.Wow.ObjectManager.MyPlayer.Facing - angle) < errorMarge) : (NegativeAngle(angle - LazyLib.Wow.ObjectManager.MyPlayer.Facing) < errorMarge);

        public static float NegativeAngle(float angle)
        {
            if (angle < 0f)
            {
                angle += 6.283185f;
            }
            return angle;
        }

        public static float NegativeValue(float value)
        {
            if (value < 0f)
            {
                value *= -1f;
            }
            return value;
        }
    }
}

