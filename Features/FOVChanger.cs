using Lerawin.Classes;
using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class FOVChanger
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.FOVChangerEnabled && Menu.CSGOActive && Menu.InGame)
                {
                    float fovRate = 0.000005f;
                    if (G.Engine.LocalPlayer.ZoomLevel != 1 && G.Engine.LocalPlayer.ZoomLevel != 2 && (G.Engine.LocalPlayer.FOV != Main.S.FOVAmount || G.Engine.LocalPlayer.FOVDefault != Main.S.FOVAmount
                        || G.Engine.LocalPlayer.FOVStart != Main.S.FOVAmount || G.Engine.LocalPlayer.FOVRate != fovRate))
                    {
                        if (G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.AWP && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.G3SG1 && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.SCAR20
                            && G.Engine.LocalPlayer.enumWeaponID != enumWeaponID.SSG08)
                        {
                            G.Engine.LocalPlayer.FOV = Main.S.FOVAmount;
                            G.Engine.LocalPlayer.FOVDefault = Main.S.FOVAmount;
                            G.Engine.LocalPlayer.FOVStart = Main.S.FOVAmount;
                            G.Engine.LocalPlayer.FOVRate = fovRate;
                        }
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
