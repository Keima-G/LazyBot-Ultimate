namespace LazyEvo.PVEBehavior.Behavior
{
    using LazyEvo.PVEBehavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;
    using System.Xml;

    internal class BehaviorController
    {
        private XmlDocument _doc;

        public void Load(string fileToLoad)
        {
            this.CombatController = new RuleController();
            this.PullController = new RuleController();
            this.BuffController = new RuleController();
            this.RestController = new RuleController();
            this.PrePullController = new RuleController();
            this.GlobalCooldown = 0x640;
            try
            {
                this.Name = Path.GetFileNameWithoutExtension(fileToLoad);
                this._doc = new XmlDocument();
                this._doc.Load(fileToLoad);
            }
            catch (Exception exception)
            {
                Logging.Debug("Error in loaded behavior: " + exception, new object[0]);
                return;
            }
            try
            {
                try
                {
                    this.CombatDistance = Convert.ToInt32(this._doc.GetElementsByTagName("CombatDistance")[0].InnerText);
                    try
                    {
                        this.PullDistance = Convert.ToInt32(this._doc.GetElementsByTagName("PullDistance")[0].InnerText);
                    }
                    catch
                    {
                        this.PullDistance = 0x19;
                    }
                    try
                    {
                        this.PrePullDistance = Convert.ToInt32(this._doc.GetElementsByTagName("PrePullDistance")[0].InnerText);
                    }
                    catch
                    {
                        this.PrePullDistance = 30;
                    }
                    this.UseAutoAttack = Convert.ToBoolean(this._doc.GetElementsByTagName("UseAutoAttack")[0].InnerText);
                    this.SendPet = Convert.ToBoolean(this._doc.GetElementsByTagName("SendPet")[0].InnerText);
                    try
                    {
                        this.GlobalCooldown = Convert.ToInt32(this._doc.GetElementsByTagName("GlobalCooldown")[0].InnerText);
                    }
                    catch
                    {
                        this.GlobalCooldown = 0x7d0;
                    }
                }
                catch
                {
                }
                this.LoadController(this._doc.GetElementsByTagName("CombatController"), this.CombatController);
                this.LoadController(this._doc.GetElementsByTagName("BuffController"), this.BuffController);
                this.LoadController(this._doc.GetElementsByTagName("RestController"), this.RestController);
                this.LoadController(this._doc.GetElementsByTagName("PullController"), this.PullController);
                this.LoadController(this._doc.GetElementsByTagName("PrePullController"), this.PrePullController);
            }
            catch (Exception exception2)
            {
                Logging.Debug("Something went wrong when loading behavior: " + exception2, new object[0]);
            }
        }

        internal AbstractCondition LoadConditions(XmlNode xmlNode)
        {
            string name = xmlNode.Name;
            if (name != null)
            {
                int num;
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bd-1 == null)
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
                    <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bd-1 = dictionary1;
                }
                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bd-1.TryGetValue(name, out num))
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
                foreach (XmlNode node2 in node.ChildNodes)
                {
                    if (node2.Name.Equals("Rule"))
                    {
                        Rule rule = new Rule();
                        foreach (XmlNode node3 in node2)
                        {
                            string name = node3.Name;
                            if (name != null)
                            {
                                int num;
                                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bc-1 == null)
                                {
                                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(6);
                                    dictionary1.Add("Name", 0);
                                    dictionary1.Add("Script", 1);
                                    dictionary1.Add("MatchAll", 2);
                                    dictionary1.Add("ShouldTarget", 3);
                                    dictionary1.Add("Priority", 4);
                                    dictionary1.Add("Action", 5);
                                    <PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bc-1 = dictionary1;
                                }
                                if (<PrivateImplementationDetails>{2B85B033-9903-4358-844B-70F0E312C212}.$$method0x60004bc-1.TryGetValue(name, out num))
                                {
                                    switch (num)
                                    {
                                        case 0:
                                        {
                                            rule.Name = node3.InnerText;
                                            continue;
                                        }
                                        case 1:
                                        {
                                            rule.Script = node3.InnerText;
                                            continue;
                                        }
                                        case 2:
                                        {
                                            rule.MatchAll = Convert.ToBoolean(node3.InnerText);
                                            continue;
                                        }
                                        case 3:
                                        {
                                            rule.ShouldTarget = (Target) Enum.Parse(typeof(Target), node3.InnerText);
                                            continue;
                                        }
                                        case 4:
                                        {
                                            rule.Priority = Convert.ToInt32(node3.InnerText);
                                            continue;
                                        }
                                        case 5:
                                        {
                                            rule.LoadAction(node3);
                                            continue;
                                        }
                                        default:
                                            break;
                                    }
                                }
                            }
                            AbstractCondition condition = this.LoadConditions(node3);
                            if (condition != null)
                            {
                                rule.AddCondition(condition);
                            }
                        }
                        ruleController.AddRule(rule);
                    }
                }
            }
        }

        public void ResetControllers()
        {
            this.CombatController = new RuleController();
            this.PullController = new RuleController();
            this.BuffController = new RuleController();
            this.RestController = new RuleController();
            this.PrePullController = new RuleController();
        }

        internal void Save()
        {
            if (!string.IsNullOrEmpty(this.Name))
            {
                if (!Directory.Exists(PVEBehaviorCombat.OurDirectory + @"\Behaviors\"))
                {
                    Directory.CreateDirectory(PVEBehaviorCombat.OurDirectory + @"\Behaviors\");
                }
                object[] objArray = new object[] { "<?xml version=\"1.0\"?>" + "<Behavior>", "<CombatDistance>", this.CombatDistance, "</CombatDistance>" };
                object[] objArray2 = new object[] { string.Concat(objArray), "<PullDistance>", this.PullDistance, "</PullDistance>" };
                object[] objArray3 = new object[] { string.Concat(objArray2), "<PrePullDistance>", this.PrePullDistance, "</PrePullDistance>" };
                object[] objArray4 = new object[] { string.Concat(objArray3), "<UseAutoAttack>", this.UseAutoAttack, "</UseAutoAttack>" };
                object[] objArray5 = new object[] { string.Concat(objArray4), "<SendPet>", this.SendPet, "</SendPet>" };
                object[] objArray6 = new object[] { string.Concat(objArray5), "<GlobalCooldown>", this.GlobalCooldown, "</GlobalCooldown>" };
                string xml = string.Concat(objArray6);
                if (this.CombatController != null)
                {
                    xml = xml + "<PrePullController>";
                    foreach (Rule rule in this.PrePullController.GetRules)
                    {
                        xml = xml + this.SaveRule(rule);
                    }
                    xml = xml + "</PrePullController>" + "<PullController>";
                    foreach (Rule rule2 in this.PullController.GetRules)
                    {
                        xml = xml + this.SaveRule(rule2);
                    }
                    xml = xml + "</PullController>" + "<CombatController>";
                    foreach (Rule rule3 in this.CombatController.GetRules)
                    {
                        xml = xml + this.SaveRule(rule3);
                    }
                    xml = xml + "</CombatController>" + "<BuffController>";
                    foreach (Rule rule4 in this.BuffController.GetRules)
                    {
                        xml = xml + this.SaveRule(rule4);
                    }
                    xml = xml + "</BuffController>" + "<RestController>";
                    foreach (Rule rule5 in this.RestController.GetRules)
                    {
                        xml = xml + this.SaveRule(rule5);
                    }
                    xml = xml + "</RestController>";
                }
                xml = xml + "</Behavior>";
                try
                {
                    XmlDocument document = new XmlDocument();
                    document.LoadXml(xml);
                    document.Save(PVEBehaviorCombat.OurDirectory + @"\Behaviors\" + this.Name + ".xml");
                }
                catch (Exception exception)
                {
                    MessageBox.Show("Could not save behavior " + exception);
                }
            }
        }

        internal string SaveRule(Rule rule)
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

        internal RuleController CombatController { get; set; }

        internal RuleController PullController { get; set; }

        internal RuleController BuffController { get; set; }

        internal RuleController RestController { get; set; }

        internal RuleController PrePullController { get; set; }

        internal string Name { get; set; }

        internal int CombatDistance { get; set; }

        internal int PullDistance { get; set; }

        internal int PrePullDistance { get; set; }

        internal bool UseAutoAttack { get; set; }

        internal bool SendPet { get; set; }

        internal int GlobalCooldown { get; set; }
    }
}

