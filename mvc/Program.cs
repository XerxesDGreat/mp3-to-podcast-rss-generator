using Microsoft.Extensions.FileProviders;
using System;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDirectoryBrowser();

var app = builder.Build();
var config = app.Services.GetRequiredService<IConfiguration>();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();


Console.WriteLine($"looking for podcasts in {config["PODCAST_FILE_PATH"]}");
PhysicalFileProvider fileProvider;
try
{
    fileProvider = new PhysicalFileProvider(config["PODCAST_FILE_PATH"]);
}
catch (System.ArgumentException e)
{
    Console.WriteLine($"failed to find directory {config["PODCAST_FILE_PATH"]}; please ensure it exists (error was {e.Message})");
    throw e;
}
var requestPath = "/books";

// Enable displaying browser links.
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
