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
using System.IO;

namespace UC_Library
{
    public partial class FileCopy : DevExpress.XtraEditors.XtraUserControl
    {
        public FileCopy()
        {
            InitializeComponent();
            this.SetControls();
        }

        private void SetControls()
        {
            this.SetButtons();
        }

        private void SetButtons()
        {
            this.btnFindSource.Click += BtnFindSource_Click;
            this.btnFindTarget.Click += BtnFindTarget_Click;
            this.btnAsyncCopy.Click += BtnAsyncCopy_Click;
            this.btnSyncCopy.Click += BtnSyncCopy_Click;
            this.btnCancel.Click += BtnCancel_Click;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            XtraMessageBox.Show("UI반응 성공");
        }

        private void BtnSyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = CopySync(txtSource.Text, txtTarget.Text);
        }

        private async void BtnAsyncCopy_Click(object sender, EventArgs e)
        {
            long totalCopied = await CopyAsync(txtSource.Text, txtTarget.Text);
        }

        private void BtnFindTarget_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtTarget.Text = dlg.FileName;
            }
        }

        private void BtnFindSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if(dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtSource.Text = dlg.FileName;
            }
        }

        private async Task<long> CopyAsync(string FromPath, string ToPath)
        {
            this.btnSyncCopy.Enabled = false;
            long totalCopied = 0;

            using (FileStream fromStream = new FileStream(FromPath, FileMode.Open))
            {
                using(FileStream toStream = new FileStream(ToPath, FileMode.Create))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int nRead = 0;
                    while((nRead = await fromStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                    {
                        await toStream.WriteAsync(buffer, 0, nRead);
                        totalCopied += nRead;

                        //프로그래스바에 현재 파일 복사 상태 표시
                        pbCopy.EditValue = (int)
                            (
                                ((double)totalCopied / (double)fromStream.Length) * pbCopy.Properties.Maximum
                            );
                    }
                }
            }

            this.btnSyncCopy.Enabled = true;
            return totalCopied;
        }

        private long CopySync(string FromPath, string ToPath)
        {
            this.btnAsyncCopy.Enabled = false;
            long totalCopied = 0;

            using(FileStream fromStream = new FileStream(FromPath, FileMode.Open))
            {
                using(FileStream toStream = new FileStream(ToPath, FileMode.Create))
                {
                    byte[] buffer = new byte[1024 * 1024];
                    int nRead = 0;
                    while((nRead = fromStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        toStream.Write(buffer, 0, nRead);
                        totalCopied += nRead;

                        //프로그래스바에 현재 파일 복사 상태 표시
                        pbCopy.EditValue = (int)
                            (
                                ((double)totalCopied / (double)fromStream.Length) * pbCopy.Properties.Maximum
                            );
                    }
                }
            }

            this.btnAsyncCopy.Enabled = true;
            return totalCopied;
        }
    }
}
