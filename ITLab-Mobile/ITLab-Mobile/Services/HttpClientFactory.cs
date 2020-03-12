using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ITLab_Mobile.Models.Options;
using Newtonsoft.Json;
using Models.PublicAPI.Responses.Login;
using System.Net;
using Xamarin.Forms;
using System.IO;

namespace ITLab_Mobile.Services
{
    class HttpClientFactory
    {
        public static HttpClient HttpClient { get; } = CreateHttpClient();
        
        private static HttpClient CreateHttpClient()
        {
            //var assembly = IntrospectionExtensions.GetTypeInfo(typeof(HttpClientFactory)).Assembly;
            var assembly = typeof(HttpClientFactory).GetTypeInfo().Assembly;
            string raw_json = "";
            Stream stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.Data.appsettings.json");
            using (var reader = new StreamReader(stream))
            {
                raw_json = reader.ReadToEnd();
            }
            var opt = JsonConvert.DeserializeObject<ApiOptions>(raw_json);
            string baseUrl = "https://dev.rtuitlab.ru";

            var httpClient = new HttpClient(new AutoRefreshHttpHandler(baseUrl));
            httpClient.DefaultRequestHeaders.Add("User-Agent", UserAgent());
            httpClient.BaseAddress = new Uri(baseUrl);

            return httpClient;
        }

        private static string UserAgent()
        {
            //TODO: Get correct app version
            var version = $"1.0.0";
            return $"Xamarin.{Device.RuntimePlatform}/{version}";
        }
    }

    class AutoRefreshHttpHandler : HttpClientHandler
    {
        private readonly string baseUri;

        public AutoRefreshHttpHandler(string baseUri)
        {
            this.baseUri = baseUri;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage httpRequest, CancellationToken cancellationToken)
        {
            httpRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AccessToken);
            var request = await base.SendAsync(httpRequest, cancellationToken);
            var content = await request.Content.ReadAsStringAsync();
            if (request.StatusCode == HttpStatusCode.Unauthorized)
            {
                var refreshResponse = await base.SendAsync(HttpClientExtensions.RefreshTokenMessage(baseUri), cancellationToken);
                await HttpClientExtensions.HandleRefreshResponse(refreshResponse);
                return await SendAsync(httpRequest, cancellationToken);
            }
            return request;
        }
    }

    static class HttpClientExtensions
    {
        public static async Task<LoginResponse> RefreshToken(this HttpClient httpClient)
        {
            var response = await httpClient.SendAsync(RefreshTokenMessage(httpClient.BaseAddress.ToString()));
            var handled = await HandleRefreshResponse(response);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", handled.AccessToken);
            return handled;
        }
        public static async Task<LoginResponse> HandleRefreshResponse(HttpResponseMessage responseMessage)
        {
            string result = await responseMessage.Content.ReadAsStringAsync();
            var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(result);
            if (responseMessage.StatusCode == HttpStatusCode.OK)
            {
                Settings.RefreshToken = loginResponse.RefreshToken;
                Settings.AccessToken = loginResponse.AccessToken;
            }
            return loginResponse;
        }
        public static HttpRequestMessage RefreshTokenMessage(string baseUri)
        {
            string token = Settings.RefreshToken;
            var jsonContent = JsonConvert.SerializeObject(token);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            return new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = content,
                RequestUri = new Uri($"{baseUri}/api/Authentication/refresh")
            };
        }
    }
}
