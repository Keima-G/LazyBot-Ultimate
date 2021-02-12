namespace LazyLib.LazyRadar.Drawer
{
    using LazyLib.LazyRadar;
    using LazyLib.Wow;
    using System;
    using System.Drawing;

    internal class DrawEnemies : IDrawItem
    {
        private readonly Color _colorEnemies = Color.Red;

        public string CheckBoxName() => 
            "Show enemies";

        public void Draw(RadarForm form)
        {
            foreach (PPlayer player in from cur in LazyLib.Wow.ObjectManager.GetPlayers
                where !cur.PlayerFaction.Equals(LazyLib.Wow.ObjectManager.MyPlayer.PlayerFaction)
                select cur)
            {
                ulong gUID = player.GUID;
                if (!gUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID))
                {
                    string name = player.Name;
                    form.PrintArrow(this._colorEnemies, form.OffsetY(player.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(player.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), (double) player.Facing, name, (" Lvl: " + player.Level).TrimEnd(new char[0]));
                }
            }
        }

        public string SettingName() => 
            "DrawEnemies";
    }
}

