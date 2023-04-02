using monitoring.business.dal;
using monitoring.dal.cache;

namespace monitoring.dal.ef
{
    public  class MonitoringUnitOfWorkEf : IMonitoringToolUnitOfWork
    {

        private readonly MonitoringToolDbContext _Context;
        private IApiEndpointRepository _ApiEndpointRepository;
        private int _RefreshTime = 1800;

        public MonitoringUnitOfWorkEf(MonitoringToolDbContext context, int refreshTime)
        {
            _Context = context;
            context.Database.EnsureCreated();
            _RefreshTime = refreshTime;
        }

        public IApiEndpointRepository ApiEndpointRepository
        {
            get
            {
                if (_ApiEndpointRepository == null)
                {
                    _ApiEndpointRepository = new ApiEndpointRepositoryCache( new ApiEndpointRepositoryEf(_Context), _RefreshTime);
                }
                return _ApiEndpointRepository;
            }
        }


    }
}
