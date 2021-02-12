namespace LazyEvo.Plugins.RotationPlugin
{
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    public class Rotation
    {
        internal bool Active;
        internal bool Alt;
        internal bool Ctrl;
        internal int GlobalCooldown = 0x640;
        internal string Key = string.Empty;
        internal string Name;
        internal RuleController Rules = new RuleController();
        internal bool Shift;
        internal bool Windows;

        public void Load(XmlNode rotation)
        {
            this.Rules = new RuleController();
            this.GlobalCooldown = 0x640;
            try
            {
                foreach (XmlNode node in rotation.ChildNodes)
                {
                    string name = node.Name;
                    if (name != null)
                    {
                        int num;
                        if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041b-1 == null)
                        {
                            Dictionary<string, int> dictionary1 = new Dictionary<string, int>(9);
                            dictionary1.Add("Name", 0);
                            dictionary1.Add("GlobalCooldown", 1);
                            dictionary1.Add("Active", 2);
                            dictionary1.Add("Ctrl", 3);
                            dictionary1.Add("Alt", 4);
                            dictionary1.Add("Shift", 5);
                            dictionary1.Add("Key", 6);
                            dictionary1.Add("Windows", 7);
                            dictionary1.Add("Rules", 8);
                            <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041b-1 = dictionary1;
                        }
                        if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041b-1.TryGetValue(name, out num))
                        {
                            switch (num)
                            {
                                case 0:
                                    this.Name = node.InnerText;
                                    break;

                                case 1:
                                    this.GlobalCooldown = Convert.ToInt32(node.InnerText);
                                    break;

                                case 2:
                                    this.Active = Convert.ToBoolean(node.InnerText);
                                    break;

                                case 3:
                                    this.Ctrl = Convert.ToBoolean(node.InnerText);
                                    break;

                                case 4:
                                    this.Alt = Convert.ToBoolean(node.InnerText);
                                    break;

                                case 5:
                                    this.Shift = Convert.ToBoolean(node.InnerText);
                                    break;

                                case 6:
                                    this.Key = node.InnerText;
                                    break;

                                case 7:
                                    this.Windows = Convert.ToBoolean(node.InnerText);
                                    break;

                                case 8:
                                    this.LoadController(node.ChildNodes, this.Rules);
                                    break;

                                default:
                                    break;
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Debug("Something went wrong when loading Rotation: " + exception, new object[0]);
            }
        }

        internal AbstractCondition LoadConditions(XmlNode xmlNode)
        {
            string name = xmlNode.Name;
            if (name != null)
            {
                int num;
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041d-1 == null)
                {
                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(0x10);
                    dictionary1.Add("HealthPowerCondition", 0);
                    dictionary1.Add("BuffCondition", 1);
                    dictionary1.Add("CombatCountCondition", 2);
                    dictionary1.Add("DistanceToTargetCondition", 3);
                    dictionary1.Add("SoulShardCountCondition", 4);
                    dictionary1.Add("HealthStoneCountCondition", 5);
                    dictionary1.Add("ComboPointsCondition", 6);
                    dictionary1.Add("MageWaterCondition", 7);
                    dictionary1.Add("MageFoodCondition", 8);
                    dictionary1.Add("TempEnchantCondition", 9);
                    dictionary1.Add("RuneCondition", 10);
                    dictionary1.Add("PotentialAddsCondition", 11);
                    dictionary1.Add("FunctionCondition", 12);
                    dictionary1.Add("TickerCondition", 13);
                    dictionary1.Add("PetCondition", 14);
                    dictionary1.Add("SpellCondition", 15);
                    <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041d-1 = dictionary1;
                }
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041d-1.TryGetValue(name, out num))
                {
                    AbstractCondition condition;
                    switch (num)
                    {
                        case 0:
                            condition = new HealthPowerCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 1:
                            condition = new BuffCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 2:
                            condition = new CombatCountCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 3:
                            condition = new DistanceToTarget();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 4:
                            condition = new SoulShardCountCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 5:
                            condition = new HealthStoneCount();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 6:
                            condition = new ComboPointsCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 7:
                            condition = new MageWaterCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 8:
                            condition = new MageFoodCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 9:
                            condition = new TempEnchantCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 10:
                            condition = new RuneCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 11:
                            condition = new PotentialAddsCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 12:
                            condition = new FunctionsCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 13:
                            condition = new TickerCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 14:
                            condition = new PetCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        case 15:
                            condition = new SpellCondition();
                            condition.LoadData(xmlNode);
                            return condition;

                        default:
                            break;
                    }
                }
            }
            return null;
        }

        internal void LoadController(XmlNodeList xmlNodeList, RuleController ruleController)
        {
            foreach (XmlNode node in xmlNodeList)
            {
                if (node.Name.Equals("Rule"))
                {
                    Rule rule = new Rule();
                    foreach (XmlNode node2 in node)
                    {
                        string name = node2.Name;
                        if (name != null)
                        {
                            int num;
                            if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041c-1 == null)
                            {
                                Dictionary<string, int> dictionary1 = new Dictionary<string, int>(6);
                                dictionary1.Add("Name", 0);
                                dictionary1.Add("Script", 1);
                                dictionary1.Add("MatchAll", 2);
                                dictionary1.Add("ShouldTarget", 3);
                                dictionary1.Add("Priority", 4);
                                dictionary1.Add("Action", 5);
                                <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041c-1 = dictionary1;
                            }
                            if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x600041c-1.TryGetValue(name, out num))
                            {
                                switch (num)
                                {
                                    case 0:
                                    {
                                        rule.Name = node2.InnerText;
                                        continue;
                                    }
                                    case 1:
                                    {
                                        rule.Script = node2.InnerText;
                                        continue;
                                    }
                                    case 2:
                                    {
                                        rule.MatchAll = Convert.ToBoolean(node2.InnerText);
                                        continue;
                                    }
                                    case 3:
                                    {
                                        rule.ShouldTarget = (Target) Enum.Parse(typeof(Target), node2.InnerText);
                                        continue;
                                    }
                                    case 4:
                                    {
                                        rule.Priority = Convert.ToInt32(node2.InnerText);
                                        continue;
                                    }
                                    case 5:
                                    {
                                        rule.LoadAction(node2);
                                        continue;
                                    }
                                    default:
                                        break;
                                }
                            }
                        }
                        AbstractCondition condition = this.LoadConditions(node2);
                        if (condition != null)
                        {
                            rule.AddCondition(condition);
                        }
                    }
                    ruleController.AddRule(rule);
                }
            }
        }

        public void ResetControllers()
        {
            this.Rules = new RuleController();
        }

        internal string Save()
        {
            Func<string, Rule, string> func = null;
            object[] objArray = new object[] { (string.Empty + "<Rotation>") + "<Name>" + this.Name + "</Name>", "<Active>", this.Active, "</Active>" };
            object[] objArray2 = new object[] { string.Concat(objArray), "<Ctrl>", this.Ctrl, "</Ctrl>" };
            object[] objArray3 = new object[] { string.Concat(objArray2), "<Alt>", this.Alt, "</Alt>" };
            object[] objArray4 = new object[] { string.Concat(objArray3), "<Shift>", this.Shift, "</Shift>" };
            object[] objArray5 = new object[] { string.Concat(objArray4), "<Windows>", this.Windows, "</Windows>" };
            object[] objArray6 = new object[] { string.Concat(objArray5) + "<Key>" + this.Key + "</Key>", "<GlobalCooldown>", this.GlobalCooldown, "</GlobalCooldown>" };
            string str = string.Concat(objArray6);
            if (this.Rules != null)
            {
                str = str + "<Rules>";
                if (func == null)
                {
                    func = (current, rule) => current + this.SaveRule(rule);
                }
                str = Enumerable.Aggregate<Rule, string>(this.Rules.GetRules, str, func) + "</Rules>";
            }
            return (str + "</Rotation>");
        }

        private string SaveRule(Rule rule)
        {
            object[] objArray = new object[] { ("<Rule>" + "<Name>" + rule.Name + "</Name>") + "<Script><![CDATA[" + rule.Script + "]]></Script>", "<MatchAll>", rule.MatchAll, "</MatchAll>" };
            object[] objArray2 = new object[] { string.Concat(objArray), "<ShouldTarget>", rule.ShouldTarget, "</ShouldTarget>" };
            string str = string.Concat(new object[] { string.Concat(objArray2), "<Priority>", rule.Priority, "</Priority>" }) + rule.SaveAction();
            foreach (AbstractCondition condition in rule.GetConditions)
            {
                str = ((str + "<" + condition.XmlName + ">") + condition.GetXML) + "</" + condition.XmlName + ">";
            }
            return (str + "</Rule>");
        }
    }
}

