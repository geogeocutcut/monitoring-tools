using Microsoft.AspNetCore.Mvc;
using monitoring.business.model;
using monitoring.business.service;

namespace monitoring.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApiEndpointController : ControllerBase
    {
        private  IApiEndpointService _Service ;

        private readonly ILogger<ApiEndpointController> _Logger;

        public ApiEndpointController(ILogger<ApiEndpointController> logger,IApiEndpointService service)
        {
            _Logger = logger;
            _Service = service; 
        }

        [HttpGet(Name = "GetAllApiEndpoint")]
        public async Task<IEnumerable<ApiEndpoint>> Get()
        {
            _Logger.LogInformation("Call GetAllApiEndpoint ++++");
            return await _Service.GetAllApiEndpointsAsync();
        }
    }
}