using System.Drawing;
using System.Threading;
using Lerawin.Classes;
using Lerawin.Utilities;

// TODO: Alpha of colors.

namespace Lerawin.Features
{
    public class Glow
    {
        public static void Run()
        {
            while (true)
            {
                // if the checkmark is checked...
                if ((Main.S.GlowEnemyEnabled || Main.S.GlowTeamEnabled) && Menu.CSGOActive && Menu.InGame)
                {
                    // loop through our player list
                    foreach (Entity Player in G.EntityList)
                    {
                        if (!Player.Valid) continue;
                        if (Main.S.GlowVisibleEnabled && !Player.Spotted && !(Tools.HoldingKey(86))) continue;
                        if (Player.IsTeammate && Main.S.GlowTeamEnabled)
                        {
                            if (Main.S.GlowHealthBEnabled)
                                GlowHealth();
                            else if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                                Player.Glow(Main.S.GlowScopedCLR);
                            else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.RadarOnKeyEnabled && !Main.S.GlowHealthBEnabled)
                                Player.Glow(Main.S.GlowSpottedCLR);
                            else
                                Player.Glow(Main.S.GlowTeamCLR);
                        }
                        else if (!Player.IsTeammate && Main.S.GlowEnemyEnabled)
                        {
                            if (Main.S.GlowHealthBEnabled)
                                GlowHealth();
                            else if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                                Player.Glow(Main.S.GlowScopedCLR);
                            else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
                                Player.Glow(Main.S.GlowSpottedCLR);
                            else
                                Player.Glow(Main.S.GlowEnemyCLR);
                            //Player.Glow(Color.FromArgb(150, Main.S.GlowEnemyCLR));
                        }

                        void GlowHealth()
                        {
                            Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
                            Player.Glow(Color.FromArgb(Gradient.R, Gradient.G, Gradient.B));
                        }
                    }
                }

                if ((Main.S.SndGlowEnemyEnabled || Main.S.SndGlowTeamEnabled) && Menu.CSGOActive && Menu.InGame)
                {
                    foreach (Entity Player in G.EntityList)
                    {
                        if (!Player.Valid) continue;
                        if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.RadarOnKeyEnabled && !Main.S.GlowHealthBEnabled)
                            Player.Glow(Main.S.GlowSpottedCLR);
                        else if (!Player.IsWalking && !Player.IsLocalPlayer && Player.Distance < 1000 && Player.Velocity > 210)
                        {
                            if (Player.IsTeammate && Main.S.SndGlowTeamEnabled)
                            {
                                if (Main.S.GlowHealthBEnabled)
                                    GlowHealth();
                                else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
                                    Player.Glow(Main.S.GlowSpottedCLR);
                                else if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                                    Player.Glow(Main.S.GlowScopedCLR);
                                else
                                    Player.Glow(Main.S.GlowTeamCLR);

                                if (!Player.Spotted)
                                {
                                    Player.Spotted = true;
                                    Thread.Sleep(1);
                                    Player.Spotted = false;
                                }

                                //if (!Tools.HoldingKey(86))
                                //    Thread.Sleep(300);

                                if (Main.S.GlowVisibleEnabled && !Player.Spotted) continue;
                            }
                            else if (!Player.IsTeammate && Main.S.SndGlowEnemyEnabled)
                            {
                                if (Main.S.GlowHealthBEnabled)
                                    GlowHealth();
                                else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
                                    Player.Glow(Main.S.GlowSpottedCLR);
                                else if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                                    Player.Glow(Main.S.GlowScopedCLR);
                                else
                                    Player.Glow(Main.S.GlowEnemyCLR);

                                if (!Player.Spotted)
                                {
                                    Player.Spotted = true;
                                    Thread.Sleep(1);
                                    Player.Spotted = false;
                                }

                                // To prevent flickering when pressing V.
                                //if (!Tools.HoldingKey(86))
                                //    Thread.Sleep(300);

                                if (Main.S.GlowVisibleEnabled && !Player.Spotted) continue;
                            }

                            void GlowHealth()
                            {
                                Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
                                Player.Glow(Color.FromArgb(Gradient.R, Gradient.G, Gradient.B));
                            }
                        }
                    }
                }


                Thread.Sleep(Main.S.GlowInterval);
            }
        }
    }
}