namespace XmlViewer.PDSMRegistry.MVC_Model
{
    public class SeonRegistry
    {
        private string _key;
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        private string _ValueName;
        public string ValueName
        {
            get { return _ValueName; }
            set { _ValueName = value; }
        }

        static private string _ValueData;
        static public string ValueData
        {
            get { return _ValueData; }
            set { _ValueData = value; }
        }

        public SeonRegistry(string key, string value_name)
        {
            Key = key;
            ValueName = value_name;
        }
    }
}
