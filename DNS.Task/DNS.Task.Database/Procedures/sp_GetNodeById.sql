CREATE PROCEDURE [dbo].[sp_GetNodeById]
	@Id INT
AS
	SELECT * FROM [dbo].Nodes WHERE Id = @Id
RETURN 0
