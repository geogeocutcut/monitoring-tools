using monitoring.business.dal;
using monitoring.business.model;

namespace monitoring.dal.cache
{
    public class ApiEndpointRepositoryCache : IApiEndpointRepository
    {
        private IApiEndpointRepository _Next;
        private IList<ApiEndpoint> _ApiEndpointsCached;
        private DateTime _LastCached;
        private int _RefreshTime;
        public ApiEndpointRepositoryCache(IApiEndpointRepository next,int refreshTime=1800)
        {
            _Next = next;
            _RefreshTime= refreshTime;
        }

        public async Task<IList<ApiEndpoint>> GetAllApiEndpointsAsync()
        {
            var ts = DateTime.Now - _LastCached;
            if(_ApiEndpointsCached is null || ts.TotalSeconds>1800)
            {

                _ApiEndpointsCached = await _Next.GetAllApiEndpointsAsync();
                _LastCached = DateTime.Now;
            }

            return _ApiEndpointsCached;
        }

        public async Task SaveChangesAsync()
        {
            await _Next.SaveChangesAsync();
        }
    }
}