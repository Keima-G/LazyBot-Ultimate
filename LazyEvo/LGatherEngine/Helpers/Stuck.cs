namespace LazyEvo.LGatherEngine.Helpers
{
    using LazyLib.Wow;
    using System;
    using System.Threading;

    internal class Stuck
    {
        private static bool _pIsStuck;
        private static Thread _thread = new Thread(new ThreadStart(Stuck.Loop));
        private static float _oldX;
        private static float _oldY;
        private static float _oldZ;
        private static float _dx;
        private static float _dy;
        private static float _dz;

        private static void Loop()
        {
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if (LazyLib.Wow.ObjectManager.MyPlayer.Speed == 0f)
                        {
                            Thread.Sleep(0x3e8);
                            _oldX = 0f;
                            _oldY = 0f;
                            _oldZ = 0f;
                        }
                        else
                        {
                            try
                            {
                                _oldX = LazyLib.Wow.ObjectManager.MyPlayer.X;
                                _oldY = LazyLib.Wow.ObjectManager.MyPlayer.Y;
                                _oldZ = LazyLib.Wow.ObjectManager.MyPlayer.Z;
                                Thread.Sleep(0x4b0);
                                if (LazyLib.Wow.ObjectManager.MyPlayer.Speed != 0f)
                                {
                                    _dx = _oldX - LazyLib.Wow.ObjectManager.MyPlayer.X;
                                    _dy = _oldY - LazyLib.Wow.ObjectManager.MyPlayer.Y;
                                    _dz = _oldZ - LazyLib.Wow.ObjectManager.MyPlayer.Z;
                                    _pIsStuck = Math.Sqrt((double) (((_dx * _dx) + (_dy * _dy)) + (_dz * _dz))) < 2.0;
                                }
                            }
                            catch (Exception)
                            {
                                Thread.Sleep(100);
                            }
                        }
                        break;
                    }
                }
                catch
                {
                    break;
                }
            }
        }

        public static void Reset()
        {
            _oldX = 0f;
            _oldY = 0f;
            _oldZ = 0f;
        }

        internal static void Run()
        {
            Start();
        }

        private static void Start()
        {
            try
            {
                if ((_thread == null) || !_thread.IsAlive)
                {
                    Thread thread = new Thread(new ThreadStart(Stuck.Loop)) {
                        IsBackground = true
                    };
                    _thread = thread;
                    _thread.Name = "Stuck";
                    _thread.Start();
                }
            }
            catch
            {
            }
        }

        internal static void Stop()
        {
            try
            {
                if ((_thread != null) && _thread.IsAlive)
                {
                    _thread.Abort();
                }
                _pIsStuck = false;
            }
            catch
            {
            }
            _pIsStuck = false;
        }

        public static bool IsStuck =>
            (LazyLib.Wow.ObjectManager.MyPlayer.Speed != 0f) && (LazyLib.Wow.ObjectManager.MyPlayer.IsMounted && _pIsStuck);
    }
}

