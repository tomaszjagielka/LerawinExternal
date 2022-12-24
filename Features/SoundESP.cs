//using Lerawin.Classes;
//using Lerawin.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Lerawin.Features
//{
//    public class SoundESP
//    {
//        public static void Run()
//        {
//            while (true)
//            {
//                if ((Main.S.SndGlowEnemyEnabled || Main.S.SndGlowTeamEnabled) && Menu.CSGOActive)
//                {
//                    foreach (Entity Player in G.EntityList)
//                    {
//                        if (!Player.Valid)
//                        {
//                            Console.WriteLine("Player isnt valid");
//                            continue;
//                        }
//                        if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
//                        {
//                            Player.Glow(Main.S.GlowSpottedCLR);
//                            Console.WriteLine("Glow Spotted 1");
//                            continue;
//                        }
//                        else if (!Player.IsWalking && Player.Distance < 800 && Player.Velocity > 210)
//                        {
//                            Console.WriteLine("Walking, distance, velocity check");
//                            if (Player.IsTeammate && Main.S.SndGlowTeamEnabled)
//                            {
//                                if (Main.S.GlowHealthBEnabled)
//                                    GlowHealth();
//                                else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
//                                    Player.Glow(Main.S.GlowSpottedCLR);
//                                else if (Player.Scoped && !Player.IsWalking)
//                                    Player.Glow(Main.S.GlowScopedCLR);
//                                else
//                                    Player.Glow(Main.S.GlowTeamCLR);

//                                if (!Player.Spotted)
//                                {
//                                    Player.Spotted = true;
//                                    Thread.Sleep(1);
//                                    Player.Spotted = false;
//                                }

//                                Thread.Sleep(300);

//                                if (Main.S.GlowVisibleEnabled && !Player.Spotted) continue;
//                            }
//                            else if (!Player.IsTeammate && Main.S.SndGlowEnemyEnabled)
//                            {
//                                Console.WriteLine("Is Enemy");
//                                if (Main.S.GlowHealthBEnabled)
//                                {
//                                    GlowHealth();
//                                    Console.WriteLine("Glow Health");
//                                }
//                                else if (Player.Spotted && !Main.S.RadarEnabled && !Main.S.GlowHealthBEnabled)
//                                {
//                                    Player.Glow(Main.S.GlowSpottedCLR);
//                                    Console.WriteLine("Glow Spotted 2");
//                                }
//                                else if (Player.Scoped && !Player.IsWalking)
//                                {
//                                    Player.Glow(Main.S.GlowScopedCLR);
//                                    Console.WriteLine("Glow Scoped");
//                                }
//                                else
//                                {
//                                    Player.Glow(Main.S.GlowEnemyCLR);
//                                    Console.WriteLine("Glow Enemy");
//                                }

//                                if (!Player.Spotted)
//                                {
//                                    Player.Spotted = true;
//                                    Thread.Sleep(1);
//                                    Player.Spotted = false;
//                                    Console.WriteLine("Radar");
//                                }

//                                Thread.Sleep(300);
//                                Console.WriteLine("Sleeping");

//                                if (Main.S.GlowVisibleEnabled && !Player.Spotted)
//                                {
//                                    Console.WriteLine("GlowVisible check");
//                                    continue;
//                                }
//                            }

//                            void GlowHealth()
//                            {
//                                Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
//                                Player.Glow(Color.FromArgb(Gradient.R, Gradient.G, Gradient.B));
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
