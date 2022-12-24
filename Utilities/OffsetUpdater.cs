using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Lerawin.Classes;
using System.Net;

namespace Lerawin.Utilities
{
    // hazedumper auto offset updater - veri cool
    public class OffsetUpdater
    {
        public static void GetOffsetsFromFile()
        {
            string json = File.ReadAllText($@"{Application.StartupPath}\csgo.json");
            Main.O = JsonConvert.DeserializeObject<RootObject>(json);

            //string json2 = File.ReadAllText($@"{Application.StartupPath}\offsets.json");

            //try
            //{
            //    Main.hcO = JsonConvert.DeserializeObject<RootObject>(json2);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("An unexpected error occured: " + ex.Message + ". Please try again.");
            //}



            //Console.WriteLine(json2);


            //string netvarsJson = File.ReadAllText($@"{Application.StartupPath}\offsets.json");
            //Main.N = JsonConvert.DeserializeObject<RootObject>(netvarsJson);
        }
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (client.OpenRead("http://clients3.google.com/generate_204"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public static void UpdateOffsets()
        {
            if (CheckForInternetConnection())
            {
                System.Net.WebClient wc = new System.Net.WebClient();
                byte[] raw = wc.DownloadData("https://raw.githubusercontent.com/frk1/hazedumper/master/csgo.json");
                string webData = Encoding.UTF8.GetString(raw);
                File.WriteAllText($@"{Application.StartupPath}\csgo.json", webData);
            }

            //if (!File.Exists($@"{Application.StartupPath}\offsets.json"))
            //{
            //    hcNetvars sendtooffsets = Main.hcO2;
            //    string json = JsonConvert.SerializeObject(sendtooffsets, Formatting.Indented);
            //    File.WriteAllText($@"{Application.StartupPath}\offsets.json", json);
            //    Console.WriteLine("Wrote netvars");
            //}

            GetOffsetsFromFile();

            //File.Delete($@"{Application.StartupPath}\csgo.json");

            //// Store integer
            //int intValue = Main.O.netvars.m_bIsWalking;
            //// Convert integer as a hex in a string variable
            //string hexValue = intValue.ToString("X");

            //File.WriteAllText($@"{Application.StartupPath}\LeraWinOffsets.txt", hexValue);
        }
    }
}
