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

        public async Task<IList<ApiEndpoint>> GetAllApiEndpointsAsync()
        {
            var repo = _UoW.ApiEndpointRepository;

            return await repo.GetAllApiEndpointsAsync();
        }

        public async Task SaveChangesAsync()
        {
            var repo = _UoW.ApiEndpointRepository;

            await repo.SaveChangesAsync();
        }
    }
}