# 1. 시작

<br />

## 처음 웹폼 생성해보기

<br />

시작은 프로젝트 를 생성합니다. 솔루션은 하나만 존재하고 프로젝트는 솔루션 하위에 여러개가 있을수 있습니다. 네임스페이스는 곧 패키지 입니다. 솔루션은 왜 만드냐면 프로젝트는 솔루션 아래 있어야해서 그렇습니다. 

<img src="../img/asp_img/asp_start/asp_start001.png" width="500" heigth="500" />

<br />

생성할때 ASP.NET 웹 애플리케이션(.NET Framework)를 선택해주세요

<img src="../img/asp_img/asp_start/asp_start002.png" width="500" heigth="500" />

<br />

선택하셨다면 다음에 뜨는창은 2021 기준으로 설명하자면 비어있음을 선택하시고 웹폼을 선택후 만들기를 눌러주세요.

<img src="../img/asp_img/asp_start/asp_start003.png" width="500" heigth="500" />

<br />
 
그러면 아래와 같은 솔루션과 프로젝트가 생성되는것을 확인할 수 있습니다. 

<img src="../img/asp_img/asp_start/asp_start004.png" width="500" heigth="500" />

<br />

마우스 왼쪽 클릭으로 새로운 항목을 클릭합니다

<img src="../img/asp_img/asp_start/asp_start005.png" width="500" heigth="500" />

<br />

웹폼을 만들어 줍니다. 확장자는 .aspx입니다. 그림과 같이 하세요. 이름은 원하는대로 씁니다.

<img src="../img/asp_img/asp_start/asp_start006.png" width="500" heigth="500" />

<br />

만들어지면 웹폼을 오른쪽클릭하고 디자이너 보기를 누릅니다. 그러면 디자인뷰 화면이 나옵니다.

<img src="../img/asp_img/asp_start/asp_start007.png" width="500" heigth="500" />

<br />

보기 -> 도구상자 에서 원하는 컨트롤을 드래그 앤 드랍으로 가져다놓을 수 있습니다. 오른쪽클릭-> 속성 에 들어가면 여러가지 attribute(속성) 들을 확인할 수 있고 수정도 가능합니다. 이게 디자인뷰가 윈폼이아니라 웹폼이어서 HTML이고 이 HTML에 드래그 엔 드랍으로 편리하게 디자인한다고 볼 수 있습니다.

<img src="../img/asp_img/asp_start/asp_start008.png" width="500" heigth="500" />

<br />

HelloWorld.aspx 파일을 더블클릭하면 HTML에 웹폼 컨트롤이 사이에 껴있는것을 확인할 수 있습니다..'ㅁ'!!! 여기서 속성을 관리해도 됩니다. 사진의  runat="server" 속성이 있는데 이는 서버 컨트롤로 바꿔주는 기능을 합니다. 

그러면 서버 컨트롤은 무었인가 하면, HTML에 모양이 있고 동작가능한 컨트롤을 말합니다. 근데 이게 C#코드랑 같이 동작하게 된다면 ASP.NET컨트롤이라고 할 수 있습니다. ASP.NET은 여러가지 컨트롤을 제공합니다.

- HTML서버컨트롤
- Web 서버컨트롤(표준)
- Validation 컨트롤(유효성검사)
- User컨트롤(사용자정의)

그런데 서버컨트롤을 보면 HTML에 코드가 엄청 늘어나는것을 볼 수 있습니다. HTML크기가 크면 데이터 전송 량도 많아지는것이기떄문에 이는 별로 좋지 않다고 볼 수 있습니다. 

<img src="../img/asp_img/asp_start/asp_start009.png" width="500" heigth="500" />

<br />

숨김파일에 HelloWorld.aspx.cs를 확인하면 콜백함수들이 있는것을 확인할 수 있습니다. 직접 콜백함수를 작성하거나 다자이너 뷰에서 컨트롤을 더블클릭하면 자동생성됩니다. 리스너 등록은 위에 HTML파일에서 하는겁니다. OnClick 속성에 콜백함수 버튼이름을 스트링으로 등록하면 됩니다. 

상속클래스는 System.Web.UI.Page클래스입니다. 모든 ASP.NET의 각 웹페이지는 Page클래스에서 모든기능을 받아옵니다. 여기서 Page_Load 이벤트가 Main메서드같은 역할을 합니다. 여기가 페이지의 실행 시작점입니다. 

<img src="../img/asp_img/asp_start/asp_start010.png" width="500" heigth="500" />

<br />

빌드하고 실행하면 아래와같이 로컬호스트를 통해서 브라우저가 열리고 컨트롤이 보이는것을 확인할 수 있습니다. 처음 웹폼 만들기를 해봤습니다.

<img src="../img/asp_img/asp_start/asp_start011.png" width="200" heigth="200" />
<img src="../img/asp_img/asp_start/asp_start012.png" width="200" heigth="200" />

<br />

## 관련 확장자

- aspx : 웹폼 입니다.
- ascx : aspx에 포함된 웹폽 사용자정의  컨트롤입니다. 혼자서는 안돕니다.
- cshtml : ASP>NET MVC에서 뷰 페이지를 C#문법으로 작성합니다.
- config : 환경설정하는 파일입니다. web.config, bundle.config등이 있습니다.
- master : 웹사이트 공통 레이아웃을 담고있는 마스터 페이지 확장자.

<hr />
<br />
<br />
<br />
