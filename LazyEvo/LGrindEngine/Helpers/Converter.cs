namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib.IPlugin;
    using System;

    public class Converter : ILazyPlugin
    {
        public void BotStart()
        {
        }

        public void BotStop()
        {
        }

        public string GetName() => 
            "Profile Converter";

        public void PluginLoad()
        {
        }

        public void PluginUnload()
        {
        }

        public void Pulse()
        {
        }

        public void Settings()
        {
            new ConverterForm().Show();
        }
    }
}

