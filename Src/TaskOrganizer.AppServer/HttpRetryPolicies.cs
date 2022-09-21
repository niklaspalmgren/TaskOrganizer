using Polly.Contrib.WaitAndRetry;
using Polly.Retry;
using Polly;
using System.Net;

namespace TaskOrganizer.AppServer
{
    internal static class HttpRetryPolicies
    {
        internal static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy => Policy<HttpResponseMessage>
            .Handle<HttpRequestException>()
            .OrResult(x => x.StatusCode is >= HttpStatusCode.InternalServerError or HttpStatusCode.RequestTimeout)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(TimeSpan.FromSeconds(1), 5));

    }
}
