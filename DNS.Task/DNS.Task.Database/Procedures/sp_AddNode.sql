CREATE PROCEDURE [dbo].[sp_AddNode]
	@ParentId	INT = NULL,
	@Title		NVARCHAR(50) = NULL,
	@NodeType	INT = NULL
AS
	INSERT INTO [dbo].Nodes ([ParentId], [Title], [NodeType])
	OUTPUT inserted.Id
	VALUES(@ParentId, @Title, @NodeType)
RETURN 0
