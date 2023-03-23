using Microsoft.EntityFrameworkCore;
using monitoring.business.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring.dal.ef
{
    public  class EfMonitoringToolUnitOfWork : IMonitoringToolUnitOfWork
    {

        private readonly MonitoringToolDbContext _Context;
        private IApiEndpointRepository _ApiEndpointRepository;

        public EfMonitoringToolUnitOfWork(MonitoringToolDbContext context)
        {
            _Context = context;
        }

        public IApiEndpointRepository ApiEndpointRepository
        {
            get
            {
                if (_ApiEndpointRepository == null)
                {
                    _ApiEndpointRepository = new EfApiEndpointRepository(_Context);
                }
                return _ApiEndpointRepository;
            }
        }

    }
}
