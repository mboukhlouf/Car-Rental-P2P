using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CarRentalP2P.BindingModels;
using CarRentalP2P.Models.Api;
using CarRentalP2P.Models;
using Newtonsoft.Json;

namespace CarRentalP2P.Helpers
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

        public async Task<bool> GetTokenAsync(AuthenticationModel auth)
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

        public async Task<User> GetUserAsync()
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

        public async Task<User> GetUserAsync(int id)
        {
            Endpoint endpoint = ApiHelper.UsersEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{id}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<User>(responseBody);
            }

            return null;
        }

        public async Task<Advertisement> GetAdvertisementAsync(int id)
        {
            Endpoint endpoint = ApiHelper.AdvertisementsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{id}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Advertisement>(responseBody);
            }

            return null;
        }

        public async Task<IEnumerable<Advertisement>> GetUserAdvertisementsAsync(int ownerId)
        {
            Endpoint endpoint = ApiHelper.UserAdvertisementsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{ownerId}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<Advertisement>>(responseBody);
            }

            return null;
        }

        public async Task<AdvertisementsBindingModel> GetAdvertisementsAsync(Filter filter)
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

        public async Task<Reservation> GetReservationAsync(int id)
        {
            Endpoint endpoint = ApiHelper.ReservationsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{id}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Reservation>(responseBody);
            }

            return null;
        }

        public async Task<Reservation> AddReservationAsync(Reservation reservation)
        {
            Endpoint endpoint = ApiHelper.AddReservationEndpoint;
            var jsonBody = JsonConvert.SerializeObject(reservation);
            using var message = new HttpRequestMessage
            {
                RequestUri = endpoint.Uri,
                Method = HttpMethod.Post,
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<Reservation>(responseBody);
            }

            return null;
        }

        public async Task<bool> UpdateReservation(Reservation reservation)
        {
            Endpoint endpoint = ApiHelper.UpdateReservationsEndpoint;
            var jsonBody = JsonConvert.SerializeObject(reservation);
            using var message = new HttpRequestMessage
            {
                RequestUri = new Uri(endpoint.Uri + $"/{reservation.Id}", UriKind.Relative),
                Method = HttpMethod.Put,
                Content = new StringContent(jsonBody, Encoding.UTF8, "application/json")
            };

            using var response = await SendAsync(endpoint, message);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Reservation>> GetUserReservationsAsync(int userId)
        {
            Endpoint endpoint = ApiHelper.UserReservationsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{userId}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(responseBody);
            }

            return null;
        }

        public async Task<IEnumerable<Reservation>> GetAdvertisementReservations(int advertisementId)
        {
            Endpoint endpoint = ApiHelper.AdvertisementReservationsEndpoint;
            using var message = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(endpoint.Uri + $"/{advertisementId}", UriKind.Relative)
            };

            using var response = await SendAsync(endpoint, message);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<IEnumerable<Reservation>>(responseBody);
            }

            return null;
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
