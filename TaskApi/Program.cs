using Microsoft.EntityFrameworkCore;
using TaskApi.Data;
using TaskApi.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Register the DbContext to use SQL Server
builder.Services.AddDbContext<TaskDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ Register your service
builder.Services.AddScoped<TaskService>();

// Register controller services
builder.Services.AddControllers();

var app = builder.Build();

// Seed database with sample data
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<TaskDbContext>();
    DbInitializer.Seed(dbContext);
}

app.MapControllers();

app.Run();
