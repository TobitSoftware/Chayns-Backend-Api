using System.Net.Http;
using System.Threading.Tasks;
using Chayns.Backend.Api.Credentials.Base;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Repository.Base;

namespace Chayns.Backend.Api.Repository
{
    public sealed class EmailRepository : BaseApiRepository<EmailResult>
    {
        /// <summary>
        /// Creates a new object to send Emails
        /// </summary>
        public EmailRepository() : base(null) { }

        /// <summary>
        /// Creates a new object to send Emails
        /// </summary>
        /// <param name="credentials"></param>
        public EmailRepository(ICredentials credentials) : base(credentials) { }

        /// <summary>
        /// Sends an Email to given receivers async
        /// </summary>
        /// <param name="data">Data for email</param>
        /// <returns></returns>
        public async Task<SingleResult<EmailResult>> SendEmailAsync(EmailData data)
        {
            return (await Caller.CallApiAsync(data, this, HttpMethod.Post));
        }

        /// <summary>
        /// Sends an Email to given receivers
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SingleResult<EmailResult> SendEmail(EmailData data)
        {
            return Caller.CallApi(data, this, HttpMethod.Post);
        }

        /// <summary>
        /// Gets the controllername for the WebApi
        /// </summary>
        /// <param name="caller">Calling function</param>
        /// <returns>Controllername for the WebApi</returns>
        public override string Controller(string caller)
        {
            return "Email";
        }
    }
}
