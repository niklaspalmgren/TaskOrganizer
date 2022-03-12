using webStep.AppServer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

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

builder.Services.AddScoped<ITaskService, TaskService>();
builder.Services.AddScoped<ITaskBoardService, TaskBoardService>();
builder.Services.AddScoped<IThrowApiExceptionService, ThrowApiExceptionService>();


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
