using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraEditors;

namespace SMJOWizard.Views
{
    public partial class BaseWizardPage : XtraUserControl, ISupportNavigation
    {
        public BaseWizardPage()
        {
            InitializeComponent();
        }
        protected IWizardPageViewModel PageViewModel
        {
            get { return WizardViewModel.CurrentPage; }
        }
        protected IWizardViewModel WizardViewModel
        {
            get { return wizardViewModel; }
        }
        #region ISupportNavigation Members
        IWizardViewModel wizardViewModel;
        void ISupportNavigation.OnNavigatedTo(INavigationArgs args)
        {
            wizardViewModel = args.Parameter as IWizardViewModel;
        }
        void ISupportNavigation.OnNavigatedFrom(INavigationArgs args)
        {
        }
        #endregion
    }
}