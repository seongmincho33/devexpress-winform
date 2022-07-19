using XmlViewer.PDSMRegistry.MVC_Model;

namespace XmlViewer.PDSMRegistry.MVC_Controller
{
    public interface IPDMSRegistry
    {
        void SetController(PDMSRegistryController controller);

        void UpdateRichResultWithRegistryValue(string registry_value);

        void ClearRichTextBox();

        void UpdateRichTrackListWithRegistryKeyAndValue(RegistryKeyAndValue registryKeyAndValue);

        string KeyName { get; set; }

        string KeyValue { get; set; }
    }
}
