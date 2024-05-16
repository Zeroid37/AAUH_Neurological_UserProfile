namespace FrontEndAAUH.Service {
    public interface IServiceConnection {

        public string baseUrl { get; set; }
        public string useUrl { get; set; }

        Task<HttpResponseMessage> serviceGet();
        Task<HttpResponseMessage> servicePost(StringContent jsonPost);
    }
}
