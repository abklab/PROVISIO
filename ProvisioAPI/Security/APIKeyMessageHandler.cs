using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ProvisioAPI.Security
{
   
    public class APIKeyMessageHandler : DelegatingHandler
    {
        //GetAPIKeysToCheck
        private const string APIKey = "PROVISIO-APIKEY-V2019-V1000-FIELD020919";
        //private const string APIKey = "VEC2019-v1000-FIELD020919-ABK02";
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Please Use the following if you want to enforce https/SSL in web apps;

            //if (request.RequestUri.Scheme != Uri.UriSchemeHttps)
            //{
            //    // Forbidden (or do a redirect)...
            //    return request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "You are NOT authorized to access this resource.");
            //}

            bool isValidKey = false;
            var checkHeaderKey = request.Headers.TryGetValues("CLIENT_ACCESS_APIKEY", out IEnumerable<string> requestHeaders);

            if (checkHeaderKey)
            {
                if (requestHeaders.FirstOrDefault().Equals(APIKey))
                {
                    isValidKey = true;
                }
            }
            if (!isValidKey)
            {
                return request.CreateResponse(System.Net.HttpStatusCode.Forbidden, "You are NOT authorized to access this resource.");
            }

            var response = await base.SendAsync(request, cancellationToken);
            return response;
        }

    }
}