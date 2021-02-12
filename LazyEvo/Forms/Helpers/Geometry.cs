namespace LazyEvo.Forms.Helpers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    public class Geometry
    {
        public static void GeometryFromString(string thisWindowGeometry, Form formIn)
        {
            if (!string.IsNullOrEmpty(thisWindowGeometry))
            {
                string[] strArray = thisWindowGeometry.Split(new char[] { '|' });
                string str = strArray[4];
                if (str != "Normal")
                {
                    if (str == "Maximized")
                    {
                        formIn.Location = new Point(100, 100);
                        formIn.StartPosition = FormStartPosition.Manual;
                        formIn.WindowState = FormWindowState.Maximized;
                    }
                }
                else
                {
                    Point loc = new Point(int.Parse(strArray[0]), int.Parse(strArray[1]));
                    Size size = new Size(int.Parse(strArray[2]), int.Parse(strArray[3]));
                    bool flag2 = GeometryIsBizarreSize(size);
                    if (GeometryIsBizarreLocation(loc, size) && flag2)
                    {
                        formIn.Location = loc;
                        formIn.Size = size;
                        formIn.StartPosition = FormStartPosition.Manual;
                        formIn.WindowState = FormWindowState.Normal;
                    }
                    else if (flag2)
                    {
                        formIn.Size = size;
                    }
                }
            }
        }

        private static bool GeometryIsBizarreLocation(Point loc, Size size) => 
            ((loc.X >= 0) && (loc.Y >= 0)) && (((loc.X + size.Width) <= Screen.PrimaryScreen.WorkingArea.Width) ? ((loc.Y + size.Height) <= Screen.PrimaryScreen.WorkingArea.Height) : false);

        private static bool GeometryIsBizarreSize(Size size) => 
            (size.Height <= Screen.PrimaryScreen.WorkingArea.Height) && (size.Width <= Screen.PrimaryScreen.WorkingArea.Width);

        public static string GeometryToString(Form mainForm)
        {
            object[] objArray = new object[] { mainForm.Location.X, "|", mainForm.Location.Y, "|", mainForm.Size.Width, "|", mainForm.Size.Height, "|", mainForm.WindowState };
            return string.Concat(objArray);
        }
    }
}

