using Lerawin.Classes;
using Lerawin.Utilities;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Lerawin.Features
{
    public class Hitsound
    {
        public static void Run()
        {
            int OldHits = G.Engine.LocalPlayer.TotalHitsOnServer; // To prevent executing hitsound.wav after enabling this feature.

            while (true)
            {
                if (Main.S.HitsoundEnabled && Menu.CSGOActive && Menu.InGame && File.Exists($@"{Application.StartupPath}\hitsound.wav"))
                {
                    int Hits = G.Engine.LocalPlayer.TotalHitsOnServer;
                    if (Hits > OldHits)
                    {
                        if (Main.S.HitsoundVisOnlyEnabled) // So it doesn't give me unwanted informations.
                        {
                            foreach (Entity Player in G.EntityList)
                            {
                                if (!Player.Valid) continue;
                                if (Player.Spotted)
                                {
                                    System.Media.SoundPlayer hitsound = new System.Media.SoundPlayer($@"{Application.StartupPath}\hitsound.wav");
                                    hitsound.Play();
                                }
                            }
                        }
                        else
                        {
                            System.Media.SoundPlayer hitsound = new System.Media.SoundPlayer($@"{Application.StartupPath}\hitsound.wav");
                            hitsound.Play();
                        }
                        OldHits = Hits;
                    }
                }
                Thread.Sleep(1);
            }
        }
    }
}
