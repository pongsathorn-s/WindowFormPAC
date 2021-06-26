using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapForm
{

    public partial class Form1 : Form
    {

        public int x1, y1, x2, y2;
        int width = Screen.PrimaryScreen.Bounds.Width;
        int height = Screen.PrimaryScreen.Bounds.Height;
        public FolderBrowserDialog fbd = new FolderBrowserDialog();

        public Form1()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            popupConfig popup = new popupConfig();
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                x1 = Math.Min(popup.x1, popup.x2);
                y1 = Math.Min(popup.y1, popup.y2);
                x2 = Math.Max(popup.x1, popup.x2);
                y2 = Math.Max(popup.y1, popup.y2);
                width = x2 - x1;
                height = y2 - y1;
            }
            else
            {
                popup.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Thread.Sleep(100);
            picPopupFunction();
            this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            selectAreaFunction();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                System.Windows.Forms.MessageBox.Show("Your selected path is " + fbd.SelectedPath, "Messege");
            }
        }

        public void selectAreaFunction()
        {
            popupConfig popup = new popupConfig();
            selectArea selectArea = new selectArea();
            DialogResult dialogresult = selectArea.ShowDialog();
            if (selectArea.isMouseDown == false)
            {
                x1 = Math.Min(selectArea.pos1.X, selectArea.pos2.X);
                y1 = Math.Min(selectArea.pos1.Y, selectArea.pos2.Y);
                x2 = Math.Max(selectArea.pos1.X, selectArea.pos2.X);
                y2 = Math.Max(selectArea.pos1.Y, selectArea.pos2.Y);
                width = x2 - x1;
                height = y2 - y1;
            }
        }

        public void picPopupFunction()
        {
            picPopup picForm = new picPopup();
            if (GetScreenshot().Width < 600)
            {
                picForm.Size = new Size(620, GetScreenshot().Height + 75);
            }
            if (GetScreenshot().Height < 400)
            {
                picForm.Size = new Size(GetScreenshot().Width + 20, 475);
            }
            else
            {
                picForm.Size = new Size(GetScreenshot().Width + 20, GetScreenshot().Height + 75);
            }
            picForm.pictureBox1.Image = GetScreenshot();
            picForm.ShowDialog();
            
        }
       
        public Bitmap GetScreenshot()
        {
            Bitmap bm = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bm);
            g.CopyFromScreen(x1, y1, 0, 0, bm.Size);
            return bm;
        }
    }
}
