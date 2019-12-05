using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;
using Polly.Wrap;

namespace DevOps.Products.Website
{
    public static class Policies
    {
        private static AsyncTimeoutPolicy<HttpResponseMessage> TimeoutPolicy
        {
            get { return Policy.TimeoutAsync<HttpResponseMessage>(2, TimeoutStrategy.Pessimistic); }
        }

        private static AsyncRetryPolicy<HttpResponseMessage> RetryPolicy
        {
            get
            {
                return Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode >= 400 && (int)message.StatusCode < 500)
                    .Or<TimeoutRejectedException>()
                    .WaitAndRetryAsync(5, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
            }
        }

        private static AsyncCircuitBreakerPolicy<HttpResponseMessage> CircuitBreakerPolicy
        {
            get
            {
                return Policy.HandleResult<HttpResponseMessage>(message => (int)message.StatusCode == 408 || (int)message.StatusCode >= 500)
                    .CircuitBreakerAsync(
                        3,
                        TimeSpan.FromSeconds(10)
                    );
            }
        }

        public static AsyncPolicyWrap<HttpResponseMessage> PolicyStrategy => Policy.WrapAsync(RetryPolicy, TimeoutPolicy, CircuitBreakerPolicy);
    }
}