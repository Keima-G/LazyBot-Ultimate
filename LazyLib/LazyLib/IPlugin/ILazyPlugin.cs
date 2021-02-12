namespace LazyLib.IPlugin
{
    using System;

    public interface ILazyPlugin
    {
        void BotStart();
        void BotStop();
        string GetName();
        void PluginLoad();
        void PluginUnload();
        void Pulse();
        void Settings();
    }
}

