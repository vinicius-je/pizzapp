using Microsoft.EntityFrameworkCore;
using PizzApp.Application;
using PizzApp.Infrastructure;
using PizzApp.Infrastructure.Context;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Configuration["Env"];

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureInfrastructure(builder.Configuration);
builder.Services.ConfigureApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

if (env == "DEV")
{
    CreateDatabase(app);
}

app.Run();

void CreateDatabase(WebApplication app)
{
    var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetService<AppDbContext>();
    context?.Database.EnsureCreated();
    
    var migrations = context?.Database.GetMigrations();

    if (migrations == null)
    {
        context?.Database.Migrate();
    }

    var seedFilePath = Path.Combine(Environment.CurrentDirectory, "Presentation/Scripts/inserts.sql");

    if (File.Exists(seedFilePath))
    {
        var sql = File.ReadAllText(seedFilePath);
        context?.Database.ExecuteSqlRaw(sql);
    }
}