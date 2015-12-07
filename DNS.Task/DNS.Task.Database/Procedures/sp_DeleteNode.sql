CREATE PROCEDURE [dbo].[sp_DeleteNode]
	@Id INT
AS
	WITH NodeCTE AS
	(
		SELECT [Id], [ParentId], [Title], [NodeType] FROM [dbo].Nodes
		WHERE Id = @Id
		UNION ALL 
		SELECT Child.Id, Child.ParentId, Child.Title, Child.NodeType FROM NodeCTE Parent
		INNER JOIN [dbo].Nodes Child ON Parent.Id = Child.ParentId
		ORDER BY Child.NodeType, Child.Title		
	)
	
	DELETE FROM [dbo].Nodes
	WHERE Id IN (SELECT Id FROM NodeCTE)
RETURN 0
