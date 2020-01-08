using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LocationDeVoitures.BindingModels;
using LocationDeVoitures.Models;
using LocationDeVoitures.Models.Api;
using Newtonsoft.Json;

namespace LocationDeVoitures.Helpers
{
    public class ApiClient : IDisposable
    {
        private HttpClientHandler httpClientHandler;
        private HttpClient httpClient;
        private bool disposed = false;

        public String Token { get; set; }

        public String UserAgent { get; set; } = "Car Rental";

        public ApiClient()
        {
            httpClientHandler = new HttpClientHandler()
            {
                UseCookies = false,
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
            httpClient = new HttpClient(httpClientHandler)
            {
                BaseAddress = ApiHelper.BaseUrl
            };
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
            }

            httpClientHandler.Dispose();
            httpClient.Dispose();

            disposed = true;
        }

        ~ApiClient()
        {
            Dispose(false);
        }

        public async Task<bool> GetToken(AuthenticationModel auth)
        {
            Endpoint endpoint = ApiHelper.TokenEndpoint;
            using var message = new HttpRequestMessage
            {
                RequestUri = endpoint.Uri,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(auth), Encoding.UTF8, "application/json")
            };
            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                dynamic bodyJson = JsonConvert.DeserializeObject(body);
                Token = bodyJson.token;
                return true;
            }

            return false;
        }

        public async Task<User> GetUser()
        {
            Endpoint endpoint = ApiHelper.TokenGetUserEndpoint;
            using var message = new HttpRequestMessage
            {
                RequestUri = endpoint.Uri,
                Method = HttpMethod.Get
            };

            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<User>(body);
            }

            return null;
        }

        public async Task<AdvertisementsBindingModel> GetAdvertisements(Filter filter)
        {
            Endpoint endpoint = ApiHelper.AdvertisementsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = endpoint.Uri,
                Content = new StringContent(JsonConvert.SerializeObject(filter), Encoding.UTF8, "application/json")
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<AdvertisementsBindingModel>(responseBody);
            }

            return null;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            Endpoint endpoint = ApiHelper.CreateUserEndpoint;
            using var message = new HttpRequestMessage
            {
                RequestUri = endpoint.Uri,
                Method = HttpMethod.Post,
                Content = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json")
            };

            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> CreateAdvertisementAsync(Advertisement advertisement)
        {
            Endpoint endpoint = ApiHelper.AddAdvertisementEndpoint;
            var jsonBody = JsonConvert.SerializeObject(advertisement);
            using var message = new HttpRequestMessage
            {
                RequestUri = endpoint.Uri,
                Method = HttpMethod.Post,
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public Task<HttpResponseMessage> SendAsync(Endpoint endpoint, HttpRequestMessage message)
        {
            // Accept-Encoding
            if (!message.Headers.Contains("Accept-Encoding"))
                message.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            // User-Agent
            if (!message.Headers.Contains("User-Agent"))
                message.Headers.Add("User-Agent", UserAgent);

            // Authorization
            if (!message.Headers.Contains("User-Agent"))
                message.Headers.Add("User-Agent", UserAgent);

            if (endpoint.RequiresAuthorization)
            {
                message.Headers.Add("Authorization", $"Bearer {Token}");
            }

            return httpClient.SendAsync(message);
        }
    }
}
