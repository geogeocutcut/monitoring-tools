using monitoring.business.dal;
using monitoring.dal.cache;

namespace monitoring.dal.ef
{
    public  class MonitoringUnitOfWorkEf : IMonitoringToolUnitOfWork
    {

        private readonly MonitoringToolDbContext _Context;
        private IApiEndpointRepository _ApiEndpointRepository;

        public MonitoringUnitOfWorkEf(MonitoringToolDbContext context)
        {
            _Context = context;
            context.Database.EnsureCreated();
        }

        public IApiEndpointRepository ApiEndpointRepository
        {
            get
            {
                if (_ApiEndpointRepository == null)
                {
                    _ApiEndpointRepository = new ApiEndpointRepositoryCache( new ApiEndpointRepositoryEf(_Context));
                }
                return _ApiEndpointRepository;
            }
        }


    }
}
