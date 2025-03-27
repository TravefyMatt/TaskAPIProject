using Microsoft.AspNetCore.Mvc;
using TaskApi.Models;
using TaskApi.Services;

namespace TaskApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskService _taskService; // declares a private, read-only field _taskService of type TaskService.
        //The readonly keyword ensures that the field can only be assigned a value once (in the constructor) and cannot be modified afterward.

        // ✅ Constructor Injection (Dependency Injection)
        public TaskController(TaskService taskService)
        {
            _taskService = taskService; //The injected taskService is assigned to the _taskService field, making it available for use throughout the controller.
        }

        // ✅ GET /api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            return Ok(tasks);
        }

        // ✅ GET /api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            return task == null ? NotFound() : Ok(task);
        }

        // ✅ POST /api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] TaskItem task)
        {
            try
            {
                var createdTask = await _taskService.CreateTaskAsync(task);
                return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ PUT /api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id, [FromBody] TaskItem task)
        {
            var success = await _taskService.UpdateTaskAsync(id, task);
            return success ? NoContent() : NotFound();
        }

        // ✅ DELETE /api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var success = await _taskService.DeleteTaskAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
