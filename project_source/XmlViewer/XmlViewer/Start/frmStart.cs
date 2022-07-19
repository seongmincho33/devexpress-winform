using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlViewer.PDSMRegistry.MVC_Controller;
using XmlViewer.PDSMRegistry.MVC_Model;

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
            //1. 뷰 생성
            frmPDMSRegistry view = new frmPDMSRegistry();
            view.Visible = false;

            //2. 데이터 생성
            IList registrys = new List<RegistryKeyAndValue>();
            registrys.Add(new RegistryKeyAndValue(@"HKEY_CURRENT_USER\Software\PEDAS\", @"PDMSCommand"));
            registrys.Add(new RegistryKeyAndValue(@"HKEY_CURRENT_USER\Software\PEDAS\", @"PDMS_REFNO"));

            //3. 컨트롤 생성
            PDMSRegistryController controller = new PDMSRegistryController(view, registrys);
            controller.LoadView();
            view.Show();
        }

        private void BtnStartXMLViewer_Click(object sender, EventArgs e)
        {
            frmViewer frmViewer = new frmViewer();
            frmViewer.Show();
        }
    }
}
