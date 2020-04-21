using System;
using System.Net.Http;
using Xamarin.Forms;

namespace ITLab_Mobile.Services
{
    static class HttpClientFactory
    {
        public static HttpClient HttpClient { get; } = CreateHttpClient();
        
        private static HttpClient CreateHttpClient()
        {

            var refreshTokenHandler = Settings.RefreshTokenHandler;
            refreshTokenHandler.InnerHandler = new HttpClientHandler();

            var httpClient = new HttpClient(refreshTokenHandler);

            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent());
            httpClient.BaseAddress = new Uri(Settings.ApiOptions.BaseUrl);

            return httpClient;
        }

        private static string UserAgent()
        {
            //TODO: Get correct app version
            var version = $"1.0.0";
            return $"Xamarin.{Device.RuntimePlatform}/{version}";
        }
    }
}
