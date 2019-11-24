using System.Net.Http;
using Flurl.Http.Configuration;

namespace DevOps.Products.Website
{
    public class PollyHttpClientFactory : DefaultHttpClientFactory
    {
        public override HttpMessageHandler CreateMessageHandler()
        {
            return new PolicyHandler
            {
                InnerHandler = base.CreateMessageHandler()
            };
        }
    }
}