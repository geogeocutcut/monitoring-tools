using Microsoft.EntityFrameworkCore;
using monitoring.business.dal;
using monitoring.business.service;
using monitoring.dal.ef;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<MonitoringToolDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")),ServiceLifetime.Singleton);
builder.Services.AddSingleton<IMonitoringToolUnitOfWork, EfMonitoringToolUnitOfWork>();
builder.Services.AddSingleton<IApiEndpointService, ApiEndpointService>();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
