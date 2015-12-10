CREATE PROCEDURE [dbo].[sp_GetNodesList]
	@ParentId INT = NULL
AS
	SELECT * FROM [dbo].[Nodes]
	WHERE (@ParentId IS NULL AND ParentId IS NULL) OR ([ParentId] = @ParentId)
	ORDER BY NodeType, Title
RETURN 0
