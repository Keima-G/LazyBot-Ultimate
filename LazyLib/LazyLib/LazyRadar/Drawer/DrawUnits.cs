namespace LazyLib.LazyRadar.Drawer
{
    using LazyLib.LazyRadar;
    using LazyLib.Wow;
    using System;
    using System.Drawing;

    internal class DrawUnits : IDrawItem
    {
        private readonly Color _colorUnits = Color.BlueViolet;

        public string CheckBoxName() => 
            "Show units";

        public void Draw(RadarForm form)
        {
            foreach (PUnit unit in LazyLib.Wow.ObjectManager.GetUnits)
            {
                ulong gUID = unit.GUID;
                if (!gUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID) && (!unit.IsPlayer && !unit.IsDead))
                {
                    string botString = (unit.Name + " ").TrimEnd(new char[0]);
                    form.PrintArrow(this._colorUnits, form.OffsetY(unit.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(unit.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), (double) unit.Facing, "", botString);
                }
            }
        }

        public string SettingName() => 
            "DrawUnits";
    }
}

