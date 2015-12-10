CREATE PROCEDURE [dbo].[sp_GetNodeByIdWithChilds]
	@Id INT = NULL
AS
	WITH NodeCTE AS
	(
		SELECT [Id], [ParentId], [Title], [NodeType] FROM [dbo].Nodes
		WHERE Id = @Id
		UNION ALL 
		SELECT Child.Id, Child.ParentId, Child.Title, Child.NodeType FROM NodeCTE Parent
		INNER JOIN [dbo].Nodes Child ON Parent.Id = Child.ParentId		
	)
	SELECT * FROM NodeCTE v
	ORDER BY v.ParentId, v.NodeType, v.Title
RETURN 0
