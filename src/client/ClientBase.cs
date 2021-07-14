using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace amazon_customer_reviews_client
{
    public abstract class ClientBase
    {
        public Func<Task<string>> RetrieveAuthorizationToken { get; set; }

        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            if (RetrieveAuthorizationToken != null)
            {
                var token = await RetrieveAuthorizationToken();
                msg.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }
            return msg;
        }
    }
}
