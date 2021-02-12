namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using LazyLib;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    internal class BuffCondition : AbstractCondition
    {
        private TextBox valueInput;

        public BuffCondition()
        {
            this.ConditionTarget = ConditionTargetEnum.Player;
            this.Condition = BuffConditionEnum.HasBuff;
            this.ValueType = BuffValueEnum.Id;
            this.OwnerCondition = BuffOwnerConditionEnum.DoesNotMatter;
        }

        public BuffCondition(ConditionTargetEnum conditionTargetEnum, BuffConditionEnum buffConditionEnum, BuffValueEnum id, string value)
        {
            this.ConditionTarget = conditionTargetEnum;
            this.Condition = buffConditionEnum;
            this.ValueType = id;
            this.Value = value;
            this.OwnerCondition = BuffOwnerConditionEnum.DoesNotMatter;
        }

        private void CreateConditionCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("HasBuff", "Has Buff", "BuffConditionEnum", this.Condition.Equals(BuffConditionEnum.HasBuff)));
            item.Nodes.Add(base.CreateRadioButton("DoesNotHave", "Does Not Have Buff", "BuffConditionEnum", this.Condition.Equals(BuffConditionEnum.DoesNotHave)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionOwnerCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Owner"
            };
            item.Nodes.Add(base.CreateRadioButton("Owner", "I am owner", "BuffConditionOwnerEnum", this.OwnerCondition.Equals(BuffOwnerConditionEnum.Owner)));
            item.Nodes.Add(base.CreateRadioButton("NotOwner", "Other owner", "BuffConditionOwnerEnum", this.OwnerCondition.Equals(BuffOwnerConditionEnum.NotOwner)));
            item.Nodes.Add(base.CreateRadioButton("DoesNotMatter", "Does not matter", "BuffConditionOwnerEnum", this.OwnerCondition.Equals(BuffOwnerConditionEnum.DoesNotMatter)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionTarget(List<Node> re)
        {
            Node item = new Node {
                Text = "Check if"
            };
            item.Nodes.Add(base.CreateRadioButton("Player", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Player)));
            item.Nodes.Add(base.CreateRadioButton("Pet", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Pet)));
            item.Nodes.Add(base.CreateRadioButton("Target", "ConditionTargetEnum", this.ConditionTarget.Equals(ConditionTargetEnum.Target)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateConditionValue(List<Node> re)
        {
            Node item = new Node {
                Text = "Value"
            };
            item.Nodes.Add(base.CreateRadioButton("Id", "By Id", "BuffValueEnum", this.ValueType.Equals(BuffValueEnum.Id)));
            item.Nodes.Add(base.CreateRadioButton("Name", "By Name", "BuffValueEnum", this.ValueType.Equals(BuffValueEnum.Name)));
            item.Expanded = true;
            this.valueInput = new TextBox();
            this.valueInput.Text = this.Value;
            this.valueInput.TextChanged += new EventHandler(this.TB_ValueChanged);
            item.Nodes.Add(base.CreateControl("Value", "Value", this.valueInput));
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
                Text = "This condition will allow you to check for buffs on a specific unit/player. <br/><br/>You can get the id's from wowhead.com by searching for the name or use the 'Log own buffs' button in the debug tab<br/><br/> You can enter multiple id's separating each id using ',' (Do not use spaces). <br/><br/> If you are using the name function the bot will warn you if the name is invalid",
                Visible = true,
                BackColor = Color.Transparent
            };
            item.Nodes.Add(base.CreateControl("Info", "Info", control));
            item.Expanded = true;
            re.Add(item);
        }

        public string GetBuffName() => 
            !this.ValueType.Equals(BuffValueEnum.Name) ? string.Empty : this.Value;

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateConditionTarget(re);
            this.CreateConditionCondition(re);
            this.CreateConditionOwnerCondition(re);
            this.CreateConditionValue(re);
            this.CreateText(re);
            return re;
        }

        private bool HasBuffByName(PUnit target, string buffIds)
        {
            bool flag;
            string[] strArray2 = buffIds.Split(new char[] { ',' });
            int index = 0;
            while (true)
            {
                if (index >= strArray2.Length)
                {
                    return false;
                }
                string buff = strArray2[index];
                try
                {
                    switch (this.OwnerCondition)
                    {
                        case BuffOwnerConditionEnum.Owner:
                            if (!target.HasBuff(buff, true))
                            {
                                break;
                            }
                            return true;

                        case BuffOwnerConditionEnum.NotOwner:
                            if (!target.HasBuff(buff, false))
                            {
                                break;
                            }
                            return true;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (Exception)
                {
                }
                index++;
            }
            return flag;
        }

        private bool HasBuffId(PUnit target, string buffIds)
        {
            bool flag;
            string[] strArray2 = buffIds.Split(new char[] { ',' });
            int index = 0;
            while (true)
            {
                if (index >= strArray2.Length)
                {
                    return false;
                }
                string str = strArray2[index];
                try
                {
                    switch (this.OwnerCondition)
                    {
                        case BuffOwnerConditionEnum.Owner:
                            if (!target.HasBuff(Convert.ToInt32(str), true))
                            {
                                break;
                            }
                            return true;

                        case BuffOwnerConditionEnum.NotOwner:
                            if (!target.HasBuff(Convert.ToInt32(str), false))
                            {
                                break;
                            }
                            return true;

                        case BuffOwnerConditionEnum.DoesNotMatter:
                            if (!target.HasBuff(Convert.ToInt32(str), false))
                            {
                                break;
                            }
                            return true;

                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
                catch (ThreadAbortException)
                {
                }
                catch (Exception exception)
                {
                    Logging.Debug("Error checking buff: " + exception, new object[0]);
                }
                index++;
            }
            return flag;
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
                if (node.Name.Equals("OwnerCondition"))
                {
                    this.OwnerCondition = (BuffOwnerConditionEnum) Enum.Parse(typeof(BuffOwnerConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("ValueType"))
                {
                    this.ValueType = (BuffValueEnum) Enum.Parse(typeof(BuffValueEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (BuffConditionEnum) Enum.Parse(typeof(BuffConditionEnum), node.InnerText);
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
                if (node.Tag.Equals("ConditionTargetEnum"))
                {
                    this.ConditionTarget = (ConditionTargetEnum) Enum.Parse(typeof(ConditionTargetEnum), node.Name);
                }
                if (node.Tag.Equals("BuffConditionOwnerEnum"))
                {
                    this.OwnerCondition = (BuffOwnerConditionEnum) Enum.Parse(typeof(BuffOwnerConditionEnum), node.Name);
                }
                if (node.Tag.Equals("BuffConditionEnum"))
                {
                    this.Condition = (BuffConditionEnum) Enum.Parse(typeof(BuffConditionEnum), node.Name);
                }
                if (node.Tag.Equals("BuffValueEnum"))
                {
                    this.ValueType = (BuffValueEnum) Enum.Parse(typeof(BuffValueEnum), node.Name);
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
            this.Value = this.valueInput.Text;
        }

        private ConditionTargetEnum ConditionTarget { get; set; }

        private BuffOwnerConditionEnum OwnerCondition { get; set; }

        private BuffConditionEnum Condition { get; set; }

        private BuffValueEnum ValueType { get; set; }

        private string Value { get; set; }

        public override string Name =>
            "Buff detection";

        public override string XmlName =>
            "BuffCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<ConditionTarget>" + this.ConditionTarget + "</ConditionTarget>", "<Condition>", this.Condition, "</Condition>" };
                object[] objArray2 = new object[] { string.Concat(objArray), "<OwnerCondition>", this.OwnerCondition, "</OwnerCondition>" };
                object[] objArray3 = new object[] { string.Concat(objArray2), "<ValueType>", this.ValueType, "</ValueType>" };
                return (string.Concat(objArray3) + "<Value>" + this.Value + "</Value>");
            }
        }

        public override bool IsOk
        {
            get
            {
                PUnit target = null;
                switch (this.ConditionTarget)
                {
                    case ConditionTargetEnum.Player:
                        target = LazyLib.Wow.ObjectManager.MyPlayer;
                        break;

                    case ConditionTargetEnum.Pet:
                        target = LazyLib.Wow.ObjectManager.MyPlayer.Pet;
                        break;

                    case ConditionTargetEnum.Target:
                        target = LazyLib.Wow.ObjectManager.MyPlayer.Target;
                        break;

                    default:
                        break;
                }
                if (target == null)
                {
                    return false;
                }
                switch (this.Condition)
                {
                    case BuffConditionEnum.HasBuff:
                        return (!this.ValueType.Equals(BuffValueEnum.Name) ? this.HasBuffId(target, this.Value) : this.HasBuffByName(target, this.Value));

                    case BuffConditionEnum.DoesNotHave:
                        return (!this.ValueType.Equals(BuffValueEnum.Name) ? !this.HasBuffId(target, this.Value) : !this.HasBuffByName(target, this.Value));
                }
                return true;
            }
        }
    }
}

