namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using DevComponents.DotNetBar;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class FunctionsCondition : AbstractCondition
    {
        public FunctionsCondition()
        {
            this.ConditionTarget = ConditionTargetEnum.Target;
            this.Condition = FunctionsConditionEnum.Is;
            this.Function = FunctionEnum.Casting;
        }

        public FunctionsCondition(ConditionTargetEnum target, FunctionsConditionEnum doing, FunctionEnum function)
        {
            this.ConditionTarget = target;
            this.Condition = doing;
            this.Function = function;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("Is", "FunctionsConditionEnum", this.Condition.Equals(FunctionsConditionEnum.Is)));
            item.Nodes.Add(base.CreateRadioButton("Not", "FunctionsConditionEnum", this.Condition.Equals(FunctionsConditionEnum.Not)));
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

        private void CreateFunction(List<Node> re)
        {
            Node item = new Node {
                Text = "Function"
            };
            item.Nodes.Add(base.CreateRadioButton("InCombat", "In Combat", "FunctionEnum", this.Function.Equals(FunctionEnum.InCombat)));
            item.Nodes.Add(base.CreateRadioButton("Casting", "FunctionEnum", this.Function.Equals(FunctionEnum.Casting)));
            item.Nodes.Add(base.CreateRadioButton("FacingAway", "Facing Away", "FunctionEnum", this.Function.Equals(FunctionEnum.FacingAway)));
            item.Nodes.Add(base.CreateRadioButton("Fleeing", "Fleeing", "FunctionEnum", this.Function.Equals(FunctionEnum.Fleeing)));
            item.Nodes.Add(base.CreateRadioButton("IsStunned", "Stunned", "FunctionEnum", this.Function.Equals(FunctionEnum.IsStunned)));
            item.Nodes.Add(base.CreateRadioButton("IsPlayer", "Player", "FunctionEnum", this.Function.Equals(FunctionEnum.IsPlayer)));
            item.Nodes.Add(base.CreateRadioButton("IsPet", "Pet", "FunctionEnum", this.Function.Equals(FunctionEnum.IsPet)));
            item.Nodes.Add(base.CreateRadioButton("IsAutoAttacking", "Auto Attacking", "FunctionEnum", this.Function.Equals(FunctionEnum.IsAutoAttacking)));
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
                Text = "This condition will allow you to call a true/false function.",
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
            this.CreateConditionTarget(re);
            this.CreateCondition(re);
            this.CreateFunction(re);
            this.CreateText(re);
            return re;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("ConditionTarget"))
                {
                    this.ConditionTarget = (ConditionTargetEnum) Enum.Parse(typeof(ConditionTargetEnum), node.InnerText);
                }
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (FunctionsConditionEnum) Enum.Parse(typeof(FunctionsConditionEnum), node.InnerText);
                }
                if (node.Name.Equals("ConditionFunction"))
                {
                    this.Function = (FunctionEnum) Enum.Parse(typeof(FunctionEnum), node.InnerText);
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
                if (node.Tag.Equals("FunctionsConditionEnum"))
                {
                    this.Condition = (FunctionsConditionEnum) Enum.Parse(typeof(FunctionsConditionEnum), node.Name);
                }
                if (node.Tag.Equals("FunctionEnum"))
                {
                    this.Function = (FunctionEnum) Enum.Parse(typeof(FunctionEnum), node.Name);
                }
            }
        }

        private FunctionsConditionEnum Condition { get; set; }

        private ConditionTargetEnum ConditionTarget { get; set; }

        private FunctionEnum Function { get; set; }

        public override string Name =>
            "Function";

        public override string XmlName =>
            "FunctionCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<Condition>" + this.Condition + "</Condition>", "<ConditionTarget>", this.ConditionTarget, "</ConditionTarget>" };
                object[] objArray2 = new object[] { string.Concat(objArray), "<ConditionFunction>", this.Function, "</ConditionFunction>" };
                return string.Concat(objArray2);
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
                if (myPlayer != null)
                {
                    bool flag = this.Condition.Equals(FunctionsConditionEnum.Is);
                    switch (this.Function)
                    {
                        case FunctionEnum.InCombat:
                            return (myPlayer.IsInCombat == flag);

                        case FunctionEnum.Casting:
                            return (myPlayer.IsCasting == flag);

                        case FunctionEnum.FacingAway:
                            return (myPlayer.IsFacingAway == flag);

                        case FunctionEnum.Fleeing:
                            return (myPlayer.IsFleeing == flag);

                        case FunctionEnum.IsStunned:
                            return (myPlayer.IsStunned == flag);

                        case FunctionEnum.IsPlayer:
                            return (myPlayer.IsPlayer == flag);

                        case FunctionEnum.IsPet:
                            return (myPlayer.IsPet == flag);

                        case FunctionEnum.IsAutoAttacking:
                            return (myPlayer.IsAutoAttacking == flag);
                    }
                }
                return false;
            }
        }
    }
}

