using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using XmlViewer.PDSMRegistry.MVC_Controller;
using XmlViewer.PDSMRegistry.MVC_Model;

namespace XmlViewer
{
    public partial class frmPDMSRegistry : Form, IPDMSRegistry
    {
        ModeSwitch modeSwitch { get; set; }

        PDMSRegistryController _controller;

        public string Key
        {
            get { return this.txtRegistryKeyName.Text; }
            set { this.txtRegistryKeyName.Text = value; }
        }

        public string ValueName
        {
            get { return this.txtRegistryValueName.Text; }
            set { this.txtRegistryValueName.Text = value; }
        }

        public IList SelectedTrackInfoToDelete
        {
            get { return this.dataGridView_RegistryInfos.SelectedRows; }
        }

        public frmPDMSRegistry()
        {
            InitializeComponent();            
            this.btn_TrackStop.Click += Btn_TrackStop_Click;
            this.btn_SetTrackValues.Click += Btn_SetTrackValues_Click;
            this.btn_TrackStart.Click += Btn_TrackStart_Click;
            this.btn_TrackDelete.Click += Btn_TrackDelete_Click;
            this.btn_ClearRichResult.Click += Btn_ClearRichResult_Click;
            this.FormClosing += FrmPDMSRegistry_FormClosing;
            this.FormClosed += FrmPDMSRegistry_FormClosed;
            this.dataGridView_RegistryInfos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView_RegistryInfos.RowPostPaint += DataGridView_RegistryInfos_RowPostPaint;
            modeSwitch = new ModeSwitch(this.btn_SetTrackValues, this.btn_TrackStart, this.btn_TrackStop, this.btn_TrackDelete);
        }

        private void Btn_ClearRichResult_Click(object sender, EventArgs e)
        {
            this._controller.ClearRichResult();
        }

        private void Btn_TrackDelete_Click(object sender, EventArgs e)
        {
            this._controller.DeleteTrack();
        }

        private void DataGridView_RegistryInfos_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            var grid = sender as DataGridView;
            var rowIdx = (e.RowIndex + 1).ToString();

            var centerFormat = new StringFormat()
            {
                // right alignment might actually make more sense for numbers
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
            e.Graphics.DrawString(rowIdx, this.Font, SystemBrushes.ControlText, headerBounds, centerFormat);
        }

        private void Btn_SetTrackValues_Click(object sender, EventArgs e)
        {           
            this._controller.AddTrack();
        }

        private void Btn_TrackStart_Click(object sender, EventArgs e)
        {
            this._controller.StartTrack();
            modeSwitch.onSwitch();            
        }

        private void Btn_TrackStop_Click(object sender, EventArgs e)
        {
            this._controller.StopTrack();
            modeSwitch.onSwitch();      
        }

        public void ClearRichTextBox()
        {
            this.richTextBox_Result.Clear();           
        } 

        public void ClearDataGridView()
        {
            this.dataGridView_RegistryInfos.DataSource = null;
        }

        public void UpdateDataGridViewTrackListWithRegistryKeyAndValueName(IList registryList)
        {
            if (registryList != null)
            {                
                this.dataGridView_RegistryInfos.DataSource = null;
                this.dataGridView_RegistryInfos.DataSource = registryList;
            }
        }

        public void UpdateRichResultWithValueData(string valueData)
        {
            int length = richTextBox_Result.TextLength;
            this.richTextBox_Result.AppendText(valueData + "\n\n");
            richTextBox_Result.SelectionStart = length;
            richTextBox_Result.SelectionLength = valueData.Length;
            if (valueData.StartsWith("PDMS") && !valueData.Contains("ERROR"))
            {
                richTextBox_Result.SelectionColor = Color.Blue;
            }
            else if (valueData.StartsWith("CONVERTER") && !valueData.Contains("ERROR"))
            {
                richTextBox_Result.SelectionColor = Color.Green;
            }
            else if (valueData.Contains("ERROR"))
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
