using Lerawin.Utilities;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Lerawin.Features
{
    public class AutoAccept
    {
        //internal static bool allow;
        public static void Run()
        {
            while (true)
            {
                if (Main.S.AutoAcceptEnabled && Menu.CSGOActive && G.Engine.GameState == 0)
                    SearchPixel("#4CAF50"); // Color of Accept button.
                //SearchPixel("#9BFF67");
                Thread.Sleep(2000); // Optimization.
            }
        }

        private static bool SearchPixel(string hexcode)
        {
            // Take an image from the screen
            // Bitmap bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height); // Create an empty bitmap with the size of the current screen 
            Bitmap bitmap = new Bitmap(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height); // Create an empty bitmap with the size of all connected screen 

            // Take a screenshoot of the primary screen.
            Graphics graphics = Graphics.FromImage(bitmap as Image); // Create a new graphics objects that can capture the screen
            graphics.CopyFromScreen(0, 0, 0, 0, bitmap.Size); // Screenshot moment → screen content to graphics object

            Color desiredPixelColor = ColorTranslator.FromHtml(hexcode);

            // Go one pixel to the right and then check from top to bottom every pixel (next iteration -> go one pixel to right and check all the way down again)
            for (int x = 823; x < 1097; x++)
            {
                for (int y = 519; y < 608; y++)
                {
                    // Get the current pixel color.
                    Color currentPixelColor = bitmap.GetPixel(x, y);

                    // Finally compare the pixel hex color and the desired hex color (if they match we found a pixel).
                    if (desiredPixelColor == currentPixelColor)
                    {
                        //MessageBox.Show("Found Pixel - Now set mouse cursor");
                        ClickAtPosition(x, y);
                        return true;
                    }

                }
            }

            //MessageBox.Show("Did not find pixel");
            return false;
        }

        private static void ClickAtPosition(int posX, int posY)
        {
            Memory.SetCursorPos(posX, posY);
            Click();
            //allow = false;
        }

        private static void Click()
        {
            Memory.mouse_event(Mouse.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
            Memory.mouse_event(Mouse.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
        }
    }
}
