using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using monitoring.business.dal;
using monitoring.business.model;

namespace monitoring.dal.ef
{
    public class ApiEndpointRepositoryEf : IApiEndpointRepository
    {
        private readonly MonitoringToolDbContext _Context;

        public ApiEndpointRepositoryEf(MonitoringToolDbContext context)
        {
            _Context = context;
        }


        public async Task<IList<ApiEndpoint>> GetAllApiEndpointsAsync()
        {
            return await this._Context.ApiEndpoints.ToListAsync();
        }


        public async Task SaveChangesAsync()
        {
            await _Context.SaveChangesAsync();
        }
    }
}