using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Chayns.Backend.Api.Helper;
using Chayns.Backend.Api.Models.Data;
using Chayns.Backend.Api.Models.Data.Base;
using Chayns.Backend.Api.Models.Result;
using Chayns.Backend.Api.Models.Result.Base;
using Newtonsoft.Json;

namespace Chayns.Backend.Api.Repositories.Base
{
    internal sealed class WebApiCaller<TResult> where TResult : IApiResult
    {
        internal async Task<Result<TResult>> CallApiAsync<TData>(TData request, IApiRepository caller, HttpMethod method, int? id = null, [CallerMemberName] string callingFunction = null) where TData : ChangeableData, IApiData
        {
            var url = Config.WebApiUrl + (string.IsNullOrWhiteSpace(request?.GetLocationIdentifier()) ? "" : request.GetLocationIdentifier() + "/") + caller.Controller(callingFunction) + (id.HasValue ? ("/" + id) : "");
            Task<string> dataTask = null;
            if (request != null)
            {
                if (method == HttpMethod.Get)
                {
                    dataTask = Task.Factory.StartNew(() => UrlParameterConverter.SerializeObject(request));
                }
                else
                {
                    dataTask = Task.Factory.StartNew(() => JsonConvert.SerializeObject(request));
                }
            }

            using (var hc = new HttpClient())
            {
                if (method == HttpMethod.Get)
                {
                    url += dataTask == null ? "" : await dataTask;
                }
                var req = new HttpRequestMessage(method, url);

                var credentials = caller.GetCredentials(callingFunction);
                if (credentials != null)
                {
                    req.Headers.Authorization = new AuthenticationHeaderValue(credentials.Scheme(), credentials.Parameter());
                }

                if (method != HttpMethod.Get)
                {
                    const string mediaType = "application/json";
                    var content = new StringContent(dataTask == null ? "" : await dataTask, Encoding.UTF8, mediaType);

                    req.Content = content;
                }

                try
                {
                    var respone = await hc.SendAsync(req);
                    return ParseResponse(respone);
                }
                catch (WebException)
                {
                    return ParseResponse(null);
                }
            }
        }

        internal Result<TResult> CallApi<TData>(TData request, IApiRepository caller, HttpMethod method, int? id = null, [CallerMemberName] string callingFunction = null) where TData : ChangeableData, IApiData
        {
            var url = Config.WebApiUrl + (string.IsNullOrWhiteSpace(request?.GetLocationIdentifier()) ? "" : request.GetLocationIdentifier() + "/") + caller.Controller(callingFunction) + (id.HasValue ? ("/" + id) : "");
            var data = "";
            if (request != null)
            {
                if (method == HttpMethod.Get)
                {
                    data = UrlParameterConverter.SerializeObject(request);
                }
                else
                {
                    data = JsonConvert.SerializeObject(request);
                }
            }

            using (var hc = new HttpClient())
            {
                if (method == HttpMethod.Get)
                {
                    url += data ?? "";
                }
                var req = new HttpRequestMessage(method, url);

                var credentials = caller.GetCredentials(callingFunction);
                if (credentials != null)
                {
                    req.Headers.Authorization = new AuthenticationHeaderValue(credentials.Scheme(), credentials.Parameter());
                }

                if (method != HttpMethod.Get)
                {
                    const string mediaType = "application/json";
                    var content = new StringContent(data ?? "", Encoding.UTF8, mediaType);

                    req.Content = content;
                }

                try
                {
                    var response = hc.SendAsync(req).Result;
                    return ParseResponse(response);
                }
                catch (WebException)
                {
                    return ParseResponse(null);
                }
            }
        }

        private Result<TResult> ParseResponse(HttpResponseMessage respMsg)
        {
            var result = new Result<TResult>();

            if (respMsg?.IsSuccessStatusCode == true)
            {
                var respBodyTask = respMsg.Content.ReadAsStringAsync();
                respBodyTask.Wait();
                var respBody = respBodyTask.Result;
                result = JsonConvert.DeserializeObject<Result<TResult>>(respBody) ?? result;
            }

            result.Status = GetStatus(respMsg);

            return result;
        }

        private Status GetStatus(HttpResponseMessage respMsg)
        {
            if (respMsg == null)
            {
                return new Status(-1, Guid.Empty, "Internal error");
            }

            if (respMsg.IsSuccessStatusCode)
            {
                return new Status((int)respMsg.StatusCode);
            }

            try
            {
                var respBodyTask = respMsg.Content.ReadAsStringAsync();
                respBodyTask.Wait();
                var respBody = respBodyTask.Result;

                var err = JsonConvert.DeserializeObject<ErrorResponse>(respBody);
                return new Status((int)respMsg.StatusCode, err.ErrorGuid, err.Message);
            }
            catch (Exception ex)
            {
                return new Status(-1, Guid.Empty, ex.Message);
            }
        }
    }
}
