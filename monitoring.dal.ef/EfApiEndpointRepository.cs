using monitoring.business.dal;
using monitoring.business.model;

namespace monitoring.dal.ef
{
    public class EfApiEndpointRepository : IApiEndpointRepository
    {
        private readonly MonitoringToolDbContext _context;

        public EfApiEndpointRepository(MonitoringToolDbContext context)
        {
            _context = context;
        }


        public IList<ApiEndpoint> GetAllApiEndpoints()
        {
            return this._context.ApiEndpoints.ToList();
        }
    }
}