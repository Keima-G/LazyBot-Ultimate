namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.Editors;
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class TickerCondition : AbstractCondition
    {
        private Ticker _ticker = new Ticker(0.0);
        private IntegerInput valueInput;

        public TickerCondition()
        {
            this.Condition = TickerConditionEnum.Is;
            this.Value = 0;
            this._ticker = new Ticker(0.0);
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Ticker "
            };
            item.Nodes.Add(base.CreateRadioButton("Is", "Is Ready", "TickerConditionEnum", this.Condition.Equals(TickerConditionEnum.Is)));
            item.Nodes.Add(base.CreateRadioButton("Not", "Not Ready", "TickerConditionEnum", this.Condition.Equals(TickerConditionEnum.Not)));
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
                Text = "This condition will allow you to add a timer to your rule. <br/> To use the ticker condition simply specify how long the ticker should take to become ready. <br/> The ticker is always ready when starting the bot.",
                Visible = true,
                BackColor = Color.Transparent
            };
            item.Nodes.Add(base.CreateControl("Info", "Info", control));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateValue(List<Node> re)
        {
            Node item = new Node {
                Text = "Count downtime in milliseconds"
            };
            this.valueInput = new IntegerInput();
            this.valueInput.Value = this.Value;
            this.valueInput.ValueChanged += new EventHandler(this.IntegerInput_ValueChanged);
            item.Nodes.Add(base.CreateControl("Value", "Value", this.valueInput));
            item.Expanded = true;
            re.Add(item);
        }

        public void ForceReady()
        {
            this._ticker.ForceReady();
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateCondition(re);
            this.CreateValue(re);
            this.CreateText(re);
            return re;
        }

        private void IntegerInput_ValueChanged(object sender, EventArgs e)
        {
            this.Value = this.valueInput.Value;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (TickerConditionEnum) Enum.Parse(typeof(TickerConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Value"))
                {
                    this.Value = Convert.ToInt32(node.InnerText);
                    this._ticker = new Ticker((double) this.Value);
                    this._ticker.ForceReady();
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("TickerConditionEnum"))
                {
                    this.Condition = (TickerConditionEnum) Enum.Parse(typeof(TickerConditionEnum), node.Name);
                }
                if (node.Tag.Equals("Value"))
                {
                    IntegerInput hostedControl = (IntegerInput) node.HostedControl;
                    this.Value = hostedControl.Value;
                }
            }
        }

        public void Reset()
        {
            this._ticker.Reset();
        }

        private TickerConditionEnum Condition { get; set; }

        private int Value { get; set; }

        public override string Name =>
            "Ticker";

        public override string XmlName =>
            "TickerCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<Condition>" + this.Condition + "</Condition>", "<Value>", this.Value, "</Value>" };
                return string.Concat(objArray);
            }
        }

        public override bool IsOk =>
            !this.Condition.Equals(TickerConditionEnum.Is) ? (this.Condition.Equals(TickerConditionEnum.Not) && !this._ticker.IsReady) : this._ticker.IsReady;
    }
}

