using SimpleBlazor.Components;
using SimpleBlazor.Services.ApiClient;
using SimpleBlazor.Services.Configuration;
using SimpleBlazor.Services.GenericService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddBlazorBootstrap();
builder.Services.AddHttpClient<IApiClient, ApiClient>();
builder.Services.AddSingleton<TaskService>();
builder.Services.AddSingleton<UserService>();
builder.Services.AddSingleton<ScheduleService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
