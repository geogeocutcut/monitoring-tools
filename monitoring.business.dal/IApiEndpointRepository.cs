using monitoring.business.model;

namespace monitoring.business.dal
{
    public interface IApiEndpointRepository
    {
        Task<IList<ApiEndpoint>> GetAllApiEndpointsAsync();

        Task SaveChangesAsync();
    }
}