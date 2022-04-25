using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using System.Windows.Forms;

namespace SMJOWizard.ViewModels
{
    internal class WizardViewModel : IWizardViewModel
    {
        WindowsUIView view;
        IWizardPageViewModel[] pages;
        Form mainForm;
        int index = 0;
        public WizardViewModel(IWizardPageViewModel[] pages, WindowsUIView view, Form mainForm)
        {
            this.pages = pages;
            this.view = view;
            this.mainForm = mainForm;
        }
        public bool CanNext()
        {
            return (index >= 0) && (index < pages.Length - 1) && CurrentPage.IsComplete;
        }
        public void Next()
        {
            ActivatePage(++index);
        }
        public bool CanPrev()
        {
            return index > 0 && index < pages.Length && CurrentPage.CanReturn;
        }
        public void Prev()
        {
            ActivatePage(--index);
        }
        public IWizardPageViewModel CurrentPage
        {
            get { return pages[index]; }
        }
        public void PageCompleted()
        {
            if (CanNext())
                Next();
        }
        void ActivatePage(int index)
        {
            PageGroup pageGroup = view.ContentContainers["pageGroup"] as PageGroup;
            view.ActivateDocument(pageGroup.Items[index]);
        }
        public bool CanClose()
        {
            return index >= 0 && index < pages.Length - 1;
        }
        public void Close(bool isClosing)
        {
            if (isClosing)
            {
                Flyout flyout = view.ContentContainers["closeFlyout"] as Flyout;
                flyout.Action = new FlyoutAction()
                {
                    Caption = "Setup Wizard",
                    Description = "Are you sure you want to exit the setup?"
                };
                view.FlyoutHidden += view_FlyoutHidden;
                view.ActivateContainer(flyout);
            }
            else Close();
        }
        void view_FlyoutHidden(object sender, FlyoutResultEventArgs e)
        {
            view.FlyoutHidden -= view_FlyoutHidden;
            if (e.Result == System.Windows.Forms.DialogResult.Yes)
                Close();
        }
        void Close()
        {
            mainForm.BeginInvoke(new System.Action(mainForm.Close));
        }
    }
}