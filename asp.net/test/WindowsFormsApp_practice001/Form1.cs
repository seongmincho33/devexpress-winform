using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp_practice001
{
    public partial class Form1 : Form
    {
        MyButtonClass.Button btn001 = new MyButtonClass.Button();
        public Form1()
        {
            InitializeComponent();
            btnTest01.Click += BtnTest01_Click;

            btn001.Clicked += Btn001_Clicked;
            btn001.Clicked2 += Btn001_Clicked2;
        }

        private void BtnTest01_Click(object sender, EventArgs e)
        {
            lstBox.Items.Add("클릭 이벤트 처리");
            btn001.OnClick();

            btn001.CallBackTest01(Add);
            btn001.CallBackTest01((v1, v2) => { lstBox.Items.Add($"{v1} + {v2}"); });
            btn001.CallBackTest01(delegate (int v1, int v2) { lstBox.Items.Add($"{v1} + {v2}"); });
            btn001.CallBackTest02(Add, 2, 2);
            btn001.CallBackTest02((v1, v2) => { lstBox.Items.Add($"{v1} + {v2}"); }, 2, 2);
            btn001.CallBackTest02(delegate (int v1, int v2) { lstBox.Items.Add($"{v1} + {v2}"); }, 2, 2);
        }

        private void Btn001_Clicked(object sender, EventArgs e)
        {
            lstBox.Items.Add("내 버튼 클릭 이벤트 처리");
        }

        private void Btn001_Clicked2(object arg1, EventArgs arg2)
        {
            lstBox.Items.Add("내 버튼 클릭 이벤트 처리2");
        }

        private void Add(int v1, int v2)
        {
            lstBox.Items.Add($"{v1} + {v2}");
        }
    }
}

