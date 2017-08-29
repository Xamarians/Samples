using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ChatDemo.Services;

namespace ChatDemo.Services
{

    public class WebServiceUrls
    {
    }

    class WebService
    {
        const string BaseUrl = "http://messaging.aspcore.net/api/";
        const int TIMEOUT = 30;

        static WebService()
        {
            ServicePointManager.Expect100Continue = false;
        }

        class MyWebClient : WebClient
        {
            protected override WebRequest GetWebRequest(Uri address)
            {
                var request = base.GetWebRequest(address);
                request.Timeout = 1000 * TIMEOUT;
                return request;
            }
        }

        #region Private Methods

        private MyWebClient CreateClient()
        {
            var client = new MyWebClient();
            client.Proxy = null;
            return client;
        }

        private async Task<Result<TData>> SendRequestAsync<TData>(string action, Dictionary<string, string> parameters, string method)
        {
            if (parameters == null)
                parameters = new Dictionary<string, string>();

            var postData = string.Join("&", parameters.Select(x => Uri.EscapeDataString(x.Key.Trim()) + "=" + x.Value));
            method = (method ?? "GET").ToUpper();

            using (var webClient = CreateClient())
            {
                try
                {
                    if (Helpers.AppSecurity.IsAuthenticated)
                    {
                        webClient.Headers.Add(HttpRequestHeader.Authorization, "BEARER " + Helpers.AppSecurity.Token);
                    }
                    string responseString;
                    if ("GET".Equals(method))
                    {
                        responseString = await webClient.DownloadStringTaskAsync(BaseUrl + action + "?" + postData);
                    }
                    else if ("DELETE".Equals(method))
                    {
                        responseString = await webClient.UploadStringTaskAsync(BaseUrl + action + "?" + postData, method, "");
                    }
                    else
                    {
                        webClient.Headers.Add(HttpRequestHeader.ContentType, "application/x-www-form-urlencoded");
                        responseString = await webClient.UploadStringTaskAsync(BaseUrl + action, method, postData);
                    }
                    var data = JsonConvert.DeserializeObject<TData>(responseString);
                    return new Result<TData>(data);
                }
                catch (WebException ex)
                {
                    var statusCode = GetCode(ex.Response as HttpWebResponse);
                    var responseStr = ReadResponseStream(ex.Response);
                    return Result.Create<TData>(ex.Message, ResultStatus.Error);
                }
                catch (Exception ex)
                {
                    return Result.Create<TData>(ex.Message, ResultStatus.Error);
                }
            }
        }

        private int GetCode(HttpWebResponse response)
        {
            if (response == null)
                return 0;
            return (int)response.StatusCode;
        }

        private string ReadResponseStream(WebResponse response)
        {
            if (response == null)
                return null;
            try
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var reader = new System.IO.StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        public Task<Result<T>> GetAsync<T>(string action, Dictionary<string, string> parameters)
        {
            return SendRequestAsync<T>(action, parameters, "GET");
        }

        public Task<Result<T>> PostAsync<T>(string action, Dictionary<string, string> parameters)
        {
            return SendRequestAsync<T>(action, parameters, "POST");
        }

        public Task<Result<T>> PutAsync<T>(string action, Dictionary<string, string> parameters)
        {
            return SendRequestAsync<T>(action, parameters, "PUT");
        }

        public Task<Result<T>> DeleteAsync<T>(string action, Dictionary<string, string> parameters)
        {
            return SendRequestAsync<T>(action, parameters, "DELETE");
        }

    }
}

