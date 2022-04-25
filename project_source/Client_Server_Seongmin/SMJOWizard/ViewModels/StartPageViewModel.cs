namespace SMJOWizard.ViewModels
{
    internal class StartPageViewModel : IWizardPageViewModel
    {
        public bool IsComplete { get { return true; } }
        public bool CanReturn { get { return false; } }
    }
}