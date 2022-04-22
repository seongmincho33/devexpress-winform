namespace PDMS_TEST
{
    public class ExampleCommand003 : Aveva.ApplicationFramework.Presentation.Command
    {
        public ExampleCommand003()
        {
            Key = "TEST_SMJO_TREE";

            this.List.Add("TEST_SMJO_TREE");
            this.Value = "TEST_SMJO_TREE";
        }
    }
}
