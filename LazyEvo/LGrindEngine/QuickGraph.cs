namespace LazyEvo.LGrindEngine
{
    using LazyLib;
    using LazyLib.Wow;
    using QuickGraph;
    using QuickGraph.Algorithms;
    using QuickGraph.Serialization;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;

    public class QuickGraph
    {
        private int _distance = 6;
        private AdjacencyGraph<Location, DirectedLazyEdge> _graph;
        private NodeType _nodeType;
        private Thread _record;

        public QuickGraph()
        {
            this.New();
        }

        public void AddConnection(Location a, Location b)
        {
            try
            {
                this.AddEdge(a, b);
            }
            catch
            {
            }
        }

        public void AddEdge(Location source, Location target)
        {
            if (!this._graph.ContainsVertex(source) || !this._graph.ContainsVertex(target))
            {
                Logging.Write(string.Concat(new object[] { "Vertex: ", source, " : ", target }), new object[0]);
            }
            else
            {
                DirectedLazyEdge e = new DirectedLazyEdge(source, target);
                this._graph.AddEdge(e);
                e = new DirectedLazyEdge(target, source);
                this._graph.AddEdge(e);
            }
        }

        public void AddNode(Location loc)
        {
            loc.NodeType = this._nodeType;
            this._graph.AddVertex(loc);
            this.ConnectNode(loc);
        }

        public void AddNodeNoConnection(Location toAdd)
        {
            toAdd.NodeType = this._nodeType;
            this._graph.AddVertex(toAdd);
        }

        public void ConnectNode(Location toAdd)
        {
            foreach (Location location in this._graph.Vertices)
            {
                if (!ReferenceEquals(location, toAdd) && (toAdd.DistanceFrom(location) <= (this._distance + 2)))
                {
                    this.AddEdge(toAdd, location);
                }
            }
        }

        private void DoRecording()
        {
            Location loc = LazyLib.Wow.ObjectManager.MyPlayer.Location;
            this.AddNode(loc);
            bool flag = false;
            while (true)
            {
                try
                {
                    while (true)
                    {
                        if (LazyLib.Wow.ObjectManager.Initialized)
                        {
                            Location location = LazyLib.Wow.ObjectManager.MyPlayer.Location;
                            if (location.DistanceFrom(loc) >= this._distance)
                            {
                                foreach (Location location3 in this._graph.Vertices)
                                {
                                    if (location.DistanceFrom(location3) < 3.0)
                                    {
                                        flag = true;
                                    }
                                }
                                if (!flag)
                                {
                                    this.AddNode(location);
                                    loc = location;
                                }
                                flag = false;
                            }
                            Thread.Sleep(100);
                        }
                        break;
                    }
                }
                catch (Exception exception1)
                {
                    Logging.Debug(exception1.ToString(), new object[0]);
                }
            }
        }

        public List<Location> FindPath(Location sourced, Location targetd)
        {
            try
            {
                IEnumerable<DirectedLazyEdge> enumerable;
                Func<DirectedLazyEdge, double> edgeWeights = new Func<DirectedLazyEdge, double>(QuickGraph.GetDistance);
                Location closest = this.GetClosest(sourced);
                Location arg = this.GetClosest(targetd);
                TryFunc<Location, IEnumerable<DirectedLazyEdge>> func2 = null;
                try
                {
                    func2 = this._graph.ShortestPathsDijkstra<Location, DirectedLazyEdge>(edgeWeights, closest);
                }
                catch (Exception exception)
                {
                    Logging.Debug("Could not create path: " + exception, new object[0]);
                }
                List<Location> list = new List<Location>();
                if ((func2 != null) && func2(arg, out enumerable))
                {
                    list.AddRange(from e in enumerable select e.Source);
                }
                return list;
            }
            catch (ArgumentException)
            {
                Logging.Write(LogType.Warning, "Could not create path, make sure you got a path loaded and it is valid", new object[0]);
                return new List<Location>();
            }
        }

        public Location GetClosest(Location loc)
        {
            double maxValue = double.MaxValue;
            Location location = null;
            foreach (Location location2 in this._graph.Vertices)
            {
                if (location2.DistanceFromXY(loc) < maxValue)
                {
                    maxValue = location2.DistanceFromXY(loc);
                    location = location2;
                }
            }
            return location;
        }

        public static double GetDistance(DirectedLazyEdge edge) => 
            edge.Source.GetDistanceTo(edge.Target);

        public List<DirectedLazyEdge> GetEdges()
        {
            List<DirectedLazyEdge> list = new List<DirectedLazyEdge>();
            return this._graph.Edges.ToList<DirectedLazyEdge>();
        }

        public List<Location> GetNodes()
        {
            List<Location> list = new List<Location>();
            return this._graph.Vertices.ToList<Location>();
        }

        public void LoadGraph(string file)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            this._graph = (AdjacencyGraph<Location, DirectedLazyEdge>) formatter.Deserialize(File.Open(file, FileMode.Open, FileAccess.Read));
        }

        public void New()
        {
            this._graph = new AdjacencyGraph<Location, DirectedLazyEdge>();
        }

        public void RecordMesh()
        {
            if ((this._record == null) || !this._record.IsAlive)
            {
                this._record = new Thread(new ThreadStart(this.DoRecording));
                this._record.IsBackground = true;
                this._record.Start();
            }
        }

        public void RemoveNode(Location loc)
        {
            Func<DirectedLazyEdge, bool> func = null;
            lock (this._graph)
            {
                if (func == null)
                {
                    func = lazyEdge => lazyEdge.Source.Equals(loc) || lazyEdge.Target.Equals(loc);
                }
                foreach (DirectedLazyEdge edge in Enumerable.Where<DirectedLazyEdge>(this._graph.Edges, func).ToList<DirectedLazyEdge>())
                {
                    this._graph.RemoveEdge(edge);
                }
                foreach (Location location in this._graph.Vertices)
                {
                    if (location.Equals(loc))
                    {
                        this._graph.RemoveVertex(location);
                        break;
                    }
                }
            }
        }

        public void SaveGraph(string file)
        {
            using (FileStream stream = File.Open(file, FileMode.OpenOrCreate, FileAccess.Write))
            {
                this._graph.SerializeToBinary<Location, DirectedLazyEdge>(stream);
            }
        }

        public void SetNodeDistance(int distance)
        {
            this._distance = distance;
        }

        public void SetNodeType(NodeType type)
        {
            this._nodeType = type;
        }

        public void StopRecordMesh()
        {
            if ((this._record != null) && this._record.IsAlive)
            {
                this._record.Abort();
                this._record = null;
            }
        }
    }
}

