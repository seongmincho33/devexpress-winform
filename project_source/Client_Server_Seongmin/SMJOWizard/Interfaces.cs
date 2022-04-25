namespace SMJOWizard
{
    public interface IWizardViewModel
    {
        void PageCompleted();
        bool CanPrev();
        void Prev();
        bool CanNext();
        void Next();
        IWizardPageViewModel CurrentPage { get; }
        bool CanClose();
        void Close(bool isClosing);
    }
    public interface IWizardPageViewModel
    {
        bool IsComplete { get; }
        bool CanReturn { get; }
    }
}
