namespace LazyLib.Helpers
{
    using LazyLib.Wow;
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    internal class Camera
    {
        internal static uint BaseAddress
        {
            get
            {
                uint[] addresses = new uint[] { Memory.ReadRelative<uint>(new uint[] { 0x77436c }) + 0x7e20 };
                return Memory.Read<uint>(addresses);
            }
        }

        internal static float X
        {
            get
            {
                uint[] addresses = new uint[] { BaseAddress + 8 };
                return Memory.Read<float>(addresses);
            }
        }

        internal static float Y
        {
            get
            {
                uint[] addresses = new uint[] { BaseAddress + 12 };
                return Memory.Read<float>(addresses);
            }
        }

        internal static float Z
        {
            get
            {
                uint[] addresses = new uint[] { BaseAddress + 0x10 };
                return Memory.Read<float>(addresses);
            }
        }

        internal static LazyLib.Helpers.Matrix Matrix
        {
            get
            {
                byte[] buffer = Memory.ReadBytes(BaseAddress + ((uint) 20), 0x24);
                return new LazyLib.Helpers.Matrix(BitConverter.ToSingle(buffer, 0), BitConverter.ToSingle(buffer, 4), BitConverter.ToSingle(buffer, 8), BitConverter.ToSingle(buffer, 12), BitConverter.ToSingle(buffer, 0x10), BitConverter.ToSingle(buffer, 20), BitConverter.ToSingle(buffer, 0x18), BitConverter.ToSingle(buffer, 0x1c), BitConverter.ToSingle(buffer, 0x20));
            }
        }

        internal class World2Screen
        {
            private const float Deg2Rad = 0.01745329f;

            [DllImport("user32.dll")]
            private static extern bool GetClientRect(IntPtr hWnd, ref Rect rect);
            internal static Point WorldToScreen(Location position, bool realPos) => 
                WorldToScreen(position.X, position.Y, position.Z, realPos);

            internal static unsafe Point WorldToScreen(float x, float y, float z, bool realPos)
            {
                Point point = new Point {
                    X = 0,
                    Y = 0
                };
                Vector vector4 = (Vector) ((new Vector(x, y, z) - new Vector(Camera.X, Camera.Y, Camera.Z)) * Camera.Matrix.Inverse());
                Vector vector5 = new Vector(-vector4.Y, -vector4.Z, vector4.X);
                float num = Convert.ToSingle((uint) InterfaceHelper.WindowWidth);
                float num2 = Convert.ToSingle((uint) InterfaceHelper.WindowHeight);
                float num3 = ((num / num2) >= 1.6f) ? 55f : 44f;
                float num4 = num / 2f;
                float num5 = num2 / 2f;
                Rect rect = new Rect();
                GetClientRect(Memory.WindowHandle, ref rect);
                float num6 = 1f;
                float num7 = 1.08f;
                if (((1.0 * rect.right) / ((double) rect.bottom)) > 1.5)
                {
                    num6 *= 1.15f;
                    num7 = 1f;
                }
                float num8 = num4 / ((float) Math.Tan((double) ((((((num / num2) * num3) * num7) * num6) / 2f) * 0.01745329f)));
                float num9 = num5 / ((float) Math.Tan((double) ((((num / num2) * 35f) / 2f) * 0.01745329f)));
                point.X = (int) (num4 + ((vector5.X * num8) / vector5.Z));
                point.Y = (int) (num5 + ((vector5.Y * num9) / vector5.Z));
                if ((point.X < 0) || (point.Y < 0))
                {
                    point.X = 0;
                    point.Y = 0;
                }
                else
                {
                    if (realPos)
                    {
                        Point lpPoint = new Point();
                        Frame.ScreenToClient(Memory.WindowHandle, ref lpPoint);
                        Point* pointPtr1 = &point;
                        pointPtr1.X += Math.Abs(lpPoint.X);
                        Point* pointPtr2 = &point;
                        pointPtr2.Y += Math.Abs(lpPoint.Y);
                    }
                    Point* pointPtr3 = &point;
                    pointPtr3.Y -= 20;
                }
                return point;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct Rect
            {
                public int left;
                public int top;
                public int right;
                public int bottom;
            }
        }
    }
}

