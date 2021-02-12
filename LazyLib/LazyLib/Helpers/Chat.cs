namespace LazyLib.Helpers
{
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Chat
    {
        private object _chatReaderLock = new object();
        private List<ChatMsg> _listCompleteChat;
        private List<string> _listCurrentChat;
        private List<string> _listLastChat;
        private List<ChatMsg> _listLastestChat;
        private static EventHandler<GChatEventArgs> NewChatMessage;

        [Obfuscation(Feature="renaming")]
        public static event EventHandler<GChatEventArgs> NewChatMessage
        {
            add
            {
                EventHandler<GChatEventArgs> newChatMessage = NewChatMessage;
                while (true)
                {
                    EventHandler<GChatEventArgs> comparand = newChatMessage;
                    EventHandler<GChatEventArgs> handler3 = comparand + value;
                    newChatMessage = Interlocked.CompareExchange<EventHandler<GChatEventArgs>>(ref NewChatMessage, handler3, comparand);
                    if (ReferenceEquals(newChatMessage, comparand))
                    {
                        return;
                    }
                }
            }
            remove
            {
                EventHandler<GChatEventArgs> newChatMessage = NewChatMessage;
                while (true)
                {
                    EventHandler<GChatEventArgs> comparand = newChatMessage;
                    EventHandler<GChatEventArgs> handler3 = comparand - value;
                    newChatMessage = Interlocked.CompareExchange<EventHandler<GChatEventArgs>>(ref NewChatMessage, handler3, comparand);
                    if (ReferenceEquals(newChatMessage, comparand))
                    {
                        return;
                    }
                }
            }
        }

        private void FindNewMessages()
        {
            for (int i = 0; i < 60; i++)
            {
                if (this._listCurrentChat[i] != this._listLastChat[i])
                {
                    ChatMsg item = ParseChatMsg(this._listCurrentChat[i]);
                    if (NewChatMessage != null)
                    {
                        GChatEventArgs e = new GChatEventArgs {
                            Msg = item
                        };
                        NewChatMessage(this, e);
                    }
                    this._listCompleteChat.Add(item);
                    this._listLastestChat.Add(item);
                }
            }
        }

        private static ChatMsg ParseChatMsg(string strChatMsg)
        {
            ChatMsg msg3;
            try
            {
                int startIndex = strChatMsg.IndexOf("Type: [") + 7;
                int index = strChatMsg.IndexOf("]", startIndex);
                if ((startIndex <= 0) && (index <= 0))
                {
                    msg3 = new ChatMsg();
                }
                else
                {
                    ChatMsg msg;
                    msg.Type = (Constants.ChatType) ((byte) int.Parse(strChatMsg.Substring(startIndex, index - startIndex)));
                    startIndex = strChatMsg.IndexOf("Channel: [") + 10;
                    msg.Channel = strChatMsg.Substring(startIndex, strChatMsg.IndexOf("]", startIndex) - startIndex);
                    startIndex = strChatMsg.IndexOf("Player Name: [") + 14;
                    msg.Player = strChatMsg.Substring(startIndex, strChatMsg.IndexOf("]", startIndex) - startIndex);
                    startIndex = strChatMsg.IndexOf("Sender GUID: [") + 14;
                    msg.chatGUID = strChatMsg.Substring(startIndex, strChatMsg.IndexOf("]", startIndex) - startIndex);
                    startIndex = strChatMsg.IndexOf("Text: [") + 7;
                    index = startIndex;
                    while (true)
                    {
                        int num3 = strChatMsg.IndexOf("]", (int) (index + 1));
                        if (num3 == -1)
                        {
                            msg.Msg = strChatMsg.Substring(startIndex, index - startIndex);
                            msg3 = msg;
                            break;
                        }
                        index = num3;
                    }
                }
            }
            catch
            {
                msg3 = new ChatMsg();
            }
            return msg3;
        }

        public void PrepareReading()
        {
            this._listCurrentChat = new List<string>();
            this._listCompleteChat = new List<ChatMsg>();
            this._listLastestChat = new List<ChatMsg>();
        }

        public void ReadChat()
        {
            try
            {
                if (this._chatReaderLock == null)
                {
                    this.PrepareReading();
                    this._chatReaderLock = new object();
                }
                lock (this._chatReaderLock)
                {
                    this._listCurrentChat.Clear();
                    int num = 0;
                    while (true)
                    {
                        if (num >= 60)
                        {
                            this._listLastChat ??= new List<string>(this._listCurrentChat);
                            this.FindNewMessages();
                            this._listLastChat = new List<string>(this._listCurrentChat);
                            break;
                        }
                        this._listCurrentChat.Add(Memory.ReadUtf8StringRelative(Convert.ToUInt32((long) (0x775a9cL + (num * 0x17c0L))), 0x200));
                        num++;
                    }
                }
            }
            catch
            {
            }
        }
    }
}

