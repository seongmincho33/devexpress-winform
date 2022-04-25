namespace SMJOWizard
{
    public partial class ucFinishPage : Views.BaseWizardPage
    {
        public ucFinishPage()
        {
            InitializeComponent();
        }
        void finishButton_Click(object sender, System.EventArgs e)
        {
            WizardViewModel.Close(false);
        }
    }
}