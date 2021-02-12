namespace LazyEvo.PVEBehavior.Builders
{
    using LazyEvo.PVEBehavior.Behavior;
    using LazyEvo.PVEBehavior.Behavior.Conditions;
    using LazyLib.ActionBar;
    using System;
    using System.Collections.Generic;

    internal class Deathknight
    {
        public static List<AddToBehavior> Load()
        {
            List<AddToBehavior> list = new List<AddToBehavior>();
            string nameFromSpell = BarMapper.GetNameFromSpell(0xdff2);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xc1a8);
            List<AbstractCondition> conditions = new List<AbstractCondition> {
                new DistanceToTarget(ConditionEnum.MoreThan, 10)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 2, conditions)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb1a5);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 3)));
            string name = "Auto Attack";
            list.Add(new AddToBehavior(name, LazyEvo.PVEBehavior.Behavior.Type.Pull, Spec.Normal, new Rule(name, new ActionSpell(name), 4)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb5f8);
            List<AbstractCondition> list3 = new List<AbstractCondition> {
                new PetCondition(PetConditionEnum.DoesNotHave)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 7, list3)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbf78);
            List<AbstractCondition> list4 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.MoreThan, 80),
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list4)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xc033);
            List<AbstractCondition> list5 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 4, list5)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xc046);
            List<AbstractCondition> list6 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x4b)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree1, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 2, list6)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xd7c1);
            List<AbstractCondition> list7 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 20)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree1, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list7)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbf56);
            List<AbstractCondition> list8 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x4b)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree1, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 3, list8)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbf84);
            List<AbstractCondition> list9 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree1, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 4, list9)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb9d0);
            List<AbstractCondition> list10 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1),
                new RuneCondition(ConditionEnum.EqualTo, RuneEnum.Blood, 0),
                new RuneCondition(ConditionEnum.EqualTo, RuneEnum.Frost, 0),
                new RuneCondition(ConditionEnum.EqualTo, RuneEnum.Unholy, 0)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 5, list10)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb5f8);
            List<AbstractCondition> list11 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1),
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 40)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 6, list11)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbe67);
            List<AbstractCondition> list12 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x16)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 8, list12)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xa69a);
            List<AbstractCondition> list13 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 2),
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 30)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 8, list13)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbe98);
            List<AbstractCondition> list14 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 40)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 9, list14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xdff2);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 10)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb9a8);
            List<AbstractCondition> list15 = new List<AbstractCondition> {
                new FunctionsCondition(ConditionTargetEnum.Target, FunctionsConditionEnum.Is, FunctionEnum.Casting)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 11, list15)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb974);
            List<AbstractCondition> list16 = new List<AbstractCondition> {
                new FunctionsCondition(ConditionTargetEnum.Target, FunctionsConditionEnum.Is, FunctionEnum.Casting)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 12, list16)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbe43);
            List<AbstractCondition> list17 = new List<AbstractCondition> {
                new FunctionsCondition(ConditionTargetEnum.Target, FunctionsConditionEnum.Is, FunctionEnum.Casting)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 13, list17)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xddef);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbff7);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 15)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb9b5);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xc34e);
            List<AbstractCondition> list18 = new List<AbstractCondition> {
                new HealthPowerCondition(ConditionTargetEnum.Player, ConditionTypeEnum.Health, ConditionEnum.LessThan, 0x37)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 15, list18)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbf7c);
            List<AbstractCondition> list19 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.HasBuff, BuffValueEnum.Name, "Blood Plague"),
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.HasBuff, BuffValueEnum.Name, "Frost Fever")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x10, list19)));
            nameFromSpell = BarMapper.GetNameFromSpell(0x14fbc);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xc020);
            List<AbstractCondition> list20 = new List<AbstractCondition> {
                new CombatCountCondition(ConditionEnum.MoreThan, 1)
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree2, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 15, list20)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb1a5);
            List<AbstractCondition> list21 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Frost Fever")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x10, list21)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xd732);
            List<AbstractCondition> list22 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.HasBuff, BuffValueEnum.Name, "Blood Plague"),
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.HasBuff, BuffValueEnum.Name, "Frost Fever")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree3, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x11, list22)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb196);
            List<AbstractCondition> list23 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Target, BuffConditionEnum.DoesNotHave, BuffValueEnum.Name, "Blood Plague")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x12, list23)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xd70a);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Tree1, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 0x13)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xb34e);
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Combat, Spec.Normal, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 14)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbc87);
            List<AbstractCondition> list24 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "48263")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list24)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbc8a);
            List<AbstractCondition> list25 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "48266")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list25)));
            nameFromSpell = BarMapper.GetNameFromSpell(0xbc89);
            List<AbstractCondition> list26 = new List<AbstractCondition> {
                new BuffCondition(ConditionTargetEnum.Player, BuffConditionEnum.DoesNotHave, BuffValueEnum.Id, "48265")
            };
            list.Add(new AddToBehavior(nameFromSpell, LazyEvo.PVEBehavior.Behavior.Type.Buff, Spec.Special, new Rule(nameFromSpell, new ActionSpell(nameFromSpell), 1, list26)));
            return list;
        }
    }
}

