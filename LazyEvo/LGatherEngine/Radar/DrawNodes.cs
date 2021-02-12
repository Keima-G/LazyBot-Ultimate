namespace LazyEvo.LGatherEngine.Radar
{
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib.LazyRadar;
    using LazyLib.LazyRadar.Drawer;
    using LazyLib.Wow;
    using System;
    using System.Drawing;

    internal class DrawNodes : IDrawItem
    {
        private readonly Color _colorBadNodes = Color.Red;
        private readonly Color _colorObjects = Color.ForestGreen;

        public string CheckBoxName() => 
            "Show nodes";

        public void Draw(RadarForm form)
        {
            foreach (PGameObject obj2 in LazyLib.Wow.ObjectManager.GetGameObject)
            {
                if (FindNode.IsHerb(obj2) || FindNode.IsMine(obj2))
                {
                    if (GatherBlackList.IsBlacklisted(obj2))
                    {
                        form.PrintCircle(this._colorBadNodes, form.OffsetY(obj2.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(obj2.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), obj2.Name);
                        continue;
                    }
                    form.PrintCircle(this._colorObjects, form.OffsetY(obj2.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(obj2.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), obj2.Name);
                }
            }
        }

        public string SettingName() => 
            "DrawNodes";
    }
}

