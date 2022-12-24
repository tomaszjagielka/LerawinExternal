//using Lerawin.Classes;
//using Lerawin.Utilities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Reflection;
//using System.Diagnostics;
//using System.Windows.Forms;
//using System.Timers;
//using WindowsInput;
//using WindowsInput.Native;
//using System.Runtime.InteropServices;

//namespace Lerawin.Features
//{

//    public class test
//    {
//        public static void Run()
//        {

//            while (true)
//            {
//                //foreach (Entity Player in G.EntityList)
//                //{
//                //    Console.WriteLine(Player.DetectedByEnemySensorTime);
//                //    Player.DetectedByEnemySensorTime = 100000.0f;
//                //}

//                Thread.Sleep(100);
//            }

//            //// Can't make a secondary attack.
//            //foreach (Entity Player in G.EntityList)
//            //{
//            //    if (Player.Distance <= 80 && G.Engine.LocalPlayer.CrosshairID != 0 && G.Engine.LocalPlayer.enumWeaponID == enumWeaponID.KNIFE)
//            //    {
//            //        G.Engine.Shoot2();
//            //    }
//            //}

//            //Console.WriteLine(Main.O.signatures.dwForceAttack);
//            //Console.WriteLine(Main.O.signatures.dwForceAttack2);

//            //Console.WriteLine(G.Engine.LocalPlayer.Flags);
//            //InputSimulator sim = new InputSimulator();
//            //int oldKills = 0;
//            //while (true)
//            //{
//            //    int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + 1 * 4);
//            //    if (kills > oldKills)
//            //    {
//            //        Console.WriteLine("killed");

//            //        sim.Keyboard.KeyPress(VirtualKeyCode.VK_P);
//            //        oldKills = kills;
//            //    }

//            //    Thread.Sleep(1); // reduce cpu usage again
//            //}

//            //Console.WriteLine(G.Engine.LocalPlayer.InBuyZone);
//            //if (G.Engine.LocalPlayer.Flags == (1 << 0))
//            //    Console.WriteLine("True");
//            //else
//            //    Console.WriteLine("False");
//            //int Local = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwLocalPlayer);
//            //int ActiveWeapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hActiveWeapon) & 0xFFF;
//            //ActiveWeapon = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (ActiveWeapon - 1) * 0x10);
//            //short ActiveWeaponID = Memory.ReadMemory<short>(ActiveWeapon + Main.O.netvars.m_iItemDefinitionIndex);
//            //var CSGOActiveWeapon = (enumWeaponID)ActiveWeaponID;
//            //int ActiveWeaponViewModelID = Memory.ReadMemory<int>(ActiveWeapon + Main.O.netvars.m_iViewModelIndex);
//            //int LocalViewModel = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hViewModel);
//            ////int WEAPON_KNIFECHANGER_ID = (int)Config.WEAPON_KNIFECHANGER;

//            //for (int i = 0; i < 8; i++)
//            //{
//            //    int Weapon = Memory.ReadMemory<int>(Local + Main.O.netvars.m_hMyWeapons + i * 0x4) & 0xFFF;
//            //    Weapon = Memory.ReadMemory<int>((int)Memory.Client + Main.O.signatures.dwEntityList + (Weapon - 1) * 0x10);
//            //    short enumWeaponID = Memory.ReadMemory<short>(Weapon + Main.O.netvars.m_iItemDefinitionIndex);
//            //    if (enumWeaponID == 0) { continue; }
//            //    var CSGOWeaponID = (enumWeaponID)enumWeaponID;

//            //    int PaintKit = 0;
//            //    int EntityQuality = 0;
//            //    float Wear = 0.0001f;
//            //    int Seed = 0;
//            //    int StatTrack = 1337;
//            //    string CustomName = "Name";

//            //    switch (CSGOWeaponID)
//            //    {
//            //        case Classes.enumWeaponID.DEAGLE:
//            //            PaintKit = 37;
//            //            CustomName = "Deagle";
//            //            break;
//            //        case Classes.enumWeaponID.GLOCK:
//            //            PaintKit = 353;
//            //            break;
//            //        case Classes.enumWeaponID.AK47:
//            //            PaintKit = 180;
//            //            Seed = 321;
//            //            break;
//            //        case Classes.enumWeaponID.AWP:
//            //            PaintKit = 446;
//            //            CustomName = "AWP";
//            //            break;
//            //        case Classes.enumWeaponID.M4A1S:
//            //            PaintKit = 449;
//            //            break;
//            //    }

//            //    if (PaintKit != 0)
//            //    {
//            //        if (Memory.ReadMemory<int>(Weapon + Main.O.netvars.m_iItemIDHigh) != -1)
//            //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iItemIDHigh, -1);

//            //        Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_OriginalOwnerXuidLow, 0);
//            //        Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_OriginalOwnerXuidHigh, 0);
//            //        Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nFallbackPaintKit, PaintKit);
//            //        Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nFallbackSeed, Seed);
//            //        Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_nFallbackStatTrak, StatTrack);
//            //        Memory.WriteMemory<float>(Weapon + Main.O.netvars.m_flFallbackWear, Wear);
//            //        //WriteString(Weapon + Main.O.netvars.m_szCustomName.ToString(), CustomName);

//            //        if (StatTrack >= 0)
//            //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iEntityQuality, 9);
//            //        else
//            //            Memory.WriteMemory<int>(Weapon + Main.O.netvars.m_iEntityQuality, EntityQuality);
//            //    }
//            //}

//            //KeyListener.Run();


//            //for (int i = 0; i <= 64; i++)
//            //{
//            //    Console.WriteLine(Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iPing + (i * 4)));
//            //}

//            //Console.WriteLine(G.Engine.LocalPlayer.SilencerOn);

//            //DateTime date = DateTime.Now;
//            //TimeSpan time = new TimeSpan(0, 0, 10, 0);
//            //DateTime combined = date.Add(time);
//            //int Compare = DateTime.Compare(date, combined);
//            //bool hasBeenFalse = true;
//            //long compared = 0;
//            //long seconds2 = 0;
//            //while (true)
//            //{
//            //    //DateTime currentTime = DateTime.Now;
//            //    //DateTime detonateTime = new DateTime(0);
//            //    //long seconds1 = currentTime.Ticks / TimeSpan.TicksPerSecond;
//            //    ////currentTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, currentTime.Hour, currentTime.Minute, currentTime.Second, 0, 0);
//            //    //if (!G.Engine.BombPlanted)
//            //    //{
//            //    //    detonateTime = currentTime.AddSeconds(40);
//            //    //    seconds2 = detonateTime.Ticks / TimeSpan.TicksPerSecond;
//            //    //    //detonateTime = new DateTime(detonateTime.Year, detonateTime.Month, detonateTime.Day, detonateTime.Hour, detonateTime.Minute, detonateTime.Second, 0, 0);
//            //    //    //compared = DateTime.Compare(currentTime, detonateTime);
//            //    //}
//            //    //compared = seconds2 - seconds1;

//            //    //if (compared == 0)
//            //    //{
//            //    //    Console.WriteLine("Detonated.");
//            //    //    MessageBox.Show("Detonated.");
//            //    //}

//            //    //Console.WriteLine("Compared = " + compared);
//            //    //Console.WriteLine("Current Time = " + seconds1);
//            //    //Console.WriteLine("Detonate Time = " + seconds2);

//            //    //if (G.Engine.BombPlanted && hasBeenFalse)
//            //    //{
//            //    //    hasBeenFalse = false;
//            //    //    date = DateTime.Now;
//            //    //    time = new TimeSpan(0, 0, 0, 40);
//            //    //    combined = date.Add(time);
//            //    //    Compare = DateTime.Compare(date, combined);

//            //    //    Console.WriteLine(combined);
//            //    //}
//            //    //Console.WriteLine("Compare: " + Compare);
//            //    //if (Compare == 0)
//            //    //{
//            //    //    System.Windows.Forms.MessageBox.Show("Boom!");
//            //    //    Console.WriteLine("Boom!");
//            //    //}

//            //    //if (G.Engine.BombPlanted == false)
//            //    //{
//            //    //    hasBeenFalse = true;
//            //    //}

//            //    //int i = 0;
//            //    //bool enabled = true;
//            //    //System.Timers.Timer aTimer = new System.Timers.Timer();
//            //    //aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
//            //    //aTimer.Interval = 45000;

//            //    //if (G.Engine.LocalPlayer.StartedArming)
//            //    //{
//            //    //    System.Timers.Timer aTimer = new System.Timers.Timer();
//            //    //    aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
//            //    //    aTimer.Interval = 45000;
//            //    //    aTimer.Enabled = true;

//            //    //    // Specify what you want to happen when the Elapsed event is raised.
//            //    //    void OnTimedEvent(object source, ElapsedEventArgs e)
//            //    //    {
//            //    //        for (int i = 0; i <= aTimer.Interval; i++)
//            //    //            Console.WriteLine(i);
//            //    //    }
//            //    //}

//            //    //DateTime date = DateTime.Now;
//            //    //Console.WriteLine(date);



//            //    // Specify what you want to happen when the Elapsed event is raised.
//            //    //void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
//            //    //{
//            //    //    Console.WriteLine("Raised: {0}", e.SignalTime);
//            //    //}

//            //    //Console.WriteLine(G.Engine.BombPlanted);
//            //    Thread.Sleep(1);
//            //}
//        }
//    }
//}

////    InputSimulator sim = new InputSimulator();
////    int oldKills = 0;
////    int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + 1 * 4);
////    if (kills > oldKills)
////    {
////        Console.WriteLine("killed");
////        sim.Keyboard.TextEntry("Oh I'm so sorry :(");
////        sim.Keyboard.KeyPress(VirtualKeyCode.VK_Y);
////        //Menu.Killmessage();
////        oldKills = kills;
////    }

////Thread.Sleep(1); // reduce cpu usage again

////    int weaponbase = Memory.ReadMemory<int>(G.Engine.LocalPlayer.EntityBase + Main.O.netvars.m_hActiveWeapon);
////int ammo = Memory.ReadMemory<int>(Main.O.netvars.m_hActiveWeapon + Main.O.netvars.m_iClip1);
////    if (Tools.HoldingKey(86))
////    {
////        Console.WriteLine("Pressed.");
////    }

////    Thread.Sleep(20);

////    for (int i = 1; i< 64; i++)
////    {
////        char RadarPlayerName = Memory.ReadMemory<char>(G.Engine.Radar + (0x1E0 * (i + 1) + 0x24));
////Console.WriteLine(RadarPlayerName);
////    }
////    Thread.Sleep(100);

////    public static string get_wcharZ(byte[] strbytes)
////{
////    return System.Text.Encoding.Unicode.GetString(strbytes).TrimEnd('\0');
////}

////InputSimulator sim = new InputSimulator();
////int oldKills = 0;
////    while (true)
////    {
////        int kills = Memory.ReadMemory<int>(G.Engine.PlayerResource + Main.O.netvars.m_iKills + 1 * 4);
////        if (kills > oldKills)
////        {
////            Console.WriteLine("killed");
////            //sim.Keyboard.KeyPress(VirtualKeyCode.F5);
////            //Menu.Killmessage();
////            oldKills = kills;
////        }

////        Thread.Sleep(1); // reduce cpu usage again
////    }

////    for (int i = 1; i< 64; i++)
////    {
////        char RadarPlayerName = Memory.ReadMemory<char>(G.Engine.Radar + (0x1E0 * (i + 1) + 0x24));
////Console.WriteLine(RadarPlayerName.ToString());
////    }
////}
