using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XMLViewer;
using DezeroSharp;

namespace Main
{
    public partial class frmStart : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public frmStart()
        {
            InitializeComponent();
            this.SetControls();
        }

        private void SetControls()
        {
            this.accordionControlElement_XmlViewer.Click += AccordionControlElement_XmlViewer_Click;
            this.accordionControlElement_DezeroSharp.Click += AccordionControlElement_DezeroSharp_Click;
        }

        private void AccordionControlElement_DezeroSharp_Click(object sender, EventArgs e)
        {
            frmDezeroSharp frmDezeroSharp = new frmDezeroSharp();
            frmDezeroSharp.ShowDialog();
        }

        private void AccordionControlElement_XmlViewer_Click(object sender, EventArgs e)
        {
            frmXmlViewer frmXmlViewer = new frmXmlViewer();
            frmXmlViewer.ShowDialog();
        }
    }
}
