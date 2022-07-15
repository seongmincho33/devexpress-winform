using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using XmlViewer.Properties;

namespace XmlViewer
{
    public partial class frmPDMSRegistry : Form
    {
        private Timer timer = null;

        string previousValue = null;

        public frmPDMSRegistry()
        {
            InitializeComponent();
            this.btnClear.Click += BtnClear_Click;
            this.btnRegistrySearch.Click += BtnRegistrySearch_Click;
            this.FormClosing += FrmPDMSRegistry_FormClosing;
            this.FormClosed += FrmPDMSRegistry_FormClosed;
            this.txtRegistryKeyName.Text = Settings.Default.RegistryKeyName;
            this.txtRegistryValueName.Text = Settings.Default.RegistryValueName;
        }

        private void BtnRegistrySearch_Click(object sender, EventArgs e)
        {
            Settings.Default.RegistryKeyName = this.txtRegistryKeyName.Text;
            Settings.Default.RegistryValueName = this.txtRegistryValueName.Text;
            Settings.Default.Save();
            this.SetTimer();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            this.richTextBox.Clear();
            if(timer != null)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
            }
            previousValue = null;
        }

        private void SetTimer()
        {
            timer = new Timer();
            timer.Interval = 10;
            timer.Tick += Timer_Tick;           
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            try
            {
                //레지스트리로부터 값을 받아옵니다.
                var registry_value = this.GetRegistryValue();
                if(previousValue != registry_value)
                {
                    int length = richTextBox.TextLength;
                    this.richTextBox.AppendText(registry_value + "\n\n");
                    richTextBox.SelectionStart = length;
                    richTextBox.SelectionLength = registry_value.Length;
                    if (registry_value.StartsWith("PDMS") && !registry_value.Contains("ERROR"))
                    {
                        richTextBox.SelectionColor = Color.Blue;
                    }
                    else if (registry_value.StartsWith("CONVERTER") && !registry_value.Contains("ERROR"))
                    {
                        richTextBox.SelectionColor = Color.Green;
                    }                    
                    else if (registry_value.Contains("ERROR"))
                    {
                        richTextBox.SelectionColor = Color.Red;
                    }
                    previousValue = registry_value;
                }                
            }
            catch(Exception ex)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
            }                
        }

        private string GetRegistryValue()
        {
            try
            {
                object obj;
                obj = Registry.GetValue(this.txtRegistryKeyName.Text, this.txtRegistryValueName.Text, "");

                if (obj == null)
                    return "";
                else
                    return obj.ToString();
            }
            catch (Exception ex)
            {
                timer.Tick -= Timer_Tick;
                timer.Stop();
                MessageBox.Show(ex.ToString());
            }
            return "";
        }

        private void FrmPDMSRegistry_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (timer != null)
            {
                timer.Dispose();
                timer = null;
            }
        }

        private void FrmPDMSRegistry_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(timer != null)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
        }      
    }
}
