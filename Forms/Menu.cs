using Lerawin.Features;
using Lerawin.Classes;
using Lerawin.Utilities;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Lerawin
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            OffsetUpdater.UpdateOffsets();

            #region Start Threads
            // Found the process and everything, let's start our features in a new thread.
            if (Main.RunStartup()) // Not needed if Loader is present.
                Tools.InitializeGlobals();
            else
            {
                DialogResult waitForGame = MessageBox.Show("Waiting for CS:GO...", "Lerawin", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                if (waitForGame == DialogResult.OK)
                {
                    while (true)
                    {
                        if (Main.RunStartup())
                        {
                            Tools.InitializeGlobals();
                            break;
                        }
                        Thread.Sleep(200);
                    }
                }
                else
                    Environment.Exit(1);
            }

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    test.Run();
            //}).Start();

            // Menu
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                CheckMenu();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                CheckCSGO();
            }).Start();

            // Map Brightness
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                MapBrightness.Run();
            }).Start();

            // Skin Changer
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Features.SkinChanger.Run(); // Because VS falsely thinks I want to reference a tabPage.
            }).Start();

            // Aim
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                ViewangleAimbot.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                MouseAimbot.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Triggerbot.Run();
            }).Start();

            // Visuals
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Glow.Run();
            }).Start();

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    SoundESP.Run();
            //}).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Radar.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Chams.Run();
            }).Start();

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Visuals v = new Visuals();
            //    v.Initialize();
            //    v.Run();
            //}).Start();

            // Miscellaneous
            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                RankRevealer.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Bunnyhop.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                ReduceFlash.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                AutoPistol.Run();
            }).Start();

            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    Thirdperson.Run();
            //}).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                AutoAccept.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Hitsound.Run();
            }).Start();

            new Thread(() =>
            {
                Thread.CurrentThread.IsBackground = true;
                Fakelag.Run();
            }).Start();

            // Crashes when setting FOV.
            //new Thread(() =>
            //{
            //    Thread.CurrentThread.IsBackground = true;
            //    FOVChanger.Run();
            //}).Start();

            // Needed for Trigger Bot weapon list.
            //foreach (string weapons in Enum.GetNames(typeof(Weapon)))
            //{
            //    TriggerDropdown.Items.Add(weapons);
            //}
            #endregion
        }

        #region Menu_Load
        private void Menu_Load(object sender, EventArgs e)
        {
            //TopMost = true; // make this hover over the game, can remove if you want

            ESPBoxTypeDropdown.Items.Add("Box");
            ESPBoxTypeDropdown.Items.Add("Filled Box");
            ESPBoxTypeDropdown.Items.Add("Edges");

            ESPHealthTypeDropdown.Items.Add("Bar");
            ESPHealthTypeDropdown.Items.Add("Text");
            ESPHealthTypeDropdown.Items.Add("Bar & Text");

            ESPAmmoTypeDropdown.Items.Add("Bar");
            ESPAmmoTypeDropdown.Items.Add("Text");
            ESPAmmoTypeDropdown.Items.Add("Bar & Text");

            ESPBombTimerTypeDropdown.Items.Add("Bar");
            ESPBombTimerTypeDropdown.Items.Add("Text");
            ESPBombTimerTypeDropdown.Items.Add("Bar & Text");

            ESPDefuseTimerTypeDropdown.Items.Add("Bar");
            ESPDefuseTimerTypeDropdown.Items.Add("Text");
            ESPDefuseTimerTypeDropdown.Items.Add("Bar & Text");

            foreach (string WeaponCategory in Enum.GetNames(typeof(WeaponCategory)))
                AimWeaponDropdown.Items.Add(WeaponCategory);
            foreach (string Bones in Enum.GetNames(typeof(Bones)))
                AimBoneDropdown.Items.Add(Bones);
            foreach (string PaintKit in Enum.GetNames(typeof(PaintKit)))
            {
                //SkinChangerDropdown.Items.Add(PaintKit);
                SCglock.Items.Add(PaintKit);
                SCp2000.Items.Add(PaintKit);
                SCusps.Items.Add(PaintKit);
                SCberettas.Items.Add(PaintKit);
                SCp250.Items.Add(PaintKit);
                SCtec9.Items.Add(PaintKit);
                SCfiveseven.Items.Add(PaintKit);
                SCcz75a.Items.Add(PaintKit);
                SCrevolver.Items.Add(PaintKit);
                SCdeagle.Items.Add(PaintKit);
                SCnova.Items.Add(PaintKit);
                SCxm1014.Items.Add(PaintKit);
                SCsawedoff.Items.Add(PaintKit);
                SCmag7.Items.Add(PaintKit);
                SCnegev.Items.Add(PaintKit);
                SCm249.Items.Add(PaintKit);
                SCmac10.Items.Add(PaintKit);
                SCmp9.Items.Add(PaintKit);
                SCmp7.Items.Add(PaintKit);
                SCmp5sd.Items.Add(PaintKit);
                SCump45.Items.Add(PaintKit);
                SCp90.Items.Add(PaintKit);
                SCbizon.Items.Add(PaintKit);
                SCgalil.Items.Add(PaintKit);
                SCfamas.Items.Add(PaintKit);
                SCak47.Items.Add(PaintKit);
                SCm4a1s.Items.Add(PaintKit);
                SCm4a4.Items.Add(PaintKit);
                SCsg553.Items.Add(PaintKit);
                SCaug.Items.Add(PaintKit);
                SCssg08.Items.Add(PaintKit);
                SCawp.Items.Add(PaintKit);
                SCg3sg1.Items.Add(PaintKit);
                SCscar20.Items.Add(PaintKit);
            }

            foreach (string Weapons in Enum.GetNames(typeof(Weapons)))
            {
                SCdpRandomize.Items.Add(Weapons);
            }

            foreach (string EntityQuality in Enum.GetNames(typeof(EntityQuality)))
            {
                SCQuality.Items.Add(EntityQuality);
            }

            // Build informations.
            labBuild.Text = "Build Version: " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
            labBuildDate.Text = "Build Date: " + Properties.Resources.BuildDate;

            // Load last config at startup.
            //if (File.Exists($@"{Application.StartupPath}\LeraWin Configs\{Main.S.LastConfig}.json"))
            //{
            //    LoadConfig();
            //}
            //txtReduceFlashAmount.Text = trackBar1.Value.ToString();
            //txtChamsBrightnessAmount.Text = trackBar2.Value.ToString();
        }
        #endregion

        public static bool CSGOActive;
        public static bool InGame;

        public void CheckCSGO()
        {
            while (true)
            {
                // Close program if csgo isn't running.
                var CSGO = Process.GetProcessesByName("csgo");
                if (CSGO.Length == 0)
                {
                    Application.Exit();
                }

                Thread.Sleep(200);
            }
        }

        #region CheckMenu
        public void CheckMenu()
        {
            // Set default colors.
            GlowEnemyCLR.BackColor = Color.Red;
            GlowTeamCLR.BackColor = Color.DeepSkyBlue;
            GlowSpottedCLR.BackColor = Color.Lime;
            GlowScopedCLR.BackColor = Color.Yellow;
            ChamEnemyCLR.BackColor = Color.Red;
            ChamTeamCLR.BackColor = Color.DeepSkyBlue;
            ChamScopedCLR.BackColor = Color.Yellow;
            ESPEnemyCLR.BackColor = Color.Red;
            ESPTeamCLR.BackColor = Color.DeepSkyBlue;
            ESPSpottedCLR.BackColor = Color.Lime;
            ESPScopedCLR.BackColor = Color.Yellow;
            ESPCrosshairCLR.BackColor = Color.Cyan;

            bool StreamModeEnabled = false;
            bool StreamModeWarning = true;

            bool subtractOneKill = true;
            // Here we make the main variables equal to what our menu checkboxes say
            while (true)
            {
                this.Invoke((MethodInvoker)delegate
                {
                    // Aim
                    Main.S.ViewangleAimbotEnabled = ViewangleAimbotCheck.Checked;
                    Main.S.MouseAimbotEnabled = MouseAimbotCheck.Checked;
                    Main.S.AimEnemyEnabled = AimEnemyCheck.Checked;
                    Main.S.AimTeamEnabled = AimTeamCheck.Checked;
                    Main.S.AimSilentEnabled = AimSilentCheck.Checked;
                    Main.S.AimITAEnabled = AimITACheck.Checked;
                    Main.S.AimTVEnabled = AimTVCheck.Checked;
                    Main.S.AimNIAEnabled = AimNIACheck.Checked;
                    Main.S.AimNMEnabled = AimNMCheck.Checked;
                    Main.S.AimASEnabled = AimASCheck.Checked;
                    Main.S.AimScopedEnabled = AimScopedCheck.Checked;
                    Main.S.AimFOVPixelDistance = (int)AimFOVPixelDistance.Value;
                    Main.S.AimSmooth = (int)AimSmooth.Value;
                    Main.S.AimIgnoreBullets = (int)AimIgnoreBullets.Value;
                    Main.S.AimNextTargetCooldown = (int)AimNextTargetCooldown.Value;
                    AimBoneDropdown.Invoke(new Action(() => Main.S.AimBone = AimBoneDropdown.Text));

                    Main.S.TriggerEnemyEnabled = TriggerEnemyCheck.Checked;
                    Main.S.TriggerTeamEnabled = TriggerTeamCheck.Checked;
                    Main.S.TriggerStickyEnabled = TriggerStickyCheck.Checked;
                    //Main.S.TriggerNMEnabled = TriggerNMCheck.Checked;
                    //Main.S.TriggerScopedEnabled = TriggerScopedCheck.Checked;
                    Main.S.TriggerMinFirerate = (int)TriggerMinFirerate.Value;
                    Main.S.TriggerMaxFirerate = (int)TriggerMaxFirerate.Value;
                    Main.S.TriggerMinShotDelay = (int)TriggerMinShotDelay.Value;
                    Main.S.TriggerMaxShotDelay = (int)TriggerMaxShotDelay.Value;
                    Main.S.TriggerStickyFOV = (int)TriggerStickyFOV.Value;
                    Main.S.TriggerStickySmooth = (int)TriggerStickySmooth.Value;

                    // Visuals
                    Main.S.GlowEnemyEnabled = GlowEnemyCheck.Checked;
                    Main.S.GlowTeamEnabled = GlowTeamCheck.Checked;
                    Main.S.SndGlowEnemyEnabled = SndGlowEnemyCheck.Checked;
                    Main.S.SndGlowTeamEnabled = SndGlowTeamCheck.Checked;
                    Main.S.GlowVisibleEnabled = GlowVisibleCheck.Checked;
                    Main.S.GlowHealthBEnabled = GlowHealthBCheck.Checked;

                    Main.S.RadarEnabled = RadarCheck.Checked;
                    Main.S.RadarOnKeyEnabled = RadarOnKeyCheck.Checked;
                    Main.S.ChamsEnabled = ChamsCheck.Checked;
                    Main.S.ChamsHealthBEnabled = ChamsHealthBCheck.Checked;
                    Main.S.MapBrightnessEnabled = MapBrightnessCheck.Checked;
                    Main.S.MapBrightnessAmount = (int)MapBrightnessAmount.Value;

                    Main.S.ESPEnemyEnabled = ESPEnemyCheck.Checked;
                    Main.S.ESPTeamEnabled = ESPTeamCheck.Checked;
                    Main.S.SndESPEnabled = SndESPCheck.Checked;
                    Main.S.ESPVisibleEnabled = ESPVisibleCheck.Checked;
                    Main.S.ESPHealthBEnabled = ESPHealthBCheck.Checked;
                    Main.S.RecoilCrosshairEnabled = RecoilCrosshairCheck.Checked;
                    Main.S.RecoilCrosshairSize = (int)RecoilCrosshairSize.Value;
                    Main.S.ESPBoxEnabled = ESPBoxCheck.Checked;
                    Main.S.ESPHealthEnabled = ESPHealthCheck.Checked;
                    Main.S.ESPAmmoEnabled = ESPAmmoCheck.Checked;
                    Main.S.ESPBombTimerEnabled = ESPBombTimerCheck.Checked;
                    Main.S.ESPDefuseTimerEnabled = ESPDefuseTimerCheck.Checked;
                    Main.S.ESPDefusingEnabled = ESPDefuseTimerCheck.Checked;
                    Main.S.ESPPlantingEnabled = ESPPlantingCheck.Checked;
                    Main.S.ESPDefuseKitOwnerEnabled = ESPDefuseKitOwnerCheck.Checked;
                    Main.S.ESPSniperCrosshairEnabled = ESPSniperCrosshairCheck.Checked;
                    Main.S.ESPDistanceEnabled = ESPDistanceCheck.Checked;
                    Main.S.ESPReloadingEnabled = ESPReloadingCheck.Checked;
                    Main.S.ESPArmorEnabled = ESPArmorCheck.Checked;
                    Main.S.ESPWeaponEnabled = ESPWeaponCheck.Checked;
                    Main.S.ESPHeadEnabled = ESPHeadCheck.Checked;
                    Main.S.ESPSnaplinesEnabled = ESPSnaplinesCheck.Checked;
                    Main.S.ESPBombTimerEnabled = ESPBombTimerCheck.Checked;
                    Main.S.ESPC4OwnerEnabled = ESPC4OwnerCheck.Checked;

                    // Miscellaneous
                    Main.S.BunnyhopEnabled = BunnyhopCheck.Checked;
                    Main.S.BunnyhopVelocityEnabled = BunnyhopVelocityCheck.Checked;
                    Main.S.ReduceFlashEnabled = ReduceFlashCheck.Checked;
                    Main.S.AutoPistolEnabled = AutoPistolCheck.Checked;
                    //Main.S.ThirdpersonEnabled = ThirdpersonCheck.Checked;
                    Main.S.AutoAcceptEnabled = AutoAcceptCheck.Checked;
                    //if (AutoAccept.allow == false)
                    //{
                    //    AutoAcceptCheck.Checked = false;
                    //}
                    Main.S.HitsoundEnabled = HitsoundCheck.Checked;
                    Main.S.HitsoundVisOnlyEnabled = HitsoundVisOnlyCheck.Checked;
                    Main.S.FakelagEnabled = FakelagCheck.Checked;
                    Main.S.FOVChangerEnabled = FOVChangerCheck.Checked;
                    Main.S.FOVAmount = (int)FOVAmount.Value;

                    // Skin Changer
                    Main.S.SCEnabled = SCCheck.Checked;
                    Main.S.SCBuyZoneOnlyEnabled = SCBuyZoneOnlyCheck.Checked;
                    Main.S.SCStatTrackEnabled = SCStatTrack.Checked;
                    Main.S.SCStatTrackAmount = (int)SCStatTrackAmount.Value;
                    Main.S.SCRealStatTrackEnabled = SCRealStatTrackCheck.Checked;
                    if (SCStatTrack.Checked && SCRealStatTrackCheck.Checked && Features.SkinChanger.realStattrack)
                    {
                        if (!subtractOneKill)
                        {
                            SCStatTrackAmount.Value++;
                        }
                        subtractOneKill = false;
                        //SCStatTrackAmount.Value++;
                        //if (subtractOneKill)
                        //{
                        //    SCStatTrackAmount.Value--;
                        //    subtractOneKill = false;
                        //}
                    }
                    Main.S.SCWearAmount = (float)SCWearAmount.Value;
                    Main.S.SCSeedAmount = (int)SCSeedAmount.Value;

                    // Settings
                    Main.S.ONMVelocity = (int)ONMVelocity.Value;
                    Main.S.GlowInterval = (int)GlowInterval.Value;
                    Main.S.ESPInterval = (int)ESPInterval.Value;
                    Main.S.RadarInterval = (int)RadarInterval.Value;
                    Main.S.BunnyhopMinInterval = (int)BunnyhopMinInterval.Value;
                    Main.S.BunnyhopMaxInterval = (int)BunnyhopMaxInterval.Value;
                    Main.S.ReduceFlashAmount = (int)ReduceFlashAmount.Value;
                    Main.S.ChamsBrightness = (int)ChamsBrightness.Value;
                    Main.S.FakelagAmount = (int)FakelagAmount.Value;
                    Main.S.FakelagInterval = (int)FakelagInterval.Value;
                    Main.S.StreamMode = StreamMode.Checked;
                });
                
                // Colors
                Main.S.GlowEnemyCLR = GlowEnemyCLR.BackColor;
                Main.S.GlowTeamCLR = GlowTeamCLR.BackColor;
                Main.S.GlowSpottedCLR = GlowSpottedCLR.BackColor;
                Main.S.GlowScopedCLR = GlowScopedCLR.BackColor;
                Main.S.ChamEnemyCLR = ChamEnemyCLR.BackColor;
                Main.S.ChamTeamCLR = ChamTeamCLR.BackColor;
                Main.S.ChamScopedCLR = ChamScopedCLR.BackColor;
                Main.S.ESPEnemyCLR = ESPEnemyCLR.BackColor;
                Main.S.ESPTeamCLR = ESPTeamCLR.BackColor;
                Main.S.ESPSpottedCLR = ESPSpottedCLR.BackColor;
                Main.S.ESPScopedCLR = ESPScopedCLR.BackColor;
                Main.S.ESPCrosshairCLR = ESPCrosshairCLR.BackColor;

                // Experimental
                Main.S.ESPRanksEnabled = ESPRanksCheck.Checked;

                // Streaming mode
                if (StreamMode.Checked)
                {
                    if (GlowEnemyCheck.Checked)
                        GlowEnemyCheck.Checked = false;
                    if (GlowTeamCheck.Checked)
                        GlowTeamCheck.Checked = false;
                    if (SndGlowEnemyCheck.Checked)
                        SndGlowEnemyCheck.Checked = false;
                    if (RadarCheck.Checked)         // Bugs out without this if.
                        RadarCheck.Checked = false;
                    if (RadarOnKeyCheck.Checked)
                        RadarOnKeyCheck.Checked = false;
                    if (MapBrightnessCheck.Checked)
                        MapBrightnessCheck.Checked = false;
                    if (ChamsCheck.Checked)         // Bugs out without this if.
                        ChamsCheck.Checked = false;
                    if (MapBrightnessCheck.Checked)
                        MapBrightnessCheck.Checked = false;
                    if (ReduceFlashCheck.Checked)   // Bugs out without this if.
                        ReduceFlashCheck.Checked = false;
                    if (AutoAcceptCheck.Checked)
                        AutoAcceptCheck.Checked = false;
                    if (HitsoundCheck.Checked)
                        HitsoundCheck.Checked = false;
                    if (FakelagCheck.Checked)
                        FakelagCheck.Checked = false;
                    if (FOVChangerCheck.Checked)
                        FakelagCheck.Checked = false;
                    if (SCCheck.Checked)
                        SCCheck.Checked = false;
                    if (SCStatTrack.Checked)
                        SCStatTrack.Checked = false;

                    this.Invoke((MethodInvoker)delegate
                    {
                        if (GlowEnemyCheck.Enabled)
                            GlowEnemyCheck.Enabled = false;
                        if (GlowTeamCheck.Enabled)
                            GlowTeamCheck.Enabled = false;
                        if (SndGlowEnemyCheck.Enabled)
                            SndGlowEnemyCheck.Enabled = false;
                        if (SndGlowTeamCheck.Enabled)
                            SndGlowTeamCheck.Enabled = false;
                        if (RadarCheck.Enabled)
                            RadarCheck.Enabled = false;
                        if (RadarOnKeyCheck.Enabled)
                            RadarOnKeyCheck.Enabled = false;
                        if (MapBrightnessCheck.Enabled)
                            MapBrightnessCheck.Enabled = false;
                        if (ChamsCheck.Enabled)
                            ChamsCheck.Enabled = false;
                        if (ReduceFlashCheck.Enabled)
                            ReduceFlashCheck.Enabled = false;
                        if (AutoAcceptCheck.Enabled)
                            AutoAcceptCheck.Enabled = false;
                        if (HitsoundCheck.Enabled)
                            HitsoundCheck.Enabled = false;
                        if (FakelagCheck.Enabled)
                            FakelagCheck.Enabled = false;
                        if (FOVChangerCheck.Enabled)
                            FOVChangerCheck.Enabled = false;
                        if (SCCheck.Enabled)
                            SCCheck.Enabled = false;
                        if (SCStatTrack.Enabled)
                            SCStatTrack.Enabled = false;
                    });

                    StreamModeEnabled = true;

                    if (Chams.Enabled && StreamModeWarning)
                    {
                        MessageBox.Show("You have enabled Chams before enabling Stream Mode, thus the models of players become yellowish. Please restart the game to avoid exposing.", "Lerawin", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        StreamModeWarning = false;
                    }
                }
                else if (StreamModeEnabled)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        GlowEnemyCheck.Enabled = true;
                        GlowTeamCheck.Enabled = true;
                        SndGlowEnemyCheck.Enabled = true;
                        SndGlowTeamCheck.Enabled = true;
                        RadarCheck.Enabled = true;
                        RadarOnKeyCheck.Enabled = true;
                        MapBrightnessCheck.Enabled = true;
                        ChamsCheck.Enabled = true;
                        ReduceFlashCheck.Enabled = true;
                        AutoAcceptCheck.Enabled = true;
                        HitsoundCheck.Enabled = true;
                        FakelagCheck.Enabled = true;
                        FOVChangerCheck.Enabled = true;
                        SCCheck.Enabled = true;
                        SCStatTrack.Enabled = true;
                    });

                    StreamModeEnabled = false;
                }

                this.Invoke((MethodInvoker)delegate
                {
                    if ((Memory.GetAsyncKeyState(Keys.VK_INSERT) & 1) > 0)
                        Visible = !Visible;
                    if ((Memory.GetAsyncKeyState(Keys.VK_END) & 1) > 0)
                        Application.Exit();
                });

                //IntPtr MWhandle = Tools.handle;
                //IntPtr CWhandle = Memory.GetForegroundWindow();

                //if (MWhandle == CWhandle)
                //    CSGOActive = true;
                //else
                //    CSGOActive = false;

                CSGOActive = true;

                if (G.Engine.GameState == 6)
                    InGame = true;
                else
                {
                    InGame = false;
                    Features.Visuals.ESPRanksAllow = false;
                }

                //if ((Memory.GetAsyncKeyState(Keys.VK_F2) & 1) > 0)
                //    MouseAimbotCheck.Checked = !MouseAimbotCheck.Checked;

                Thread.Sleep(200); // Greatly reduces cpu usage.
            }
        }

        //delegate void SetTextCallback(string text);
        //private void SetText(string text)
        //{
        //    // InvokeRequired required compares the thread ID of the
        //    // calling thread to the thread ID of the creating thread.
        //    // If these threads are different, it returns true.
        //    if (this.AimBoneDropdown.InvokeRequired)
        //    {
        //        SetTextCallback d = new SetTextCallback(SetText);
        //        this.Invoke(d, new object[] { text });
        //    }
        //    else
        //    {
        //        this.AimBoneDropdown.Text = text;
        //    }
        //}
        #endregion

        #region Menu_FormClosed
        // Close program without leaving marks.
        public static bool formIsClosed;
        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            formIsClosed = true;
            resetFeatures();
        }
        #endregion

        #region Configs
        #region LoadConfig()
        private void LoadConfig()
        {
            string json = File.ReadAllText($@"{Application.StartupPath}\LeraWin Configs\{ConfigDropdown.Text}.json");

            Settings s = JsonConvert.DeserializeObject<Settings>(json);

            // Aim
            ViewangleAimbotCheck.Checked = s.ViewangleAimbotEnabled;
            MouseAimbotCheck.Checked = s.MouseAimbotEnabled;
            AimEnemyCheck.Checked = s.AimEnemyEnabled;
            AimTeamCheck.Checked = s.AimTeamEnabled;
            AimSilentCheck.Checked = s.AimSilentEnabled;
            AimITACheck.Checked = s.AimITAEnabled;
            AimTVCheck.Checked = s.AimTVEnabled;
            AimNIACheck.Checked = s.AimNIAEnabled;
            AimNMCheck.Checked = s.AimNMEnabled;
            AimASCheck.Checked = s.AimASEnabled;
            AimScopedCheck.Checked = s.AimScopedEnabled;
            AimFOVPixelDistance.Value = s.AimFOVPixelDistance;
            AimSmooth.Value = s.AimSmooth;
            AimIgnoreBullets.Value = s.AimIgnoreBullets;
            AimNextTargetCooldown.Value = s.AimNextTargetCooldown;
            AimBoneDropdown.Text = s.AimBone;

            TriggerEnemyCheck.Checked = s.TriggerEnemyEnabled;
            TriggerTeamCheck.Checked = s.TriggerTeamEnabled;
            TriggerStickyCheck.Checked = s.TriggerStickyEnabled;
            //TriggerNMCheck.Checked = s.TriggerNMEnabled;
            //TriggerScopedCheck.Checked = s.TriggerScopedEnabled;
            TriggerMinFirerate.Value = s.TriggerMinFirerate;
            TriggerMaxFirerate.Value = s.TriggerMaxFirerate;
            TriggerMinShotDelay.Value = s.TriggerMinShotDelay;
            TriggerMaxShotDelay.Value = s.TriggerMaxShotDelay;
            TriggerStickyFOV.Value = s.TriggerStickyFOV;
            TriggerStickySmooth.Value = s.TriggerStickySmooth;

            // Visuals
            GlowEnemyCheck.Checked = s.GlowEnemyEnabled;
            GlowTeamCheck.Checked = s.GlowTeamEnabled;
            SndGlowEnemyCheck.Checked = s.SndGlowEnemyEnabled;
            SndGlowTeamCheck.Checked = s.SndGlowTeamEnabled;
            GlowVisibleCheck.Checked = s.GlowVisibleEnabled;
            GlowHealthBCheck.Checked = s.GlowHealthBEnabled;

            RadarCheck.Checked = s.RadarEnabled;
            RadarOnKeyCheck.Checked = s.RadarOnKeyEnabled;
            ChamsCheck.Checked = s.ChamsEnabled;
            ChamsHealthBCheck.Checked = s.ChamsHealthBEnabled;
            MapBrightnessCheck.Checked = s.MapBrightnessEnabled;
            MapBrightnessAmount.Value = s.MapBrightnessAmount;

            ESPEnemyCheck.Checked = s.ESPEnemyEnabled;
            ESPTeamCheck.Checked = s.ESPTeamEnabled;
            SndESPCheck.Checked = s.SndESPEnabled;
            ESPVisibleCheck.Checked = s.ESPVisibleEnabled;
            ESPHealthBCheck.Checked = s.ESPHealthBEnabled;
            RecoilCrosshairCheck.Checked = s.RecoilCrosshairEnabled;
            RecoilCrosshairSize.Value = s.RecoilCrosshairSize;
            ESPBoxCheck.Checked = s.ESPBoxEnabled;
            ESPBoxTypeDropdown.Text = s.ESPBoxType;
            ESPHealthCheck.Checked = s.ESPHealthEnabled;
            ESPHealthTypeDropdown.Text = s.ESPHealthType;
            ESPAmmoCheck.Checked = s.ESPAmmoEnabled;
            ESPAmmoTypeDropdown.Text = s.ESPAmmoType;
            ESPBombTimerCheck.Checked = s.ESPBombTimerEnabled;
            ESPBombTimerTypeDropdown.Text = s.ESPBombTimerType;
            ESPDefuseTimerCheck.Checked = s.ESPDefuseTimerEnabled;
            ESPDefuseTimerTypeDropdown.Text = s.ESPDefuseTimerType;
            ESPDefusingCheck.Checked = s.ESPDefusingEnabled;
            ESPPlantingCheck.Checked = s.ESPPlantingEnabled;
            ESPDefuseKitOwnerCheck.Checked = s.ESPDefuseKitOwnerEnabled;
            ESPSniperCrosshairCheck.Checked = s.ESPSniperCrosshairEnabled;
            ESPDistanceCheck.Checked = s.ESPDistanceEnabled;
            ESPReloadingCheck.Checked = s.ESPReloadingEnabled;
            ESPArmorCheck.Checked = s.ESPArmorEnabled;
            ESPWeaponCheck.Checked = s.ESPWeaponEnabled;
            ESPHeadCheck.Checked = s.ESPHeadEnabled;
            ESPSnaplinesCheck.Checked = s.ESPSnaplinesEnabled;
            ESPC4OwnerCheck.Checked = s.ESPC4OwnerEnabled;

            // Miscellaneous
            BunnyhopCheck.Checked = s.BunnyhopEnabled;
            BunnyhopVelocityCheck.Checked = s.BunnyhopVelocityEnabled;
            ReduceFlashCheck.Checked = s.ReduceFlashEnabled;
            AutoPistolCheck.Checked = s.AutoPistolEnabled;
            AutoAcceptCheck.Checked = s.AutoAcceptEnabled;
            HitsoundCheck.Checked = s.HitsoundEnabled;
            HitsoundVisOnlyCheck.Checked = s.HitsoundVisOnlyEnabled;
            FakelagCheck.Checked = s.FakelagEnabled;
            //FOVChangerCheck.Checked = s.FOVChangerEnabled;
            FOVAmount.Value = s.FOVAmount;

            // Skin Changer
            SCCheck.Checked = s.SCEnabled;
            SCBuyZoneOnlyCheck.Checked = s.SCBuyZoneOnlyEnabled;
            SCStatTrack.Checked = s.SCStatTrackEnabled;
            SCRealStatTrackCheck.Checked = s.SCRealStatTrackEnabled;
            SCStatTrackAmount.Value = s.SCStatTrackAmount;
            Main.S.SCWearAmount = s.SCWearAmount;
            SCSeedAmount.Value = s.SCSeedAmount;
            SCQuality.Text = s.SCQuality;

            SCglock.Text = s.glockSkin;
            SCp2000.Text = s.p2000Skin;
            SCusps.Text = s.uspsSkin;
            SCberettas.Text = s.berettasSkin;
            SCp250.Text = s.p250Skin;
            SCtec9.Text = s.tec9Skin;
            SCfiveseven.Text = s.fivesevenSkin;
            SCcz75a.Text = s.cz75aSkin;
            SCrevolver.Text = s.revolverSkin;
            SCdeagle.Text = s.deagleSkin;
            SCnova.Text = s.novaSkin;
            SCxm1014.Text = s.xm1014Skin;
            SCsawedoff.Text = s.sawedoffSkin;
            SCmag7.Text = s.mag7Skin;
            SCnegev.Text = s.negevSkin;
            SCm249.Text = s.m249Skin;
            SCmac10.Text = s.mac10Skin;
            SCmp9.Text = s.mp9Skin;
            SCmp7.Text = s.mp7Skin;
            SCmp5sd.Text = s.mp5sdSkin;
            SCump45.Text = s.ump45Skin;
            SCp90.Text = s.p90Skin;
            SCbizon.Text = s.bizonSkin;
            SCgalil.Text = s.galilSkin;
            SCfamas.Text = s.famasSkin;
            SCak47.Text = s.ak47Skin;
            SCm4a1s.Text = s.m4a1sSkin;
            SCm4a4.Text = s.m4a4Skin;
            SCsg553.Text = s.sg553Skin;
            SCaug.Text = s.augSkin;
            SCssg08.Text = s.ssg08Skin;
            SCawp.Text = s.awpSkin;
            SCg3sg1.Text = s.g3sg1Skin;
            SCscar20.Text = s.scar20Skin;

            // Settings
            ONMVelocity.Value = s.ONMVelocity;
            GlowInterval.Value = s.GlowInterval;
            ESPInterval.Value = s.ESPInterval;
            RadarInterval.Value = s.RadarInterval;
            BunnyhopMinInterval.Value = s.BunnyhopMinInterval;
            BunnyhopMaxInterval.Value = s.BunnyhopMaxInterval;
            ReduceFlashAmount.Value = s.ReduceFlashAmount;
            ChamsBrightness.Value = s.ChamsBrightness;
            FakelagAmount.Value = s.FakelagAmount;
            FakelagInterval.Value = s.FakelagInterval;
            StreamMode.Checked = s.StreamMode;

            // Colors
            GlowEnemyCLR.BackColor = s.GlowEnemyCLR;
            GlowTeamCLR.BackColor = s.GlowTeamCLR;
            GlowSpottedCLR.BackColor = s.GlowSpottedCLR;
            GlowScopedCLR.BackColor = s.GlowScopedCLR;

            ChamEnemyCLR.BackColor = s.ChamEnemyCLR;
            ChamTeamCLR.BackColor = s.ChamTeamCLR;
            ChamScopedCLR.BackColor = s.ChamScopedCLR;

            ESPEnemyCLR.BackColor = s.ESPEnemyCLR;
            ESPTeamCLR.BackColor = s.ESPTeamCLR;
            ESPSpottedCLR.BackColor = s.ESPSpottedCLR;
            ESPScopedCLR.BackColor = s.ESPScopedCLR;
            ESPCrosshairCLR.BackColor = s.ESPCrosshairCLR;

            // Experimental
            //ThirdpersonCheck.Checked = s.ThirdpersonEnabled;
        }
        #endregion

        private void ConfigDropdown_Click(object sender, EventArgs e)
        {
            if (IfConfigsExists())
            {
                UpdateConfigList();
            }
        }

        private void ConfigDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadConfig();
            txtConfig.Text = ConfigDropdown.Text;
            //Main.S.LastConfig = ConfigDropdown.Text;
        }

        public void UpdateConfigList()
        {
            DirectoryInfo d = new DirectoryInfo($@"{Application.StartupPath}\LeraWin Configs");
            FileInfo[] Files = d.GetFiles("*.json");
            ConfigDropdown.Items.Clear();
            foreach (FileInfo file in Files)
            {
                ConfigDropdown.Items.Add(file.Name.Substring(0, file.Name.Length - 5));
            }

            // Doesn't update last config from config list after deleting (there aren't any configs).
            //DirectoryInfo d = new DirectoryInfo($@"{Application.StartupPath}\LeraWin Configs");

            //if (IfConfigsExists())
            //{
            //    FileInfo[] Files = d.GetFiles("*.json");
            //    ConfigDropdown.Items.Clear();
            //    foreach (FileInfo file in Files)
            //    {
            //        ConfigDropdown.Items.Add(file.Name.Substring(0, file.Name.Length - 12));
            //    }
            //}
        }

        private bool IfConfigsExists()
        {
            try
            {
                return new DirectoryInfo($@"{Application.StartupPath}\LeraWin Configs").EnumerateFiles("*json").Any();
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            ConfigManager.CreateEnvironment();
            ConfigManager.AddConfig(txtConfig.Text);

            foreach (string config in Main.Configs)
            {
                ConfigDropdown.Items.Add(config);
            }

            ConfigManager.SaveConfig(txtConfig.Text);
            UpdateConfigList();
            ConfigDropdown.Text = txtConfig.Text;
        }

        private void btnConfigDelete_Click(object sender, EventArgs e)
        {
            File.Delete($@"{Application.StartupPath}\LeraWin Configs\{ConfigDropdown.Text}.json");
            UpdateConfigList();
        }
        #endregion

        public static bool RankRevealerClicked;
        private void btnRankRevealer_Click(object sender, EventArgs e)
        {
            RankRevealerClicked = true;
        }

        private void RadarCheck_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Entity Player in G.EntityList)
            {
                if (!Player.Valid) continue;
                Player.Spotted = false;
            }
        }

        private void ESPBoxTypesDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.S.ESPBoxType = ESPBoxTypeDropdown.Text;
        }

        private void ESPHealthTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.S.ESPHealthType = ESPHealthTypeDropdown.Text;
        }

        private void ESPAmmoTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.S.ESPAmmoType = ESPAmmoTypeDropdown.Text;
        }

        private void ESPBombTimerTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.S.ESPBombTimerType = ESPBombTimerTypeDropdown.Text;
        }

        private void ESPDefuseTimerTypeDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            Main.S.ESPDefuseTimerType = ESPDefuseTimerTypeDropdown.Text;
        }

        //private void AutoAcceptCheck_CheckedChanged(object sender)
        //{
        //    AutoAccept.allow = true;
        //}

        private void HitsoundCheck_CheckedChanged(object sender)
        {
            if (!File.Exists($@"{Application.StartupPath}\hitsound.wav") && HitsoundCheck.Checked == true)
            {
                MessageBox.Show("You need hitsound.wav file in the program directory in order to use Hitsound.", "Lerawin", MessageBoxButtons.OK,  MessageBoxIcon.Warning);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FakelagCheck_CheckedChanged(object sender)
        {
            G.Engine.SendPackets = 1;
        }

        private void ChamsCheck_CheckedChanged(object sender)
        {
            if (!ChamsCheck.Checked)
            {
                foreach (Entity Player in G.EntityList)
                {
                    if (!Player.Valid) continue;
                    Player.ResetChams();
                    Player.Spotted = false;
                }
                G.Engine.ModelBrightness = 0;
            }
        }

        private void MapBrightnessCheck_CheckedChanged(object sender)
        {
            if (!MapBrightnessCheck.Checked)
                G.Engine.LocalPlayer.MapBrightness = 1;
        }

        private void FOVChangerCheck_CheckedChanged(object sender)
        {
            //if (!FOVChangerCheck.Checked)
            //    G.Engine.LocalPlayer.FOV = 90;
        }

        private void ReduceFlashCheck_CheckedChanged(object sender)
        {
            if (!ReduceFlashCheck.Checked)
                G.Engine.LocalPlayer.FlashMaxAlpha = 255;
        }

        private void SCCheck_CheckedChanged(object sender)
        {
            if (!SCCheck.Checked)
            {
                Features.SkinChanger.PaintKit = 0;
                Features.SkinChanger.StatTrack = 0;
                Features.SkinChanger.Wear = 0;
                Features.SkinChanger.Seed = 0;
                Features.SkinChanger.EntityQuality = 0;
                G.Engine.FullUpdate();
            }
        }

        private void resetFeatures()
        {
            // Chams
            if (Main.S.ChamsEnabled)
            {
                foreach (Entity Player in G.EntityList)
                {
                    if (!Player.Valid) continue;
                    Player.ResetChams();
                    Player.Spotted = false;
                }
                G.Engine.ModelBrightness = 0;
            }

            // MapBrightness
            if (Main.S.MapBrightnessEnabled)
            {
                G.Engine.LocalPlayer.MapBrightness = 1;
            }

            // ReduceFlash
            if (Main.S.ReduceFlashEnabled)
            {
                G.Engine.LocalPlayer.FlashMaxAlpha = 255;
            }

            // Fakelag
            if (Main.S.FakelagEnabled)
            {
                G.Engine.SendPackets = 1;
            }

            // Delete temp file needed for Reveal Ranks feature.
            File.Delete($@"{Application.StartupPath}\ranks.txt");

            // FOVChanger
            //if (Main.S.FOVChangerEnabled)
            //{
            //    G.Engine.SendPackets = 1;
            //}

            // SkinChanger
            if (Main.S.SCEnabled)
            {
                Features.SkinChanger.PaintKit = 0;
                Features.SkinChanger.StatTrack = 0;
                Features.SkinChanger.Wear = 0;
                Features.SkinChanger.Seed = 0;
                Features.SkinChanger.EntityQuality = 0;

                G.Engine.FullUpdate();
            }

        }

        #region SkinChanger

        private void nsButton1_Click(object sender, EventArgs e)
        {
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
            G.Engine.FullUpdate();
        }

        private void SkinChangerDropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (primaryEnabled)
            //{
            //    ak47Skin = 0;
            //    G.Engine.FullUpdate();
            //}
            //if (secondaryEnabled)
            //{
            //    glockSkin = 0;
            //    G.Engine.FullUpdate();
            //}

            // WORKS FINE ->
            //for (int i = 0; i < 8; i++)
            //{
            //    int Local = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwLocalPlayer);
            //    int Weapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hMyWeapons + i * 0x4) & 0xFFF;
            //    Weapon = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (Weapon - 1) * 0x10);
            //    short WeaponID2 = Memory.ReadMemory<short>(Weapon + Main.O.netvars.m_iItemDefinitionIndex);
            //    //if (WeaponID2 == 0) { continue; }
            //    var CSGOWeaponID = (enumWeaponID)WeaponID2;

            //    switch (CSGOWeaponID)
            //    {
            //        case enumWeaponID.AK47:
            //            string ak47 = SkinChangerDropdown.Text;
            //            ak47Skin = (int)(PaintKit)Enum.Parse(typeof(PaintKit), ak47);
            //            //primaryEnabled = true;
            //            //secondaryEnabled = false;
            //            break;
            //        case enumWeaponID.GLOCK:
            //            string glock = SkinChangerDropdown.Text;
            //            glockSkin = (int)(PaintKit)Enum.Parse(typeof(PaintKit), glock);
            //            //secondaryEnabled = true;
            //            //primaryEnabled = false;
            //            break;
            //    }
            //}

            //switch (G.Engine.LocalPlayer.enumWeaponID)
            //{
            //    case enumWeaponID.AK47:
            //        string ak47 = SkinChangerDropdown.Text;
            //        ak47Skin = (int)(PaintKit)Enum.Parse(typeof(PaintKit), ak47);
            //        //primaryEnabled = true;
            //        //secondaryEnabled = false;
            //        break;
            //    case enumWeaponID.GLOCK:
            //        string glock = SkinChangerDropdown.Text;
            //        glockSkin = (int)(PaintKit)Enum.Parse(typeof(PaintKit), glock);
            //        //secondaryEnabled = true;
            //        //primaryEnabled = false;
            //        break;
            //}

            //int Local = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwLocalPlayer);
            //for (int i = 0; i < 8; i++)
            //{
            //    int Weapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hMyWeapons + i * 0x4) & 0xFFF;
            //    Weapon = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (Weapon - 1) * 0x10);
            //    short WeaponID2 = Memory.ReadMemory<short>(Weapon + Main.O.netvars.m_iItemDefinitionIndex);
            //    if (WeaponID2 == 0) { continue; }
            //    var CSGOWeaponID = (enumWeaponID)WeaponID2;
            //    //string CustomName = "Name";

            //    switch (CSGOWeaponID)
            //    {
            //        case enumWeaponID.GLOCK:
            //            string glock = this.SkinChangerDropdown.Text;
            //            Lerawin.Menu.glockSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), glock));
            //            break;
            //        case enumWeaponID.AK47:
            //            string ak47 = this.SkinChangerDropdown.Text;
            //            Lerawin.Menu.ak47Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), ak47));
            //            break;
            //    }


            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //    G.Engine.FullUpdate();
            //}
        }

        public static int glockSkin;
        public static int p2000Skin;
        public static int uspsSkin;
        public static int berettasSkin;
        public static int p250Skin;
        public static int tec9Skin;
        public static int fivesevenSkin;
        public static int cz75aSkin;
        public static int revolverSkin;
        public static int deagleSkin;
        public static int novaSkin;
        public static int xm1014Skin;
        public static int sawedoffSkin;
        public static int mag7Skin;
        public static int negevSkin;
        public static int m249Skin;
        public static int mac10Skin;
        public static int mp9Skin;
        public static int mp7Skin;
        public static int mp5sdSkin;
        public static int ump45Skin;
        public static int p90Skin;
        public static int bizonSkin;
        public static int galilSkin;
        public static int famasSkin;
        public static int ak47Skin;
        public static int m4a1sSkin;
        public static int m4a4Skin;
        public static int sg553Skin;
        public static int augSkin;
        public static int ssg08Skin;
        public static int awpSkin;
        public static int g3sg1Skin;
        public static int scar20Skin;
        public static int entQuality;
        private void SCGlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCglock.Text != "")
            {
                string skin = SCglock.Text;
                glockSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.glockSkin = SCglock.Text;
        }

        private void SCP2000_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCp2000.Text != "")
            {
                string skin = SCp2000.Text;
                p2000Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.p2000Skin = SCp2000.Text;
        }

        private void SCUSPS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCusps.Text != "")
            {
                string skin = SCusps.Text;
                uspsSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.uspsSkin = SCusps.Text;
        }

        private void SCberettas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCberettas.Text != "")
            {
                string skin = SCberettas.Text;
                berettasSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.berettasSkin = SCberettas.Text;
        }

        private void SCP250_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCp250.Text != "")
            {
                string skin = SCp250.Text;
                p250Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.p250Skin = SCp250.Text;
        }

        private void SCTec9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCtec9.Text != "")
            {
                string skin = SCtec9.Text;
                tec9Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.tec9Skin = SCtec9.Text;
        }

        private void SCfiveseven_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCfiveseven.Text != "")
            {
                string skin = SCfiveseven.Text;
                fivesevenSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.fivesevenSkin = SCfiveseven.Text;
        }

        private void SCCZ75Auto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCcz75a.Text != "")
            {
                string skin = SCcz75a.Text;
                cz75aSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.cz75aSkin = SCcz75a.Text;
        }

        private void SCRevolver_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCrevolver.Text != "")
            {
                string skin = SCrevolver.Text;
                revolverSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));

            }
            Main.S.revolverSkin = SCrevolver.Text;
        }

        private void SCDeagle_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCdeagle.Text != "")
            {
                string skin = SCdeagle.Text;
                deagleSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.deagleSkin = SCdeagle.Text;
        }

        private void SCNova_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCnova.Text != "")
            {
                string skin = SCnova.Text;
                novaSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.novaSkin = SCnova.Text;
        }

        private void SCXM1014_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCxm1014.Text != "")
            {
                string skin = SCxm1014.Text;
                xm1014Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.xm1014Skin = SCxm1014.Text;
        }

        private void SCSawedOff_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCsawedoff.Text != "")
            {
                string skin = SCsawedoff.Text;
                sawedoffSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.sawedoffSkin = SCsawedoff.Text;
        }

        private void SCMAG7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCmag7.Text != "")
            {
                string skin = SCmag7.Text;
                mag7Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.mag7Skin = SCmag7.Text;
        }

        private void SCNegev_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCnegev.Text != "")
            {
                string skin = SCnegev.Text;
                negevSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.negevSkin = SCnegev.Text;
        }

        private void SCM249_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCm249.Text != "")
            {
                string skin = SCm249.Text;
                m249Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.m249Skin = SCm249.Text;
        }

        private void SCMAC10_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCmac10.Text != "")
            {
                string skin = SCmac10.Text;
                mac10Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.mac10Skin = SCmac10.Text;
        }

        private void SCMP9_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCmp9.Text != "")
            {
                string skin = SCmp9.Text;
                mp9Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.mp9Skin = SCmp9.Text;
        }

        private void SCMP7_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCmp7.Text != "")
            {
                string skin = SCmp7.Text;
                mp7Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.mp7Skin = SCmp7.Text;
        }

        private void SCMP5SD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCmp5sd.Text != "")
            {
                string skin = SCmp5sd.Text;
                mp5sdSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.mp5sdSkin = SCmp5sd.Text;
        }

        private void SCUMP45_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCump45.Text != "")
            {
                string skin = SCump45.Text;
                ump45Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.ump45Skin = SCump45.Text;
        }

        private void SCP90_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCp90.Text != "")
            {
                string skin = SCp90.Text;
                p90Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.p90Skin = SCp90.Text;
        }

        private void SCPPBizon_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCbizon.Text != "")
            {
                string skin = SCbizon.Text;
                bizonSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.bizonSkin = SCbizon.Text;
        }

        private void SCgalil_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCgalil.Text != "")
            {
                string skin = SCgalil.Text;
                galilSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.galilSkin = SCgalil.Text;
        }

        private void SCFAMAS_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCfamas.Text != "")
            {
                string skin = SCfamas.Text;
                famasSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.famasSkin = SCfamas.Text;
        }

        private void SCAK47_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCak47.Text != "")
            {
                string skin = SCak47.Text;
                ak47Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.ak47Skin = SCak47.Text;
        }

        private void SCM4A1S_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCm4a1s.Text != "")
            {
                string skin = SCm4a1s.Text;
                m4a1sSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.m4a1sSkin = SCm4a1s.Text;
        }

        private void SCm4a4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCm4a4.Text != "")
            {
                string skin = SCm4a4.Text;
                m4a4Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.m4a4Skin = SCm4a4.Text;
        }

        private void SCsg553_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCsg553.Text != "")
            {
                string skin = SCsg553.Text;
                sg553Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.sg553Skin = SCsg553.Text;
        }

        private void SCAUG_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCaug.Text != "")
            {
                string skin = SCaug.Text;
                augSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.augSkin = SCaug.Text;
        }



        private void SCSSG08_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCssg08.Text != "")
            {
                string skin = SCssg08.Text;
                ssg08Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.ssg08Skin = SCssg08.Text;
        }

        private void SCAWP_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCawp.Text != "")
            {
                string skin = SCawp.Text;
                awpSkin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.awpSkin = SCawp.Text;
        }

        private void SCG3SG1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCg3sg1.Text != "")
            {
                string skin = SCg3sg1.Text;
                g3sg1Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.g3sg1Skin = SCg3sg1.Text;
        }

        private void SCSCAR20_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCscar20.Text != "")
            {
                string skin = SCscar20.Text;
                scar20Skin = (int)((PaintKit)Enum.Parse(typeof(PaintKit), skin));
            }
            Main.S.scar20Skin = SCscar20.Text;
        }

        private void SCQuality_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SCQuality.Text != "")
            {
                string quality = SCQuality.Text;
                entQuality = (int)((EntityQuality)Enum.Parse(typeof(EntityQuality), quality));
            }
            Main.S.SCQuality = SCQuality.Text;
        }

        //Function to get a random number.
        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        private static int randomSkin()
        {
            lock (syncLock)
            { // synchronize
                return random.Next(2, 818);
            }
        }

        private void SCbtnRandomize_Click(object sender, EventArgs e)
        {
            switch (SCdpRandomize.Text)
            {
                case "ALL":
                    glockSkin = randomSkin();
                    p2000Skin = randomSkin();
                    uspsSkin = randomSkin();
                    berettasSkin = randomSkin();
                    p250Skin = randomSkin();
                    tec9Skin = randomSkin();
                    fivesevenSkin = randomSkin();
                    cz75aSkin = randomSkin();
                    revolverSkin = randomSkin();
                    deagleSkin = randomSkin();
                    novaSkin = randomSkin();
                    xm1014Skin = randomSkin();
                    sawedoffSkin = randomSkin();
                    mag7Skin = randomSkin();
                    negevSkin = randomSkin();
                    m249Skin = randomSkin();
                    mac10Skin = randomSkin();
                    mp9Skin = randomSkin();
                    mp7Skin = randomSkin();
                    mp5sdSkin = randomSkin();
                    ump45Skin = randomSkin();
                    p90Skin = randomSkin();
                    bizonSkin = randomSkin();
                    galilSkin = randomSkin();
                    famasSkin = randomSkin();
                    ak47Skin = randomSkin();
                    m4a1sSkin = randomSkin();
                    m4a4Skin = randomSkin();
                    sg553Skin = randomSkin();
                    augSkin = randomSkin();
                    ssg08Skin = randomSkin();
                    awpSkin = randomSkin();
                    g3sg1Skin = randomSkin();
                    scar20Skin = randomSkin();

                    PaintKit glockPK = (PaintKit)glockSkin;
                    PaintKit p2000PK = (PaintKit)p2000Skin;
                    PaintKit uspsPK = (PaintKit)uspsSkin;
                    PaintKit berettasPK = (PaintKit)berettasSkin;
                    PaintKit p250PK = (PaintKit)p250Skin;
                    PaintKit tec9PK = (PaintKit)tec9Skin;
                    PaintKit fivesevenPK = (PaintKit)fivesevenSkin;
                    PaintKit cz75aPK = (PaintKit)cz75aSkin;
                    PaintKit revolverPK = (PaintKit)revolverSkin;
                    PaintKit deaglePK = (PaintKit)deagleSkin;
                    PaintKit novaPK = (PaintKit)novaSkin;
                    PaintKit xm1014PK = (PaintKit)xm1014Skin;
                    PaintKit sawedoffPK = (PaintKit)sawedoffSkin;
                    PaintKit mag7PK = (PaintKit)mag7Skin;
                    PaintKit negevPK = (PaintKit)negevSkin;
                    PaintKit m249PK = (PaintKit)m249Skin;
                    PaintKit mac10PK = (PaintKit)mac10Skin;
                    PaintKit mp9PK = (PaintKit)mp9Skin;
                    PaintKit mp7PK = (PaintKit)mp7Skin;
                    PaintKit mp5sdPK = (PaintKit)mp5sdSkin;
                    PaintKit ump45PK = (PaintKit)ump45Skin;
                    PaintKit p90PK = (PaintKit)p90Skin;
                    PaintKit bizonPK = (PaintKit)bizonSkin;
                    PaintKit galilPK = (PaintKit)galilSkin;
                    PaintKit famasPK = (PaintKit)famasSkin;
                    PaintKit ak47PK = (PaintKit)ak47Skin;
                    PaintKit m4a1sPK = (PaintKit)m4a1sSkin;
                    PaintKit m4a4PK = (PaintKit)m4a4Skin;
                    PaintKit sg553PK = (PaintKit)sg553Skin;
                    PaintKit augPK = (PaintKit)augSkin;
                    PaintKit ssg08PK = (PaintKit)ssg08Skin;
                    PaintKit awpPK = (PaintKit)awpSkin;
                    PaintKit g3sg1PK = (PaintKit)g3sg1Skin;
                    PaintKit scar20PK = (PaintKit)scar20Skin;

                    SCglock.Text = glockPK.ToString();
                    SCp2000.Text = p2000PK.ToString();
                    SCusps.Text = uspsPK.ToString();
                    SCberettas.Text = berettasPK.ToString();
                    SCp250.Text = p250PK.ToString();
                    SCtec9.Text = tec9PK.ToString();
                    SCfiveseven.Text = fivesevenPK.ToString();
                    SCcz75a.Text = cz75aPK.ToString();
                    SCrevolver.Text = revolverPK.ToString();
                    SCdeagle.Text = deaglePK.ToString();
                    SCnova.Text = novaPK.ToString();
                    SCxm1014.Text = xm1014PK.ToString();
                    SCsawedoff.Text = sawedoffPK.ToString();
                    SCmag7.Text = mag7PK.ToString();
                    SCnegev.Text = negevPK.ToString();
                    SCm249.Text = m249PK.ToString();
                    SCmac10.Text = mac10PK.ToString();
                    SCmp9.Text = mp9PK.ToString();
                    SCmp7.Text = mp7PK.ToString();
                    SCmp5sd.Text = mp5sdPK.ToString();
                    SCump45.Text = ump45PK.ToString();
                    SCp90.Text = p90PK.ToString();
                    SCbizon.Text = bizonPK.ToString();
                    SCgalil.Text = galilPK.ToString();
                    SCfamas.Text = famasPK.ToString();
                    SCak47.Text = ak47PK.ToString();
                    SCm4a1s.Text = m4a1sPK.ToString();
                    SCm4a4.Text = m4a4PK.ToString();
                    SCsg553.Text = sg553PK.ToString();
                    SCaug.Text = augPK.ToString();
                    SCssg08.Text = ssg08PK.ToString();
                    SCawp.Text = awpPK.ToString();
                    SCg3sg1.Text = g3sg1PK.ToString();
                    SCscar20.Text = scar20PK.ToString();
                    break;
                case "GLOCK":
                    glockSkin = randomSkin();
                    glockPK = (PaintKit)glockSkin;
                    SCglock.Text = glockPK.ToString();
                    break;
                case "P2000":
                    p2000Skin = randomSkin();
                    p2000PK = (PaintKit)p2000Skin;
                    SCp2000.Text = p2000PK.ToString();
                    break;
                case "USPS":
                    uspsSkin = randomSkin();
                    uspsPK = (PaintKit)uspsSkin;
                    SCusps.Text = uspsPK.ToString();
                    break;
                case "BERETTAS":
                    berettasSkin = randomSkin();
                    berettasPK = (PaintKit)berettasSkin;
                    SCberettas.Text = berettasPK.ToString();
                    break;
                case "P250":
                    p250Skin = randomSkin();
                    p250PK = (PaintKit)p250Skin;
                    SCp250.Text = p250PK.ToString();
                    break;
                case "TEC9":
                    tec9Skin = randomSkin();
                    tec9PK = (PaintKit)tec9Skin;
                    SCtec9.Text = tec9PK.ToString();
                    break;
                case "FIVESEVEN":
                    fivesevenSkin = randomSkin();
                    fivesevenPK = (PaintKit)fivesevenSkin;
                    SCfiveseven.Text = fivesevenPK.ToString();
                    break;
                case "CZ75A":
                    cz75aSkin = randomSkin();
                    cz75aPK = (PaintKit)cz75aSkin;
                    SCcz75a.Text = cz75aPK.ToString();
                    break;
                case "REVOLVER":
                    revolverSkin = randomSkin();
                    revolverPK = (PaintKit)revolverSkin;
                    SCrevolver.Text = revolverPK.ToString();
                    break;
                case "DEAGLE":
                    deagleSkin = randomSkin();
                    deaglePK = (PaintKit)deagleSkin;
                    SCdeagle.Text = deaglePK.ToString();
                    break;
                case "NOVA":
                    novaSkin = randomSkin();
                    novaPK = (PaintKit)novaSkin;
                    SCnova.Text = novaPK.ToString();
                    break;
                case "XM1014":
                    xm1014Skin = randomSkin();
                    xm1014PK = (PaintKit)xm1014Skin;
                    SCxm1014.Text = xm1014PK.ToString();
                    break;
                case "SAWEDOFF":
                    sawedoffSkin = randomSkin();
                    sawedoffPK = (PaintKit)sawedoffSkin;
                    SCsawedoff.Text = sawedoffPK.ToString();
                    break;
                case "MAG7":
                    mag7Skin = randomSkin();
                    mag7PK = (PaintKit)mag7Skin;
                    SCmag7.Text = mag7PK.ToString();
                    break;
                case "NEGEV":
                    negevSkin = randomSkin();
                    negevPK = (PaintKit)negevSkin;
                    SCnegev.Text = negevPK.ToString();
                    break;
                case "M249":
                    m249Skin = randomSkin();
                    m249PK = (PaintKit)m249Skin;
                    SCm249.Text = m249PK.ToString();
                    break;
                case "MAC10":
                    mac10Skin = randomSkin();
                    mac10PK = (PaintKit)mac10Skin;
                    SCmac10.Text = mac10PK.ToString();
                    break;
                case "MP9":
                    mp9Skin = randomSkin();
                    mp9PK = (PaintKit)mp9Skin;
                    SCmp9.Text = mp9PK.ToString();
                    break;
                case "MP7":
                    mp7Skin = randomSkin();
                    mp7PK = (PaintKit)mp7Skin;
                    SCmp7.Text = mp7PK.ToString();
                    break;
                case "MP5SD":
                    mp5sdSkin = randomSkin();
                    mp5sdPK = (PaintKit)mp5sdSkin;
                    SCmp5sd.Text = mp5sdPK.ToString();
                    break;
                case "UMP45":
                    ump45Skin = randomSkin();
                    ump45PK = (PaintKit)ump45Skin;
                    SCump45.Text = ump45PK.ToString();
                    break;
                case "P90":
                    p90Skin = randomSkin();
                    p90PK = (PaintKit)p90Skin;
                    SCp90.Text = p90PK.ToString();
                    break;
                case "BIZON":
                    bizonSkin = randomSkin();
                    bizonPK = (PaintKit)bizonSkin;
                    SCbizon.Text = bizonPK.ToString();
                    break;
                case "GALIL":
                    galilSkin = randomSkin();
                    galilPK = (PaintKit)galilSkin;
                    SCgalil.Text = galilPK.ToString();
                    break;
                case "FAMAS":
                    famasSkin = randomSkin();
                    famasPK = (PaintKit)famasSkin;
                    SCfamas.Text = famasPK.ToString();
                    break;
                case "AK47":
                    ak47Skin = randomSkin();
                    ak47PK = (PaintKit)ak47Skin;
                    SCak47.Text = ak47PK.ToString();
                    break;
                case "M4A1S":
                    m4a1sSkin = randomSkin();
                    m4a1sPK = (PaintKit)m4a1sSkin;
                    SCm4a1s.Text = m4a1sPK.ToString();
                    break;
                case "M4A4":
                    m4a4Skin = randomSkin();
                    m4a4PK = (PaintKit)m4a4Skin;
                    SCm4a4.Text = m4a4PK.ToString();
                    break;
                case "SG553":
                    sg553Skin = randomSkin();
                    sg553PK = (PaintKit)sg553Skin;
                    SCsg553.Text = sg553PK.ToString();
                    break;
                case "AUG":
                    augSkin = randomSkin();
                    augPK = (PaintKit)augSkin;
                    SCaug.Text = augPK.ToString();
                    break;
                case "SSG08":
                    ssg08Skin = randomSkin();
                    ssg08PK = (PaintKit)ssg08Skin;
                    SCssg08.Text = ssg08PK.ToString();
                    break;
                case "AWP":
                    awpSkin = randomSkin();
                    awpPK = (PaintKit)awpSkin;
                    SCawp.Text = awpPK.ToString();
                    break;
                case "G3SG1":
                    g3sg1Skin = randomSkin();
                    g3sg1PK = (PaintKit)g3sg1Skin;
                    SCg3sg1.Text = g3sg1PK.ToString();
                    break;
                case "SCAR20":
                    scar20Skin = randomSkin();
                    scar20PK = (PaintKit)scar20Skin;
                    SCscar20.Text = scar20PK.ToString();
                    break;
            }
        }

        #region Colors
        private void GlowEnemyCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GlowEnemyCLR.BackColor = colorDialog1.Color;
            }
        }

        private void GlowTeamCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GlowTeamCLR.BackColor = colorDialog1.Color;
            }
        }

        private void GlowSpottedCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GlowSpottedCLR.BackColor = colorDialog1.Color;
            }
        }

        private void GlowScopedCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                GlowScopedCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ChamEnemyCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ChamEnemyCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ChamTeamCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ChamTeamCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ChamScopedCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ChamScopedCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ESPEnemyCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ESPEnemyCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ESPTeamCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ESPTeamCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ESPSpottedCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ESPSpottedCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ESPScopedCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ESPScopedCLR.BackColor = colorDialog1.Color;
            }
        }

        private void ESPCrosshairCLR_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                ESPCrosshairCLR.BackColor = colorDialog1.Color;
            }
        }
        #endregion

        #endregion

        Forms.Overlay overlay = new Forms.Overlay();
        private void OverlayEnabledCheck_CheckedChanged(object sender)
        {
            if (OverlayEnabledCheck.Checked) overlay.Show();
            else overlay.Hide();
        }
    }
}