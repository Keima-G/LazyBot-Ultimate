namespace LazyLib.LazyRadar.Drawer
{
    using LazyLib.LazyRadar;
    using System;

    public interface IDrawItem
    {
        string CheckBoxName();
        void Draw(RadarForm form);
        string SettingName();
    }
}

