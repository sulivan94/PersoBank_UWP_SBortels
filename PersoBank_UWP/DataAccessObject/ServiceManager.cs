using PersoBank_UWP.Util;
using System.Net.Http;
using Windows.ApplicationModel.Resources;

namespace PersoBank_UWP.DataAccessObject
{
    public class ServiceManager
    {
        public static HttpClient getHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add(ApiConstants.Authorization, ApiConstants.Token);
            return client;
        }

        public static string AnalyzeRequestResult(HttpResponseMessage result)
        {
            if (result.IsSuccessStatusCode)
            {
                return null;
            }
            string error = GetError(result);
            return error;
        }

        public static string GetError(HttpResponseMessage result)
        {
            var loader = new ResourceLoader();

            switch ((int)result.StatusCode)
            {
                case ApiConstants.BadRequest:
                    if (result.Content != null)
                    {
                        return result.Content.ReadAsStringAsync().Result;
                    }
                    return loader.GetString("apiBadRequest");

                case ApiConstants.Unauthorized:
                    return loader.GetString("apiUnauthorized");

                case ApiConstants.NotFound:
                    return loader.GetString("apiNotFound");

                case ApiConstants.InternalServerError:
                    return loader.GetString("apiInternalServerError");

                case ApiConstants.ServiceUnavailable:
                    return loader.GetString("apiUnavailable");

                default:
                    return string.Format(loader.GetString("apiDefaultError"), (int)result.StatusCode);
            }
        }
    }
}
