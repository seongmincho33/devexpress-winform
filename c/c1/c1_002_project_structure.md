# 2. 프로젝트 구성

1. 다중 소스코드 파일
2. 라이브러리
    1. csc.exe로 라이브러리 생성 및 사용
    2. 비주얼 스튜디오에서 라이브러리 생성 및 사용
3. 응용프로그램 구성 파일 : app.config
    1. supportedRuntime
    2. appSettings
4. 디버그 빌드와 릴리스 빌드
    1. DEBUG, TRACE 전처리 상수
    2. Debug 타입과 Trace 타입
5. 플랫폼 (x86, x64, AnyCPU) 선택
6. 버전관리 
    1. 어셈블리의 버전과 이름
    2. 공개키 토큰과 어셈블리 서명
    3. 전용 어셈블리, 전역 어셈블리

<hr />
<br />
<br />
<br />

## 프로젝트 구성 

프로젝트는 비주얼 스튜디오만의 소스코드 관리를 위해 도입된 개념입니다. 한 프로젝트는 여러개의 소스코드를 담고있고, 해당 프로젝트를 빌드하면 하나의 EXE 또는 DLL 파일이 떨어집니다. 

프로젝트를 하나 솔루션에서 생성하면 그 프로젝트에서 관리하는 모든 정보를 담는 "프로젝트 파일" 이 만들어집니다. 

프로젝트 파일은 언어마다 확장자가 다릅니다. C#의 경우는 확장자가 "csproj"입니다. 프로젝트 이름이 "LinqSample001"이라면 프로젝트 파일은 "LinqSample001.csproj"가 되고 탐색기에서 이 파일을 찾을 수 있습니다. 아래는 메모장으로 열었을때의 예시입니다. 아래 코드의 생김새를 보면 xml 형식임을 알 수 있습니다. XML(eXtensible Markup Language) 은 닷넷 전반적으로 많이 사용됩니다. 

```xml
<?xml version="1.0" encoding="utf-8"?>
<Project ToolsVersion="15.0" xmlns="http://schemas.microsoft.com/developer/msbuild/2003">
  <Import Project="$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props" Condition="Exists('$(MSBuildExtensionsPath)\$(MSBuildToolsVersion)\Microsoft.Common.props')" />
  <PropertyGroup>
    <Configuration Condition=" '$(Configuration)' == '' ">Debug</Configuration>
    <Platform Condition=" '$(Platform)' == '' ">AnyCPU</Platform>
    <ProjectGuid>{57DFF80D-C249-4B42-9D4D-C4A83F740AC8}</ProjectGuid>
    <OutputType>Exe</OutputType>
    <RootNamespace>LinqSample001</RootNamespace>
    <AssemblyName>LinqSample001</AssemblyName>
    <TargetFrameworkVersion>v4.7.2</TargetFrameworkVersion>
    <FileAlignment>512</FileAlignment>
    <AutoGenerateBindingRedirects>true</AutoGenerateBindingRedirects>
    <Deterministic>true</Deterministic>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugSymbols>true</DebugSymbols>
    <DebugType>full</DebugType>
    <Optimize>false</Optimize>
    <OutputPath>bin\Debug\</OutputPath>
    <DefineConstants>DEBUG;TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <PropertyGroup Condition=" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' ">
    <PlatformTarget>AnyCPU</PlatformTarget>
    <DebugType>pdbonly</DebugType>
    <Optimize>true</Optimize>
    <OutputPath>bin\Release\</OutputPath>
    <DefineConstants>TRACE</DefineConstants>
    <ErrorReport>prompt</ErrorReport>
    <WarningLevel>4</WarningLevel>
  </PropertyGroup>
  <ItemGroup>
    <Reference Include="System" />
    <Reference Include="System.Core" />
    <Reference Include="System.Xml.Linq" />
    <Reference Include="System.Data.DataSetExtensions" />
    <Reference Include="Microsoft.CSharp" />
    <Reference Include="System.Data" />
    <Reference Include="System.Net.Http" />
    <Reference Include="System.Xml" />
  </ItemGroup>
  <ItemGroup>
    <Compile Include="Program.cs" />
    <Compile Include="Properties\AssemblyInfo.cs" />
  </ItemGroup>
  <ItemGroup>
    <None Include="App.config" />
  </ItemGroup>
  <Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />
</Project>
```

프로젝트에 소스코드 파일을 추가하거나 프로젝트의 속성 창으로 통해 설정을 바꾸는 경우 변경사항은 모두 프로젝트 파일에 저장됩니다. 

프로젝트보다 큰 단위는 솔루션입니다. 솔루션 또한 위와같이 파일로 저장됩니다. 확장자는 .sln이고 메모장으로 열어보면 솔루션에 등록된 프로젝트의 경로가 포함된것을 확인할 수 있습니다. 

비주얼 스튜디오를 이용해 프로그램을 만든다면 맨 먼저 하는 작업이 솔루션을 만드는 것입니다. 그리고 그 아래에 제품을 구성하기 위한 프로젝트를 만듭니다. 따라서 비주얼 스튜디오로 만들어진 프로그램의 소스코드를 보관하려면 프로젝트 파일과 솔루션 파일까지 함께 저장해야 합니다. 마우스로 솔루션을 윈도위 탐색기에서 찾아서 더블클릭하면 비주얼 스튜디오가 실행되면서 솔루션 탐색기가 열리는것을 확인 할 수 있습니다. 여러개의 프로그램을 개발한다면 그만큼의 비주얼 스튜디오를 띄우고 개발을 진행해야 합니다. 

<hr />
<br />
<br />
<br />

## 1. 다중 소스코드 파일

.cs 실행파일을 여러개 만드는건 따로 문법이 필요하지 않습니다. 단지 코드파일을 원하는 만큼 만들고 C#컴파일러에게 추가된 파일을 함께 지정하면 됩니다.

```C#
using System;

class Program
{
    static void Main(string[] args) 
    {
        HelloWorld helloworld = new HelloWorld();
        helloworld.Write("start");
    }
}
```

```C#
using System;

class HelloWorld
{
    public void Write(string txt)
    {
        Console.WriteLine(txt);
    }
}
```


예를들어 위와 같이 program.cs 파일과 helloworld.cs파일이 있다고 하면, 두 파일을 컴파일 하기 위해 csc.exe의 인자로 두개의 파일명을 지정하면 됩니다. 

```
c:\temp> csc program.cs helloworld.cs
```

여러개의 파일명이 지정된 경우 csc컴파일러는 기본적으로 Main 메서드를 담고 있는 파일명을 실행 파일의 이름으로 선택합니다!!! 'ㅁ' !!! 실행 파일의 이름을 임의로 지정하고 싶다면 /out 옵션을 지정합니다. 

```
c:\temp> csc /out:test.exe program.cs helloworld.cs
```

비주얼 스튜디오를 사용한다면 솔루션 탐색기에서 프로젝트 항목 우클릭 -> 추가(add) -> 클래스(Class) 를 선택하면 됩니다. 위와같이 커맨드라인 칠 필요가 없습니다. 

실행 파일의 이름은 프로젝트 속성 창의 "응용 프로그램" 탭에서 "어셈블리 이름" 입력상자에 원하는 값으로 지정할 수 있습니다. 비주얼 스튜디오에서 빌드하는 경우 프로젝트에 포함된 C# 코드 파일을 자동으로 인식해 EXE/DLL 결과물에 포함 시킵니다. (csc.exe를 직접 실행할 때처럼 파일명을 불편하게 입력하지 않아도 됩니다).

다중소스코드파일을 구성하는데 관례가 있습니다. 보통 클래스 하나당 파일 하나를 만듭니다. 또한 유사한 기능으로 묶을 수 있는 파일은 폴더를 이용해 정리합니다. (개발자가 보기 편한 용도입니다.)

<hr />
<br />
<br />
<br />

## 2. 라이브러리
프로그래밍 언어에서 라이브러리는 일반적으로 재사용 가능한 단위를 의미합니다. 그리고 그것이 파일로 저장될 때는 확장자로 DLL이 붙습니다. 평소에 사용하는 Console.WriteLine의 Console타입은 mscorlib.dll파일에 포함된 것으로서, 알게모르게 사용하고 있었습니다.

닷넷 프레임워크가 설치되면 일부 라이브러리가 함께 컴퓨터에 설치되는데, 바로 이것들을 가리켜 BCL(Base Class Library) 또는 FCL(Framework Class Library) 라고 합니다. 이것들은 마이크로소프트가 미리 만들어둔 라이브러리들입니다. 

이런 라이브러리를 우리가 만들수도 있습니다 'ㅁ'!!!

<BR />

### 1). csc.exe로 라이브러리 생성 및 사용

위의 파일을 라이브러리(DLL파일)로 만들기 전에 HelloWorld클래스의 접근제한자를 public 으로 변경해야 합니다. 

```C#
using System;

public class HelloWorld
{
    public void Write(string txt)
    {
        Console.WriteLine(txt);
    }
}
```

class의 경우 접근제한자를 생략하면 기본값이 internal입니다. 'ㅁ'!?!??! internal은 같은 어셈블리(EXE 또는 DLL)내에서만 그 기능을 사용할 수 있게 제한하는 역할을 합니다. 따라서 HelloWorld.cs와 그 타입을 사용하는 Program.cs파일이 같은 EXE/DLL로 묶일 때는 문제가 안되지만 별도의 DLL에 담길 때는 internal 접근 제한자가 적용된다면 다른 EXE/DLL에 서 그 기능을 가져다 쓸수 없습니다. ㅠㅠ

따라서 라이브러리에서 특정 기능을 노출하고 싶다면 그것의 접근 제한자를 public으로 바꿔야 합니다. 바뀐 내용이 저장된 HelloWold.cs파일을 csc.exe에 "/target:library"컴파일러 옵션과 함께 빌드하면 라이브러리를 생성 할 수 있습니다. 

```
c:\temp> csc /target:library HelloWold.cs

/target 옵션을 생략하면 csc컴파일러는 기본값으로 /target:exe로 인식해서 컴파일 합니다. 콘솔 응용프로그램이 바로 exe유형에 해당합니다. 
```

위 명령어를 실행하면 HelloWorld.dll파일이 만들어집니다. DLL파일은 그 단위로 재사용하는것이 가능합니다. 

라이브러리를 만들었으니 그걸 사용해봅시다. Program.cs에서 HelloWorld.dll기능을 사용할 때 소스코드상으로 바뀌는것은 없습니다. 대신 이전에는 HelloWorld.cs파일을 csc.exe에 직접 전달했던 반면 이번에는 HelloWorld.dll 파일을 별도로 /r 옵션을 이용해 다음과 같이 컴파일 해야 합니다. 

```
c:\temp> csc Program.cs /r:HelloWorld.dll
```

이렇게 빌드하면 Program.exe가 만들어지고 이 프로그램을 실행하려면 반드시 HelloWorld.dll파일이 있어야 합니다. 따라서 다른 사람의 컴퓨터에서 이 프로그램을 실행하려면 Program.exe 와 HelloWorld.dll 파일 2개를 전달해줘야합니다.

DLL을 다른프로그램에서 사용하는 것을 일반적으로 "참조(reference)한다"라고 표현합니다. 따라서 위의 경우에는 Program.exe파일이 HelloWorld.dll을 참조하는겁니다. 근데 Console타입이 정의된 mscorlib.dll에 대해서는 참조하지 않았는데 그동안 실습한 예제 파일은 어떻게 컴파일 된걸까요? csc컴파일러는 BCL에서 주로 사용되는 몇가지 어셈블리를 /r 옵션으로 명시하지 않아도 사용할 수 있게 해줍니다. 하지만 우리가 만든 HelloWorld.dll파일은 csc컴파일러가 알 수 없으니 직접 명시해주어야 합니다. 

```
C# 컴파일러에 의해 자동 참조되는 어셈블리 목록은 csc.exe파일과 같은 폴더에 위치한 csc.rsp파일 내에 명시되어 있습니다. 

예를들어 C:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.rsp파일을 메모장으로 열어보면 그 목록을 알 수 있습니다. 바꿔 말하면 항상 /r 옵션으로 지정하는 어셈블리가 있다면 csc.rsp파일에 포함해 두면 자동으로 참조되는 효과를 볼 수 있습니다.`!~! 'ㅁ'아래는 메모장으로 열었을때 입니다. 

# This file contains command-line options that the C#
# command line compiler (CSC) will process as part
# of every compilation, unless the "/noconfig" option
# is specified. 

# Reference the common Framework libraries
/r:Accessibility.dll
/r:Microsoft.CSharp.dll
/r:System.Configuration.dll
/r:System.Configuration.Install.dll
/r:System.Core.dll
/r:System.Data.dll
/r:System.Data.DataSetExtensions.dll
/r:System.Data.Linq.dll
/r:System.Data.OracleClient.dll
/r:System.Deployment.dll
/r:System.Design.dll
/r:System.DirectoryServices.dll
/r:System.dll
/r:System.Drawing.Design.dll
/r:System.Drawing.dll
/r:System.EnterpriseServices.dll
/r:System.Management.dll
/r:System.Messaging.dll
/r:System.Runtime.Remoting.dll
/r:System.Runtime.Serialization.dll
/r:System.Runtime.Serialization.Formatters.Soap.dll
/r:System.Security.dll
/r:System.ServiceModel.dll
/r:System.ServiceModel.Web.dll
/r:System.ServiceProcess.dll
/r:System.Transactions.dll
/r:System.Web.dll
/r:System.Web.Extensions.Design.dll
/r:System.Web.Extensions.dll
/r:System.Web.Mobile.dll
/r:System.Web.RegularExpressions.dll
/r:System.Web.Services.dll
/r:System.Windows.Forms.Dll
/r:System.Workflow.Activities.dll
/r:System.Workflow.ComponentModel.dll
/r:System.Workflow.Runtime.dll
/r:System.Xml.dll
/r:System.Xml.Linq.dll
```

마지막으로 의문점이 있습니다. 왜 소스코드 파일을 직접 포함하지 않고 궅이 DLL을 만들어서 참조하는 것일까? 이는 프로그램의 규모가 제법 커져야만 그 필요성을 체감할 수 있습니다. 프로그램 하나를 만들기 위해서는 수백 개의 소스코드 파일이 생성되고 그중에서 수십개의 파일이 다른 프로그램에서도 재사용 할 수 있다는 상황을 가정해봅니다. 다른 프로그램을 만들 때 기존의 프로젝트로부터 수십개의 파일을 골라서 재사용하는것은 여간 불편한 일이 아닐 수 없습니다. 차라리 그 수십개의 소스코드 파일을 담은 1개의 DLL파일을 재사용 하는것이 낫습니다. 그 밖에 컴파일 시간도 문제가 됩니다. 매번 수십개의 파일을 함께 컴파일 하기보다 이미 컴파일된 DLL파일을 참조하는 것이 개발 생산성 측면에서도 좋습니다. 

<BR />

### 2). 비주얼 스튜디오에서 라이브러리 생성 및 사용

|프로젝트 템플릿 이름|유형|의미|
|--|--|--|
|Windows Forms앱 \n (Windows Forms App)|EXE|윈도우 폼 응용프로그램을 만들기 위한 기본 설정이 포함된 프로젝트를 생성합니다.|
|WPF 앱 \n (WPF App)|EXE|Windows Presentation Foundation 응용프로그램을 위한 템플릿입니다.|
|콘솔 앱 (Console App)|EXE|실행시 "명령프롬프트"에서 실행되는 응용프로그램을 만들도록 옵션이 설정되어 있습니다.|
|클래스 라이브러리 \n (Class Library)|DLL|라이브러리(DLL) 유형의 프로젝트 템플릿|

```
참고)
같은 프로젝트 템플릿 이름인데 각각 ".NET Core", ".NET Framework"로 나뉘는 경우가 있습니다.
.NET Core유형을 선택하면 리눅스 및 윈도우에서도 실행 가능한 바이너리가 생성되는 반면, .NET Framework 유형을 선택하면 윈도우 운영체제에서만 실행 가능한 바이너리가 생성됩니다.
```

각 템플릿은 해당 유형의 응용프로그램을 만드는데 필요한 기본 설정을 담고 있습니다. 하지만 결국 프로젝트를 생성하고 나면 동일한 포멧의 csproj파일로 생성되고 그 안의 옵션값이 일부 다른것에 불과합니다~!!! 'ㅁ'?!?

```
참고)
나중에 좀더 익숙해지면 "Console App"으로 만드러진 프로젝트일지라도 일부 옵션을 변경해 "Class Library"로 바꿀 수 있습니다.??!
```

비주얼 스튜디오를 이용해 라이브러리를 만들려면 새 프로젝트 대화상자에서 "클래스 라이브러리(Class Library)"유형의 프로젝트 템플릿을 선택하면 된다. 기본적으로 생성되는 Class1.cs파일을 삭제하고 HelloWorld.cs라는 이름의 코드파일을 새로 추가한 후 위의 코드예제를 입력합니다. 마지막으로 "빌드(build)" -> "솔루션 빌드(Build Solution)"메뉴를 선택해서 빌드하면 DLL파일이 생성됩니다.

이제 비주얼 스튜디오에서 DLL을 참조하는 방법을 살펴봅니다. 이를 위해 HelloWorld 라이브러리를 사용하는 콘솔 앱을 솔루션에 추가합니다. "파일(File)" -> "추가(Add)" -> "새 프로젝트(New Project)" 메뉴를 선택하고 "콘솔 앱(Console App)" 유형의 프로젝트를 선택하면 션재의 솔루션에 새로운 프로젝트가 추가됩니다.

```
비주얼 스튜디오 내에서는 "디버그(Debug)" -> "디버깅 시작(Start Debugging)"메뉴 또는 "디버그하지 않고 시작(Start Without Debugging)"메뉴를 선택하면 솔루션의 대표 응용프로그램을 곧바로 실행할 수 있습니다. 이처럼 개발 환경에서 직접 실행할 수 있는 프로젝트를 "시작 프로젝트(Start Up Project)"라고 하며, 솔루션에서 단 하나만 지정할 수 있습니다. 이를 구분하는 방법은 솔루션 탑색기에서 프로젝트 이름이 굵은 글씨로 되어있는것이 시작프로젝트임을 알 수 있습니다. 

그런데 현재 ClassLibrary1 프로젝트는 DLL을 생성하는 라이브러리 유형의 프로젝트이기 때문에 실행 할 수없습니다. 따라서 실행 가능한 파일인 exe를 생성하는 ConsoleApp1(가칭) 프로젝트를 대상으로 마우스 오른쪽버튼을 눌러 "시작프로젝트로 설정" 메뉴를 선택해야 합니다.
```

비주얼 스튜디오에서는 참조 방법이 크게 파일 참조와 프로젝트 참조로 나뉩니다. 참조하려는 프로젝트와 참조되는 프로젝트가 같은 솔루션 내에 함께 있다면 "프로젝트 참조"를 사용할 수 있습니다. 프로젝트의 하위에 있는 "참조(references)"항목을 대상으로 마우스 오른쪽 클릭을 눌러 "참조 추가(Add Reference)..."메뉴를 선택하면 참조관리자(Reference Manager) 대화상자가 나타납니다. 좌측의 "솔루션(Solution)" 범주를 선택하면 현재 솔루션에 포함된 프로젝트 목록이 우측에 나오고 원하는 프로젝트의 체크박스를 설정하고 확인버튼을 누릅니다. 이것이 프로젝트 참조입니다.

반면 파일 참조는 우즉 하단의 "찾아보기(Browse)..." 버튼을 눌러 DLL파일을 직접 지정하는 것을 의미합니다. 일반적으로 외부에서 구한 DLL파일을 사용하려는 경우 파일 참조 방식을 이용합니다. 현재 어떤 DLL을 참조하고 있는지 확인하고 싶다면 솔루션 탐색기의 프로젝트 항목에서 "참조(Reference)"를 펼치면 됩니다. 가장 기본이되는 mscorlib.dll은 목록에 나타나지 않습니다. (모든 프로젝트에는 mscorlib에 대한 암시적 참조가 포함되어있습니다. System.Core가 참조 목록에서 제거되더라도 모든 프로젝트에 System.Core 에 대한 암시적 참조가 포함됩니다.)

## 3. 응용프로그램 구성 파일 : app.config

닷넷 응용 프로그램을 실행하면 맨 처음 CLR환경이 초기화된 후 개발자가 작성한 코드가 실행되는 순서로 진행됩니다. 그런데 가끔은 CLR초기화 과정에 어떤값을 전달해야 할 때가 있습니다. 아쉽게도 이를 위해 C#코드를 작성 할 수는 없습니다. 왜냐하면 C#코드는 CLR이 초기화된 후에야 실행되기 때문입니다. 