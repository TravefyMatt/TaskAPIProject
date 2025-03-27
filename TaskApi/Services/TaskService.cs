using System;
using System.Collections.Generic;
using System.Linq;
using TaskApi.Models;
using TaskApi.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskApi.Services
{
    public class TaskService
    {
        private readonly TaskDbContext _context;

        public TaskService(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasksAsync()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> CreateTaskAsync(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title) || task.Title.Length > 500)
                throw new ArgumentException("Title is required and must be 500 characters or less.");

            if (task.DueDate.HasValue && task.DueDate.Value < DateTime.Now)
                throw new ArgumentException("DueDate cannot be in the past.");

            task.DateCompleted = task.IsCompleted ? DateTime.Now : null;

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<bool> UpdateTaskAsync(int id, TaskItem updatedTask)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            task.Title = updatedTask.Title;
            task.DueDate = updatedTask.DueDate;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DateCompleted = updatedTask.IsCompleted ? DateTime.Now : null;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTaskAsync(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
