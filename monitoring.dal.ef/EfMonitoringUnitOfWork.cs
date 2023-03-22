using Microsoft.EntityFrameworkCore;
using monitoring.business.dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring.dal.ef
{
    public  class EfMonitoringUnitOfWork : IMonitoringUnitOfWork
    {

        private readonly MonitoringToolDbContext _context;
        private IApiEndpointRepository _apiEndpointRepository;

        public EfMonitoringUnitOfWork(MonitoringToolDbContext _context)
        {

        }

        public IApiEndpointRepository ApiEndpointRepository
        {
            get
            {
                if (_apiEndpointRepository == null)
                {
                    _apiEndpointRepository = new EfApiEndpointRepository(_context);
                }
                return _apiEndpointRepository;
            }
        }

    }
}
