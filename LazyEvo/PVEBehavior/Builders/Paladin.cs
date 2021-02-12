namespace LazyEvo.PVEBehavior.Builders
{
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib.ActionBar;
    using System;
    using System.Collections.Generic;

    internal class Paladin
    {
        public static List<AddToBehavior> Load()
        {
            List<AddToBehavior> list = new List<AddToBehavior>();
            string nameFromSpell = BarMapper.GetNameFromSpell(0x7cbf);
            List<AbstractCondition> conditions = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, conditions)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4f2f);
            List<AbstractCondition> list3 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 2, list3)));
            List<AbstractCondition> list4 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior("Auto Attack", LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule("Auto Attack", new ActionSpell("Auto Attack"), 3, list4)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x279);
            List<AbstractCondition> list5 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 10),
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "25771")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list5)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x282);
            List<AbstractCondition> list6 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 15),
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "25771")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 2, list6)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x7c6a);
            List<AbstractCondition> list7 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 10)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 3, list7)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x3fe);
            List<AbstractCondition> list8 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 15),
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "25771")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 4, list8)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x1f2);
            List<AbstractCondition> list9 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x55)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 5, list9)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x355);
            List<AbstractCondition> list10 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x23)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 6, list10)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4d26);
            List<AbstractCondition> list11 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x19)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 7, list11)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14196);
            List<AbstractCondition> list12 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x23)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 8, list12)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x27b);
            List<AbstractCondition> list13 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 50)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 9, list13)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14ea9);
            List<AbstractCondition> list14 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x37),
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.HolyPower, ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 10, list14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x15086);
            List<AbstractCondition> list15 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 11, list15)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14d25);
            List<AbstractCondition> list16 = new List<AbstractCondition> {
                new FunctionsCondition(ConditionTargetEnum.Target, FunctionsConditionEnum.Is, FunctionEnum.Casting)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 12, list16)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x7c8c);
            List<AbstractCondition> list17 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 13, list17)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14ec0);
            List<AbstractCondition> list18 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.HolyPower, ConditionEnum.EqualTo, 3)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 14, list18)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x67cd);
            List<AbstractCondition> list19 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 2)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 15, list19)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xd160);
            List<AbstractCondition> list20 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.HolyPower, ConditionEnum.EqualTo, 3)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x10, list20)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14be3);
            nameFromSpell = BarMapper.GetNameFromSpell(0x5ed3);
            List<AbstractCondition> list21 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x19)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x11, list21)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x36f);
            List<AbstractCondition> list22 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.HasBuff, BuffValueEnum.Id, "59578")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x12, list22)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14d08);
            List<AbstractCondition> list23 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.HolyPower, ConditionEnum.MoreThan, 2)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x13, list23)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xd15b);
            List<AbstractCondition> list24 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 20, list24)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x8a43);
            List<AbstractCondition> list25 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x15, list25)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4f2f);
            List<AbstractCondition> list26 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x16, list26)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x7cbf);
            List<AbstractCondition> list27 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x17, list27)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xafc);
            List<AbstractCondition> list28 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Target, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x18, list28)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4eba);
            List<AbstractCondition> list29 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Seal of Righteousness")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list29)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4ec5);
            List<AbstractCondition> list30 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Seal of Insight")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 2, list30)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x7c39);
            List<AbstractCondition> list31 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Seal of Truth")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 3, list31)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x1d1);
            List<AbstractCondition> list32 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Devotion Aura")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 4, list32)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x1c7e);
            List<AbstractCondition> list33 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Retribution Aura")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 5, list33)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4d22);
            List<AbstractCondition> list34 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Concentration Aura")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 6, list34)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x7ddf);
            List<AbstractCondition> list35 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Crusader Aura")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 7, list35)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4ef9);
            List<AbstractCondition> list36 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Blessing of Kings")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 8, list36)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x4d1c);
            List<AbstractCondition> list37 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Blessing of Might")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 9, list37)));
            return list;
        }
    }
}

