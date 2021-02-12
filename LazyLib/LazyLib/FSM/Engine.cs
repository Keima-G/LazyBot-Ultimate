namespace LazyLib.FSM
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.IEngine;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Engine
    {
        public static bool Paused;
        public static MainState LastState;
        private static Thread _workerThread;
        private static EventHandler<NotifyStateChanged> StateChange;
        private static ILazyEngine _engine;

        public static event EventHandler<NotifyStateChanged> StateChange
        {
            add
            {
                EventHandler<NotifyStateChanged> stateChange = StateChange;
                while (true)
                {
                    EventHandler<NotifyStateChanged> comparand = stateChange;
                    EventHandler<NotifyStateChanged> handler3 = comparand + value;
                    stateChange = Interlocked.CompareExchange<EventHandler<NotifyStateChanged>>(ref StateChange, handler3, comparand);
                    if (ReferenceEquals(stateChange, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<NotifyStateChanged> stateChange = StateChange;
                while (true)
                {
                    EventHandler<NotifyStateChanged> comparand = stateChange;
                    EventHandler<NotifyStateChanged> handler3 = comparand - value;
                    stateChange = Interlocked.CompareExchange<EventHandler<NotifyStateChanged>>(ref StateChange, handler3, comparand);
                    if (ReferenceEquals(stateChange, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public static void AddState(MainState state)
        {
            States ??= new List<MainState>();
            if (!States.Contains(state))
            {
                States.Add(state);
            }
        }

        public static void Clear()
        {
            if (States != null)
            {
                States.Clear();
            }
        }

        public static void Pause()
        {
            if (Running)
            {
                Paused = !Paused;
                Logging.Write(Paused ? "Paused bot" : "Resumed bot", new object[0]);
            }
        }

        private static void Run()
        {
            try
            {
                goto TR_001F;
            TR_0002:
                Thread.Sleep(1);
                Application.DoEvents();
            TR_001F:
                while (true)
                {
                    if (!Running)
                    {
                        break;
                    }
                    try
                    {
                        if (Paused)
                        {
                            _engine.Pause();
                            while (true)
                            {
                                if (!Paused)
                                {
                                    _engine.Resume();
                                    break;
                                }
                                Thread.Sleep(50);
                            }
                        }
                        while (true)
                        {
                            if (ChatQueu.QueueCount == 0)
                            {
                                using (IEnumerator<MainState> enumerator = (from state in States
                                    where state.NeedToRun
                                    select state).GetEnumerator())
                                {
                                    if (enumerator.MoveNext())
                                    {
                                        MainState current = enumerator.Current;
                                        if (!ReferenceEquals(LastState, current) && (StateChange != null))
                                        {
                                            StateChange(new object(), new NotifyStateChanged(current.Name()));
                                            Logging.Debug("State changed: " + current.Name(), new object[0]);
                                        }
                                        current.DoWork();
                                        LastState = current;
                                    }
                                }
                                break;
                            }
                            _engine.Pause();
                            KeyHelper.ChatboxSendText(ChatQueu.GetItem);
                            Thread.Sleep(100);
                            _engine.Resume();
                        }
                    }
                    catch (ThreadAbortException)
                    {
                    }
                    catch (ThreadStateException exception)
                    {
                        Logging.Write("Thread in odd state, restarting: " + exception, new object[0]);
                    }
                    catch (Exception exception2)
                    {
                        if (LazyLib.Wow.ObjectManager.InGame)
                        {
                            Logging.Debug("[Engine] Exception " + exception2, new object[0]);
                        }
                        Thread.Sleep(0xfa0);
                    }
                    goto TR_0002;
                }
            }
            catch (ThreadAbortException)
            {
            }
            catch (Exception)
            {
            }
            finally
            {
                Running = false;
            }
        }

        public static void StartEngine(ILazyEngine engine)
        {
            _engine = engine;
            Clear();
            foreach (MainState state in _engine.States)
            {
                AddState(state);
            }
            lock (States)
            {
                States.Add(new StateIdle());
                LastState = States[States.Count<MainState>() - 1];
            }
            States.Sort();
            Logging.Write("[Engine]Initializing", new object[0]);
            Paused = false;
            Running = true;
            Thread thread = new Thread(new ThreadStart(Engine.Run)) {
                IsBackground = true
            };
            _workerThread = thread;
            _workerThread.Name = "Engine";
            _workerThread.SetApartmentState(ApartmentState.STA);
            _workerThread.Start();
            Logging.Write("[Engine]Started bot thread", new object[0]);
        }

        public static void StopEngine()
        {
            if (Running)
            {
                Running = false;
                if (_workerThread.IsAlive)
                {
                    _workerThread.Abort();
                }
                _workerThread = null;
                States = null;
                LastState = null;
                _engine = null;
                GC.Collect();
            }
        }

        public static List<MainState> States { get; private set; }

        public static bool Running { get; private set; }

        [Obfuscation(Feature="renaming", ApplyToMembers=true)]
        public class NotifyStateChanged : EventArgs
        {
            public NotifyStateChanged(string name)
            {
                this.Name = name;
            }

            public string Name { get; set; }
        }
    }
}

