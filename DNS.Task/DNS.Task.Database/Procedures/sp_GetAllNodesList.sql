CREATE PROCEDURE [dbo].[sp_GetAllNodesList]
AS
	SELECT * FROM [dbo].Nodes
	ORDER BY ParentId, NodeType, Title
RETURN 0
