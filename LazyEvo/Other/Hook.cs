namespace LazyEvo.Other
{
    using curmoho;
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Drawing;

    internal class Hook
    {
        private static EntryPoint _entryPoint;

        internal static void Block()
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                try
                {
                    if ((_entryPoint != null) && !_entryPoint.IsInstalled)
                    {
                        try
                        {
                            _entryPoint.Install(Memory.ProcessId, Memory.ProcessHandle);
                        }
                        catch
                        {
                            Logging.Write("Could not enable background curser: Block", new object[0]);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private static void BlockMessage(object sender, MouseBlocMessasge e)
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                if (e.Block)
                {
                    Block();
                }
                else
                {
                    ReleaseMouse();
                }
            }
        }

        internal static void Close()
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                try
                {
                    if (_entryPoint != null)
                    {
                        _entryPoint.Dispose();
                    }
                }
                catch
                {
                }
            }
        }

        private static Point CursorPos()
        {
            try
            {
                if (LazySettings.HookMouse && (_entryPoint != null))
                {
                    return _entryPoint.GetMouseCursorPos();
                }
            }
            catch
            {
            }
            return new Point(0, 0);
        }

        internal static void DoHook()
        {
            if (LazySettings.HookMouse)
            {
                MouseHelper.SetDelegate(new MouseHelper.FunctionToCall(Hook.CursorPos));
                try
                {
                    MouseHelper.MouseBlockMessage -= new EventHandler<MouseBlocMessasge>(Hook.BlockMessage);
                    MouseHelper.MouseMoveMessage -= new EventHandler<MouseMoveMessasge>(Hook.MouseMove);
                    if (_entryPoint != null)
                    {
                        ReleaseMouse();
                    }
                    _entryPoint = EntryPoint.Instance;
                    _entryPoint.Install(Memory.ProcessId, Memory.ProcessHandle);
                    Logging.Write("Background enabled: " + _entryPoint.IsInstalled, new object[0]);
                    MouseHelper.MouseBlockMessage += new EventHandler<MouseBlocMessasge>(Hook.BlockMessage);
                    MouseHelper.MouseMoveMessage += new EventHandler<MouseMoveMessasge>(Hook.MouseMove);
                }
                catch
                {
                    Logging.Write("Could not enable background cursor", new object[0]);
                }
            }
        }

        private static void MouseMove(object sender, MouseMoveMessasge e)
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                MoveMouse(e.X, e.Y);
            }
        }

        internal static void MoveMouse(int x, int y)
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                try
                {
                    if (_entryPoint != null)
                    {
                        _entryPoint.SetCursorPos(x, y);
                    }
                }
                catch
                {
                }
            }
        }

        internal static void ReleaseMouse()
        {
            if (LazySettings.HookMouse && (_entryPoint != null))
            {
                try
                {
                    if (_entryPoint != null)
                    {
                        _entryPoint.UnlockCursor();
                    }
                }
                catch
                {
                }
            }
        }
    }
}

