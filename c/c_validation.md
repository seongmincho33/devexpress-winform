# Validaition (유효성 검사)

1. 주민등록번호
2. 전화번호
3. 이메일
4. 계좌번호
5. 주소
6. 시간(날짜)
7. Guid?

_________________________________________________________________________
<br>

# 1. 주민등록번호

```c#
private bool IsAvailableRegistrationNumber(string registerNumber)
{
    //공백 제거
    registerNumber = registerNumber.Replace(" ", "");
    //문자 '-' 제거
    registerNumber = registerNumber.Replace("-", "");
    //주민등록번호가 13자리인가?
    if (registerNumber.Length != 13)
    {
        return false;
    }
    int sum = 0;
    for (int i = 0; i < registerNumber.Length - 1; i++)
    {
        char c = registerNumber[i];
        //숫자로 이루어져 있는가?
        if (!char.IsNumber(c))
        {
            return false;
        }
        else
        {
            if (i < registerNumber.Length)
            {
                //지정된 숫자로 각 자리를 나눈 후 더한다.
                sum += int.Parse(c.ToString()) * ((i % 8) + 2);
            }
        }
    }
    // 검증코드와 결과 값이 같은가?
    if (!((((11 - (sum % 11)) % 10).ToString()) == ((registerNumber[registerNumber.Length - 1]).ToString())))
    {
        return false;
    }
    return true;
}
```

```c#
private string SetRegistrationNumberWithHypen(string registerNumber)
{
    string tmp = "";
    tmp = registerNumber.Substring(0, 6) + "-" + registerNumber.Substring(6, 7);
    return tmp;
}
```

_________________________________________________________________________
<br>

# 2. 전화번호

```c#
private bool IsAvailablePhoneNumber(string phoneNumber)
{
    bool success = false;
    if(phoneNumber == null)
    {
        return false;
    }
    if (this.IsPhoneNumberWithHyphen(phoneNumber) || this.IsPhoneNumberWithOutHyphen(phoneNumber))
    {                
        return true;
    }           
    return success;
}
```

```c#
private bool IsPhoneNumberWithHyphen(string phoneNumber)
{
    bool success = false;
    if (phoneNumber.Length == 12 || phoneNumber.Length == 13)
    {
        Regex regex = new Regex(@"01{1}[016789]{1}-[0-9]{3,4}-[0-9]{4}");
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
```

```c#
private bool IsPhoneNumberWithOutHyphen(string phoneNumber)
{
    bool success = false;
    if (phoneNumber.Length == 10 || phoneNumber.Length == 11)
    {
        Regex regex = new Regex(@"01{1}[016789]{1}[0-9]{7,8}");
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
```

```c#
private string SetPhoneNumberWithHyphen(string phoneNumber)
{
    string phoneNumberWithHyphen = "";
    if(phoneNumber.Length == 11)
    {
        phoneNumberWithHyphen = phoneNumber.Insert(3, "-");
        phoneNumberWithHyphen = phoneNumberWithHyphen.Insert(8, "-");
    }
    else
    {
        phoneNumberWithHyphen = phoneNumber.Insert(3, "-");
        phoneNumberWithHyphen = phoneNumberWithHyphen.Insert(7, "-");
    }
    return phoneNumberWithHyphen;
}
```

_________________________________________________________________________
<br>

# 3. 이메일

```c#
private bool IsAvailableEmail(string email)
{
    if (email.Trim().EndsWith("."))
    {
        return false; // suggested by @TK-421
    }
    try
    {
        var addr = new System.Net.Mail.MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}
```