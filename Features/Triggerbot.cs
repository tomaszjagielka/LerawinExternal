using Lerawin.Classes;
using Lerawin.Utilities;
using System;
using System.Threading;

namespace Lerawin.Features
{
    class Triggerbot
    {
        //public static bool allow;
        public static void Run()
        {
            while (true)
            {
                if ((Main.S.TriggerEnemyEnabled || Main.S.TriggerTeamEnabled) && Menu.CSGOActive && Menu.InGame)
                {
                    //allow = true;
                    if (G.Engine.LocalPlayer.FlashDuration == 0 && G.Engine.LocalPlayer.CrosshairID != 0 && G.Engine.LocalPlayer.CrosshairID < 64 && (G.Engine.LocalPlayer.Flags == 257
                        || G.Engine.LocalPlayer.Flags == 263))
                    {
                        Entity Player = new Entity(Tools.GetEntityBaseFromCrosshair(G.Engine.LocalPlayer.CrosshairID));

                        if (!Player.Valid) continue;
                        if (Player.IsTeammate && !Main.S.TriggerTeamEnabled) continue;
                        if (!Player.IsTeammate && !Main.S.TriggerEnemyEnabled) continue;
                        if (Main.S.AimScopedEnabled && G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2 &&
                            (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SCAR20
                            || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.G3SG1)) continue;
                        if (Main.S.AimNMEnabled && !G.Engine.LocalPlayer.IsStill) continue;

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
                            case enumWeaponID.REVOLVER:
                                continue;
                        }

                        if (Tools.HoldingKey(Keys.VK_MBUTTON) && !Tools.HoldingKey(Keys.VK_LBUTTON) && (G.Engine.LocalPlayer.AimPunchAngle.X == 0 && G.Engine.LocalPlayer.AimPunchAngle.X == 0))
                        {
                            Thread.Sleep(new Random().Next(Main.S.TriggerMinFirerate, Main.S.TriggerMaxFirerate));
                            G.Engine.Shoot();
                            Thread.Sleep(new Random().Next(Main.S.TriggerMinShotDelay, Main.S.TriggerMaxShotDelay));
                        }
                    }
                }
                else
                {
                    //allow = false;
                }
                //Console.WriteLine("allow = " + allow);
                Thread.Sleep(1);
            }

            //if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AWP)
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(5, 50); // Creates random number between numbers entered by user for random cooldown.
            //    int fireRate = rnd.Next(1000 - Main.S.FirerateRandAmount, 1000 + Main.S.FirerateRandAmount);
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}

            //else if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.AK47 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.M4A4
            //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.M4A1S || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.galil
            //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FAMAS)
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(5, 25); // Creates random number between numbers entered by user for random cooldown.
            //    int fireRate = rnd.Next(150 - Main.S.FirerateRandAmount, 150 + Main.S.FirerateRandAmount);
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}


            //else if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DEAGLE)
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(5, 20); // Creates random number between numbers entered by user for random cooldown.
            //    int fireRate = rnd.Next(700 - Main.S.FirerateRandAmount, 700 + Main.S.FirerateRandAmount);
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}

            //else if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P250 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.TEC9
            //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.BERETTAS || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.USPS
            //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P2000 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.GLOCK
            //    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN)
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(5, 10); // Creates random number between numbers entered by user for random cooldown.
            //    int fireRate = rnd.Next(200 - Main.S.FirerateRandAmount, 200 + Main.S.FirerateRandAmount);
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}

            //else if (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.SSG08)
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(5, 20);
            //    int fireRate = rnd.Next(500 - Main.S.FirerateRandAmount, 500 + Main.S.FirerateRandAmount);
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}

            //else
            //{
            //    Random rnd = new Random();
            //    Random rnd2 = new Random();
            //    int shotDelay = rnd.Next(Main.S.ShotDelayAmount - Main.S.ShotDelayRandAmount, Main.S.ShotDelayAmount + Main.S.ShotDelayRandAmount); // Creates random number between numbers entered by user for random cooldown.
            //    int fireRate = rnd2.Next(Main.S.FirerateAmount - Main.S.FirerateRandAmount, Main.S.FirerateAmount + Main.S.FirerateRandAmount); // Creates random number between numbers entered by user for random cooldown.
            //    Thread.Sleep(shotDelay); // shot delay
            //    G.Engine.Shoot();
            //    Thread.Sleep(fireRate); // firerate
            //}

            //void OneMoreBullet()
            //{
            //    while (true)
            //    {
            //        foreach (Entity Player in G.EntityList)
            //        {
            //            Random rnd = new Random();
            //            int randomNumber = rnd.Next(1, 2);

            //            if (!Player.Alive && randomNumber == 2)
            //            {
            //                Thread.Sleep(500);
            //                G.Engine.Shoot();
            //            }
            //        }
            //        break;
            //    }
            //}
        }
    }
}
