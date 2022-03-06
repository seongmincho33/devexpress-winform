using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MainForm
{
    public partial class Loading : Form
    {

        System.Windows.Forms.Timer tm;
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
         (
         int nLeftRect,     // x-coordinate of upper-left corner
         int nTopRect,      // y-coordinate of upper-left corner
         int nRightRect,    // x-coordinate of lower-right corner
         int nBottomRect,   // y-coordinate of lower-right corner
         int nWidthEllipse, // width of ellipse
         int nHeightEllipse // height of ellipse
        ); 
        public Loading()
        {
            this.Load += Loading_Load;
            InitializeComponent();
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            this.SetButtons();
            this.SetTimer();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
        }

        private void SetButtons()
        {
            this.btnClose.Click += BtnClose_Click;
            this.btnFinsish.Click += BtnFinsish_Click;
        }

        private void BtnFinsish_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetTimer()
        {
            this.tm = new System.Windows.Forms.Timer();
            this.tm.Interval = 100;
            this.tm.Tick += Tm_Tick;
            this.tm.Start();
        }

        private void Tm_Tick(object sender, EventArgs e)
        {
            bool bEndCheck = false;
            int iPer = this.pBar.Value;

            Random rd = new Random();

            iPer = iPer + rd.Next(5);

            if(iPer > 100)
            {
                iPer = 100;
                bEndCheck = true;
            }

            this.pBar.Value = iPer;
            this.lblStart.Text = iPer.ToString();

            if (bEndCheck) this.tm.Stop();            
        }
    }
}
