using monitoring.business.model;

namespace monitoring.business.dal
{
    public interface IApiEndpointRepository
    {
        IList<ApiEndpoint> GetAllApiEndpoints();
    }
}