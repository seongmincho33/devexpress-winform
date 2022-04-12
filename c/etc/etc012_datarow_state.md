# DataRow의 상태

- [DataRow의 상태](#datarow의-상태)
  - [1. DataRow.RowState](#1-datarowrowstate)
  - [2. DataRow.AcceptChanges()](#2-datarowacceptchanges)

## 1. DataRow.RowState

DataRow.RowState는 DataRowState 열거형 형식입니다. DataRowState는 5가지 상태가 있는데 DataRow는 이중 하나의 상태를 가지게 됩니다.

|필드|number|설명|
|--|--|--|
|Added|4|행이 DataRowCollection에 추가되었고 AcceptChanges()는 호출 전일때 입니다.|
|Deleted|8|Delete()메서드를 사용해서 DataRow행을 삭제한 상태입니다.|
|Detached|1|행이 만들어졌지만 DataRowCollection에 속하지 않을때 입니다. DataRow가 만들어진 직후에 컬렉션에 추가되기 전 또는 컬랙션에 제거된 경우에 이상태가 됩니다.|
|Modified|16|행이 수정되었고 AcceptChanges()는 호출 전일때 입니다.|
|Unchanged|2|AcceptChanges()가 호출된 후에 아무것도 바뀌지 않았을때 상태입니다.|

아래는 예제입니다.

```C#
private void DemonstrateRowState() {
   //Run a function to create a DataTable with one column.
   DataTable myTable = MakeTable();
   DataRow myRow;

   // Create a new DataRow.
   myRow = myTable.NewRow();
   // Detached row.
   Console.WriteLine("New Row " + myRow.RowState);

   myTable.Rows.Add(myRow);
   // New row.
   Console.WriteLine("AddRow " + myRow.RowState);

   myTable.AcceptChanges();
   // Unchanged row.
   Console.WriteLine("AcceptChanges " + myRow.RowState);

   myRow["FirstName"] = "Scott";
   // Modified row.
   Console.WriteLine("Modified " + myRow.RowState);

   myRow.Delete();
   // Deleted row.
   Console.WriteLine("Deleted " + myRow.RowState);
}

private DataTable MakeTable(){
   // Make a simple table with one column.
   DataTable dt = new DataTable("myTable");
   DataColumn dcFirstName = new DataColumn("FirstName", Type.GetType("System.String"));
   dt.Columns.Add(dcFirstName);
   return dt;
}
```

## 2. DataRow.AcceptChanges()