# DataSet

- [DataSet](#dataset)
  - [1. DataSet](#1-dataset)
  - [2. DataTable](#2-datatable)
    - [데이터 테이블안의 중복 데이터 제거방법](#데이터-테이블안의-중복-데이터-제거방법)
  - [3. DataView](#3-dataview)
  - [4. DataRow](#4-datarow)
   
<hr />
<br />

## 1. DataSet

DataSet은 클라이언트단에서 메모리에 테이블들을 저장하고 있습니다. 서버와 연결이 안되어있어 바인딩 작업이 따로 필요합니다. 만약 서버와의 데이터 바인딩을 원하면 보통 DataAdapter(ex: SqlDataAdapter)를 사용하여 메모리상의 DataSetd인스턴스에 할당후 사용합니다. 보통 DataSet은 DataGridView와 같은 그리드 형태의 컨트롤들과 바인딩하여 사용합니다. 

```C#
SqlConnection conn = new SqlConnection(strConn);
conn.Open();
SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Tab1", conn);

// DataSet에 테이블 데이타를 넣음
DataSet ds = new DataSet();
adapter.Fill(ds, "Tab1");        

conn.Close();
```

<br />

## 2. DataTable

DataTable 클래스는 메모리에 테이블을 표현하는 클래스입니다. 보통 DataSet.Tables 에 포함됨니다. DataSet로부터 접근하기 위해서는 dataSet.Tables[0]과 같이 인덱스로도 접근 가능합니다. 혹은 테이블 명을 이용해서 접근도 가능합니다.

```C#
DataSet ds = new DataSet();
adapter.Fill(ds, "Tab1");
DataTable dt = ds.Tables["Tab1"];
```

### 데이터 테이블안의 중복 데이터 제거방법

```C#
DataTable RemoveDuplicateRows(DataTable dTable, string colName)
{
    Hashtable hTable = new Hashtable();
    ArrayList duplicateList = new ArrayList();

    //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
    //And add duplicate item value in arraylist.
    foreach (DataRow drow in dTable.Rows)
    {
        if (hTable.Contains(drow[colName]))
            duplicateList.Add(drow);
        else
            hTable.Add(drow[colName], string.Empty);
    }

    //Removing a list of duplicate items from datatable.
    foreach (DataRow dRow in duplicateList)
        dTable.Rows.Remove(dRow);

    //Datatable which contains unique records will be return as output.
    return dTable;
}
```

<br />

## 3. DataView

DataView 클래스는 DataTable객체를 소트하거나 필터링 혹은 편집, 검색등을 할때 사용됩니다. 만약 서버로부터 데이터를 가져다 클라이언트에 놓고 일부 데이터만 보여주는 필터링 혹은 소팅을 해서 보여주는 일들은 DataView를 사용합니다. DataTable은 기본적으로 DefaultView라는 프로퍼티를 가지고있는데 이는 DataTable에서 기본으로 제공하는 DataView로서 이것을 사용하여 소트나 필터링을 할 수 있습니다. 

```C#
DataSet ds = new DataSet();

using (SqlConnection conn = new SqlConnection(cn))
{
   conn.Open();
   SqlDataAdapter adpt = new SqlDataAdapter("SELECT * FROM AAA", conn);
   adpt.Fill(ds, "AAA");
}

// DataTable.DefaultView를 사용하여
// 필터링 (name컬럼이 L로 시직하는 경우)
DataTable dt = ds.Tables["AAA"];
dt.DefaultView.RowFilter = "name like 'L%'";

dataGridView1.DataSource = dt;
```

## 4. DataRow

DataTable 은 DataRow의 이차원 배열 형태를 저장하고 있습니다. 첫번째 차원은 컬럼을, 두번째 차원은 행을 나타냅니다. 컬럼은 인덱스 혹은 컬럼명으로 접근 가능하고, 행은 인덱스로 접근 가능합니다. 

```C#
DataTable dt = new DataTable();
DataRow row = null;

//컬럼 먼저 생성
dt.Columns.Add(new DataColumn("Name", typeof(string)));
dt.Columns.Add(new DataColumn("Age", typeof(int)));

//데이터 로우 생성
row = dt.NewRow();

row["NAME"] = "철수";
row["AGE"] = 20;

//테이블에 추가
dt.Rows.Add(row);

//데이터 로우 생성2
row = dt.NewRow();

row["NAME"] = "영희";
row["AGE"] = "20";

//테이블에 추가2
dt.Rows.Add(row);

//결과 출력
foreach(DataRow dataRow in dt.Rows)
{
    foreach(var item in dataRow.ItemArray)
    {
        Console.WriteLine(item);
    }
}
```

위의 코드를 좀더 다듬어서 만들어보겠습니다. 

```C#
class Program
{
    static void Main(string[] args)
    {
        //임의의 데이터테이블을 받았다고 합시다.
        DataTable dataTable = new DataTable();            

        //데이터테이블헬퍼 인스턴스를 하나 만들어 줍니다.
        DataTableHelper datatablehelper = DataTableHelperFactory.CreateDataTableHelper(dataTable);

        //데이터 테이블에 컬럼을 추가합니다.            
         datatablehelper.SetColunms((typeof(string), "NAME"), (typeof(int), "age"), (typeof(string), "tel"));

        //데이터 테이블에 행을 추가합니다. 
        datatablehelper.SetDataRow("SMJO", 30, "010-0010-3320");

        //데이터 테이블을 출력합니다.
        datatablehelper.PrintDataTable((string colName, string value) => { Console.WriteLine(colName + " : " + value); });        

        Console.ReadKey();
    }  
}
```

```C#
class DataTableHelperFactory
{
    public static DataTableHelper CreateDataTableHelper(DataTable dataTable)
    {
        DataTableHelper dataTableHelper = new DataTableHelper();
        dataTableHelper.MainDataTable = dataTable;
        return dataTableHelper;
    }
}
```

```C#
public class DataTableHelper : IDisposable
{
    private bool _disposed = false;
    public DataTable MainDataTable { get; set; }

    public void AddColunms(string columnName, Type columnType)
    {
        this.MainDataTable.Columns.Add(new DataColumn(columnName, columnType));
    }

    /// <summary>
    /// 컬럼을 셋팅합니다.
    /// </summary>
    /// <param name="columnNames">컬럼의 형식과 이름을 튜플로 받습니다.</param>
    public void SetColunms(params (Type, string)[] columnNames)
    {            
        foreach(var columnName in columnNames)
        {
            this.MainDataTable.Columns.Add(new DataColumn(columnName.Item2, columnName.Item1));
        }
    }

    public void SetData(int rowIndex, string columnName, object value)
    {
        if(this.MainDataTable.Rows[rowIndex] != null)
            this.MainDataTable.Rows[rowIndex][columnName] = value;           
    }

    public void SetDataRow(params object[] data)
    {
        DataRow dataRow = null;         
        dataRow = this.MainDataTable.NewRow();
        try
        {
            for (int i = 0; i < this.MainDataTable.Columns.Count; i++)
            {
                dataRow[i] = data[i];
            }
        }
        catch(Exception ex)
        {
            //인덱스 에러
        }            
        this.MainDataTable.Rows.Add(dataRow);
    }

    public void PrintDataTable(Action<string, string> func)
    {
        foreach (DataRow dataRow in this.MainDataTable.Rows)
        {
            foreach (DataColumn column in this.MainDataTable.Columns)
            {
                func(column.ColumnName, dataRow[column].ToString());
            }
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        _disposed = true;
    }
}
```