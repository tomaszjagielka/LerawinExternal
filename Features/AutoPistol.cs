using Lerawin.Classes;
using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class AutoPistol
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.AutoPistolEnabled && Menu.CSGOActive && Menu.InGame)
                {
                    if (Tools.HoldingKey(Keys.VK_LBUTTON) && (G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P250 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.TEC9
                    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.BERETTAS || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.DEAGLE || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.USPS
                    || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.P2000 || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.GLOCK || G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.FIVESEVEN)
                    && G.Engine.LocalPlayer.Velocity > 0) // To prevent shooting while in Buy Menu etc.
                    {
                        G.Engine.Shoot();
                    }
                }
                Thread.Sleep(5);
            }
        }
    }
}
