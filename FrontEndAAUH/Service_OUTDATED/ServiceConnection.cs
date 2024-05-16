
namespace FrontEndAAUH.Service {
    public class ServiceConnection : IServiceConnection{
        public HttpClient httpClient { get; init; }
        public string baseUrl { get; set; }
        public string useUrl { get; set; }

        public ServiceConnection(String inputUrl) {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            httpClient = new HttpClient(clientHandler);
            baseUrl = inputUrl;
            useUrl = inputUrl;
        }

        public async Task<HttpResponseMessage> serviceGet() {
            HttpResponseMessage response = null;

            if (useUrl != null) {
                response = await httpClient.GetAsync(useUrl);
            }
            return response;
        }

        public async Task<HttpResponseMessage> servicePost(StringContent jsonPost) {
            HttpResponseMessage response = null;

            if (useUrl != null) {
                response = await httpClient.PostAsync(useUrl, jsonPost);
            }
            return response;
        }
    }
}
