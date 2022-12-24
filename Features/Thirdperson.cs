//using Lerawin.Classes;
//using Lerawin.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Lerawin.Features
//{
//    // TODO: Spectator mode is bugged, because it requires other ObserverMode to work correctly.

//    public class Thirdperson
//    {
//        public static void Run()
//        {
//            while (true)
//            {
//                if (Main.S.ThirdpersonEnabled)
//                {
//                    G.Engine.LocalPlayer.ObserverMode = 1;
//                }
//                else
//                {
//                    G.Engine.LocalPlayer.ObserverMode = 6;
//                }
//                Thread.Sleep(100);
//            }
//        }
//    }
//}
