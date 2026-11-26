using MyAPP.Common.DB.IDBUnits;
using MyAPP.Drivers.MariaDB;
using MyAPP.Drivers.SQLite;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Выбор драйвера БД из конфигурации (по умолчанию SQLite)
var provider = builder.Configuration["Database:Provider"] ?? "SQLite";
IDatabase database = provider.Equals("MariaDB", StringComparison.OrdinalIgnoreCase)
    ? new MariaDbDatabase(
        builder.Configuration.GetConnectionString("MariaDB")
        ?? throw new InvalidOperationException("ConnectionStrings:MariaDB is not configured."))
    : new SqliteDatabase(
        builder.Configuration.GetConnectionString("SQLite") ?? "Data Source=warehouse.db");

builder.Services.AddSingleton<IDatabase>(database);

var app = builder.Build();

// Освобождение ресурсов БД при остановке приложения
app.Lifetime.ApplicationStopping.Register(() =>
{
    database.Release();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

// Инициализация структуры таблиц БД до приёма HTTP-запросов
database.Initialize();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
