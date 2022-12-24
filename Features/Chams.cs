using Lerawin.Classes;
using Lerawin.Utilities;
using System.Drawing;
using System.Threading;

// TODO: Check if scoped works.

namespace Lerawin.Features
{
    public class Chams
    {
        public static bool Enabled;
        public static void Run()
        {
            while (true)
            {
                if (Main.S.ChamsEnabled && Menu.formIsClosed == false)
                {
                    Enabled = true;
                    if (Menu.CSGOActive && Menu.InGame)
                    {
                        G.Engine.ModelBrightness = Main.S.ChamsBrightness;
                        // Loop through our player list.
                        foreach (Entity Player in G.EntityList)
                        {
                            if (!Player.Valid) continue;
                            if (Main.S.ChamsHealthBEnabled)
                            {
                                // Color depends on players health amount.
                                Color Gradient = Tools.HealthGradient(Tools.HealthToPercent(Player.Health));
                                Player.Cham(Color.FromArgb(Gradient.R, Gradient.G, Gradient.B));
                            }
                            else if (Player.ZoomLevel == 1 || Player.ZoomLevel == 2)
                            {
                                if (Player.enumWeaponID == enumWeaponID.SSG08 || Player.enumWeaponID == enumWeaponID.AWP || Player.enumWeaponID == enumWeaponID.SCAR20 || Player.enumWeaponID == enumWeaponID.G3SG1)
                                {
                                    Player.Cham(Main.S.ChamScopedCLR);
                                }
                            }
                            else if (Player.IsTeammate)
                            {
                                Player.Cham(Main.S.ChamTeamCLR);
                            }
                            else if (!Player.IsTeammate)
                            {
                                Player.Cham(Main.S.ChamEnemyCLR);
                            }
                        }
                    }                    
                }
                Thread.Sleep(10);
            }
        }
    }
}
