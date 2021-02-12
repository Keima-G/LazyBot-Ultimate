namespace LazyEvo.LGatherEngine.Radar
{
    using LazyEvo.LGatherEngine.Helpers;
    using LazyLib;
    using LazyLib.LazyRadar;
    using LazyLib.Wow;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    internal class MouseHandler : IMouseClick
    {
        public void Click(RadarForm form, MouseEventArgs e)
        {
            Point point = form.PointToClient(Cursor.Position);
            Rectangle b = new Rectangle(point.X, point.Y, 5, 5);
            try
            {
                foreach (PGameObject obj2 in LazyLib.Wow.ObjectManager.GetGameObject)
                {
                    if (FindNode.IsHerb(obj2) || FindNode.IsMine(obj2))
                    {
                        float num = form.OffsetX(obj2.Location.X, LazyLib.Wow.ObjectManager.MyPlayer.Location.X);
                        Rectangle a = new Rectangle(form.OffsetY(obj2.Location.Y, LazyLib.Wow.ObjectManager.MyPlayer.Location.Y), (int) num, 5, 5);
                        if (Rectangle.Intersect(a, b) != Rectangle.Empty)
                        {
                            if (GatherBlackList.IsBlacklisted(obj2))
                            {
                                GatherBlackList.Unblacklist(obj2);
                                continue;
                            }
                            Logging.Write("Added the node to the permanent blacklist", new object[0]);
                            GatherBlackList.AddBadNode(obj2);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}

