namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.Editors;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class RuneCondition : AbstractCondition
    {
        private RuneEnum Rune;
        private IntegerInput valueInput;

        public RuneCondition()
        {
            this.Condition = ConditionEnum.MoreThan;
            this.Value = 1;
            this.Rune = RuneEnum.Blood;
        }

        public RuneCondition(ConditionEnum conditionEnum, RuneEnum runeEnum, int value)
        {
            this.Condition = conditionEnum;
            this.Value = value;
            this.Rune = runeEnum;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Player has "
            };
            item.Nodes.Add(base.CreateRadioButton("LessThan", "Less Than ", "ConditionEnum", this.Condition.Equals(ConditionEnum.LessThan)));
            item.Nodes.Add(base.CreateRadioButton("EqualTo", "Equal To", "ConditionEnum", this.Condition.Equals(ConditionEnum.EqualTo)));
            item.Nodes.Add(base.CreateRadioButton("MoreThan", "More Than", "ConditionEnum", this.Condition.Equals(ConditionEnum.MoreThan)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateRuneCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Rune(s) "
            };
            item.Nodes.Add(base.CreateRadioButton("Blood", "Blood ", "RuneEnum", this.Rune.Equals(RuneEnum.Blood)));
            item.Nodes.Add(base.CreateRadioButton("Frost", "Frost", "RuneEnum", this.Rune.Equals(RuneEnum.Frost)));
            item.Nodes.Add(base.CreateRadioButton("Unholy", "Unholy", "RuneEnum", this.Rune.Equals(RuneEnum.Unholy)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateValue(List<Node> re)
        {
            Node item = new Node {
                Text = "Value"
            };
            this.valueInput = new IntegerInput();
            this.valueInput.Value = this.Value;
            this.valueInput.ValueChanged += new EventHandler(this.IntegerInput_ValueChanged);
            item.Nodes.Add(base.CreateControl("Value", "Value", this.valueInput));
            item.Expanded = true;
            re.Add(item);
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateCondition(re);
            this.CreateValue(re);
            this.CreateRuneCondition(re);
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
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.InnerText);
                }
                if (node.Name.Equals("Value"))
                {
                    this.Value = Convert.ToInt32(node.InnerText);
                }
                if (node.Name.Equals("Rune"))
                {
                    this.Rune = (RuneEnum) Enum.Parse(typeof(RuneEnum), node.InnerText);
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("ConditionEnum"))
                {
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.Name);
                }
                if (node.Tag.Equals("RuneEnum"))
                {
                    this.Rune = (RuneEnum) Enum.Parse(typeof(RuneEnum), node.Name);
                }
                if (node.Tag.Equals("Value"))
                {
                    IntegerInput hostedControl = (IntegerInput) node.HostedControl;
                    this.Value = hostedControl.Value;
                }
            }
        }

        private ConditionEnum Condition { get; set; }

        private int Value { get; set; }

        public override string Name =>
            "Rune";

        public override string XmlName =>
            "RuneCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<Condition>" + this.Condition + "</Condition>", "<Value>", this.Value, "</Value>" };
                object[] objArray2 = new object[] { string.Concat(objArray), "<Rune>", this.Rune, "</Rune>" };
                return string.Concat(objArray2);
            }
        }

        public override bool IsOk
        {
            get
            {
                int num = 0;
                if (this.Rune.Equals(RuneEnum.Blood))
                {
                    if (LazyLib.Wow.ObjectManager.MyPlayer.BloodRune1Ready)
                    {
                        num++;
                    }
                    if (LazyLib.Wow.ObjectManager.MyPlayer.BloodRune2Ready)
                    {
                        num++;
                    }
                }
                if (this.Rune.Equals(RuneEnum.Frost))
                {
                    if (LazyLib.Wow.ObjectManager.MyPlayer.FrostRune1Ready)
                    {
                        num++;
                    }
                    if (LazyLib.Wow.ObjectManager.MyPlayer.FrostRune2Ready)
                    {
                        num++;
                    }
                }
                if (this.Rune.Equals(RuneEnum.Unholy))
                {
                    if (LazyLib.Wow.ObjectManager.MyPlayer.UnholyRune1Ready)
                    {
                        num++;
                    }
                    if (LazyLib.Wow.ObjectManager.MyPlayer.UnholyRune2Ready)
                    {
                        num++;
                    }
                }
                return (!this.Condition.Equals(ConditionEnum.EqualTo) ? (!this.Condition.Equals(ConditionEnum.LessThan) ? (this.Condition.Equals(ConditionEnum.MoreThan) && (num > this.Value)) : (num < this.Value)) : (num == this.Value));
            }
        }
    }
}

