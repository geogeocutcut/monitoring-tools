using Quartz.Impl;
using Quartz;
using monitoring.business.service;
using monitoring.business.model;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MySqlX.XDevAPI;
using System.Text.Json;
using System.Net;
using Microsoft.OpenApi.Extensions;

namespace monitoring.api.Quartz
{
    public class MonitoringJob : IJob
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<MonitoringJob> _Logger;
         
        public MonitoringJob(ILogger<MonitoringJob> logger,IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _Logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            _Logger.LogInformation("Job de surveillance des API en cours d'exécution...");
            
            var apiService = _serviceProvider.GetService<IApiEndpointService>();

            var ApiEndpointList = await apiService.GetAllApiEndpointsAsync();

            // Store the data in a database or cache for display on the web page
            var tasks = new List<Task>();

            foreach (var apiEndpoint in ApiEndpointList)
            {
                tasks.Add(UpdateApiStatusAsync(apiEndpoint));
            }
            await Task.WhenAll(tasks);

            //await apiService.SaveChangesAsync();
        }

        private async Task UpdateApiStatusAsync(ApiEndpoint apiEndpoint)
        {
            apiEndpoint.LastUpdated = DateTime.Now;
            HttpClient client = new HttpClient();
            try
            {
                var getResult = await client.GetAsync(new Uri(apiEndpoint.Endpoint));
                if (getResult != null && ((int)getResult.StatusCode)<400)
                {
                    var content = await getResult.Content.ReadAsStringAsync();
                    try
                    {
                        var infos = JsonSerializer.Deserialize<dynamic>(await getResult.Content.ReadAsStringAsync());

                        apiEndpoint.StatusHttp = ((int)(infos.StatusCode ?? getResult.StatusCode)).ToString();
                        apiEndpoint.Information = infos.Information ?? "No Information";
                    }
                    catch
                    {
                        apiEndpoint.StatusHttp = ((int)(getResult.StatusCode)).ToString();
                        apiEndpoint.Information = "No Information";
                    }
                }
                else
                {
                    apiEndpoint.StatusHttp = ((int)(getResult.StatusCode)).ToString();
                    apiEndpoint.Information = getResult.ReasonPhrase; 
                }

            }
            catch (Exception exception)
            {
                apiEndpoint.StatusHttp = ((int) HttpStatusCode.BadRequest).ToString();
                apiEndpoint.Information = exception.Message;
            }
        }
    }
}
