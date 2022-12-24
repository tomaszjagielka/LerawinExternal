using Lerawin.Classes;
using Lerawin.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lerawin.Forms
{
    public partial class Overlay : Form
    {
        Classes.RECT rect;

        public struct RECT
        {
            public int left, top, right, bottom;
        }

        Graphics g;
        Pen myPen = new Pen(Color.Red);

        public Overlay()
        {
            InitializeComponent();
        }

        private void Overlay_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.Wheat;
            this.TransparencyKey = Color.Wheat;
            this.TopMost = true;

            int initialStyle = Memory.GetWindowLong(this.Handle, -20);
            Memory.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);


            Memory.GetWindowRect(Tools.handle, out rect);
            this.Size = new Size(rect.right - rect.left, rect.bottom - rect.top);
            this.Top = rect.top;
            this.Left = rect.left;
        }

        private void Overlay_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            while (true)
            {
                g.Clear(Color.Wheat);
                foreach (Entity Player in G.EntityList)
                {
                    if (!Player.Valid) continue;
                    Vector2 Player2DPos = Tools.WorldToScreen(new Vector3(Player.Position.X, Player.Position.Y, Player.Position.Z));
                    Vector2 Player2DHeadPos = Tools.WorldToScreen(new Vector3(Player.HeadPosition.X, Player.HeadPosition.Y, Player.HeadPosition.Z));

                    if (!Tools.IsNullVector2(Player2DPos) && !Tools.IsNullVector2(Player2DHeadPos) && Player.Valid)
                    {
                        float BoxHeight = Player2DPos.Y - Player2DHeadPos.Y;
                        float BoxWidth = (BoxHeight / 2) * 1.25f; //little bit wider box

                        g.DrawRectangle(myPen, Player2DPos.X - (BoxWidth / 2), Player2DHeadPos.Y, BoxWidth, BoxHeight);
                        //DrawOutlineBox(, , , , Color.Red);
                    }


                }

                System.Threading.Thread.Sleep(1);

            }




        }
    }
}
