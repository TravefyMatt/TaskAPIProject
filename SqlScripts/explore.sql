USE TaskDb;
GO

SELECT * FROM Tasks;

-- View only completed tasks
/*
SELECT * FROM Tasks
WHERE IsCompleted = 1;
*/

-- View tasks that are due today
/*
SELECT * FROM Tasks
WHERE CAST(DueDate AS DATE) = CAST(GETDATE() AS DATE);
*/

-- Count of completed vs incomplete
/*
SELECT IsCompleted, COUNT(*) AS Count
FROM Tasks
GROUP BY IsCompleted;
*/