using DevExpress.XtraEditors.Controls;
using System.Windows.Forms;

namespace SMJOWizard
{
    public partial class ucOptionsPage : Views.BaseWizardPage
    {
        public ucOptionsPage()
        {
            InitializeComponent();
        }
        void textEdit1_Properties_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                textEdit1.EditValue = folderBrowserDialog1.SelectedPath;
        }
        void textEdit1_EditValueChanged(object sender, System.EventArgs e)
        {
            ((ViewModels.OptionsPageViewModel)PageViewModel).Path = textEdit1.EditValue as string;
            WizardViewModel.PageCompleted();
        }
    }
}