using System.Collections;
using System.Data;
using System.Windows.Forms;
using XmlViewer.PDSMRegistry.MVC_Model;

namespace XmlViewer.PDSMRegistry.MVC_Controller
{
    public interface IPDMSRegistry
    {
        void SetController(PDMSRegistryController controller);

        void UpdateRichResultWithValueData(string vvalue);

        void ClearRichTextBox();

        void ClearDataGridView();

        void UpdateDataGridViewTrackListWithRegistryKeyAndValueName(IList registryList);

        string Key { get; set; }

        string ValueName { get; set; }

        IList SelectedTrackInfoToDelete { get; }
    }
}
