using Serilog.Sinks.Graylog;
using Serilog;
using Serilog.Sinks.GraylogGelf;
using Serilog.Events;
using Serilog.Formatting.Json;
using Serilog.Exceptions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile($"serilog.json", false)
    .Build();

// Serilog konfigürasyonu
Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
    .WriteTo.Graylog(new GraylogSinkOptions
    {
        Facility = "GraylogClient",
        HostnameOrAddress = "localhost",
        Port = 12201,
        TransportType = Serilog.Sinks.Graylog.Core.Transport.TransportType.Udp
    })
    .CreateLogger();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
