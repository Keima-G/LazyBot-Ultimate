namespace LazyEvo.LGrindEngine.Helpers
{
    using LazyLib;
    using LazyLib.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    internal class GrindingShouldTrain
    {
        private static bool CheckFrame()
        {
            int num = 0;
            while (true)
            {
                while (true)
                {
                    if (num > 12)
                    {
                        return false;
                    }
                    try
                    {
                        List<Frame> getChilds = InterfaceHelper.GetFrameByName($"SpellButton{num}").GetChilds;
                        using (List<Frame>.Enumerator enumerator = getChilds.GetEnumerator())
                        {
                            while (true)
                            {
                                if (enumerator.MoveNext())
                                {
                                    Frame current = enumerator.Current;
                                    if (current.GetName != $"SpellButton{num}SeeTrainerString")
                                    {
                                        continue;
                                    }
                                    if (current.IsVisible)
                                    {
                                        return true;
                                    }
                                }
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }
                    break;
                }
                num++;
            }
        }

        private static bool CheckPages()
        {
            InterfaceHelper.GetFrameByName("SpellBookPrevPageButton").LeftClick();
            if (CheckFrame())
            {
                CloseFrame();
                return true;
            }
            InterfaceHelper.GetFrameByName("SpellBookNextPageButton").LeftClick();
            if (!CheckFrame())
            {
                return false;
            }
            CloseFrame();
            return true;
        }

        private static int CheckPagesCount()
        {
            int num = 0;
            string getText = (from spell in InterfaceHelper.GetFrameByName($"SpellButton{"1"}").GetChilds
                where spell.GetName == "SpellButton1SpellName"
                select spell).FirstOrDefault<Frame>().GetText;
            InterfaceHelper.GetFrameByName("SpellBookPrevPageButton").LeftClick();
            num += CheckSpellCount();
            InterfaceHelper.GetFrameByName("SpellBookNextPageButton").LeftClick();
            if (getText != (from spell in InterfaceHelper.GetFrameByName($"SpellButton{"1"}").GetChilds
                where spell.GetName == "SpellButton1SpellName"
                select spell).FirstOrDefault<Frame>().GetText)
            {
                num += CheckSpellCount();
            }
            return num;
        }

        private static int CheckSpellCount()
        {
            int num = 0;
            int num2 = 0;
            while (true)
            {
                while (true)
                {
                    if (num2 > 12)
                    {
                        return num;
                    }
                    try
                    {
                        List<Frame> getChilds = InterfaceHelper.GetFrameByName($"SpellButton{num2}").GetChilds;
                        foreach (Frame frame in getChilds)
                        {
                            if (frame.GetName == $"SpellButton{num2}SeeTrainerString")
                            {
                                if (frame.IsVisible)
                                {
                                    num++;
                                }
                                break;
                            }
                        }
                    }
                    catch
                    {
                    }
                    break;
                }
                num2++;
            }
        }

        private static void CloseFrame()
        {
            if (InterfaceHelper.GetFrameByName("SpellBookFrame").IsVisible)
            {
                InterfaceHelper.GetFrameByName("SpellbookMicroButton").LeftClick();
            }
        }

        public static bool ShouldTrain()
        {
            bool flag;
            try
            {
                Logging.Write("Checking if we should train", new object[0]);
                if (!InterfaceHelper.GetFrameByName("SpellBookFrame").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("SpellbookMicroButton").LeftClick();
                }
                Thread.Sleep(100);
                InterfaceHelper.GetFrameByName("SpellBookSkillLineTab2").LeftClick();
                if (CheckPages())
                {
                    flag = true;
                }
                else
                {
                    InterfaceHelper.GetFrameByName("SpellBookSkillLineTab3").LeftClick();
                    if (CheckPages())
                    {
                        flag = true;
                    }
                    else
                    {
                        InterfaceHelper.GetFrameByName("SpellBookSkillLineTab4").LeftClick();
                        if (CheckPages())
                        {
                            flag = true;
                        }
                        else
                        {
                            CloseFrame();
                            flag = false;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Logging.Write("Exception getting Should Train: " + exception, new object[0]);
                flag = false;
            }
            return flag;
        }

        public static int TrainCount()
        {
            try
            {
                Logging.Write("Getting train count", new object[0]);
                if (!InterfaceHelper.GetFrameByName("SpellBookFrame").IsVisible)
                {
                    InterfaceHelper.GetFrameByName("SpellbookMicroButton").LeftClick();
                }
                Thread.Sleep(100);
                int num = 0;
                InterfaceHelper.GetFrameByName("SpellBookSkillLineTab2").LeftClick();
                num += CheckPagesCount();
                InterfaceHelper.GetFrameByName("SpellBookSkillLineTab3").LeftClick();
                num += CheckPagesCount();
                InterfaceHelper.GetFrameByName("SpellBookSkillLineTab4").LeftClick();
                num += CheckPagesCount();
                CloseFrame();
                return num;
            }
            catch (Exception exception)
            {
                Logging.Write("Exception getting Train Count: " + exception, new object[0]);
                return 0;
            }
        }
    }
}

