using System;
using System.Numerics;
using System.Threading;
using GameOverlay.Drawing;
using GameOverlay.Windows;
using Lerawin.Classes;
using Lerawin.Utilities;
using Color = System.Drawing.Color;

// There's a bug: if ESP is disabled after enabling - the drawings are staying on the screen.
// And another one: the Ammo bar assign itself to the non dormant player with highest amount of ammo.
// Same with Reloading, C4, Ping (BOT), Ranks
// Update: Fixed Reloading by writing to code weapon ammo instead assinging it to variable earlier (Ammo).
// I need to figure out how to clear the scene after checkbox was checked.
// TODO: Auto center text.

namespace Lerawin.Features
{
    public class Visuals
    {
        #region dx shid
        private OverlayWindow _window;
        private Graphics _graphics;

        public object Globals { get; private set; }

        public Visuals()
        {
            _window = new OverlayWindow(Main.ScreenRect.left, Main.ScreenRect.top, Main.ScreenSize.Width, Main.ScreenSize.Height)
            {
                IsTopmost = true,
                IsVisible = true
            };
            _window.SizeChanged += _window_SizeChanged;
            _graphics = new Graphics()
            {
                MeasureFPS = true,
                Height = _window.Height,
                PerPrimitiveAntiAliasing = true,
                TextAntiAliasing = true,
                UseMultiThreadedFactories = false,
                VSync = true,
                Width = _window.Width,
                WindowHandle = IntPtr.Zero
            };
        }

        ~Visuals()
        {
            _graphics.Dispose();
            _window.Dispose();
        }

        public void Initialize()
        {
            _window.CreateWindow();
            _graphics.WindowHandle = _window.Handle; // set the target handle before calling Setup()
            _graphics.Setup();
        }

        private void _window_SizeChanged(object sender, OverlaySizeEventArgs e)
        {
            if (_graphics == null) return;

            if (_graphics.IsInitialized)
            {
                // after the Graphics surface is initialized you can only use the Resize method in order to enqueue a size change
                _graphics.Resize(e.Width, e.Height);
            }
            else
            {
                // otherwise just set its members
                _graphics.Width = e.Width;
                _graphics.Height = e.Height;
            }
        }
        #endregion

        public static bool ESPRanksAllow;
        public void Run()
        {
            #region things
            var gfx = _graphics;
            SolidBrush GetBrushColor(Color color)
            {
                return gfx.CreateSolidBrush(color.R, color.G, color.B, color.A);
            }
            #endregion
            #region Draw
            long detonateTime = 0;
            long defuseTime = 0;
            long reloadTime = 0;
            long compared = 0;
            long comparedDefuse = 0;
            bool resultReload = false;
            bool resultMaxAmmo = false;

            while (true)
            {
                int posUp = 0;
                if ((Main.S.ESPEnemyEnabled || Main.S.ESPTeamEnabled) && Menu.CSGOActive && Menu.InGame) /*|| Tools.HoldingKey(Keys.VK_V)*/
                {
                    // start drawings here
                    gfx.BeginScene();
                    gfx.ClearScene();

                    //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20
                    //            || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1)
                    //{
                    //    if (G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2)
                    //    {
                    //        DrawCrosshair(CrosshairStyle.Plus, 960, 540, 5, 1, Color.White);
                    //    }
                    //}

                    //#region Recoil Crosshair
                    //// Recoil Crosshair feature
                    //if (Main.S.RecoilCrosshairEnabled)
                    //{
                    //    float mx = Main.MidScreen.X;
                    //    float my = Main.MidScreen.Y;
                    //    float dy = Main.ScreenSize.Height / 90;
                    //    float dx = Main.ScreenSize.Width / 90;
                    //    mx -= (dx * G.Engine.LocalPlayer.AimPunchAngle.Y);
                    //    my += (dy * G.Engine.LocalPlayer.AimPunchAngle.X);
                    //    if (G.Engine.LocalPlayer.AimPunchAngle.X < -0.1 || G.Engine.LocalPlayer.AimPunchAngle.X > 0.6) // Prevents from drawing crosshair if there isn't any recoil.
                    //    {
                    //        DrawCrosshair(CrosshairStyle.Plus, mx, my, Main.S.RecoilCrosshairSize, 1, Main.S.ESPCrosshairCLR);
                    //    }
                    //}
                    //#endregion

                    //#region Bomb Timer
                    //if (Main.S.ESPBombTimerEnabled)
                    //{
                    //    DateTime dateCurrentTime2 = DateTime.Now;
                    //    DateTime dateDetonateTime = new DateTime(0);
                    //    long currentTime = dateCurrentTime2.Ticks / TimeSpan.TicksPerSecond;

                    //    if (!G.Engine.BombPlanted)
                    //    {
                    //        dateDetonateTime = dateCurrentTime2.AddSeconds(40);
                    //        detonateTime = dateDetonateTime.Ticks / TimeSpan.TicksPerSecond;
                    //    }
                    //    else
                    //    {
                    //        compared = detonateTime - currentTime;

                    //        if (compared > 0)
                    //        {
                    //            if (compared > 10)
                    //            {
                    //                DrawBombTimer(Color.Lime);
                    //            }
                    //            else if (compared > 5 && G.Engine.LocalPlayer.HasDefuser)
                    //            {
                    //                DrawBombTimer(Color.Lime);
                    //            }
                    //            else if (compared < 10 && !G.Engine.LocalPlayer.HasDefuser)
                    //            {
                    //                DrawBombTimer(Color.Red);
                    //            }
                    //        }

                    //        void DrawBombTimer(Color color)
                    //        {
                    //            if (Main.S.ESPBombTimerType == "Bar")
                    //            {
                    //                // Bar
                    //                upPosition("", Color.Red, true, compared, 40);
                    //            }
                    //            else if (Main.S.ESPBombTimerType == "Text")
                    //                upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                    //            else if (Main.S.ESPBombTimerType == "Bar & Text")
                    //            {
                    //                upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                    //                upPosition("", color, true, compared, 40);
                    //            }
                    //            else
                    //                upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                    //        }
                    //    }

                    //    //Console.WriteLine("Detonated.");
                    //    //MessageBox.Show("Detonated.");


                    //    //Console.WriteLine("Compared = " + compared);
                    //    //Console.WriteLine("Current Time = " + currentTime);
                    //    //Console.WriteLine("Detonate Time = " + detonateTime);
                    //}
                    //#endregion

                    // start drawings here
                    foreach (Entity Player in G.EntityList)
                    {
                        if (Main.S.SndESPEnabled && !Player.Spotted && !Tools.HoldingKey(86))
                        {
                            if (!Player.IsWalking && !Player.IsLocalPlayer && Player.Distance < 800 && Player.Velocity > 210)
                            {
                                Thread.Sleep(300);
                            }
                            else
                                continue;
                            
                        }
                        if (Main.S.ESPVisibleEnabled && !Player.Spotted && !(Tools.HoldingKey(86))) continue;
                        if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
                        {
                            Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z));
                            Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z));
                            //Vector2 Player2DNeckPos = Tools.WorldToScreen(new Vector3(Player.NeckPosition.X, Player.NeckPosition.Y, Player.NeckPosition.Z));
                            //Vector2 Player2DChestPos = Tools.WorldToScreen(new Vector3(Player.ChestPosition.X, Player.ChestPosition.Y, Player.ChestPosition.Z));
                            //Vector2 Player2DLowerChestPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z));
                            //Vector2 Player2DStomachPos = Tools.WorldToScreen(new Vector3(Player.StomachPosition.X, Player.StomachPosition.Y, Player.StomachPosition.Z));
                            //Vector2 Player2DLegsPos = Tools.WorldToScreen(new Vector3(Player.LegsPosition.X, Player.LegsPosition.Y, Player.LegsPosition.Z));
                            //Vector2 Player2DFeetPos = Tools.WorldToScreen(new Vector3(Player.FeetPosition.X, Player.FeetPosition.Y, Player.FeetPosition.Z));
                            //Vector2 Player2DToePos = Tools.WorldToScreen(new Vector3(Player.ToePosition.X, Player.ToePosition.Y, Player.ToePosition.Z));
                            if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
                            {
                                float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;
                                float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box
                                Color drawcolor = Color.Black;

                                if (Main.S.ESPHealthBEnabled)
                                {
                                    Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
                                    drawcolor = Color.FromArgb(Gradient.R, Gradient.G, Gradient.B);
                                }
                                else
                                {
                                    if (Player.IsTeammate && Main.S.ESPTeamEnabled)
                                        drawcolor = Main.S.ESPTeamCLR;
                                    else if (!Player.IsTeammate && Main.S.ESPEnemyEnabled)
                                        drawcolor = Main.S.ESPEnemyCLR;
                                    if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                                        drawcolor = Main.S.ESPScopedCLR;
                                    else if (Player.Spotted && !Main.S.RadarEnabled)
                                        drawcolor = Main.S.ESPSpottedCLR;
                                }

                                if (Player.IsTeammate && Main.S.ESPTeamEnabled)
                                {
                                    Things();
                                }
                                else if (!Player.IsTeammate && Main.S.ESPEnemyEnabled)
                                {
                                    Things();

                                    //void DrawText(string text, float x, float y, int size, Color color, bool bold = false, bool italic = false)
                                    //{
                                    //    if (Tools.InScreenPos(x, y))
                                    //    {
                                    //        gfx.DrawText(_graphics.CreateFont("Arial", size, bold, italic), GetBrushColor(color), x, y, text);
                                    //    }
                                    //}
                                }

                                void Things()
                                {
                                    int pos = 0;
                                    #region Box
                                    if (Main.S.ESPBoxEnabled)
                                    {
                                        if (Main.S.ESPBoxType == "Box")
                                            DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);
                                        else if (Main.S.ESPBoxType == "Filled Box")
                                            DrawFillOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, Color.FromArgb(50, 198, 198, 198));
                                        else if (Main.S.ESPBoxType == "Edges")
                                            DrawBoxEdge(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, 1);
                                        else
                                            DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);
                                    }
                                    #endregion
                                    #region Health
                                    if (Main.S.ESPHealthEnabled)
                                    {
                                        if (Main.S.ESPHealthType == "Bar")
                                            Bar();
                                        else if (Main.S.ESPHealthType == "Text")
                                            Text();
                                        else if (Main.S.ESPHealthType == "Bar & Text")
                                        {
                                            Bar();
                                            Text();
                                        }
                                        else
                                            Bar();

                                        void Bar()
                                        {
                                            float Health = Player.Health;
                                            Color HealthColor = Tools.HealthGradient(Tools.HealthToPercent((int)Health));
                                            float x = Player2DPos.X - (BoxWidth / 2) - 8;
                                            float y = Player2DHeadPos.Y;
                                            float w = 4;
                                            float h = BoxHeight;
                                            float HealthHeight = (Health * h) / 100;

                                            DrawBox(x, y, w, h, Color.Black, 1);
                                            DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);
                                        }

                                        void Text()
                                        {
                                            Position(Player.Health.ToString() + " HP");
                                        }
                                    }
                                    #endregion
                                    #region Ammo
                                    bool AmmoTextEnabled = false;
                                    if (Main.S.ESPAmmoEnabled)
                                    {
                                        //bool Enabled = true;
                                        //if (Player.enumWeaponID == enumWeaponID.NEGEV)          // 150
                                        //    MaxAmmo = 150;
                                        //else if (Player.enumWeaponID == enumWeaponID.M249)      // 100
                                        //    MaxAmmo = 100;
                                        //else if (Player.enumWeaponID == enumWeaponID.BIZON)     // 64
                                        //    MaxAmmo = 64;
                                        //else if (Player.enumWeaponID == enumWeaponID.P90)       // 50
                                        //    MaxAmmo = 50;
                                        //else if (Player.enumWeaponID == enumWeaponID.galil)   // 35
                                        //    MaxAmmo = 35;
                                        //else if (Player.enumWeaponID == enumWeaponID.TEC9)      // 32
                                        //    MaxAmmo = 32;
                                        //else if (Player.enumWeaponID == enumWeaponID.BERETTAS   // 30
                                        //    || Player.enumWeaponID == enumWeaponID.MAC10        // 30
                                        //    || Player.enumWeaponID == enumWeaponID.MP9          // 30
                                        //    || Player.enumWeaponID == enumWeaponID.MP7          // 30
                                        //    || Player.enumWeaponID == enumWeaponID.AK47         // 30
                                        //    || Player.enumWeaponID == enumWeaponID.SG556        // 30
                                        //    || Player.enumWeaponID == enumWeaponID.M4A4         // 30
                                        //    || Player.enumWeaponID == enumWeaponID.AUG)         // 30
                                        //    MaxAmmo = 30;
                                        //else if (Player.enumWeaponID == enumWeaponID.M4A1S      // 25
                                        //    || Player.enumWeaponID == enumWeaponID.FAMAS        // 25
                                        //    || Player.enumWeaponID == enumWeaponID.UMP45)       // 25
                                        //    MaxAmmo = 25;
                                        //else if (Player.enumWeaponID == enumWeaponID.GLOCK      // 20
                                        //    || Player.enumWeaponID == enumWeaponID.FIVESEVEN    // 20
                                        //    || Player.enumWeaponID == enumWeaponID.G3SG1        // 20
                                        //    || Player.enumWeaponID == enumWeaponID.SCAR20)      // 20
                                        //    MaxAmmo = 20;
                                        //else if (Player.enumWeaponID == enumWeaponID.P2000      // 13
                                        //    || Player.enumWeaponID == enumWeaponID.P250)        // 13
                                        //    MaxAmmo = 13;
                                        //else if (Player.enumWeaponID == enumWeaponID.USPS       // 12
                                        //    || Player.enumWeaponID == enumWeaponID.CZ75A)       // 12
                                        //    MaxAmmo = 12;
                                        //else if (Player.enumWeaponID == enumWeaponID.SSG08      // 10
                                        //    || Player.enumWeaponID == enumWeaponID.AWP)         // 10
                                        //    MaxAmmo = 10;
                                        //else if (Player.enumWeaponID == enumWeaponID.NOVA)      // 8
                                        //    MaxAmmo = 8;
                                        //else if (Player.enumWeaponID == enumWeaponID.DEAGLE     // 7
                                        //|| Player.enumWeaponID == enumWeaponID.SAWEDOFF         // 7
                                        //|| Player.enumWeaponID == enumWeaponID.XM1014           // 7
                                        //|| Player.enumWeaponID == enumWeaponID.MAG7)            // 7
                                        //    MaxAmmo = 7;
                                        //else
                                        //    Enabled = false;


                                        int WeaponsMaxAmmo()
                                        {
                                            string weaponid = Player.enumWeaponID.ToString();
                                            //Console.WriteLine("Current weapon = " + weaponid);
                                            resultMaxAmmo = Enum.IsDefined(typeof(WeaponsMaxAmmo), weaponid);
                                            //Console.WriteLine("resultReload = " + resultReload);
                                            if (resultMaxAmmo)
                                            {
                                                int maxAmmo = (int)(WeaponsMaxAmmo)Enum.Parse(typeof(WeaponsMaxAmmo), weaponid);
                                                //Console.WriteLine(addReloadTime);
                                                return maxAmmo;
                                            }
                                            else
                                                return 0;
                                        }

                                        if (Main.S.ESPAmmoType == "Bar")
                                            Bar();
                                        else if (Main.S.ESPAmmoType == "Text")
                                        {
                                            Text();
                                        }
                                        else if (Main.S.ESPAmmoType == "Bar & Text")
                                        {
                                            Bar();
                                            Text();
                                        }
                                        else
                                            Bar();

                                        void Bar()
                                        {
                                            if (Player.Ammo > 0)
                                            {
                                                Color AmmoColor = Color.Lime;
                                                float x = Player2DPos.X - (BoxWidth / 2);
                                                float y = Player2DHeadPos.Y + (BoxHeight + 4);
                                                float w = BoxWidth;
                                                float h = 4;
                                                float AmmoWidth = (Player.Ammo * w) / WeaponsMaxAmmo(); // Setting it to WeaponsMaxAmmo() instead to Ammo fixed issue.

                                                DrawBox(x, y, w, h, Color.Black, 1);
                                                DrawFilledBox(x + 1, y + 1, AmmoWidth - 1, 2, Color.White);
                                            }

                                        }

                                        void Text()
                                        {
                                            if (Player.Ammo > 0)
                                            {
                                                if (Main.S.ESPWeaponEnabled)
                                                    AmmoTextEnabled = true;
                                                else
                                                {
                                                    Position(Player.Ammo.ToString() + " Ammo");
                                                }
                                            }

                                        }

                                        //DrawTextWithOutline(ping.ToString(), Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y + (BoxHeight + 2), 10, drawcolor, Color.Black, true); // Under box.
                                    }
                                    #endregion
                                    #region Defusing
                                    if (Main.S.ESPDefusingEnabled)
                                    {
                                        DateTime dateCurrentTime3 = DateTime.Now;
                                        DateTime dateDefuseTime = new DateTime(0);
                                        long currentTime = dateCurrentTime3.Ticks / TimeSpan.TicksPerSecond;

                                        if (Player.IsDefusing)
                                        {
                                            Position("Defusing");

                                            //DrawText("Bomb will be defused in " + comparedDefuse.ToString() + " seconds.", 0, 20, 18, drawcolor, true);

                                            comparedDefuse = defuseTime - currentTime;

                                            if (comparedDefuse <= 5 && Player.HasDefuser)
                                            {
                                                DrawDefuseTimer();
                                            }
                                            else if (comparedDefuse <= 10)
                                            {
                                                DrawDefuseTimer();
                                            }

                                            void DrawDefuseTimer()
                                            {
                                                if (Main.S.ESPBombTimerType == "Bar")
                                                {
                                                    // Bar
                                                    upPosition("", Color.Red, true, comparedDefuse, 5);
                                                }
                                                else if (Main.S.ESPBombTimerType == "Text")
                                                    upPosition("Defuse Timer - " + comparedDefuse.ToString() + " s.", Color.Red);
                                                else if (Main.S.ESPBombTimerType == "Bar & Text")
                                                {
                                                    upPosition("Defuse Timer - " + comparedDefuse.ToString() + " s.", Color.Red);
                                                    upPosition("", Color.Red, true, comparedDefuse, 5);
                                                }
                                                else
                                                    upPosition("Defuse Timer - " + comparedDefuse.ToString() + " s.", Color.Red);
                                            }

                                        }
                                        else
                                        {
                                            dateDefuseTime = dateCurrentTime3.AddSeconds(5);
                                            defuseTime = dateDefuseTime.Ticks / TimeSpan.TicksPerSecond;
                                        }
                                        //Console.WriteLine("currentTime: " + currentTime);
                                        //Console.WriteLine("defuseTime: " + defuseTime);
                                        //Console.WriteLine("comparedDefuse: " + comparedDefuse);
                                    }
                                    #endregion
                                    #region Planting
                                    if (Main.S.ESPPlantingEnabled)
                                    {
                                        if (Player.enumWeaponID == enumWeaponID.C4 && Player.StartedArming)
                                        {
                                            Position("Planting");
                                        }
                                    }
                                    #endregion
                                    #region Defuse Kit Owner
                                    if (Main.S.ESPDefuseKitOwnerEnabled)
                                    {
                                        if (Player.HasDefuser)
                                        {
                                            Position("Defuse Kit");
                                        }
                                    }
                                    #endregion
                                    #region Distance
                                    if (Main.S.ESPDistanceEnabled)
                                    {
                                        float dist = Player.Distance;
                                        dist *= 0.0254f; // Convert to meters.
                                        Position(dist.ToString("0.0") + " m.");
                                        //if (HealthTextEnabled)
                                        //    DrawTextWithOutline(Player.Distance.ToString("0.0") + " m.", Player2DPos.X + (BoxWidth / 2) + 3, Player2DHeadPos.Y + 7, 10, drawcolor, Color.Black, true);
                                        //else
                                        //    DrawTextWithOutline(Player.Distance.ToString("0.0") + " m.", Player2DPos.X + (BoxWidth / 2) + 3, Player2DHeadPos.Y - 3, 10, drawcolor, Color.Black, true);
                                    }


                                    #endregion
                                    #region Reloading
                                    if (Main.S.ESPReloadingEnabled)
                                    {
                                        DateTime dateCurrentTime = DateTime.Now;
                                        DateTime dateReloadTime = new DateTime();
                                        dateCurrentTime = new DateTime(dateCurrentTime.Year, dateCurrentTime.Month, dateCurrentTime.Day, dateCurrentTime.Hour, dateCurrentTime.Minute, dateCurrentTime.Second, dateCurrentTime.Millisecond);
                                        dateReloadTime = new DateTime(dateReloadTime.Year, dateReloadTime.Month, dateReloadTime.Day, dateReloadTime.Hour, dateReloadTime.Minute, dateReloadTime.Second, dateReloadTime.Millisecond);
                                        long currentTime2 = dateCurrentTime.Ticks / TimeSpan.TicksPerMillisecond;

                                        if (!Player.NotReloading)
                                        {
                                            dateReloadTime = dateCurrentTime.AddMilliseconds(1056 + WeaponsReloadTimes());
                                            reloadTime = dateReloadTime.Ticks / TimeSpan.TicksPerMillisecond;
                                        }
                                        else
                                        {
                                            compared = reloadTime - currentTime2;
                                            bool notReloading;
                                            if (compared > 0)
                                            {
                                                notReloading = true;
                                            }
                                            else
                                                notReloading = false;

                                            if (notReloading && resultReload && !(Player.enumWeaponID == enumWeaponID.M4A1S && Player.SilencerOn)) // Doesn't work with silenced M4A1-S.
                                            {
                                                Position("Reloading");
                                                //Console.WriteLine("Reloading");
                                                //Console.WriteLine("-");
                                            }
                                        }

                                        int WeaponsReloadTimes()
                                        {
                                            string weaponid = Player.enumWeaponID.ToString();
                                            //Console.WriteLine("Current weapon = " + weaponid);
                                            resultReload = Enum.IsDefined(typeof(WeaponsReloadTimes), weaponid);
                                            //Console.WriteLine("resultReload = " + resultReload);
                                            if (resultReload)
                                            {
                                                int addReloadTime = (int)(WeaponsReloadTimes)Enum.Parse(typeof(WeaponsReloadTimes), weaponid);
                                                //Console.WriteLine(addReloadTime);
                                                return addReloadTime;
                                            }
                                            else
                                                return 0;
                                        }

                                        //!(Player.enumWeaponID == enumWeaponID.M4A1S && Player.SilencerOn)) // Doesn't work with silenced M4A1-S.

                                        //Console.WriteLine("Compared = " + compared);
                                        //Console.WriteLine("Current Time = " + currentTime2);
                                        //Console.WriteLine("Detonate Time = " + reloadTime);
                                        //Console.WriteLine("Not Reloading = " + Player.NotReloading);
                                    }
                                    #endregion
                                    #region Armor
                                    if (Main.S.ESPArmorEnabled)
                                    {
                                        if (Player.HasArmor)
                                        {
                                            if (Player.HasHelmet)
                                            {
                                                Position("HK");
                                            }
                                            else
                                                Position("K");
                                        }
                                    }
                                    #endregion
                                    #region Weapon
                                    if (Main.S.ESPWeaponEnabled)
                                    {
                                        if (AmmoTextEnabled && resultMaxAmmo)
                                            DrawTextWithOutline(Player.WeaponName + " (" + Player.Ammo + ")", Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y - 15, 10, drawcolor, Color.Black, true); // Above box.
                                        else
                                            DrawTextWithOutline(Player.WeaponName, Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y - 15, 10, drawcolor, Color.Black, true); // Above box.
                                    }
                                    #endregion
                                    #region Head
                                    if (Main.S.ESPHeadEnabled)
                                        DrawCircle(Player2DHeadPos.X, Player2DHeadPos.Y + 10, 5, drawcolor, 1);
                                    #endregion
                                    #region Snaplines
                                    if (Main.S.ESPSnaplinesEnabled)
                                    {
                                        DrawLine(Main.MidScreen.X, Main.MidScreen.Y + Main.MidScreen.Y, Player2DPos.X, Player2DPos.Y, drawcolor);
                                    }
                                    #endregion
                                    #region C4 Owner
                                    if (Main.S.ESPC4OwnerEnabled)
                                    {
                                        if (Player.HasC4)
                                        {
                                            Position("C4");
                                        }
                                    }
                                    #endregion

                                    // KNIFE BOT
                                    //pos++;
                                    ////float dist = Player.Distance;
                                    ////dist *= 0.0254f; // Convert to meters.
                                    //Position(Player.Distance.ToString());
                                    ////87.73

                                    // Attempt to create a Bone ESP.
                                    //DrawLine(Player2DHeadPos.X, Player2DHeadPos.Y, Player2DNeckPos.X, Player2DNeckPos.Y, drawcolor);
                                    //DrawLine(Player2DNeckPos.X, Player2DNeckPos.Y, Player2DChestPos.X, Player2DChestPos.Y, drawcolor);
                                    //DrawLine(Player2DChestPos.X, Player2DChestPos.Y, Player2DLowerChestPos.X, Player2DLowerChestPos.Y, drawcolor);
                                    //DrawLine(Player2DLowerChestPos.X, Player2DLowerChestPos.Y, Player2DStomachPos.X, Player2DStomachPos.Y, drawcolor);
                                    //DrawLine(Player2DStomachPos.X, Player2DStomachPos.Y, Player2DLegsPos.X, Player2DLegsPos.Y, drawcolor);
                                    //DrawLine(Player2DLegsPos.X, Player2DLegsPos.Y, Player2DFeetPos.X, Player2DFeetPos.Y, drawcolor);
                                    //DrawLine(Player2DFeetPos.X, Player2DFeetPos.Y, Player2DToePos.X, Player2DToePos.Y, drawcolor);

                                    //pos++;
                                    //for (int i = 0; i <= 64; i++)
                                    //{
                                    //    Position(Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iPing + (i * 4)).ToString());
                                    //    Console.WriteLine(Player.Ping.ToString());
                                    //}

                                    void Position(string info)
                                    {
                                        pos++;
                                        switch (pos)
                                        {
                                            case 1:
                                                DrawTextPos(Player2DHeadPos.Y - 3);
                                                break;
                                            case 2:
                                                DrawTextPos(Player2DHeadPos.Y + 7);
                                                break;
                                            case 3:
                                                DrawTextPos(Player2DHeadPos.Y + 17);
                                                break;
                                            case 4:
                                                DrawTextPos(Player2DHeadPos.Y + 27);
                                                break;
                                            case 5:
                                                DrawTextPos(Player2DHeadPos.Y + 37);
                                                break;
                                            case 6:
                                                DrawTextPos(Player2DHeadPos.Y + 47);
                                                break;
                                            case 7:
                                                DrawTextPos(Player2DHeadPos.Y + 57);
                                                break;
                                            default:
                                                DrawTextPos(Player2DHeadPos.Y); // To let me know if there's not enough room for all ESP right-sided elements.
                                                break;
                                        }

                                        void DrawTextPos(float Y)
                                        {
                                            DrawTextWithOutline(info, Player2DPos.X + (BoxWidth / 2) + 3, Y, 10, drawcolor, Color.Black, true);
                                        }
                                    }
                                    //bool ESPRanksAllow = true;
                                    //if (Main.S.ESPRanksEnabled)
                                    //{
                                    //    int i = 0;
                                    //    int rank = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveRanking + i * 4);
                                    //    int wins = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveWins + i * 4);
                                    //    int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + Player.GlowIndex * 4);
                                    //    int enmteam = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iTeam + i * 4);
                                    //    int enmscore = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iScore + i * 4);
                                    //    int ping = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iPing + Player.GlowIndex * 4);

                                    //    DrawTextWithOutline(ping.ToString(), Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y + (BoxHeight + 2), 10, drawcolor, Color.Black, true); // Under box.
                                    //    ESPRanksAllow = false; // To prevent
                                    //}
                                }
                            }
                        }
                    }

                    //end drawings
                    gfx.EndScene();
                }

                if ((Main.S.ESPSniperCrosshairEnabled || Main.S.RecoilCrosshairEnabled || Main.S.ESPBombTimerEnabled) && Menu.CSGOActive && Menu.InGame)
                {
                    // start drawings here
                    gfx.BeginScene();
                    gfx.ClearScene();

                    #region Sniper Crosshair
                    if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20
                    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1)
                    {
                        if (G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2)
                        {
                            DrawCrosshair(CrosshairStyle.Plus, 960, 540, 5, 1, Color.White);
                        }
                    }
                    #endregion

                    #region Recoil Crosshair
                    // Recoil Crosshair feature
                    if (Main.S.RecoilCrosshairEnabled)
                    {
                        float mx = Main.MidScreen.X;
                        float my = Main.MidScreen.Y;
                        float dy = Main.ScreenSize.Height / 90;
                        float dx = Main.ScreenSize.Width / 90;
                        mx -= (dx * G.Engine.LocalPlayer.AimPunchAngle.Y);
                        my += (dy * G.Engine.LocalPlayer.AimPunchAngle.X);
                        if (G.Engine.LocalPlayer.AimPunchAngle.X < -0.1 || G.Engine.LocalPlayer.AimPunchAngle.X > 0.6) // Prevents from drawing crosshair if there isn't any recoil.
                        {
                            DrawCrosshair(CrosshairStyle.Plus, mx, my, Main.S.RecoilCrosshairSize, 1, Main.S.ESPCrosshairCLR);
                        }
                    }
                    #endregion

                    #region Bomb Timer
                    if (Main.S.ESPBombTimerEnabled)
                    {
                        DateTime dateCurrentTime2 = DateTime.Now;
                        DateTime dateDetonateTime = new DateTime(0);
                        long currentTime = dateCurrentTime2.Ticks / TimeSpan.TicksPerSecond;

                        if (!G.Engine.BombPlanted)
                        {
                            dateDetonateTime = dateCurrentTime2.AddSeconds(40);
                            detonateTime = dateDetonateTime.Ticks / TimeSpan.TicksPerSecond;
                        }
                        else
                        {
                            compared = detonateTime - currentTime;

                            if (compared > 0)
                            {
                                if (compared > 10)
                                {
                                    DrawBombTimer(Color.Lime);
                                }
                                else if (compared > 5 && G.Engine.LocalPlayer.HasDefuser)
                                {
                                    DrawBombTimer(Color.Lime);
                                }
                                else if (compared < 10 && !G.Engine.LocalPlayer.HasDefuser)
                                {
                                    DrawBombTimer(Color.Red);
                                }
                            }

                            void DrawBombTimer(Color color)
                            {
                                if (Main.S.ESPBombTimerType == "Bar")
                                {
                                    // Bar
                                    upPosition("", Color.Red, true, compared, 40);
                                }
                                else if (Main.S.ESPBombTimerType == "Text")
                                    upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                                else if (Main.S.ESPBombTimerType == "Bar & Text")
                                {
                                    upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                                    upPosition("", color, true, compared, 40);
                                }
                                else
                                    upPosition("Bomb Timer - " + compared.ToString() + " s.", color);
                            }
                        }

                        //Console.WriteLine("Detonated.");
                        //MessageBox.Show("Detonated.");


                        //Console.WriteLine("Compared = " + compared);
                        //Console.WriteLine("Current Time = " + currentTime);
                        //Console.WriteLine("Detonate Time = " + detonateTime);
                    }
                    #endregion

                    gfx.EndScene();
                }

                #region upPosition
                void upPosition(string info, Color color, bool bar = false, long barAmount = 0, int barMaxAmount = 0)
                {
                    int defaultY = 60;
                    posUp++;
                    switch (posUp)
                    {
                        case 1:
                            if (bar)
                                Bar(defaultY + 10, barAmount, barMaxAmount);
                            else
                                DrawTextPos(defaultY);
                            break;
                        case 2:
                            if (bar)
                                Bar(defaultY + 30, barAmount, barMaxAmount);
                            else
                                DrawTextPos(defaultY + 20);
                            break;
                        case 3:
                            if (bar)
                                Bar(defaultY + 50, barAmount, barMaxAmount);
                            else
                                DrawTextPos(defaultY + 40);
                            break;
                        case 4:
                            if (bar)
                                Bar(defaultY + 70, barAmount, barMaxAmount);
                            else
                                DrawTextPos(defaultY + 60);
                            break;
                        //case 5:
                        //    DrawTextPos(defaultY + 20);
                        //    break;
                        //case 6:
                        //    DrawTextPos(defaultY + 20);
                        //    break;
                        //case 7:
                        //    DrawTextPos(defaultY + 20);
                        //    break;
                        default:
                            if (bar)
                                Bar(defaultY, barAmount, barMaxAmount);
                            else
                                DrawTextPos(defaultY); // To let me know if there's not enough room for all ESP upper elements.
                            break;
                    }

                    void DrawTextPos(float Y)
                    {
                        DrawTextWithOutline(info, 880, Y, 18, color, Color.Black, true);
                    }
                    void Bar(float Y, long amount, int maxAmount)
                    {
                        float x = 860;
                        //float y = defaultY;
                        float w = 200;
                        float h = 4;
                        float Width = (amount * w) / maxAmount; // Setting it to WeaponsMaxAmmo() instead to Ammo fixed issue.

                        DrawBox(x, Y, w, h, Color.Black, 1);
                        DrawFilledBox(x + 1, Y + 1, Width - 1, 2, color);
                    }
                }
                #endregion


                //else if (Main.S.ESPVisibleEnabled)
                //{
                //    gfx.BeginScene();
                //    gfx.ClearScene();
                //    // start drawings here

                //    foreach (Entity Player in G.EntityList)
                //    {
                //        if (Player.Spotted && !Main.S.RadarEnabled)
                //        {
                //            if (Player.EntityBase != G.Engine.LocalPlayer.EntityBase)
                //            {
                //                Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z - 5));
                //                Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z + 10));
                //                if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
                //                {
                //                    float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;
                //                    float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box
                //                    Color drawcolor;

                //                    if (Main.S.ESPHealthEnabled)
                //                    {
                //                        Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
                //                        drawcolor = Color.FromArgb(Gradient.R, Gradient.G, Gradient.B);
                //                    }
                //                    else
                //                    {
                //                        if (Player.IsTeammate && Main.S.ESPTeamEnabled)
                //                        {
                //                            drawcolor = Main.S.ESPTeamCLR;
                //                        }
                //                        else
                //                        {
                //                            drawcolor = Main.S.ESPEnemyCLR;
                //                        }

                //                        if (Player.Scoped)
                //                        {
                //                            drawcolor = Main.S.ESPScopedCLR;
                //                        }

                //                    }

                //                    #region Box
                //                    DrawOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor);
                //                    //DrawFillOutlineBox(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, Color.FromArgb(50, 198, 198, 198));
                //                    //DrawBoxEdge(Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight, drawcolor, 1);
                //                    #endregion
                //                    #region Health Bar
                //                    float Health = Player.Health;
                //                    Color HealthColor = Tools.HealthGradient(Tools.HealthToPercent((int)Health));
                //                    float x = Player2DPos.X - (BoxWidth / 2) - 8;
                //                    float y = Player2DHeadPos.Y;
                //                    float w = 4;
                //                    float h = BoxHeight;
                //                    float HealthHeight = (Health * h) / 100;

                //                    DrawBox(x, y, w, h, Color.Black, 1);
                //                    DrawFilledBox(x + 1, y + 1, 2, HealthHeight - 1, HealthColor);
                //                    #endregion
                //                    #region Snaplines
                //                    DrawLine(Main.MidScreen.X, Main.MidScreen.Y + Main.MidScreen.Y, Player2DPos.X, Player2DPos.Y, drawcolor);
                //                    #endregion
                //                }
                //            }
                //        }

                //    }
                //    //end drawings
                //    gfx.EndScene();
                //}

                Thread.Sleep(Main.S.ESPInterval);
            }
            #endregion
            #region drawing functions
            void DrawBoxEdge(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangleEdges(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawText(string text, float x, float y, int size, Color color, bool bold = false, bool italic = false)
            {
                if (Tools.InScreenPos(x, y))
                {
                    gfx.DrawText(_graphics.CreateFont("Arial", size, bold, italic), GetBrushColor(color), x, y, text);
                }
            }

            void DrawTextWithOutline(string text, float x, float y, int size, Color color, Color outlinecolor, bool bold = true, bool italic = false)
            {
                DrawText(text, x - 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x + 1, y + 1, size, outlinecolor, bold, italic);
                DrawText(text, x, y, size, color, bold, italic);
            }

            void DrawTextWithBackground(string text, float x, float y, int size, Color color, Color backcolor, bool bold = false, bool italic = false)
            {
                if (Tools.InScreenPos(x, y))
                {
                    gfx.DrawTextWithBackground(_graphics.CreateFont("Arial", size, bold, italic), GetBrushColor(color), GetBrushColor(backcolor), x, y, text);
                }
            }

            void DrawLine(float fromx, float fromy, float tox, float toy, Color color, float thiccness = 2.0f)
            {
                gfx.DrawLine(GetBrushColor(color), fromx, fromy, tox, toy, thiccness);
            }

            void DrawFilledBox(float x, float y, float width, float height, Color color)
            {
                gfx.FillRectangle(GetBrushColor(color), x, y, x + width, y + height);
            }

            void DrawCircle(float x, float y, float radius, Color color, float thiccness = 1)
            {
                gfx.DrawCircle(GetBrushColor(color), x, y, radius, thiccness);
            }

            void DrawCrosshair(CrosshairStyle style, float x, float y, float size, float thiccness, Color color)
            {
                gfx.DrawCrosshair(GetBrushColor(color), x, y, size, thiccness, style);
            }

            void DrawFillOutlineBox(float x, float y, float width, float height, Color color, Color fillcolor, float thiccness = 1.0f)
            {
                gfx.OutlineFillRectangle(GetBrushColor(color), GetBrushColor(fillcolor), x, y, x + width, y + height, thiccness);
            }

            void DrawBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRectangle(GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawOutlineBox(float x, float y, float width, float height, Color color, float thiccness = 2.0f)
            {
                gfx.OutlineRectangle(GetBrushColor(Color.FromArgb(0, 0, 0)), GetBrushColor(color), x, y, x + width, y + height, thiccness);
            }

            void DrawRoundedBox(float x, float y, float width, float height, float radius, Color color, float thiccness = 2.0f)
            {
                gfx.DrawRoundedRectangle(GetBrushColor(color), x, y, x + width, y + height, radius, thiccness);
            }
            #endregion
        }
    }
}