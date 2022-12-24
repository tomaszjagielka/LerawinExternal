using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class ReduceFlash
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.ReduceFlashEnabled && G.Engine.LocalPlayer.FlashDuration > 0 && Menu.CSGOActive && Menu.InGame)
                {
                    G.Engine.LocalPlayer.FlashMaxAlpha = Main.S.ReduceFlashAmount;
                }
                Thread.Sleep(100);
            }
        }
    }
}
