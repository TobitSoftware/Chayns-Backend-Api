﻿using System.Net.Http;
using System.Threading.Tasks;
using Chayns.Backend.Api.Credentials.Base;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repositories.Base;

namespace Chayns.Backend.Api.Repositories
{
    /// <summary>
    /// Object to get user data
    /// </summary>
    public class UserRepository : BaseApiRepository<UserResult>
    {
        /// <summary>
        /// Creates a new object to get user
        /// </summary>
        public UserRepository() : base(null) { }
        /// <summary>
        /// Creates a new object to get user
        /// </summary>
        /// <param name="credentials">Sets the credentials to authenticate all calling requests within this object</param>
        public UserRepository(ICredentials credentials) : base(credentials)
        {
        }

        /// <summary>
        /// Gets all user async
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all user matching the filter</returns>
        public async Task<Result<UserResult>> GetUserAsync(UserDataGet data)
        {
            return await Caller.CallApiAsync(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets all user
        /// </summary>
        /// <param name="data">Filter for the request</param>
        /// <returns>A status and all user matching the filter</returns>
        public Result<UserResult> GetUser(UserDataGet data)
        {
            return Caller.CallApi(data, this, HttpMethod.Get);
        }

        /// <summary>
        /// Gets a user async
        /// </summary>
        /// <param name="userId">ID</param>
        /// <param name="location">Location</param>
        /// <returns>A status and the user matching the filter</returns>
        public async Task<SingleResult<UserResult>> GetUserAsync(int userId, LocationIdentifier location)
        {
            return await Caller.CallApiAsync(location, this, HttpMethod.Get, userId);
        }

        /// <summary>
        /// Gets a user
        /// </summary>
        /// <param name="userId">ID</param>
        /// <param name="location">Location</param>
        /// <returns>A status and the user matching the filter</returns>
        public SingleResult<UserResult> GetUser(int userId, LocationIdentifier location)
        {
            return Caller.CallApi(location, this, HttpMethod.Get, userId);
        }

        /// <summary>
        /// Gets the controllername for the WebApi
        /// </summary>
        /// <param name="caller">Calling function</param>
        /// <returns>Controllername for the WebApi</returns>
        public override string Controller(string caller)
        {
            return "User";
        }
    }
}
