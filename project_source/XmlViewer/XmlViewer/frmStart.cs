using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XmlViewer
{
    public partial class frmStart : Form
    {
        public frmStart()
        {
            InitializeComponent();
            this.btnStartXMLViewer.Click += BtnStartXMLViewer_Click;
            this.btnPDMSRegistry.Click += BtnPDMSRegistry_Click;
        }

        private void BtnPDMSRegistry_Click(object sender, EventArgs e)
        {
            frmPDMSRegistry frmPDMSRegistry = new frmPDMSRegistry();
            frmPDMSRegistry.Show();
        }

        private void BtnStartXMLViewer_Click(object sender, EventArgs e)
        {
            frmViewer frmViewer = new frmViewer();
            frmViewer.Show();
        }
    }
}
