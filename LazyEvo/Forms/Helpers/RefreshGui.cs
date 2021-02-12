namespace LazyEvo.Forms.Helpers
{
    using LazyLib.Wow;
    using System;

    internal class RefreshGui
    {
        public static void Refresh()
        {
            if (!LazyLib.Wow.ObjectManager.Initialized)
            {
                LazyForms.MainForm.UpdateGroupControl(LazyForms.MainForm.MainGPPlayer, "-");
                LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerHP, 0);
                LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerXP, 0);
                LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerHP, 0 + "%");
                LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerXP, 0 + "%");
            }
            else if ((LazyLib.Wow.ObjectManager.MyPlayer != null) && LazyLib.Wow.ObjectManager.MyPlayer.IsValid)
            {
                try
                {
                    LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerHP, LazyLib.Wow.ObjectManager.MyPlayer.Health);
                    if ((LazyLib.Wow.ObjectManager.MyPlayer != null) && (LazyLib.Wow.ObjectManager.MyPlayer.IsValid && (LazyLib.Wow.ObjectManager.MyPlayer.Level < 80)))
                    {
                        LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerXP, LazyLib.Wow.ObjectManager.MyPlayer.ExperiencePercentage);
                        LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerXP, LazyLib.Wow.ObjectManager.MyPlayer.ExperiencePercentage + "%");
                    }
                    LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerHP, LazyLib.Wow.ObjectManager.MyPlayer.Health + "%");
                    LazyForms.MainForm.UpdateGroupControl(LazyForms.MainForm.MainGPPlayer, LazyLib.Wow.ObjectManager.MyPlayer.Name);
                    switch (LazyLib.Wow.ObjectManager.MyPlayer.PowerTypeId)
                    {
                        case 0:
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPowerType, "Mana:");
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Mana + "%");
                            LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Mana);
                            break;

                        case 1:
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPowerType, "Rage:");
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Rage + "%");
                            LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Rage);
                            break;

                        case 2:
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPowerType, "Focus:");
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Energy + "%");
                            LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Energy);
                            break;

                        case 3:
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPowerType, "Energy:");
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Energy + "%");
                            LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.Energy);
                            break;

                        case 6:
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPowerType, "Runic Power:");
                            LazyForms.MainForm.UpdateTextLabel(LazyForms.MainForm.MainLBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.RunicPower + "%");
                            LazyForms.MainForm.UpdateProgressBar(LazyForms.MainForm.MainPBPlayerPower, LazyLib.Wow.ObjectManager.MyPlayer.RunicPower);
                            break;

                        default:
                            break;
                    }
                }
                catch
                {
                }
            }
        }
    }
}

