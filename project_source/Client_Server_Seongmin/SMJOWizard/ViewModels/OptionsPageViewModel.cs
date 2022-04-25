namespace SMJOWizard.ViewModels
{
    internal class OptionsPageViewModel : IWizardPageViewModel
    {
        public bool IsComplete
        {
            get {  return !string.IsNullOrEmpty(Path) && System.IO.Directory.Exists(Path); }
        }
        public string Path
        {
            get;
            set;
        }
        public bool CanReturn { get { return true; } }
    }
}