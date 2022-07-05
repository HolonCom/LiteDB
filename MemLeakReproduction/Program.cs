using Hangfire;
using Hangfire.LiteDB;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseWindowsService(options => { options.ServiceName = "MemLeak"; });

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseLiteDbStorage("C:\\Users\\Dries\\Documents\\HolonCom\\Desco\\External Libraries\\LiteDB\\MemLeakReproduction\\bin\\Debug\\net6.0\\test.db", new LiteDbStorageOptions {DistributedLockLifetime = TimeSpan.FromSeconds(1), QueuePollInterval = TimeSpan.FromMilliseconds(500)}));

builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
