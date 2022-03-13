using Microsoft.EntityFrameworkCore;
using TaskOrganizer.Api.Data;
using TaskOrganizer.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHealthChecks();
builder.Services.AddScoped<ITaskRepo, TaskRepo>();
builder.Services.AddScoped<ITaskBoardRepo, TaskBoardRepo>();
builder.Services.AddDbContext<TasksDb>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

var app = builder.Build();

// Creates, migrates and seeds data (if empty)
PrepDb.Prep(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/api/error-development");
}
else
{
    app.UseExceptionHandler("/api/error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapHealthChecks("/api/health");

app.MapControllers();

app.Run();
