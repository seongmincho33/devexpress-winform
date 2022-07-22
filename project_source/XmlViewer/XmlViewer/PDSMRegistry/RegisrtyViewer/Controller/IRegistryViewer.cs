using System.Collections;
using System.Data;
using System.Windows.Forms;
using Viewer.RegistryViewer.Model;

namespace Viewer.RegistryViewer.View
{
    public interface IRegistryViewer
    {
        void SetController(RegistrVieweryController controller);

        void UpdateRichResultWithValueData(string vvalue);

        void ClearRichTextBox();

        void ClearDataGridView();

        void UpdateDataGridViewTrackListWithRegistryKeyAndValueName(IList registryList);

        string Key { get; set; }

        string ValueName { get; set; }

        IList SelectedTrackInfoToDelete { get; }
    }
}
