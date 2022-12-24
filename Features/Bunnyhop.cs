using System;
using System.Threading;
using Lerawin.Utilities;

namespace Lerawin.Features
{
    public class Bunnyhop
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.BunnyhopEnabled && Menu.CSGOActive && Menu.InGame) // Make sure the cheat is enabled in the menu.
                {
                    if (Tools.HoldingKey(Keys.VK_SPACE))
                    {
                        if (Main.S.BunnyhopVelocityEnabled && G.Engine.LocalPlayer.Velocity < 100) continue;
                        // Flags show if you are on the ground or not. 257 - standing on the ground, 263 - crouching on the ground, 1281 - in puddle, 1287 - in puddle crouched,
                        // 1 << 18 - partial on ground.
                        if ((G.Engine.LocalPlayer.Flags == 257 || G.Engine.LocalPlayer.Flags == 263 || G.Engine.LocalPlayer.Flags == 1281 || G.Engine.LocalPlayer.Flags == 1287
                            || G.Engine.LocalPlayer.Flags == (1 << 18)))
                        {
                            Thread.Sleep(new Random().Next(Main.S.BunnyhopMinInterval, Main.S.BunnyhopMaxInterval));
                            G.Engine.Jump();
                        }
                    }
                }
                Thread.Sleep(1); // Optimization.
            }
        }
    }
}
