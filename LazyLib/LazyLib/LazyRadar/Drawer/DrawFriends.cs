namespace LazyLib.LazyRadar.Drawer
{
    using LazyLib.LazyRadar;
    using LazyLib.Wow;
    using System;
    using System.Drawing;

    internal class DrawFriends : IDrawItem
    {
        private readonly Color _colorFriends = Color.Blue;

        public string CheckBoxName() => 
            "Show friends";

        public void Draw(RadarForm form)
        {
            foreach (PPlayer player in from cur in LazyLib.Wow.ObjectManager.GetPlayers
                where cur.PlayerFaction.Equals(LazyLib.Wow.ObjectManager.MyPlayer.PlayerFaction)
                select cur)
            {
                ulong gUID = player.GUID;
                if (!gUID.Equals(LazyLib.Wow.ObjectManager.MyPlayer.GUID))
                {
                    string name = player.Name;
                    form.PrintArrow(this._colorFriends, form.OffsetY(player.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(player.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), (double) player.Facing, name, (" Lvl: " + player.Level).TrimEnd(new char[0]));
                }
            }
        }

        public string SettingName() => 
            "DrawFriends";
    }
}

