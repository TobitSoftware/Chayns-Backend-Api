using System.Net.Http;
using System.Threading.Tasks;
using Chayns.Backend.Api.Credentials.Base;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repository.Base;

namespace Chayns.Backend.Api.Repository
{
    /// <summary>
    /// Object to get device data
    /// </summary>
    public class DeviceRepository : BaseApiRepository<DeviceResult>
    {
        /// <summary>
        /// Creates a new object to get devices
        /// </summary>
        public DeviceRepository() : base(null) { }
        /// <summary>
        /// Creates a new object to get devices
        /// </summary>
        /// <param name="credentials">Sets the credentials to authenticate all calling requests within this object</param>
        public DeviceRepository(ICredentials credentials) : base(credentials) { }

        /// <summary>
        /// Gets all devices async
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all devices matching the filter</returns>
        public async Task<Result<DeviceResult>> GetDevicesAsync(DeviceDataGet data)
        {
            return await Caller.CallApiAsync(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets all devices
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all devices matching the filter</returns>
        public Result<DeviceResult> GetDevices(DeviceDataGet data)
        {
            return Caller.CallApi(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets a device async
        /// </summary>
        /// <param name="deviceId">ID</param>
        /// <param name="location">Location</param>
        /// <returns>A status and the device matching the filter</returns>
        public async Task<SingleResult<DeviceResult>> GetDeviceAsync(int deviceId, LocationIdentifier location)
        {
            return await Caller.CallApiAsync(location, this, HttpMethod.Get, deviceId);
        }

        /// <summary>
        /// Gets a device
        /// </summary>
        /// <param name="deviceId">ID</param>
        /// <param name="location">Location</param>
        /// <returns>A status and the device matching the filter</returns>
        public SingleResult<DeviceResult> GetDevice(int deviceId, LocationIdentifier location)
        {
            return Caller.CallApi(location, this, HttpMethod.Get, deviceId);
        }

        /// <summary>
        /// Gets the controllername for the WebApi
        /// </summary>
        /// <param name="caller">Calling function</param>
        /// <returns>Controllername for the WebApi</returns>
        public override string Controller(string caller)
        {
            return "Device";
        }
    }
}
