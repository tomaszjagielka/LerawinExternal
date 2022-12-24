using System.Numerics;
using System.Threading;
using Lerawin.Classes;
using Lerawin.Utilities;

namespace Lerawin.Features
{
    public class ViewangleAimbot
    {
        public static void Run()
        {
            Vector2 AimAngle = Tools.CalcAngle(Vector3.Zero, Vector3.Zero);
            Vector2 oldViewAngle = Tools.CalcAngle(Vector3.Zero, Vector3.Zero);
            int oldKills = 0;
            int kills = 0;
            while (true)
            {
                // Check if feature is enabled.
                if (Main.S.ViewangleAimbotEnabled && Menu.CSGOActive && Menu.InGame && G.Engine.LocalPlayer.ObserverMode == 0)
                {
                    // If holding Mouse1 or C.
                    if (Tools.HoldingKey(Keys.VK_C) || Tools.HoldingKey(Keys.VK_LBUTTON))
                    {
                        switch (G.Engine.LocalPlayer.enumWeaponID)
                        {
                            case enumWeaponID.KNIFE:
                            case enumWeaponID.TASER:
                            case enumWeaponID.C4:
                            case enumWeaponID.SMOKEGRENADE:
                            case enumWeaponID.FLASHBANG:
                            case enumWeaponID.HEGRENADE:
                            case enumWeaponID.INCGRENADE:
                            case enumWeaponID.MOLOTOV:
                            case enumWeaponID.DECOY:
                                continue;
                        }

                        //if (Main.S.ViewangleAimbotEnabled && Main.S.AimIgnoreBullets > 0 && (Tools.HoldingKey(Keys.VK_LMENU) || Tools.HoldingKey(Keys.VK_LBUTTON)) && !(G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P250 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.TEC9
                        //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.BERETTAS || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DEAGLE || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.USPS
                        //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P2000 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.GLOCK || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN))
                        //        if (G.Engine.LocalPlayer.ShotsFired <= Main.S.AimIgnoreBullets) continue;

                        if (Main.S.AimIgnoreBullets > 0 && G.Engine.LocalPlayer.ShotsFired <= Main.S.AimIgnoreBullets)
                            continue;

                        if (!Tools.HoldingKey(Keys.VK_C) &&
                            (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P250 ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.TEC9 ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.BERETTAS ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DEAGLE ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.USPS ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P2000 ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.GLOCK ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20 ||
                            G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1))
                            continue;

                        // Next Target Cooldown
                        if (Main.S.AimNextTargetCooldown > 0)
                        {
                            kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + 1 * 4);
                            if (kills > oldKills)
                                Thread.Sleep(Main.S.AimNextTargetCooldown);

                            oldKills = kills;
                        }

                        Entity Player = null;
                        //switch (Main.S.AimBone)
                        //{
                        //    case "Head":
                        //        Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    case "Neck":
                        //        Player = Tools.GetFovPlayerNeck(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    case "Chest":
                        //        Player = Tools.GetFovPlayerChest(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    case "LowerChest":
                        //        Player = Tools.GetFovPlayerLowerChest(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    case "Stomach":
                        //        Player = Tools.GetFovPlayerStomach(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    case "Legs":
                        //        Player = Tools.GetFovPlayerLegs(Main.S.AimFOVPixelDistance);
                        //        break;
                        //    default:
                        //        Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                        //        break;
                        //}

                        if (Main.S.TriggerStickyEnabled && Tools.HoldingKey(Keys.VK_MBUTTON))
                        {
                            if (Main.S.AimBone == "Head")
                                Player = Tools.GetFovPlayerHead(Main.S.TriggerStickyFOV);
                            else if (Main.S.AimBone == "Neck")
                                Player = Tools.GetFovPlayerNeck(Main.S.TriggerStickyFOV);
                            else if (Main.S.AimBone == "Chest")
                                Player = Tools.GetFovPlayerChest(Main.S.TriggerStickyFOV);
                            else if (Main.S.AimBone == "LowerChest")
                                Player = Tools.GetFovPlayerLowerChest(Main.S.TriggerStickyFOV);
                            else if (Main.S.AimBone == "Stomach")
                                Player = Tools.GetFovPlayerStomach(Main.S.TriggerStickyFOV);
                            else if (Main.S.AimBone == "Legs")
                                Player = Tools.GetFovPlayerLegs(Main.S.TriggerStickyFOV);
                            else
                                Player = Tools.GetFovPlayerHead(Main.S.TriggerStickyFOV);
                        }
                        else if (Tools.HoldingKey(Keys.VK_C) || Tools.HoldingKey(Keys.VK_LBUTTON))
                        {
                            switch (Main.S.AimBone)
                            {
                                case "Head":
                                    Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                                    break;
                                case "Neck":
                                    Player = Tools.GetFovPlayerNeck(Main.S.AimFOVPixelDistance);
                                    break;
                                case "Chest":
                                    Player = Tools.GetFovPlayerChest(Main.S.AimFOVPixelDistance);
                                    break;
                                case "LowerChest":
                                    Player = Tools.GetFovPlayerLowerChest(Main.S.AimFOVPixelDistance);
                                    break;
                                case "Stomach":
                                    Player = Tools.GetFovPlayerStomach(Main.S.AimFOVPixelDistance);
                                    break;
                                case "Legs":
                                    Player = Tools.GetFovPlayerLegs(Main.S.AimFOVPixelDistance);
                                    break;
                                default:
                                    Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                                    break;
                            }

                            //if (Main.S.AimBone == "Head")
                            //    Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                            //else if (Main.S.AimBone == "Neck")
                            //    Player = Tools.GetFovPlayerNeck(Main.S.AimFOVPixelDistance);
                            //else if (Main.S.AimBone == "Chest")
                            //    Player = Tools.GetFovPlayerChest(Main.S.AimFOVPixelDistance);
                            //else if (Main.S.AimBone == "LowerChest")
                            //    Player = Tools.GetFovPlayerLowerChest(Main.S.AimFOVPixelDistance);
                            //else if (Main.S.AimBone == "Stomach")
                            //    Player = Tools.GetFovPlayerStomach(Main.S.AimFOVPixelDistance);
                            //else if (Main.S.AimBone == "Legs")
                            //    Player = Tools.GetFovPlayerLegs(Main.S.AimFOVPixelDistance);
                            //else
                            //    Player = Tools.GetFovPlayerHead(Main.S.AimFOVPixelDistance);
                        }

                        if (Player != null)
                        {
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.TASER) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.C4) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SMOKEGRENADE) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FLASHBANG) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.HEGRENADE) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.INCGRENADE) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.MOLOTOV) continue;
                            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DECOY) continue;

                            if (Main.S.AimEnemyEnabled && Player.IsTeammate) continue;
                            if (Main.S.AimTeamEnabled && !Player.IsTeammate) continue;
                            if (Main.S.AimITAEnabled && (Player.Flags == 774 || Player.Flags == 768)) continue;
                            if (Main.S.AimTVEnabled && !Player.Spotted) continue;
                            if (Main.S.AimNIAEnabled && (G.Engine.LocalPlayer.Flags == 256 || G.Engine.LocalPlayer.Flags == 262)) continue;
                            if (Main.S.AimNMEnabled && !G.Engine.LocalPlayer.IsStill/* && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.AWP*/) continue;
                            if (Main.S.AimScopedEnabled && G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2 &&
                                (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20
                                || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1)) continue;
                            if (G.Engine.LocalPlayer.Ammo == 0) continue;

                            switch (Main.S.AimBone)
                            {
                                case "Head":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.HeadPosition);
                                    break;
                                case "Neck":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.NeckPosition);
                                    break;
                                case "Chest":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.ChestPosition);
                                    break;
                                case "LowerChest":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.LowerChestPosition);
                                    break;
                                case "Stomach":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.StomachPosition);
                                    break;
                                case "Legs":
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.LegsPosition);
                                    break;
                                default:
                                    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.HeadPosition);
                                    break;
                            }

                            AimAngle = ClampAngle(AimAngle);

                            Vector2 ClampAngle(Vector2 ViewAngle)
                            {
                                if (ViewAngle.X < -89.0f)
                                    ViewAngle.X = -89.0f;
                                if (ViewAngle.X > 89.0f)
                                    ViewAngle.X = 89.0f;
                                if (ViewAngle.Y < -180.0f)
                                    ViewAngle.Y += 360.0f;
                                if (ViewAngle.Y > 180.0f)
                                    ViewAngle.Y -= 360.0f;
                                return ViewAngle;
                            }

                            //if (Main.S.AimBone == "Head")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.HeadPosition);
                            //else if (Main.S.AimBone == "Neck")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.NeckPosition);
                            //else if (Main.S.AimBone == "Chest")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.ChestPosition);
                            //else if (Main.S.AimBone == "LowerChest")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.LowerChestPosition);
                            //else if (Main.S.AimBone == "Stomach")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.StomachPosition);
                            //else if (Main.S.AimBone == "Legs")
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.LegsPosition);
                            //else
                            //    AimAngle = Tools.CalcAngle(G.Engine.LocalPlayer.EyePosition, Player.HeadPosition);

                            if (Main.S.AimSilentEnabled) // && G.Engine.LocalPlayer.AimPunchAngle.X > -0.5 && G.Engine.LocalPlayer.AimPunchAngle.X < 0.5)
                            {
                                //G.Engine.SendPackets = 0;
                                Aimbot();
                                Thread.Sleep(20);
                                G.Engine.ViewAngles = oldViewAngle;
                                //G.Engine.SendPackets = 1;

                                //var iDesiredCmdNumber = Memory.ReadMemory<Int32>(Main.O.signatures.dwClientState + Main.O.signatures.clientstate_last_outgoing_command) + 2;

                                //var pNetChannel = Memory.ReadMemory<int>(Main.O.signatures.dwClientState + Main.O.signatures.clientstate_net_channel);
                                //while (Memory.ReadMemory<Int32>(pNetChannel + Main.O.signatures.clientstate_net_channel) < iDesiredCmdNumber)
                                //{
                                //    Thread.Yield();
                                //}
                            }
                            else
                            {
                                Aimbot();
                            }

                            void Aimbot()
                            {
                                Vector2 PunchAngle = G.Engine.LocalPlayer.AimPunchAngle * 2;
                                if (G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.AWP && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.SSG08 && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.DEAGLE)
                                    AimAngle -= PunchAngle;
                                ClampAngle(AimAngle);
                                Vector2 DeltaAngle = G.Engine.ViewAngles - AimAngle;
                                ClampAngle(DeltaAngle);
                                Vector2 FinalAngle = Tools.CalcAngle(Vector3.Zero, Vector3.Zero);
                                if (Main.S.AimSmooth > 0)
                                    FinalAngle = G.Engine.ViewAngles - DeltaAngle / Main.S.AimSmooth;
                                else
                                    FinalAngle = G.Engine.ViewAngles - DeltaAngle;
                                ClampAngle(FinalAngle);
                                G.Engine.ViewAngles = FinalAngle;
                                //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08)
                                //    G.Engine.ViewAngles = FinalAngle;
                                //else
                                //    G.Engine.ViewAngles = FinalAngle;    // Set view angles to players head position.

                                //Console.WriteLine(PunchAngle);

                                if (Main.S.AimASEnabled && G.Engine.LocalPlayer.CrosshairID != 0 && G.Engine.LocalPlayer.CrosshairID <= 64)
                                {
                                    Thread.Sleep(1); // Prevents untrusted ban.
                                    G.Engine.Shoot();
                                }
                            }


                        }
                    }
                    else
                    {
                        oldViewAngle = G.Engine.ViewAngles;
                    }

                    //Console.WriteLine("ViewAngle: " + G.Engine.ViewAngles);
                    //Console.WriteLine("AimAngle: " + AimAngle);
                }
                Thread.Sleep(1);
            }
        }
    }
}
