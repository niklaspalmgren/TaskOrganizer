using TaskOrganizer.AppServer;
using TaskOrganizer.AppServer.Services;
using TaskOrganizer.Shared.Factories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddLogging(opt => opt.AddConsole());

// Http client configurations
builder.Services.AddHttpClient("default", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ApiBaseUri"));
});

builder.Services.AddHttpClient("Tasks", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("TasksApiBaseUri"));
});

builder.Services.AddHttpClient("TaskBoards", c =>
{
    c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("TaskBoardsApiBaseUri"));
});

builder.Services.AddScoped<ITaskManager, TaskManager>();
builder.Services.AddSingleton<IDragDropService, DragDropService>();
builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskBoardService, TaskBoardService>();
builder.Services.AddScoped<ITaskBoardFactory, TaskBoardFactory>();
builder.Services.AddScoped<ITaskFactory, TaskOrganizer.Shared.Factories.TaskFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
