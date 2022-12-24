using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class Fakelag
    {
        public static void Run()
        {
            while (true)
            {
                if (Main.S.FakelagEnabled && Menu.CSGOActive && Menu.InGame)
                {
                    G.Engine.SendPackets = 0;
                    Thread.Sleep(Main.S.FakelagAmount);
                    G.Engine.SendPackets = 1;
                }
                Thread.Sleep(Main.S.FakelagInterval);
            }
        }
    }
}
