namespace LazyLib.Helpers
{
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class MoveHelper
    {
        private static bool _runForwards;
        private static bool _runBackwards;
        private static bool _strafeLeft;
        private static bool _strafeRight;
        private static bool _rotateLeft;
        private static bool _rotateRight;
        private static bool _down;
        private static readonly Ticker KeyT = new Ticker(50.0);
        private static bool _oldRunForwards;
        private static bool _oldRunBackwards;
        private static bool _oldStrafeLeft;
        private static bool _oldStrafeRight;
        private static bool _oldRotateLeft;
        private static bool _oldRotateRight;
        private static bool _oldDown;
        private static readonly Random Ran = new Random();

        public static void Backwards(bool go)
        {
            _runBackwards = go;
            if (go)
            {
                _runForwards = false;
            }
            PushKeys();
        }

        public static void Down(bool go)
        {
            _down = go;
            PushKeys();
        }

        public static void Down(int milli)
        {
            KeyHelper.PressKey("X");
            Thread.Sleep(milli);
            KeyHelper.ReleaseKey("X");
        }

        public static void Forwards(bool go)
        {
            _runForwards = go;
            if (go)
            {
                _runBackwards = false;
            }
            PushKeys();
        }

        public bool IsMoving() => 
            _runForwards || (_runBackwards || (_strafeLeft || _strafeRight));

        public bool IsRotating() => 
            _rotateLeft || _rotateRight;

        public bool IsRotatingLeft() => 
            _rotateLeft;

        public bool IsRotatingRight() => 
            _rotateLeft;

        public static void Jump()
        {
            KeyHelper.SendKey("Space");
        }

        public static void Jump(int milli)
        {
            KeyHelper.PressKey("Space");
            Thread.Sleep(milli);
            KeyHelper.ReleaseKey("Space");
        }

        public static void MoveRandom()
        {
            int num = Ran.Next(4);
            if ((num == 0) || (num == 1))
            {
                Forwards(true);
            }
            if (num == 1)
            {
                StrafeRight(true);
            }
            if ((num == 2) || (num == 3))
            {
                Backwards(true);
            }
            if (num == 3)
            {
                StrafeLeft(true);
            }
        }

        public static bool MoveToLoc(Location targetObject, double distance) => 
            MoveToLoc(targetObject, distance, false, false);

        public static bool MoveToLoc(Location targetObject, double distance, bool continueMove, bool breakOnCombat)
        {
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            Ticker ticker = new Ticker(1100.0);
            Ticker ticker2 = new Ticker(30000.0);
            int num = 0;
            while ((targetObject.DistanceToSelf > distance) && !ticker2.IsReady)
            {
                targetObject.Face();
                Forwards(true);
                if (LazyLib.Wow.ObjectManager.MyPlayer.Location.GetDistanceTo(location) > 1.0)
                {
                    location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                    ticker.Reset();
                }
                if (breakOnCombat && ((LazyLib.Wow.ObjectManager.GetAttackers.Count != 0) && LazyLib.Wow.ObjectManager.ShouldDefend))
                {
                    Forwards(false);
                    return false;
                }
                Forwards(true);
                if ((LazyLib.Wow.ObjectManager.MyPlayer.Location.GetDistanceTo(location) < 1.0) && ticker.IsReady)
                {
                    if (num > 3)
                    {
                        return false;
                    }
                    Logging.Write("[Move]I am stuck " + num, new object[0]);
                    switch (num)
                    {
                        case 0:
                            Forwards(false);
                            Forwards(true);
                            Thread.Sleep(50);
                            Jump();
                            Thread.Sleep(800);
                            break;

                        case 1:
                            Forwards(false);
                            Forwards(true);
                            StrafeLeft(true);
                            Thread.Sleep(800);
                            break;

                        case 2:
                            Forwards(false);
                            Forwards(true);
                            StrafeLeft(true);
                            Thread.Sleep(800);
                            break;

                        case 3:
                            Forwards(false);
                            Forwards(true);
                            StrafeRight(true);
                            Thread.Sleep(800);
                            break;

                        default:
                            break;
                    }
                    Thread.Sleep(200);
                    num++;
                    ReleaseKeys();
                    Thread.Sleep(500);
                    ticker.Reset();
                }
                Thread.Sleep(10);
            }
            if (ticker2.IsReady && (targetObject.DistanceToSelf > distance))
            {
                Logging.Write("Approach: " + targetObject + " failed", new object[0]);
                Forwards(false);
                return false;
            }
            if (!continueMove)
            {
                Forwards(false);
                ReleaseKeys();
            }
            targetObject.Face();
            return true;
        }

        public static bool MoveToUnit(PUnit targetObject, double distance)
        {
            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            Forwards(false);
            Ticker ticker = new Ticker(1100.0);
            Ticker ticker2 = new Ticker(16000.0);
            int num = 0;
            while ((targetObject.DistanceToSelf > distance) && !ticker2.IsReady)
            {
                targetObject.Face();
                Forwards(true);
                if (LazyLib.Wow.ObjectManager.MyPlayer.Location.GetDistanceTo(location) > 1.0)
                {
                    location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                    ticker.Reset();
                }
                Forwards(true);
                if ((LazyLib.Wow.ObjectManager.MyPlayer.Location.GetDistanceTo(location) < 1.0) && ticker.IsReady)
                {
                    if (num > 3)
                    {
                        return false;
                    }
                    Logging.Write("[Move]I am stuck " + num, new object[0]);
                    switch (num)
                    {
                        case 0:
                            Forwards(false);
                            Forwards(true);
                            Thread.Sleep(50);
                            Jump();
                            Thread.Sleep(800);
                            break;

                        case 1:
                            Forwards(false);
                            Forwards(true);
                            StrafeLeft(true);
                            Thread.Sleep(800);
                            break;

                        case 2:
                            Forwards(false);
                            Forwards(true);
                            StrafeLeft(true);
                            Thread.Sleep(800);
                            break;

                        case 3:
                            Forwards(false);
                            Forwards(true);
                            StrafeRight(true);
                            Thread.Sleep(800);
                            break;

                        default:
                            break;
                    }
                    Thread.Sleep(200);
                    num++;
                    ReleaseKeys();
                    Thread.Sleep(500);
                    ticker.Reset();
                }
                Thread.Sleep(10);
            }
            if (ticker2.IsReady && (targetObject.DistanceToSelf > distance))
            {
                Logging.Write("Approach: " + targetObject.Name + " failed", new object[0]);
                Forwards(false);
                return false;
            }
            Forwards(false);
            ReleaseKeys();
            targetObject.Face();
            return true;
        }

        public static bool MoveToUnit(PUnit targetObject, double distance, bool dummy) => 
            MoveToUnit(targetObject, distance);

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

        public static void PushKeys()
        {
            if (_oldRunForwards != _runForwards)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_runForwards)
                {
                    KeyHelper.PressKey("Up");
                }
                else
                {
                    KeyHelper.ReleaseKey("Up");
                }
            }
            if (_oldRunBackwards != _runBackwards)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_runBackwards)
                {
                    KeyHelper.PressKey("Down");
                }
                else
                {
                    KeyHelper.ReleaseKey("Down");
                }
            }
            if (_oldDown != _down)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_down)
                {
                    KeyHelper.PressKey("X");
                }
                else
                {
                    KeyHelper.ReleaseKey("X");
                }
            }
            if (_oldStrafeLeft != _strafeLeft)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_strafeLeft)
                {
                    KeyHelper.PressKey("Q");
                }
                else
                {
                    KeyHelper.ReleaseKey("Q");
                }
            }
            if (_oldStrafeRight != _strafeRight)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_strafeRight)
                {
                    KeyHelper.PressKey("E");
                }
                else
                {
                    KeyHelper.ReleaseKey("E");
                }
            }
            if (_oldRotateLeft != _rotateLeft)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_rotateLeft)
                {
                    KeyHelper.PressKey("Left");
                }
                else
                {
                    KeyHelper.ReleaseKey("Left");
                }
            }
            if (_oldRotateRight != _rotateRight)
            {
                KeyT.Wait();
                KeyT.Reset();
                if (_rotateRight)
                {
                    KeyHelper.PressKey("Right");
                }
                else
                {
                    KeyHelper.ReleaseKey("Right");
                }
            }
            _oldRunForwards = _runForwards;
            _oldRunBackwards = _runBackwards;
            _oldStrafeLeft = _strafeLeft;
            _oldStrafeRight = _strafeRight;
            _oldRotateLeft = _rotateLeft;
            _oldRotateRight = _rotateRight;
            _oldDown = _down;
        }

        public static void ReleaseKeys()
        {
            StopMove();
            if (LazyLib.Wow.ObjectManager.Initialized)
            {
                KeyHelper.ReleaseKey("Space");
                KeyHelper.ReleaseKey("X");
                KeyHelper.ReleaseKey("Up");
                KeyHelper.ReleaseKey("Down");
                KeyHelper.ReleaseKey("Left");
                KeyHelper.ReleaseKey("Right");
                KeyHelper.ReleaseKey("Q");
                KeyHelper.ReleaseKey("E");
            }
        }

        public static void ResyncKeys()
        {
            KeyT.ForceReady();
            PushKeys();
        }

        public static void RotateLeft(bool go)
        {
            _rotateLeft = go;
            if (go)
            {
                _rotateRight = false;
            }
            PushKeys();
        }

        public static void RotateRight(bool go)
        {
            _rotateRight = go;
            if (go)
            {
                _rotateLeft = false;
            }
            PushKeys();
        }

        public static void Stop()
        {
            StopMove();
            StopRotate();
        }

        public static void StopMove()
        {
            _runForwards = false;
            _runBackwards = false;
            _strafeLeft = false;
            _strafeRight = false;
            _rotateLeft = false;
            _rotateRight = false;
            _down = false;
            PushKeys();
            KeyHelper.ReleaseKey("X");
            KeyHelper.ReleaseKey("Space");
        }

        public static void StopRotate()
        {
            _rotateLeft = false;
            _rotateRight = false;
            PushKeys();
        }

        public static void StrafeLeft(bool go)
        {
            _strafeLeft = go;
            if (go)
            {
                _strafeRight = false;
            }
            PushKeys();
        }

        public static void StrafeRight(bool go)
        {
            _strafeRight = go;
            if (go)
            {
                _strafeLeft = false;
            }
            PushKeys();
        }
    }
}

