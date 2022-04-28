# Timer & Registry

타이머는 시간을 설정해주면 반복해서 돌아주는 객채를 생성해줍니다. 중요 이벤트로는 Timer.Tick가 있습니다. 

아래는 타이머를 사용해서 레지스트리안의 값을 가져와 Switch문에서 레지스트리 내용물에 따라서 작업을 수행하는 코드가 되겠습니다. 이런방법을 사용한다면 서로 분리되어 있는 exe파일끼리 소통을 할 수 있습니다. 원리는 서로다른 exe가 서로의 명령을 레지스트리에 저장합니다. 레지스트리 위치나 이름같은것은 개발자가 임의로 정합니다. 그러면 타이머가 계속 무한루프로 돌면서 레지스트리안의 값이 바뀌나 안바뀌나 지켜봅니다. 커멘드가 바뀌면 그 커맨드에 따라서 수행해야 하는 일을 하게 됩니다. 그럼 데이터는 어떻게 주고받나? 타이머와 레지스트리를 이용해서 서로에게 데이터를 어디에 어떤명령일때 저장할지 약속해두면 됩니다. 그럼 서로 명령을 내릴때마다 데이터를 주고받을 수 있게됩니다. 통신은 아닌데 유사한 기능을 냅니다. 

```C#
private void SetTimer()
{
    Timer timer = new Timer();
    timer.Interval = 500;
    timer.Tick += (object sender, EventArgs e) =>
    {        
        //레지스트리로부터 값을 받아옵니다.
        var registry_value = this.GetRegistryValue("PDMSCommand");

        switch (registry_value)
        {
            case nameof(CommandValue.CONVERTER_DATA_REQUEST):
             
                //특정 레지스트리 값을 받으면 하고싶은 일을 여기에 작성
                //아래줄은 예시입니다.
                this.SetCommandToRegistry(CommandValue.PDMS_DATA_READY.ToString());
                
                //타이머 멈춤
                ((Timer)sender).Stop();
                break;
        }
    };
    timer.Start();
}
```

아래는 레지스트리에서 값을 가져오거나 값을 셋팅하는 메서드 입니다.

```C#
public string GetRegistryValue(string value)
{
    object obj;
    obj = Registry.GetValue(RegistryPath, value, "");

    if (obj == null)
        return "";
    else
        return obj.ToString();
}
private void SetCommandToRegistry(string value)
{
    try
    {
        Registry.SetValue(RegistryPath, CommandValueName, value);
    }
    catch(Exception ex)
    {
        MessageBox.Show(ex.ToString());
    }
}
```

아래는 경로 셋팅값입니다. 

```C#
//레지스트리 경로
const string RegistryPath = "HKEY_CURRENT_USER\\SOMETHING\\SOMETHING\\";
private enum CommandValue
{
    PDMS_COMMAND_WAITING
    ,CONVERTER_DATA_REQUEST
    ,PDMS_DATA_READY
    ,CONVERTER_DRAW_REQUEST
    ,PDMS_DRAW_COMPLETE
}
```