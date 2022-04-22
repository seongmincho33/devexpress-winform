namespace PDMS_TEST
{
    public class ExampleCommand002 : Aveva.ApplicationFramework.Presentation.Command
    {
        public ExampleCommand002()
        {
            Key = "TEST_SMJO";

            this.List.Add("TEST_SMJO");
            this.Value = "TEST_SMJO";
        }
    }
}
