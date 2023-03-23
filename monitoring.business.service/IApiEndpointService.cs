using monitoring.business.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monitoring.business.service
{
    public interface IApiEndpointService
    {
        IList<ApiEndpoint> GetAllApiEndpoints();
    }
}
