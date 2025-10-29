using BugStore.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if(string.IsNullOrEmpty(connectionString))
    throw new Exception("Connection string not found");

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
var app = builder.Build();

using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
db.Database.Migrate();

app.MapGet("/", () => "Hello World!");

app.Run();
