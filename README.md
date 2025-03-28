## To Run
Be in root
dotnet run --project TaskApi

## To add / update / delete task in postman
Set Http method
Include properties in body of request
Example:
{
  "title": "Task Title Example",
  "dueDate": "2025-05-01T00:00:00Z",
  "isCompleted": false
}
url is http://localhost:5000/api/tasks/
include id if needed

## To run unit tests
be in Tests/TaskApi.Tests
run dotnet test

## To view data from Tasks table in database
open explore.sql
click green run button
choose connection profile