namespace webStep.AppServer.Services
{
    public class ThrowApiExceptionService : IThrowApiExceptionService
    {
        private readonly IHttpClientFactory _clientFactory;

        public ThrowApiExceptionService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task ThrowApiExceptionAsync()
        {
            var httpClient = _clientFactory.CreateClient("default");
            var uri = "Throw";

            await httpClient.GetAsync(uri);
        }
    }
}
