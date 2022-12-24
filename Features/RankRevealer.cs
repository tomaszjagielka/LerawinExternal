using Lerawin.Classes;
using Lerawin.Utilities;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Lerawin.Features
{
    public class RankRevealer
    {
        public static void Run()
        {
            while (true)
            {
                if (Menu.RankRevealerClicked)
                {
                    string path = $@"{Application.StartupPath}\ranks.txt";
                    //Console.Clear();
                    File.WriteAllText(path, string.Empty);
                    for (int i = 1; i < 65; i++)
                    {
                        int rank = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveRanking + i * 4);
                        int wins = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iCompetitiveWins + i * 4);
                        int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + i * 4);
                        int enmteam = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iTeam + i * 4);
                        int enmscore = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iScore + i * 4);
                        int ping = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iPing + i * 4);
                        // TODO: Add player name.
                        //char radarPlayerName = Memory.ReadMemory<char>(G.Engine.Radar + (0x1E8 * (i + 1)) + 0x24);
                        //if (rank < 0 && rank > 19) return;

                        using (StreamWriter sw = File.AppendText(path))
                        {
                            if (ping >= 5)
                            {
                                if (enmteam == 2)
                                    //Console.WriteLine("[ENEMY] [PING: " + ping + "] [SCORE: " + enmscore + "] [RANK: " + compRank + "]");
                                    sw.WriteLine("[ENEMY] [KILLS: " + kills + "] [WINS: " + wins + "] [SCORE: " + enmscore + "] [PING: " + ping + "] [RANK: " + Enum.GetName(typeof(Ranks), rank) + "]");
                                else if (enmteam == 3)
                                    sw.WriteLine("[TEAM] [KILLS: " + kills + "] [WINS: " + wins + "] [SCORE: " + enmscore + "] [PING: " + ping + "] [RANK: " + Enum.GetName(typeof(Ranks), rank) + "]");
                            }
                        }
                    }
                    
                    // Remove duplicates.
                    string[] lines = File.ReadAllLines(path);
                    File.WriteAllLines(path, lines.Distinct().ToArray());

                    Process.Start(path);
                    Menu.RankRevealerClicked = false;
                }
                Thread.Sleep(1);
            }
        }
    }
}

