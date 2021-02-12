namespace LazyEvo.PVEBehavior.Behavior.Conditions
{
    using DevComponents.AdvTree;
    using LazyLib.Wow;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Xml;

    internal class TempEnchantCondition : AbstractCondition
    {
        public TempEnchantCondition()
        {
            this.Weapon = WeaponEnum.MainHand;
            this.Condition = TempEnchantConditionEnum.DoesNotHave;
        }

        private void CreateCondition(List<Node> re)
        {
            Node item = new Node {
                Text = "Condition"
            };
            item.Nodes.Add(base.CreateRadioButton("DoesHave", "Has temporary enchant", "TempEnchantConditionEnum", this.Condition.Equals(TempEnchantConditionEnum.DoesHave)));
            item.Nodes.Add(base.CreateRadioButton("DoesNotHave", "Does not have temporary enchant", "TempEnchantConditionEnum", this.Condition.Equals(TempEnchantConditionEnum.DoesNotHave)));
            item.Expanded = true;
            re.Add(item);
        }

        private void CreateTarget(List<Node> re)
        {
            Node item = new Node {
                Text = "Weapon"
            };
            item.Nodes.Add(base.CreateRadioButton("MainHand", "Main Hand", "WeaponEnum", this.Weapon.Equals(WeaponEnum.MainHand)));
            item.Nodes.Add(base.CreateRadioButton("OffHand", "Off Hand", "WeaponEnum", this.Weapon.Equals(WeaponEnum.OffHand)));
            item.Expanded = true;
            re.Add(item);
        }

        public override List<Node> GetNodes()
        {
            List<Node> re = new List<Node>();
            this.CreateCondition(re);
            this.CreateTarget(re);
            return re;
        }

        public override void LoadData(XmlNode xmlNode)
        {
            foreach (XmlNode node in xmlNode.ChildNodes)
            {
                if (node.Name.Equals("Condition"))
                {
                    this.Condition = (TempEnchantConditionEnum) Enum.Parse(typeof(TempEnchantConditionEnum), node.InnerText);
                    continue;
                }
                if (node.Name.Equals("Weapon"))
                {
                    this.Weapon = (WeaponEnum) Enum.Parse(typeof(WeaponEnum), node.InnerText);
                }
            }
        }

        public override void NodeClick(Node node)
        {
            if ((node != null) && (node.Tag != null))
            {
                if (node.Tag.Equals("TempEnchantConditionEnum"))
                {
                    this.Condition = (TempEnchantConditionEnum) Enum.Parse(typeof(TempEnchantConditionEnum), node.Name);
                }
                if (node.Tag.Equals("WeaponEnum"))
                {
                    this.Weapon = (WeaponEnum) Enum.Parse(typeof(WeaponEnum), node.Name);
                }
            }
        }

        private TempEnchantConditionEnum Condition { get; set; }

        private WeaponEnum Weapon { get; set; }

        public override string Name =>
            "Has temporary enchant";

        public override string XmlName =>
            "TempEnchantCondition";

        public override string GetXML
        {
            get
            {
                object[] objArray = new object[] { "<Condition>" + this.Condition + "</Condition>", "<Weapon>", this.Weapon, "</Weapon>" };
                return string.Concat(objArray);
            }
        }

        public override bool IsOk =>
            !this.Weapon.Equals(WeaponEnum.MainHand) ? ((!this.Condition.Equals(TempEnchantConditionEnum.DoesHave) || !LazyLib.Wow.ObjectManager.MyPlayer.OffHandHasTempEnchant) ? (this.Condition.Equals(TempEnchantConditionEnum.DoesNotHave) && !LazyLib.Wow.ObjectManager.MyPlayer.OffHandHasTempEnchant) : true) : ((!this.Condition.Equals(TempEnchantConditionEnum.DoesHave) || !LazyLib.Wow.ObjectManager.MyPlayer.MainHandHasTempEnchant) ? (this.Condition.Equals(TempEnchantConditionEnum.DoesNotHave) && !LazyLib.Wow.ObjectManager.MyPlayer.MainHandHasTempEnchant) : true);
    }
}

