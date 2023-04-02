using Microsoft.EntityFrameworkCore;
using monitoring.api.Quartz;
using monitoring.business.dal;
using monitoring.business.service;
using monitoring.dal.ef;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
// Add framework services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAnyOrigins",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// Add services to the container.
builder.Services.AddDbContext<MonitoringToolDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("MySqlConnection")),ServiceLifetime.Singleton);
int refreshTime = builder.Configuration.GetValue<int>("RefreshTime");
builder.Services.AddSingleton<IMonitoringToolUnitOfWork, MonitoringUnitOfWorkEf>(x =>
    new MonitoringUnitOfWorkEf(x.GetRequiredService<MonitoringToolDbContext>(),
                refreshTime));
builder.Services.AddSingleton<IApiEndpointService, ApiEndpointService>();
builder.Services.AddControllers();

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("MonitoringJob");
    q.AddJob<MonitoringJob>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("MonitoringJob-trigger")
        //This Cron interval can be described as "run every minute" (when second is zero)
        .WithCronSchedule("0 * * ? * *")
    );
});


builder.Services.AddQuartzHostedService(options =>
{
    // when shutting down we want jobs to complete gracefully
    options.WaitForJobsToComplete = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowAnyOrigins");
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
