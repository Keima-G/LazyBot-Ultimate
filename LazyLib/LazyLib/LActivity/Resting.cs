namespace LazyLib.LActivity
{
    using LazyLib;
    using LazyLib.Helpers;
    using LazyLib.Wow;
    using System;
    using System.Reflection;
    using System.Threading;

    [Obfuscation(Feature="renaming", ApplyToMembers=true)]
    public class Resting
    {
        private static bool _regenHealth;
        private static bool _regenMana;
        private static readonly Ticker DrinkTimer = new Ticker(30000.0);
        private static readonly Ticker EatTimer = new Ticker(30000.0);
        private static bool _bIsDrinking;
        private static bool _bIsEating;

        private static void DrinkSomething()
        {
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead && (LazyLib.Wow.ObjectManager.GetAttackers.Count == 0))
            {
                DrinkTimer.Reset();
                Logging.Write("[Rest]Drinking", new object[0]);
                KeyHelper.SendKey("Drink");
                Thread.Sleep(300);
                _bIsDrinking = true;
            }
        }

        private static void EatSomething()
        {
            if (!LazyLib.Wow.ObjectManager.MyPlayer.IsDead && (LazyLib.Wow.ObjectManager.GetAttackers.Count == 0))
            {
                EatTimer.Reset();
                Logging.Write("[Rest]Eating", new object[0]);
                KeyHelper.SendKey("Eat");
                Thread.Sleep(300);
                _bIsEating = true;
            }
        }

        public static void Rest()
        {
            while (LazyLib.Wow.ObjectManager.MyPlayer.IsInCombat && !LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                Thread.Sleep(0x3e8);
            }
            if (!LazyLib.Wow.ObjectManager.ShouldDefend)
            {
                if (_regenHealth)
                {
                    EatSomething();
                }
                if (_regenMana)
                {
                    DrinkSomething();
                }
                if (_bIsDrinking || _bIsEating)
                {
                    do
                    {
                        Thread.Sleep(0x65);
                    }
                    while (((!LazyLib.Wow.ObjectManager.MyPlayer.IsDead && (!LazyLib.Wow.ObjectManager.MyPlayer.IsGhost && ((LazyLib.Wow.ObjectManager.GetAttackers.Count == 0) && (!_bIsEating || (_bIsDrinking || (LazyLib.Wow.ObjectManager.MyPlayer.Health != 100)))))) && ((_bIsEating || (!_bIsDrinking || (LazyLib.Wow.ObjectManager.MyPlayer.Mana != 100))) && ((!EatTimer.IsReady || !_bIsEating) && ((!DrinkTimer.IsReady || !_bIsDrinking) && !LazyLib.Wow.ObjectManager.MyPlayer.IsDead)))) && ((LazyLib.Wow.ObjectManager.MyPlayer.Health != 100) || (LazyLib.Wow.ObjectManager.MyPlayer.Mana != 100)));
                }
                Logging.Write("[Rest]We are not eating or drinking lets continue", new object[0]);
                _bIsEating = false;
                _bIsDrinking = false;
            }
        }

        public static bool NeedResting
        {
            get
            {
                bool flag = false;
                _regenMana = false;
                _regenHealth = false;
                if (!LazyLib.Wow.ObjectManager.MyPlayer.IsAlive)
                {
                    return false;
                }
                if (LazySettings.CombatBoolDrink && (LazyLib.Wow.ObjectManager.MyPlayer.PowerType.Equals("Mana") && (LazyLib.Wow.ObjectManager.MyPlayer.Mana < Convert.ToInt32(LazySettings.CombatDrinkAt))))
                {
                    _regenMana = true;
                    flag = true;
                }
                if (LazySettings.CombatBoolEat && (LazyLib.Wow.ObjectManager.MyPlayer.Health < Convert.ToInt32(LazySettings.CombatEatAt)))
                {
                    _regenHealth = true;
                    flag = true;
                }
                return flag;
            }
        }
    }
}

