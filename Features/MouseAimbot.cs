using System.Threading;
using Lerawin.Classes;
using Lerawin.Utilities;
using System.Numerics;

namespace Lerawin.Features
{
    public class MouseAimbot
    {
        public static void Run()
        {
            Vector2 PlayerBonePos = Tools.WorldToScreen(Vector3.Zero);
            int oldKills = 0;
            //int oldHits = G.Engine.LocalPlayer.TotalHitsOnServer; // To prevent executing hitsound.wav after enabling this feature.
            int kills = 0;
            while (true)
            {
                // Check if feature is enabled.
                if ((Main.S.MouseAimbotEnabled || Main.S.TriggerStickyEnabled) && Menu.CSGOActive && Menu.InGame && G.Engine.LocalPlayer.ObserverMode == 0)
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

                    if (!Tools.HoldingKey(Keys.VK_C) && !Tools.HoldingKey(Keys.VK_LBUTTON))
                        continue;

                    if (Main.S.AimIgnoreBullets > 0 && G.Engine.LocalPlayer.ShotsFired <= Main.S.AimIgnoreBullets)
                        continue;

                    if (!Tools.HoldingKey(Keys.VK_C) && (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN ||
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
                    // Player in some pixels of your crosshair.
                    if (Main.S.TriggerStickyEnabled && Tools.HoldingKey(Keys.VK_MBUTTON))
                    {
                        switch (Main.S.AimBone)
                        {
                            case "Head":
                                Player = Tools.GetFovPlayerHead(Main.S.TriggerStickyFOV);
                                break;
                            case "Neck":
                                Player = Tools.GetFovPlayerNeck(Main.S.TriggerStickyFOV);
                                break;
                            case "Chest":
                                Player = Tools.GetFovPlayerChest(Main.S.TriggerStickyFOV);
                                break;
                            case "LowerChest":
                                Player = Tools.GetFovPlayerLowerChest(Main.S.TriggerStickyFOV);
                                break;
                            case "Stomach":
                                Player = Tools.GetFovPlayerStomach(Main.S.TriggerStickyFOV);
                                break;
                            case "Legs":
                                Player = Tools.GetFovPlayerLegs(Main.S.TriggerStickyFOV);
                                break;
                            default:
                                Player = Tools.GetFovPlayerHead(Main.S.TriggerStickyFOV);
                                break;
                        }
                    }
                    else if (Main.S.MouseAimbotEnabled && ((Tools.HoldingKey(Keys.VK_C) || Tools.HoldingKey(Keys.VK_LBUTTON))))
                    {
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
                    }
                    if (Player != null)
                    {
                        if (Main.S.AimEnemyEnabled && Player.IsTeammate) continue;
                        if (Main.S.AimTeamEnabled && !Player.IsTeammate) continue;
                        if (Main.S.AimITAEnabled && (Player.Flags == 774 || Player.Flags == 768)) continue;
                        if (Main.S.AimTVEnabled && !Player.Spotted) continue;
                        if (Main.S.AimNIAEnabled && (G.Engine.LocalPlayer.Flags == 256 || G.Engine.LocalPlayer.Flags == 262)) continue;
                        if (Main.S.AimNMEnabled && !G.Engine.LocalPlayer.IsStill) continue;
                        if (Main.S.AimScopedEnabled && G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2 &&
                            (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20
                            || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1)) continue;
                        if (G.Engine.LocalPlayer.Ammo == 0) continue;

                        // No switch-case because aimbot bugs out for some reason.
                        if (Main.S.AimBone == "Head")
                            PlayerBonePos = Tools.WorldToScreen(Player.HeadPosition);
                        else if (Main.S.AimBone == "Neck")
                            PlayerBonePos = Tools.WorldToScreen(Player.NeckPosition);
                        else if (Main.S.AimBone == "Chest")
                            PlayerBonePos = Tools.WorldToScreen(Player.ChestPosition);
                        else if (Main.S.AimBone == "LowerChest")
                            PlayerBonePos = Tools.WorldToScreen(Player.LowerChestPosition);
                        else if (Main.S.AimBone == "Stomach")
                            PlayerBonePos = Tools.WorldToScreen(Player.StomachPosition);
                        else if (Main.S.AimBone == "Legs")
                            PlayerBonePos = Tools.WorldToScreen(Player.LegsPosition);
                        else
                            PlayerBonePos = Tools.WorldToScreen(Player.HeadPosition);

                        // Calculate 2D aimpunch, and subtract it from the position.
                        float dy = Main.ScreenSize.Height / 90;
                        float dx = Main.ScreenSize.Width / 90;
                        // Get 2D position on screen.

                        if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DEAGLE || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.GLOCK)
                        {
                            PlayerBonePos.X += (Main.ScreenSize.Height / 1100); // Divide by 1100 because Aimbot aims incorrectly without aimpunch value.
                            PlayerBonePos.Y -= (Main.ScreenSize.Width / 1100);
                        }
                        else
                        {
                            PlayerBonePos.X += (dx * (G.Engine.LocalPlayer.AimPunchAngle.Y));
                            PlayerBonePos.Y -= (dy * (G.Engine.LocalPlayer.AimPunchAngle.X));
                        }

                        if (!Tools.IsNullVector2(PlayerBonePos))
                        {
                            Tools.MoveCursor(PlayerBonePos);    // Move mouse position to players 2D head.
                            if (Main.S.AimASEnabled && G.Engine.LocalPlayer.CrosshairID != 0 && G.Engine.LocalPlayer.CrosshairID < 64 && (Tools.HoldingKey(Keys.VK_C) || Tools.HoldingKey(Keys.VK_LBUTTON)))
                            {
                                G.Engine.Shoot();
                            }
                        }
                    }
                }
                // Bad Smooth but works quite good.
                if (Main.S.TriggerStickyEnabled && Tools.HoldingKey(Keys.VK_MBUTTON))
                    Thread.Sleep(Main.S.TriggerStickySmooth);
                else
                    Thread.Sleep(Main.S.AimSmooth);
            }
        }
    }
}
