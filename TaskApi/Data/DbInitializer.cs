using TaskApi.Models;

namespace TaskApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(TaskDbContext context)
        {
            if (context.Tasks.Any()) return; // already seeded

            context.Tasks.AddRange(
                new TaskItem { Title = "Sample Task 1", DueDate = DateTime.Now.AddDays(2), IsCompleted = false },
                new TaskItem { Title = "Sample Task 2", DueDate = DateTime.Now.AddDays(5), IsCompleted = true, DateCompleted = DateTime.Now }
            );

            context.SaveChanges();
        }
    }
}
