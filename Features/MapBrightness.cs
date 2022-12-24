using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class MapBrightness
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.MapBrightnessEnabled && Menu.CSGOActive && Menu.InGame && !Menu.formIsClosed)
                {
                    G.Engine.LocalPlayer.MapBrightness = (float)Main.S.MapBrightnessAmount / 100; // "Convert" to float.
                }
                Thread.Sleep(1);
            }
        }
    }
}
