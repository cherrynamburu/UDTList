Use UserDefinedTypes
GO

DROP TYPE UDTList
GO

DROP ASSEMBLY UDTList
GO

CREATE ASSEMBLY UDTList  
FROM 'C:\Users\Cherry\Desktop\UDT\UDTList\UDTList\bin\Debug\UDTList.dll'   
WITH PERMISSION_SET = EXTERNAL_ACCESS; 

CREATE TYPE UDTList   
EXTERNAL NAME UDTList.[UDTList];


DECLARE @var UDTList = UDTList::Parse('cherry,tester,200,2#ramesh,hr,100,4')

SET @var = @var.AddEmployee('saikrishna,dev,300,1')
SELECT @var.ToString();

DECLARE @xml XML;
SET @xml = @var.ToXML();
SELECT @xml;

