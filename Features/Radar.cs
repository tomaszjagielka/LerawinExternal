using Lerawin.Classes;
using Lerawin.Utilities;
using System.Threading;

namespace Lerawin.Features
{
    public class Radar
    {
        public static void Run()
        {
            bool Enabled = false;
            while (true)
            {
                if (Main.S.RadarEnabled && Menu.CSGOActive && Menu.InGame && !Main.S.RadarOnKeyEnabled/* && !Main.S.StreamMode*/)
                {
                    foreach (Entity Player in G.EntityList)
                    {
                        if (!Player.Valid) continue;
                        if (!Player.Spotted)
                            Player.Spotted = true;
                    }
                }
                else if (Main.S.RadarOnKeyEnabled/* && !Main.S.StreamMode*/)
                {
                    foreach (Entity Player in G.EntityList)
                    {
                        if (!Player.Valid) continue;
                        if (Tools.HoldingKey(86))
                        {
                            Player.Spotted = true;
                            Enabled = true;
                        }
                        else if (Enabled)
                        {
                            if (Player.Spotted)
                                Player.Spotted = false;
                            else
                                Enabled = false;
                        }
                    }
                }
                Thread.Sleep(Main.S.RadarInterval);
            }
        }
    }
}
