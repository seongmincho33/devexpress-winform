using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace delegateSample003
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //btnTest.Click += BtnTest_Click;
            //btnTest.Click += delegate (object sender, EventArgs e)


            btnTest.Caption2Changed += BtnTest_Caption2Changed;
            btnChange.Click += BtnChange_Click;
        }

        private void BtnTest_Caption2Changed(object sender, Data data)
        {
            
        }





        //private void BtnTest_Click1(object sender, EventArgs e)
        //{
        //    throw new NotImplementedException();
        //}

        //private int BtnTest_Click(object sender, EventArgs e)
        //{
        //    lstBox.Items.Add("내가 만든 버튼Click");
        //}

        //private void BtnTest_Caption2Changed(object sender, Data data)
        //{
        //    lstBox.Items.Add($"이전값 : {data.OldValue}");
        //    lstBox.Items.Add($"변경값 : {data.NewValue}");
        //    lstBox.Items.Add(string.Empty);
        //}

        private void BtnChange_Click(object sender, EventArgs e)
        {
            btnTest.Caption2 = txtCaption2.Text;
        }
    }
}
