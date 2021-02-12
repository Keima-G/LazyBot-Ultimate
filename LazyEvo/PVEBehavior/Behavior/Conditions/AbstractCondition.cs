namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using System.Xml;

    internal abstract class AbstractCondition
    {
        protected AbstractCondition()
        {
        }

        public Node CreateControl(string name, string tag, Control control) => 
            new Node { 
                Expanded = true,
                Name = name,
                Tag = tag,
                HostedControl = control,
                DragDropEnabled = false
            };

        public Node CreateRadioButton(string name, string tag, bool selected = false) => 
            this.CreateRadioButton(name, name, tag, selected);

        public Node CreateRadioButton(string name, string text, string tag, bool selected = false) => 
            new Node { 
                CheckBoxStyle = eCheckBoxStyle.RadioButton,
                CheckBoxVisible = true,
                Expanded = true,
                Name = name,
                Text = text,
                Tag = tag,
                Checked = selected,
                DragDropEnabled = false
            };

        public abstract List<Node> GetNodes();
        public abstract void LoadData(XmlNode xmlNode);
        public abstract void NodeClick(Node node);

        public abstract string Name { get; }

        public abstract string XmlName { get; }

        public abstract string GetXML { get; }

        public abstract bool IsOk { get; }
    }
}

