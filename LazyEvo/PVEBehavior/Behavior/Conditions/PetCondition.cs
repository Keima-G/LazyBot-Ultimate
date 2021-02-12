namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class PetCondition : AbstractCondition
    {
        public PetCondition()
        {
            this.Condition = PetConditionEnum.DoesNotHave;
        }

        public PetCondition(PetConditionEnum petConditionEnum)
        {
            this.Condition = petConditionEnum;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("DoesHave", "Has Pet", "PetConditionEnum", this.Condition.Equals(PetConditionEnum.DoesHave)));
            item.Nodes.Add(base.CreateRadioButton("DoesNotHave", "Does not have Pet", "PetConditionEnum", this.Condition.Equals(PetConditionEnum.DoesNotHave)));
            item.Expanded = true;
            re.Add(item);
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateCondition(re);
            return re;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (PetConditionEnum) Enum.Parse(typeof(PetConditionEnum), node.InnerText);
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && ((node.Tag != null) && node.Tag.Equals("PetConditionEnum")))
            {
                this.Condition = (PetConditionEnum) Enum.Parse(typeof(PetConditionEnum), node.Name);
            }
        }

        private PetConditionEnum Condition { get; set; }

        public override string Name =>
            "Has Pet";

        public override string XmlName =>
            "PetCondition";

        public override string GetXML =>
            "<Condition>" + this.Condition + "</Condition>";

        public override bool IsOk =>
            !this.Condition.Equals(PetConditionEnum.DoesHave) ? !LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet : LazyLib.Wow.ObjectManager.MyPlayer.HasLivePet;
    }
}

