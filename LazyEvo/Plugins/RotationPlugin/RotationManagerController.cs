namespace LazyEvo.Plugins.RotationPlugin
{
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    internal class RotationManagerController
    {
        internal List<Rotation> Rotations = new List<Rotation>();
        private XmlDocument _doc;

        public void Load(string fileToLoad)
        {
            try
            {
                this.Name = Path.GetFileNameWithoutExtension(fileToLoad);
                this._doc = new XmlDocument();
                this._doc.Load(fileToLoad);
            }
            catch (Exception exception)
            {
                Logging.Debug("Error loading the rotation manager: " + exception, new object[0]);
                return;
            }
            try
            {
                foreach (XmlNode node in this._doc.GetElementsByTagName("RotationManager")[0])
                {
                    string str;
                    if (((str = node.Name) != null) && (str == "Rotation"))
                    {
                        Rotation item = new Rotation();
                        item.Load(node);
                        this.Rotations.Add(item);
                    }
                }
            }
            catch (Exception exception2)
            {
                Logging.Write("Error loading the rotation manager: " + exception2, new object[0]);
            }
        }

        public void ResetControllers()
        {
            this.Rotations = new List<Rotation>();
        }

        internal void Save()
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                if (!Directory.Exists(RotationManagerForm.OurDirectory + @"\Rotations\"))
                {
                    Directory.CreateDirectory(RotationManagerForm.OurDirectory + @"\Rotations\");
                }
                string xml = ("<?xml version=\"1.0\"?>" + "<RotationManager>") + "<Name>" + this.Name + "</Name>";
                xml = Enumerable.Aggregate<Rotation, string>(this.Rotations, xml, (current, rotation) => current + rotation.Save()) + "</RotationManager>";
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(xml);
                    document.Save(RotationManagerForm.OurDirectory + @"\Rotations\" + this.Name + ".xml");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not save rotation " + exception);
                }
            }
        }

        internal string Name { get; set; }
    }
}

