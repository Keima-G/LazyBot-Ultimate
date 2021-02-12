using System.Linq;

namespace LazyLib.Wow
{
    using LazyEvo.LGrindEngine;
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    [Serializable, Obfuscation(Feature="renaming", ApplyToMembers=true, Exclude=true)]
    public class Location
    {
        public Location(string loc)
        {
            try
            {
                string[] strArray = loc.Split(new char[] { ' ' });
                this.X = (float) Convert.ToDouble(strArray[0]);
                this.Y = (float) Convert.ToDouble(strArray[1]);
                this.Z = (float) Convert.ToDouble(strArray[2]);
            }
            catch (Exception)
            {
            }
        }

        public Location(float x, float y, float z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public float Angle(Location from) => 
            LazyMath.CalculateFace(from, this);

        public double DistanceFrom(Location pos) => 
            ((pos.Z == 0f) || (this.Z == 0f)) ? Math.Sqrt(Math.Pow((double) (this.X - pos.X), 2.0) + Math.Pow((double) (this.Y - pos.Y), 2.0)) : Math.Sqrt((Math.Pow((double) (this.X - pos.X), 2.0) + Math.Pow((double) (this.Y - pos.Y), 2.0)) + Math.Pow((double) (this.Z - pos.Z), 2.0));

        public double DistanceFrom(float nX, float nY, float nZ) => 
            ((nZ == 0f) || (this.Z == 0f)) ? Math.Sqrt(Math.Pow((double) (this.X - nX), 2.0) + Math.Pow((double) (this.Y - nY), 2.0)) : Math.Sqrt((Math.Pow((double) (this.X - nX), 2.0) + Math.Pow((double) (this.Y - nY), 2.0)) + Math.Pow((double) (this.Z - nZ), 2.0));

        public double DistanceFromXY(Location pos) => 
            Math.Sqrt(Math.Pow((double) (this.X - pos.X), 2.0) + Math.Pow((double) (this.Y - pos.Y), 2.0));

        public bool Equals(Location other) => 
            !ReferenceEquals(null, other) ? (!ReferenceEquals(this, other) ? (other.X.Equals(this.X) && (other.Y.Equals(this.Y) && other.Z.Equals(this.Z))) : true) : false;

        public override bool Equals(object obj) => 
            !ReferenceEquals(null, obj) ? (!ReferenceEquals(this, obj) ? (ReferenceEquals(obj.GetType(), typeof(Location)) ? this.Equals((Location) obj) : false) : true) : false;

        public void Face()
        {
            float num;
            if (LazyMath.NegativeAngle(this.AngleHorizontal - LazyLib.Wow.ObjectManager.MyPlayer.Facing) < 3.1415926535897931)
            {
                num = LazyMath.NegativeAngle(this.AngleHorizontal - LazyLib.Wow.ObjectManager.MyPlayer.Facing);
                bool isMoving = LazyLib.Wow.ObjectManager.MyPlayer.IsMoving;
                if (num > 1f)
                {
                    MoveHelper.ReleaseKeys();
                    isMoving = false;
                }
                FaceHorizontalWithTimer(num, "Left", isMoving);
            }
            else
            {
                num = LazyMath.NegativeAngle(LazyLib.Wow.ObjectManager.MyPlayer.Facing - this.AngleHorizontal);
                bool isMoving = LazyLib.Wow.ObjectManager.MyPlayer.IsMoving;
                if (num > 1f)
                {
                    MoveHelper.ReleaseKeys();
                    isMoving = false;
                }
                FaceHorizontalWithTimer(num, "Right", isMoving);
            }
        }

        public static void FaceAngle(float angle)
        {
            float num;
            if (LazyMath.NegativeAngle(angle - LazyLib.Wow.ObjectManager.MyPlayer.Facing) < 3.1415926535897931)
            {
                num = LazyMath.NegativeAngle(angle - LazyLib.Wow.ObjectManager.MyPlayer.Facing);
                bool isMoving = LazyLib.Wow.ObjectManager.MyPlayer.IsMoving;
                if (num > 1f)
                {
                    MoveHelper.ReleaseKeys();
                    isMoving = false;
                }
                FaceHorizontalWithTimer(num, "Left", isMoving);
            }
            else
            {
                num = LazyMath.NegativeAngle(LazyLib.Wow.ObjectManager.MyPlayer.Facing - angle);
                bool isMoving = LazyLib.Wow.ObjectManager.MyPlayer.IsMoving;
                if (num > 1f)
                {
                    MoveHelper.ReleaseKeys();
                    isMoving = false;
                }
                FaceHorizontalWithTimer(num, "Right", isMoving);
            }
        }

        private static void FaceHorizontalWithTimer(float radius, string key, bool moving)
        {
            if (radius >= 0.1f)
            {
                int num = moving ? 0x530 : 980;
                KeyHelper.PressKey(key);
                Thread.Sleep((int) (((radius * num) * 3.1415926535897931) / 10.0));
                KeyHelper.ReleaseKey(key);
            }
        }

        public static int GetClosestPositionInList(List<Location> waypoints, Location pos)
        {
            int num = 0;
            double num2 = -1.0;
            int num3 = 0;
            foreach (double num4 in from p in waypoints select LazyMath.Distance3D(pos.X, pos.Y, pos.Z, p.X, p.Y, p.Z))
            {
                if ((num2 == -1.0) || (num4 < num2))
                {
                    num = num3;
                    num2 = num4;
                }
                num3++;
            }
            return num;
        }

        public double GetDistanceTo(Location location) => 
            ((location.Z == 0f) || (this.Z == 0f)) ? Math.Sqrt(Math.Pow((double) (this.X - location.X), 2.0) + Math.Pow((double) (this.Y - location.Y), 2.0)) : Math.Sqrt((Math.Pow((double) (this.X - location.X), 2.0) + Math.Pow((double) (this.Y - location.Y), 2.0)) + Math.Pow((double) (this.Z - location.Z), 2.0));

        public double GetDistanceTo(float nX, float nY, float nZ) => 
            ((nZ == 0f) || (this.Z == 0f)) ? Math.Sqrt(Math.Pow((double) (this.X - nX), 2.0) + Math.Pow((double) (this.Y - nY), 2.0)) : Math.Sqrt((Math.Pow((double) (this.X - nX), 2.0) + Math.Pow((double) (this.Y - nY), 2.0)) + Math.Pow((double) (this.Z - nZ), 2.0));

        public override int GetHashCode() => 
            (((this.X.GetHashCode() * 0x18d) ^ this.Y.GetHashCode()) * 0x18d) ^ this.Z.GetHashCode();

        public bool IsFacing() => 
            this.IsFacing(0.1f);

        public bool IsFacing(float errorMarge) => 
            LazyMath.IsFacingH(this.AngleHorizontal, errorMarge);

        private float NegativeAngle(float angle)
        {
            if (angle < 0f)
            {
                angle += 6.283185f;
            }
            return angle;
        }

        public override string ToString() => 
            $"X, Y, Z = [{this.X}, {this.Y}, {this.Z}]";

        public int N { get; private set; }

        public float X { get; private set; }

        public float Y { get; private set; }

        public float Z { get; private set; }

        public float Bearing =>
            this.NegativeAngle((float) Math.Atan2((double) (this.Y - LazyLib.Wow.ObjectManager.MyPlayer.Y), (double) (this.X - LazyLib.Wow.ObjectManager.MyPlayer.X)));

        public bool IsFacingAway =>
            (LazyMath.NegativeAngle(this.Bearing - LazyLib.Wow.ObjectManager.MyPlayer.Facing) > 5.5) || (LazyMath.NegativeAngle(this.Bearing - LazyLib.Wow.ObjectManager.MyPlayer.Facing) < 0.6);

        public LazyEvo.LGrindEngine.NodeType NodeType { get; set; }

        public double DistanceToSelf =>
            LazyMath.Distance3D(this);

        public float AngleHorizontal =>
            LazyMath.CalculateFace(this);

        public double DistanceToSelf2D =>
            LazyMath.Distance2D(this);
    }
}

