namespace LazyLib.LazyRadar.Drawer
{
    using LazyLib;
    using LazyLib.LazyRadar;
    using LazyLib.Wow;
    using System;
    using System.Drawing;

    internal class DrawObjects : IDrawItem
    {
        private readonly Color _colorObjects = Color.ForestGreen;

        public string CheckBoxName() => 
            "Show objects";

        public void Draw(RadarForm form)
        {
            foreach (PGameObject obj2 in LazyLib.Wow.ObjectManager.GetGameObject)
            {
                if (!obj2.Name.Contains("Survey"))
                {
                    form.PrintCircle(this._colorObjects, form.OffsetY(obj2.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(obj2.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), obj2.Name);
                    continue;
                }
                try
                {
                    form.PrintArrow(this._colorObjects, form.OffsetY(obj2.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(obj2.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), (double) obj2.Facing, obj2.Name, "");
                    Logging.Write(obj2.Facing, new object[0]);
                }
                catch
                {
                }
            }
        }

        public string SettingName() => 
            "DrawObjects";
    }
}

