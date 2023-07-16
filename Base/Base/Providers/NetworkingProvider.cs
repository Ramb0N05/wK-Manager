namespace wK_Manager.Base.Providers {

    public static class NetworkingProvider {

        public static HttpClient HttpClient { get; } = new HttpClient(new HttpClientHandler() {
            UseProxy = false
        });
    }
}
