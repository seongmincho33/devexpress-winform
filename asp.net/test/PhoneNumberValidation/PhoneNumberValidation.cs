using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validation
{
    public class PhoneNumberValidation : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }


        /*
         * [대한민국 전화번호 규칙]
         * 
         * XXXX-YYYY — 같은 지역 안으로 전화를 걸 경우.국번호 XXXX는 최대 4자리까지, 가입자 개별 번호 YYYY는 4자리로 구성되며, 국번호는 2부터 9까지의 숫자로 시작한다.
         * 0NZ-XXXX-YYYY — 다른 지역으로 전화를 걸 경우, N은 2부터 6까지, Z는 1부터 5까지의 숫자이다.
         * 0NN-(0ZZ)-XXXX-YYYY — 일반 전화가 아닌 다른 통신망으로 전화를 걸 경우.통신망 식별번호 NN은 최대 4자리까지이다.지역번호는 시외전화에서 다른 지역으로 전화를 걸 때만 사용한다.
         * 00N-PPP-QQQQQQQ — 대한민국 바깥으로 전화를 걸 경우.국제전화 통신망 식별번호 N은 최대 3자리까지이며, 국가 번호 PPP는 국제 표준을 따른다.
         * (0ZZ)-1RRR — 1로 시작하는 번호는 특수번호로 별도의 서비스를 제공하는 데 사용된다.경우에 따라 지역번호가 필요한 경우도 있다.
        */
        private enum HypenLocation
        {
            BothHyphens_344,
            BothHyphens_244,
            BothHyphens_334,
            BothHyphens_234,

        }
        private HypenLocation hypenLocation;

        private string SetPhoneNumberWithHyphen(string phoneNumber)
        {
            string phoneNumberWithHyphen = "";
            if (IsAvailablePhoneNumber(phoneNumber))
            {
                if (phoneNumber.Length == 11)
                {
                    phoneNumberWithHyphen = phoneNumber.Insert(3, "-");
                    phoneNumberWithHyphen = phoneNumberWithHyphen.Insert(8, "-");
                }
                else
                {
                    phoneNumberWithHyphen = phoneNumber.Insert(3, "-");
                    phoneNumberWithHyphen = phoneNumberWithHyphen.Insert(7, "-");
                }
            }
            return phoneNumberWithHyphen;
        }

        private bool IsAvailablePhoneNumber(string phoneNumber)
        {
            bool success = false;
            if (phoneNumber == null)
            {
                return false;
            }
            if (IsHyphenRightLocation(phoneNumber))
            {
                if (IsPhoneNumberWithOrWithOutHyphen(phoneNumber))
                {
                    this.SetHyphenLocation(phoneNumber);
                    return true;
                }
            }
            return success;
        }
        private bool IsHyphenRightLocation(string phoneNumber)
        {
            bool success = false;
            int firstHyphenPosition = phoneNumber.IndexOf("-");
            int secondHyphenPosition = phoneNumber.IndexOf("-", phoneNumber.IndexOf(',') + 1);
            if (firstHyphenPosition != -1 && secondHyphenPosition != -1)
            {
                if(
                    (firstHyphenPosition == 3 && secondHyphenPosition == 8)
                    || (firstHyphenPosition == 2 && secondHyphenPosition == 7)
                    || (firstHyphenPosition == 3 && secondHyphenPosition == 7)
                    || (firstHyphenPosition == 2 && secondHyphenPosition == 6)
                    )
                {
                    return true;
                }
            }
            else if (firstHyphenPosition != -1 && secondHyphenPosition == -1)
            {
                if (firstHyphenPosition == 3 || firstHyphenPosition == 2)
                {
                    return success = true;
                }
            }
            else if (firstHyphenPosition == -1 && secondHyphenPosition != -1)
            {
                if (secondHyphenPosition == 6 || secondHyphenPosition == 7 || secondHyphenPosition == 8)
                {
                    return success = true;
                }
            }
            else
            {
                //no hyphens
                return success = true;
            }
            return success;
        }
        private bool IsPhoneNumberWithOrWithOutHyphen(string phoneNumber)
        {
            string phoneNumberWithOutHyphen = phoneNumber;
            phoneNumberWithOutHyphen = string.Join("", phoneNumberWithOutHyphen.Split('-'));
            bool success = false;
            if (phoneNumberWithOutHyphen.Length == 9 
                || phoneNumberWithOutHyphen.Length == 10
                || phoneNumberWithOutHyphen.Length == 11)
            {
                Regex regex = new Regex(@"0[0-9]{1}[016789]{0,1}-?[2-9]{1}[0-9]{2,3}-?[0-9]{4}");
                Match match = regex.Match(phoneNumber);
                if (match.Success)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            else
            {
                success = false;
            }
            return success;
        }

        private void SetHyphenLocation(string phoneNumber)
        {
            int firstHyphenPosition = phoneNumber.IndexOf("-");
            int secondHyphenPosition = phoneNumber.IndexOf("-", phoneNumber.IndexOf(',') + 1);
            string phoneNumberLength = string.Join("", phoneNumber.Split('-'));
            if (phoneNumberLength.Length == 11)
            {
                if(firstHyphenPosition == 3 && firstHyphenPosition == 8)
                {
                    //000-0000-0000
                }
                else if(firstHyphenPosition == 3 && secondHyphenPosition == -1)
                {
                    //000-00000000
                }
                else if(firstHyphenPosition == -1 && secondHyphenPosition == 8)
                {
                    //0000000-0000
                }
                else
                {
                    //00000000000
                }
            }
            else if(phoneNumberLength.Length == 10)
            {
                if(firstHyphenPosition == 2 && secondHyphenPosition == 7)
                {
                    //00-0000-0000
                }
                else if(firstHyphenPosition == 2 && secondHyphenPosition == -1)
                {
                    //00-00000000
                }
                else if(firstHyphenPosition == -1 && secondHyphenPosition == 7)
                {
                    //000000-0000
                }
                else
                {
                    //0000000000
                }
            }

        }


        
    }
}
