﻿CREATE TABLE [dbo].[Nodes]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [ParentId] INT NULL, 
    [Title] NVARCHAR(50) NULL, 
    [NodeType] INT NULL
)
GO
ALTER TABLE [dbo].[Nodes]
ADD CONSTRAINT FK_Nodes_ParentNode FOREIGN KEY (ParentId) REFERENCES Nodes (Id) ON DELETE CASCADE
GO