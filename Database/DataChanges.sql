INSERT INTO Location (Name)
SELECT DISTINCT Location FROM Game

UPDATE Game
SET Game.LocationId = Location.Id
FROM Location
WHERE Location.Name = Game.Location

UPDATE location SET BunchId = 1
UPDATE location SET BunchId = 7 WHERE Id = 1