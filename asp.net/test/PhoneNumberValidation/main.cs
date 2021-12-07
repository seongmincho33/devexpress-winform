using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public class main
    {
        static void Main(string[] args)
        {
            PhoneNumberValidation phoneNumberValidation = new PhoneNumberValidation();
            Console.WriteLine(phoneNumberValidation.IsAvailablePhoneNumber("01046301373"));
        }
    }
}
