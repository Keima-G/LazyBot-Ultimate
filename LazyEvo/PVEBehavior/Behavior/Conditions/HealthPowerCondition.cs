namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using DevComponents.Editors;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class HealthPowerCondition : AbstractCondition
    {
        private IntegerInput valueInput;

        public HealthPowerCondition()
        {
            this.ConditionTarget = ConditionTargetEnum.Player;
            this.ConditionType = ConditionTypeEnum.Health;
            this.Condition = ConditionEnum.EqualTo;
            this.Value = 50;
        }

        public HealthPowerCondition(ConditionTargetEnum conditionTarget, ConditionTypeEnum conditionType, ConditionEnum condition, int value)
        {
            this.ConditionTarget = conditionTarget;
            this.ConditionType = conditionType;
            this.Condition = condition;
            this.Value = value;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("LessThan", "Less Than ", "ConditionEnum", this.Condition.Equals(ConditionEnum.LessThan)));
            item.Nodes.Add(base.CreateRadioButton("EqualTo", "Equal To", "ConditionEnum", this.Condition.Equals(ConditionEnum.EqualTo)));
            item.Nodes.Add(base.CreateRadioButton("MoreThan", "More Than", "ConditionEnum", this.Condition.Equals(ConditionEnum.MoreThan)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionTarget(List<Node> re)
        {
            Node item = new Node {
                Text = "Check"
            };
            item.Nodes.Add(base.CreateRadioButton("Player", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Player)));
            item.Nodes.Add(base.CreateRadioButton("Pet", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Pet)));
            item.Nodes.Add(base.CreateRadioButton("Target", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Target)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionType(List<Node> re)
        {
            Node item = new Node {
                Text = "Type"
            };
            item.Nodes.Add(base.CreateRadioButton("Health", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.Health)));
            item.Nodes.Add(base.CreateRadioButton("Mana", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.Mana)));
            item.Nodes.Add(base.CreateRadioButton("Energy", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.Energy)));
            item.Nodes.Add(base.CreateRadioButton("Rage", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.Rage)));
            item.Nodes.Add(base.CreateRadioButton("RunicPower", "Runic Power", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.RunicPower)));
            item.Nodes.Add(base.CreateRadioButton("HolyPower", "Holy Power", "ConditionTypeEnum", this.ConditionType.Equals(ConditionTypeEnum.HolyPower)));
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
                Text = "This condition will allow you to check Health and Power values. <br/> ",
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
                Text = "Value in %"
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
            this.CreateConditionTarget(re);
            this.CreateConditionType(re);
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
                if (node.Name.Equals("ConditionTarget"))
                {
                    this.ConditionTarget = (ConditionTargetEnum) Enum.Parse(typeof(ConditionTargetEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("ConditionType"))
                {
                    this.ConditionType = (ConditionTypeEnum) Enum.Parse(typeof(ConditionTypeEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Value"))
                {
                    this.Value = Convert.ToInt32(node.InnerText);
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("ConditionTargetEnum"))
                {
                    this.ConditionTarget = (ConditionTargetEnum) Enum.Parse(typeof(ConditionTargetEnum), node.Name);
                }
                if (node.Tag.Equals("ConditionTypeEnum"))
                {
                    this.ConditionType = (ConditionTypeEnum) Enum.Parse(typeof(ConditionTypeEnum), node.Name);
                }
                if (node.Tag.Equals("ConditionEnum"))
                {
                    this.Condition = (ConditionEnum) Enum.Parse(typeof(ConditionEnum), node.Name);
                }
                if (node.Tag.Equals("Value"))
                {
                    IntegerInput hostedControl = (IntegerInput) node.HostedControl;
                    this.Value = hostedControl.Value;
                }
            }
        }

        public override string Name =>
            "Health/Power";

        public override string XmlName =>
            "HealthPowerCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<ConditionTarget>" + this.ConditionTarget + "</ConditionTarget>", "<ConditionType>", this.ConditionType, "</ConditionType>" };
                object[] objArray2 = new object[] { string.Concat(objArray), "<Condition>", this.Condition, "</Condition>" };
                object[] objArray3 = new object[] { string.Concat(objArray2), "<Value>", this.Value, "</Value>" };
                return string.Concat(objArray3);
            }
        }

        public override bool IsOk
        {
            get
            {
                PUnit myPlayer = null;
                switch (this.ConditionTarget)
                {
                    case ConditionTargetEnum.Player:
                        myPlayer = LazyLib.Wow.ObjectManager.MyPlayer;
                        break;

                    case ConditionTargetEnum.Pet:
                        myPlayer = LazyLib.Wow.ObjectManager.MyPlayer.Pet;
                        break;

                    case ConditionTargetEnum.Target:
                        myPlayer = LazyLib.Wow.ObjectManager.MyPlayer.Target;
                        break;

                    default:
                        break;
                }
                if (myPlayer == null)
                {
                    return false;
                }
                int health = -2147483648;
                switch (this.ConditionType)
                {
                    case ConditionTypeEnum.Health:
                        health = myPlayer.Health;
                        break;

                    case ConditionTypeEnum.Mana:
                        health = myPlayer.Mana;
                        break;

                    case ConditionTypeEnum.Energy:
                        health = myPlayer.Energy;
                        break;

                    case ConditionTypeEnum.Rage:
                        health = myPlayer.Rage;
                        break;

                    case ConditionTypeEnum.RunicPower:
                        health = myPlayer.RunicPower;
                        break;

                    case ConditionTypeEnum.HolyPower:
                        health = myPlayer.HolyPower;
                        break;

                    default:
                        break;
                }
                if (health == -2147483648)
                {
                    return false;
                }
                switch (this.Condition)
                {
                    case ConditionEnum.LessThan:
                        return (health < this.Value);

                    case ConditionEnum.EqualTo:
                        return (health == this.Value);

                    case ConditionEnum.MoreThan:
                        return (health > this.Value);
                }
                return true;
            }
        }

        private ConditionTargetEnum ConditionTarget { get; set; }

        private ConditionTypeEnum ConditionType { get; set; }

        private ConditionEnum Condition { get; set; }

        private int Value { get; set; }
    }
}

