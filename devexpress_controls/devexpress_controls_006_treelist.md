# TreeList

<hr />
<br />

## 1. DataBinding

TreeList 컨트롤은 계층적인 데이터 구조를 가지고 있습니다. 부모와 자식노드 관계를 가지고 있습니다. 여기서는 어떻게 부모-자식관계를 데이터소스 레벨에서 정의하는지 알아봅니다.

### 1). 부모-자식 관계

TreeList에 data source bound할때는 두개의 필드를 요구합니다.

1. Key Field
2. Parent Field

Key Field는 고유한 값을가져서 데이터소스의 레코드를 인식하는데 사용됩니다. Key Field의 필드이름을 TreeList.KeyFieldName 프로퍼티에 할당해야 합니다.

Parent Field 필드는 해당 부모노드의 키 필드를 나타냅니다. 이 필드 이름을 TreeList.ParentFieldNmae프로퍼티에 할당해야 합니다. 