namespace LazyEvo.LGatherEngine.Radar
{
    using LazyEvo.LGatherEngine;
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib.LazyRadar;
    using LazyLib.LazyRadar.Drawer;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    internal class DrawWaypoints : IDrawItem
    {
        private readonly Color _colorToTown = Color.Green;
        private readonly Color _colorWaypoints = Color.Red;

        public string CheckBoxName() => 
            "Show waypoints";

        public void Draw(RadarForm form)
        {
            if (GatherEngine.CurrentProfile != null)
            {
                GatherProfile currentProfile = GatherEngine.CurrentProfile;
                this.PrintWay(currentProfile.GetListSortedAfterDistance(LazyLib.Wow.ObjectManager.MyPlayer.Location), this._colorWaypoints, form);
                this.PrintWay(currentProfile.GetListSortedAfterDistance(LazyLib.Wow.ObjectManager.MyPlayer.Location, currentProfile.WaypointsToTown), this._colorToTown, form);
            }
        }

        private void PrintWay(List<Location> loc, Color color, RadarForm form)
        {
            if ((loc != null) && (loc.Count != 0))
            {
                Point point;
                PointF[] points = new PointF[loc.Count + 1];
                int index = 0;
                foreach (Location location2 in loc)
                {
                    form.PrintCircle(color, form.OffsetY(location2.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(location2.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X), "");
                    point = new Point(form.OffsetY(location2.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(location2.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X));
                    points[index] = (PointF) point;
                    index++;
                }
                Location location = loc[0];
                point = new Point(form.OffsetY(location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), form.OffsetX(location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X));
                points[index] = (PointF) point;
                form.ScreenDc.DrawLines(new Pen(this._colorWaypoints), points);
            }
        }

        public string SettingName() => 
            "DrawWaypoints";
    }
}

