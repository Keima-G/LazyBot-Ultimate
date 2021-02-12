namespace LazyEvo.Plugins.ExtraLazy
{
    using LazyLib.Helpers;
    using System;

    public class FrameViewer
    {
        private frmFrameDumper dumper = new frmFrameDumper();

        public FrameViewer()
        {
            foreach (Frame frame in InterfaceHelper.GetFrames)
            {
                this.dumper.addFrame(frame.GetName + " " + frame.GetText);
            }
            this.dumper.Show();
        }

        public static void getChildren(string name)
        {
            frmFrameDumper dumper = new frmFrameDumper();
            try
            {
                foreach (Frame frame in InterfaceHelper.GetFrameByName(name).GetChilds)
                {
                    dumper.addFrame(frame.GetName + " " + frame.GetText);
                }
                dumper.Show();
            }
            catch
            {
            }
        }
    }
}

