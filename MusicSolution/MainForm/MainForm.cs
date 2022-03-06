using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MainForm
{
    public partial class MainForm : Form
    {
        private bool dragging = false;
        private Point dragCursorPoint;
        private Point dragFormPoint;
        ucPanel.ucScreen_DashBoard ucDashBoard = new ucPanel.ucScreen_DashBoard();
        ucPanel.ucScreen_Music ucMusic = new ucPanel.ucScreen_Music();

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

        public MainForm()
        {
            this.Load += MainForm_Load;
            InitializeComponent();            
        }
        
        private void MainForm_Load(object sender, EventArgs e)
        {
            this.SetMainForm();            
        }

        private void SetMainForm()
        {
            this.SetPanel();            
            this.SetForm();            
            this.SetButtons();
        }

        #region 패널
        private void SetPanel()
        {
            this.SetPanelNavigation();
            this.SetPanelMain();
            this.SetUserPanelMovable();
        }

        private void SetPanelMain()
        {
            this.pnlMain.Controls.Add(ucDashBoard);
        }

        private void SetUserPanelMovable()
        {
            this.userPanel.MouseDown += Movable_MouseDown;
            this.userPanel.MouseMove += Movable_MouseMove;
            this.userPanel.MouseUp += Movable_MouseUp;
        }

        private void SetPanelNavigation()
        {
            this.pnlNav.Height = btnDashboard.Height;
            this.pnlNav.Top = btnDashboard.Top;
            this.pnlNav.Left = btnDashboard.Left;
            btnDashboard.BackColor = Color.FromArgb(46, 51, 73);
        }
        #endregion

        #region 폼
        private void SetForm()
        {
            this.FormBorderStyle = FormBorderStyle.None;
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            this.MouseDown += Movable_MouseDown;
            this.MouseMove += Movable_MouseMove;
            this.MouseUp += Movable_MouseUp;
        }

        private void Movable_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }

        private void Movable_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void Movable_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }
        #endregion

        #region 버튼
        private void SetButtons()
        {
            //Dashboard btn
            this.btnDashboard.Click += Navigation_Click;
            this.btnDashboard.Click += BtnDashboard_Click;
            this.btnDashboard.Leave += Navigation_Leave;
            
            //Music btn
            this.btnMusic.Click += Navigation_Click;
            this.btnMusic.Click += BtnMusic_Click;
            this.btnMusic.Leave += Navigation_Leave;

            //Close btn
            this.btnClose.Click += BtnClose_Click;
        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(ucDashBoard);
        }

        private void BtnMusic_Click(object sender, EventArgs e)
        {
            this.pnlMain.Controls.Clear();
            this.pnlMain.Controls.Add(ucMusic);
        }       
        
        private void Navigation_Leave(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            btn.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void Navigation_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            this.pnlNav.Height = btn.Height;
            this.pnlNav.Top = btn.Top;
            this.pnlNav.Left = btn.Left;
            btn.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        #endregion

    }
}
