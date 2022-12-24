using Lerawin.Classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lerawin.Utilities
{
    public class ConfigManager
    {
        public static void AddConfig(string name)
        {
            File.WriteAllText($@"{Application.StartupPath}\LeraWin Configs\{name}.json", "");
            SaveConfig(name);
        }

        public static void CreateEnvironment()
        {
            Directory.CreateDirectory($@"{Application.StartupPath}\LeraWin Configs");
        }

        public static void SaveConfig(string name)
        {
            Settings sendtoconfig = Main.S;
            string json = JsonConvert.SerializeObject(sendtoconfig, Formatting.Indented);
            File.WriteAllText($@"{Application.StartupPath}\LeraWin Configs\{name}.json", json);

            // Remove unnecessary config elements, like TrackerX and TrackerY.
            //var file = new List<string>(File.ReadAllLines($@"{Application.StartupPath}\LeraWin Configs\{name}.json"));
            //file.RemoveAt(1);
            //file.RemoveAt(1);
            //File.WriteAllLines($@"{Application.StartupPath}\LeraWin Configs\{name}.json", file.ToArray());
        }
    }
}
