using Chayns.Backend.Api.Models.Result.Base;

namespace Chayns.Backend.Api.Models.Result
{
    public class EmailResult : IApiResult
    {
        public int PostedEmails { get; set; }
    }
}
