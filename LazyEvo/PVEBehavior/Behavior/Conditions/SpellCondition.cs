namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using LazyLib.ActionBar;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    internal class SpellCondition : AbstractCondition
    {
        private TextBox _valueInput;

        public SpellCondition()
        {
            this.Condition = SpellConditionEnum.Ready;
        }

        private void CreateConditionCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("Ready", "Ready", "SpellConditionEnum", this.Condition.Equals(SpellConditionEnum.Ready)));
            item.Nodes.Add(base.CreateRadioButton("NotReady", "Not ready", "SpellConditionEnum", this.Condition.Equals(SpellConditionEnum.NotReady)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionValue(List<Node> re)
        {
            Node item = new Node {
                Text = "Name"
            };
            this._valueInput = new TextBox();
            this._valueInput.Text = this.Value;
            this._valueInput.TextChanged += new EventHandler(this.TB_ValueChanged);
            item.Nodes.Add(base.CreateControl("Value", "Value", this._valueInput));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateText(List<Node> re)
        {
            Node item = new Node {
                Text = "Info"
            };
            LabelX control = new LabelX {
                AutoSize = true,
                MaximumSize = new Size(300, 500),
                Text = "This condition will allow you to check i a spell is ready",
                Visible = true,
                BackColor = Color.Transparent
            };
            item.Nodes.Add(base.CreateControl("Info", "Info", control));
            item.Expanded = true;
            re.Add(item);
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateConditionCondition(re);
            this.CreateConditionValue(re);
            this.CreateText(re);
            return re;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (SpellConditionEnum) Enum.Parse(typeof(SpellConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Value"))
                {
                    this.Value = node.InnerText;
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("SpellConditionEnum"))
                {
                    this.Condition = (SpellConditionEnum) Enum.Parse(typeof(SpellConditionEnum), node.Name);
                }
                if (node.Tag.Equals("Value"))
                {
                    TextBox hostedControl = (TextBox) node.HostedControl;
                    this.Value = hostedControl.Text;
                }
            }
        }

        private void TB_ValueChanged(object sender, EventArgs e)
        {
            this.Value = this._valueInput.Text;
        }

        private SpellConditionEnum Condition { get; set; }

        private string Value { get; set; }

        public override string Name =>
            "Spell Detection";

        public override string XmlName =>
            "SpellCondition";

        public override string GetXML =>
            ("<Condition>" + this.Condition + "</Condition>") + "<Value>" + this.Value + "</Value>";

        public override bool IsOk
        {
            get
            {
                switch (this.Condition)
                {
                    case SpellConditionEnum.Ready:
                        return BarMapper.IsSpellReadyByName(this.Value);

                    case SpellConditionEnum.NotReady:
                        return !BarMapper.IsSpellReadyByName(this.Value);
                }
                return false;
            }
        }
    }
}

