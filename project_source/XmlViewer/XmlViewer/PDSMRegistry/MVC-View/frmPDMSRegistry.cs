using System;
using System.Drawing;
using System.Windows.Forms;
using XmlViewer.PDSMRegistry.MVC_Controller;
using XmlViewer.PDSMRegistry.MVC_Model;

namespace XmlViewer
{
    public partial class frmPDMSRegistry : Form, IPDMSRegistry
    {
        PDMSRegistryController _controller;

        public string KeyName
        {
            get { return this.txtRegistryKeyName.Text; }
            set { this.txtRegistryKeyName.Text = value; }
        }

        public string KeyValue
        {
            get { return this.txtRegistryValueName.Text; }
            set { this.txtRegistryValueName.Text = value; }
        }

        public frmPDMSRegistry()
        {
            InitializeComponent();
            this.btn_TrackStop.Click += Btn_TrackStop_Click;
            this.btn_SetTrackValues.Click += Btn_SetTrackValues_Click;
            this.btn_TrackStart.Click += Btn_TrackStart_Click;
            this.FormClosing += FrmPDMSRegistry_FormClosing;
            this.FormClosed += FrmPDMSRegistry_FormClosed;
            //this.KeyName = Settings.Default.RegistryKeyName;
            //this.KeyValue = Settings.Default.RegistryValueName;
        }

        private void Btn_SetTrackValues_Click(object sender, EventArgs e)
        {
            //Settings.Default.RegistryKeyName = KeyName;
            //Settings.Default.RegistryValueName = KeyValue;
            //Settings.Default.Save();
            this._controller.AddTrack();
        }

        private void Btn_TrackStart_Click(object sender, EventArgs e)
        {
            this._controller.StartTrack();
        }

        private void Btn_TrackStop_Click(object sender, EventArgs e)
        {
            this._controller.StopTrack();
        }

        public void ClearRichTextBox()
        {
            this.richTextBox_Result.Clear();
            this.richTextBox_TrackList.Clear();
        }

        public void UpdateRichTrackListWithRegistryKeyAndValue(RegistryKeyAndValue registryKeyAndValue)
        {
            if (registryKeyAndValue != null)
            {
                this.richTextBox_TrackList.AppendText(
                   "KeyName : "
                    + registryKeyAndValue.KeyName.ToString()
                    + "\n"
                    + " Key Value : "
                    + registryKeyAndValue.KeyValue.ToString()
                    + "\n\n");
            }
        }

        public void UpdateRichResultWithRegistryValue(string registry_value)
        {
            int length = richTextBox_Result.TextLength;
            this.richTextBox_Result.AppendText(registry_value + "\n\n");
            richTextBox_Result.SelectionStart = length;
            richTextBox_Result.SelectionLength = registry_value.Length;
            if (registry_value.StartsWith("PDMS") && !registry_value.Contains("ERROR"))
            {
                richTextBox_Result.SelectionColor = Color.Blue;
            }
            else if (registry_value.StartsWith("CONVERTER") && !registry_value.Contains("ERROR"))
            {
                richTextBox_Result.SelectionColor = Color.Green;
            }
            else if (registry_value.Contains("ERROR"))
            {
                richTextBox_Result.SelectionColor = Color.Red;
            }
        }

        private void FrmPDMSRegistry_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._controller.DisposeTimer();
        }

        private void FrmPDMSRegistry_FormClosing(object sender, FormClosingEventArgs e)
        {
            this._controller.StopTimer();
        }

        public void SetController(PDMSRegistryController controller)
        {
            this._controller = controller;
        }
    }
}
