using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskApi.Models;
using TaskApi.Services;
using TaskApi.Data;
using Moq;
using Xunit;

namespace TaskApi.Tests
{
    public class TaskServiceTests
    {
        private readonly TaskService _taskService;
        private readonly TaskDbContext _context;
        public TaskServiceTests()
        {
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Use a unique DB for each test
                .Options;

            _context = new TaskDbContext(options);
            _taskService = new TaskService(_context);
        }

        [Fact]
        public async Task CreateTask_ShouldAddTask()
        {
            var task = new TaskItem { Title = "Test Task" };
            var createdTask = await _taskService.CreateTaskAsync(task);

            Assert.NotNull(createdTask);
            Assert.Equal("Test Task", createdTask.Title);
            Assert.Equal(1, createdTask.Id);
        }

        [Fact]
        public async Task CreateTask_ShouldIncrementTaskIDByOne()
        {
            var task = new TaskItem { Title = "TaskWithId1" };
            var createdTask1 = await _taskService.CreateTaskAsync(task);
            var task2 = new TaskItem { Title = "TaskWithId2" };
            var createdTask2 = await _taskService.CreateTaskAsync(task2);

            Assert.Equal(1, createdTask1.Id);
            Assert.Equal(2, createdTask2.Id);
        }

        [Fact]
        public async Task CreateTask_WithEmptyTitle_ShouldThrowException()
        {
            var task = new TaskItem { Title = "" };
            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CreateTaskAsync(task));
        }

        [Fact]
        public async Task GetTaskById_ShouldReturnTask()
        {
            var task = new TaskItem { Title = "Find Me" };
            var createdTask = await _taskService.CreateTaskAsync(task);
            var retrievedTask = await _taskService.GetTaskByIdAsync(createdTask.Id);

            Assert.NotNull(retrievedTask);
            Assert.Equal(createdTask.Id, retrievedTask.Id);
        }

        [Fact]
        public async Task DeleteTask_ShouldRemoveTask()
        {
            var task = new TaskItem { Title = "Delete Me" };
            var createdTask = await _taskService.CreateTaskAsync(task);
            var result = await _taskService.DeleteTaskAsync(createdTask.Id);

            Assert.True(result);

            var deletedTask = await _taskService.GetTaskByIdAsync(createdTask.Id);
            Assert.Null(deletedTask);
        }

        [Fact]
        public async Task DeleteTask_WithInvalidID_ShouldReturnFalse()
        {
            var result = await _taskService.DeleteTaskAsync(2);
            var result2 = await _taskService.DeleteTaskAsync(-1);

            Assert.False(result);            
            Assert.False(result2);
        }

        [Fact]
        public async Task CreateTask_With501Characters_ShouldThrowException()
        {
            var task = new TaskItem 
            { 
                Title = "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "12345678901234567890123456789012345678901234567890" +
                        "1" // Add the 501st character
            };

            await Assert.ThrowsAsync<ArgumentException>(() => _taskService.CreateTaskAsync(task));
        }
    }
}
