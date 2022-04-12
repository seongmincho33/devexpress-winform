using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using UC_Library;

namespace Main.UC
{
    public partial class UC_User : DevExpress.XtraEditors.XtraUserControl
    {
        UC_DBConnection uC_DBConnection;
        public UC_User(UC_DBConnection uC_DBConnection)
        {
            InitializeComponent();
            this.uC_DBConnection = uC_DBConnection;
        }
    }
}
