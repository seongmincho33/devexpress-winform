namespace XmlViewer.PDSMRegistry.MVC_Model
{
    public class RegistryKeyAndValue
    {
        private string _KeyName;
        public string KeyName
        {
            get { return _KeyName; }
            set { _KeyName = value; }
        }

        private string _KeyValue;
        public string KeyValue
        {
            get { return _KeyValue; }
            set { _KeyValue = value; }
        }

        public RegistryKeyAndValue(string keyName, string keyValue)
        {
            KeyName = keyName;
            KeyValue = keyValue;
        }
    }
}
