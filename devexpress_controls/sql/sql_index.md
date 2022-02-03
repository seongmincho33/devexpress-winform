# 1. SQL

sql 관련 팁? 자주쓰는 표현들 목록

1. MSSQL에서는 BIT가 기본적으로 NULL과 0이 다르다.








<hr />
<br />
<br />
<br />
## 1. MSSQL에서는 BIT가 기본적으로 NULL과 0이 다르다.

ISNULL([IS_PLATE],CAST(0 AS BIT)) AS [IS_ASSET]
