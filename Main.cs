using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lerawin.Classes;
using Lerawin.Utilities;

namespace Lerawin
{
    public class Main
    {
        public static List<string> Configs = new List<string>();

        public static Size ScreenSize;
        public static Vector2 MidScreen;
        public static RECT ScreenRect;

        public static RootObject O;
        public static RootObject hcO;
        //public static hcNetvars hcO2 = new hcNetvars();
        public static Settings S = new Settings();
        public static bool RunStartup()
        {
            var CSGO = Process.GetProcessesByName("csgo");
            if (CSGO.Length != 0)
            {
                Memory.Process = CSGO[0];
                Memory.ProcessHandle = Memory.OpenProcess(0x0008 | 0x0010 | 0x0020, false, Memory.Process.Id);
                foreach (ProcessModule Module in Memory.Process.Modules)
                {
                    if ((Module.ModuleName == "client.dll"))
                        Memory.Client = Module.BaseAddress;
                    else if ((Module.ModuleName == "scaleformui.dll"))
                        Memory.Scaleformui = Module.BaseAddress;
                    else if ((Module.ModuleName == "engine.dll"))
                    {
                        Memory.Engine = Module.BaseAddress;
                        G.Engine = new Engine((int)Module.BaseAddress);
                    }
                    else if (Module.ModuleName == "serverbrowser.dll") // Last loaded dll.
                        return true;
                }
                return false;
            }
            else
            {
                //MessageBox.Show("Please start CSGO before running the program.", "Error", MessageBoxButtons.OK);
                //Environment.Exit(1);
                return false;
            }
        }
    }
}
