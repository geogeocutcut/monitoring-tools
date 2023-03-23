using monitoring.business.dal;
using monitoring.business.model;

namespace monitoring.business.service
{
    public class ApiEndpointService:IApiEndpointService
    {
        public IMonitoringToolUnitOfWork _UoW;

        public ApiEndpointService(IMonitoringToolUnitOfWork uow)
        {
            _UoW= uow;
        }

        public IList<ApiEndpoint> GetAllApiEndpoints()
        {
            var repo = _UoW.ApiEndpointRepository;

            return repo.GetAllApiEndpoints();
        }

    }
}