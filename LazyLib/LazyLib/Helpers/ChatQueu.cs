namespace LazyLib.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class ChatQueu
    {
        private static readonly List<string> Queue = new List<string>();

        public static void AddChat(string message)
        {
            lock (Queue)
            {
                Queue.Add(message);
            }
        }

        public static string GetItem
        {
            get
            {
                lock (Queue)
                {
                    if (Queue.Count != 0)
                    {
                        string str = Queue[0];
                        Queue.RemoveAt(0);
                        return str;
                    }
                }
                return "";
            }
        }

        public static int QueueCount
        {
            get
            {
                lock (Queue)
                {
                    return Queue.Count;
                }
            }
        }
    }
}

