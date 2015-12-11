DELETE FROM Nodes
DECLARE @RootIds TABLE (Id INT, NodeType INT);

INSERT Nodes ([Title], [NodeType]) 
VALUES	('Root 1', 0),
		('Root 2', 0), 
		('Root 3', 0), 
		('Root 4', 0),
		('Root 5', 1),
		('Root 6', 1),
		('Root 7', 1),
		('Root 8', 1),
		('Root 9', 1),
		('Root 10', 0);
GO

DECLARE @ParentId INT = (SELECT Id FROM [Nodes] WHERE Title = 'Root 1' AND ParentId IS NULL);

INSERT INTO Nodes ([Title], [ParentId], [NodeType]) 
VALUES ('Child 1-1', @ParentId, 0),
       ('Child 1-2', @ParentId, 0),
	   ('Child 1-3', @ParentId, 0),
	   ('Child 1-4', @ParentId, 0),
	   ('Child 1-5', @ParentId, 0),
	   ('Child 1-6', @ParentId, 1), 
	   ('Child 1-7', @ParentId, 1),
	   ('Child 1-7', @ParentId, 1), 
	   ('Child 1-8', @ParentId, 1),
	   ('Child 1-9', @ParentId, 1),
	   ('Child 1-10', @ParentId, 1);

SET @ParentId = (SELECT Id FROM [Nodes] WHERE Title = 'Root 2' AND ParentId IS NULL);

INSERT INTO Nodes ([Title], [ParentId], [NodeType]) 
VALUES ('Child 2-1', @ParentId, 0),
       ('Child 2-2', @ParentId, 0),
	   ('Child 2-3', @ParentId, 0),
	   ('Child 2-4', @ParentId, 0),
	   ('Child 2-5', @ParentId, 0),
	   ('Child 2-6', @ParentId, 1), 
	   ('Child 2-7', @ParentId, 1),
	   ('Child 2-7', @ParentId, 1), 
	   ('Child 2-8', @ParentId, 1),
	   ('Child 2-9', @ParentId, 1),
	   ('Child 2-10', @ParentId, 1);

SET @ParentId = (SELECT Id FROM [Nodes] WHERE Title = 'Root 8' AND ParentId IS NULL);

INSERT INTO Nodes ([Title], [ParentId], [NodeType]) 
VALUES ('Child 8-1', @ParentId, 0),
       ('Child 8-2', @ParentId, 0),
	   ('Child 8-3', @ParentId, 0),
	   ('Child 8-4', @ParentId, 0),
	   ('Child 8-5', @ParentId, 0),
	   ('Child 8-6', @ParentId, 1), 
	   ('Child 8-7', @ParentId, 1),
	   ('Child 8-7', @ParentId, 1), 
	   ('Child 8-8', @ParentId, 1),
	   ('Child 8-9', @ParentId, 1),
	   ('Child 8-10', @ParentId, 1);

SET @ParentId = (SELECT Id FROM [Nodes] WHERE Title = 'Child 1-1');

INSERT INTO Nodes ([Title], [ParentId], [NodeType]) 
VALUES ('Child 1-1-1', @ParentId, 0),
       ('Child 1-1-2', @ParentId, 0),
	   ('Child 1-1-3', @ParentId, 0),
	   ('Child 1-1-4', @ParentId, 0),
	   ('Child 1-1-5', @ParentId, 0),
	   ('Child 1-1-6', @ParentId, 1), 
	   ('Child 1-1-7', @ParentId, 1),
	   ('Child 1-1-7', @ParentId, 1), 
	   ('Child 1-1-8', @ParentId, 1),
	   ('Child 1-1-9', @ParentId, 1),
	   ('Child 1-1-10', @ParentId, 1);