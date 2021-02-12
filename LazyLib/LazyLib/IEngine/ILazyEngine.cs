namespace LazyLib.IEngine
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Windows.Forms;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public interface ILazyEngine
    {
        void Close();
        bool EngineStart();
        void EngineStop();
        List<IMouseClick> GetRadarClick();
        List<IDrawItem> GetRadarDraw();
        void Load();
        void Pause();
        void Resume();

        string Name { get; }

        List<MainState> States { get; }

        Form Settings { get; }

        Form ProfileForm { get; }
    }
}

