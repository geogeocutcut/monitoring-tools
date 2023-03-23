using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring.business.dal
{
    public interface IMonitoringToolUnitOfWork
    {
        IApiEndpointRepository ApiEndpointRepository { get; }
    }
}
