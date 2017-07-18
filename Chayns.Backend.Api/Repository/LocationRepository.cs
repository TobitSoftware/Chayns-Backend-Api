using System.Net.Http;
using System.Threading.Tasks;
using Chayns.Backend.Api.Credentials.Base;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repository.Base;

namespace Chayns.Backend.Api.Repository
{
    /// <summary>
    /// Object to get location data
    /// </summary>
    public sealed class LocationRepository : BaseApiRepository<LocationResult>
    {
        /// <summary>
        /// Creates a new object to get locations
        /// </summary>
        public LocationRepository() : base(null)
        {
        }

        /// <summary>
        /// Creates a new object to get locations
        /// </summary>
        /// <param name="credentials">Sets the credentials to authenticate all calling requests within this object</param>
        public LocationRepository(ICredentials credentials) : base(credentials)
        {
        }

        /// <summary>
        /// Gets all locations async
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all locations matching the filter</returns>
        public async Task<SingleResult<LocationResult>> GetLocationAsync(LocationDataGet data)
        {
            return await Caller.CallApiAsync(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all locations matching the filter</returns>
        public SingleResult<LocationResult> GetLocation(LocationDataGet data)
        {
            return Caller.CallApi(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets the controllername for the WebApi
        /// </summary>
        /// <param name="caller">Calling function</param>
        /// <returns>Controllername for the WebApi</returns>
        public override string Controller(string caller)
        {
            return "Location";
        }
    }
}
