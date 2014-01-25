CREATE TABLE dbo.[State]
(
	[StateID] BIGINT NOT NULL PRIMARY KEY identity, 
    [Code] NVARCHAR(2) NOT NULL unique, 
    [Description] NVARCHAR(50) NOT NULL
)
