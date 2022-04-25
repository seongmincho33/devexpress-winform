namespace SMJOWizard.ViewModels
{
    internal class InstallPageViewModel : IWizardPageViewModel
    {
        public bool IsComplete
        {
            get;
            set;
        }
        public bool CanReturn { get { return !IsComplete; } }
    }
}