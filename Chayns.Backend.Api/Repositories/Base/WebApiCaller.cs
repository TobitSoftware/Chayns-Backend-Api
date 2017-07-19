using System;
using System.IO;
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

            var jsonresult = "";
            WebException webException = null;

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
                    jsonresult = await respone.Content.ReadAsStringAsync();
                }
                catch (WebException wex)
                {
                    webException = wex;
                }
            }

            return ParseResponse(jsonresult, webException);
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

            var jsonresult = "";
            WebException webException = null;

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
                    jsonresult = response.Content.ReadAsStringAsync().Result;
                }
                catch (WebException wex)
                {
                    webException = wex;
                }
            }

            return ParseResponse(jsonresult, webException);
        }

        private Result<TResult> ParseResponse(string jsonresult, WebException wex)
        {
            Result<TResult> result;
            if (wex == null)
            {
                result = JsonConvert.DeserializeObject<Result<TResult>>(jsonresult) ?? new Result<TResult>();
            }
            else
            {
                result = new Result<TResult>();
            }

            result.Status = GetStatus(wex);

            return result;
        }

        private Status GetStatus(WebException wex)
        {
            Status status;
            if (wex != null)
            {
                var resp = (HttpWebResponse)wex.Response;
                try
                {
                    string errorResponse;
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var reader = new StreamReader(resp.GetResponseStream(), Encoding.UTF8))
                    {
                        errorResponse = reader.ReadToEnd();
                    }
                    var err = JsonConvert.DeserializeObject<ErrorResponse>(errorResponse);
                    status = new Status((int)resp.StatusCode, err.ErrorGuid, err.Message);
                }
                catch
                {
                    status = new Status((int)resp.StatusCode, Guid.Empty, resp.StatusDescription);
                }

            }
            else
            {
                status = new Status(200);
            }

            return status;
        }
    }
}
