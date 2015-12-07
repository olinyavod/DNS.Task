CREATE PROCEDURE [dbo].[sp_UpdateNode]
	@Id			INT,
	@ParentId	INT = NULL,
	@Title		NVARCHAR(50) = NULL,
	@NodeType	INT = NULL
AS
	UPDATE [dbo].[Nodes] 
	SET [ParentId] = @ParentId,
		[Title]= @Title,
		[NodeType] = @NodeType
	WHERE [Id] = @Id
RETURN 0
