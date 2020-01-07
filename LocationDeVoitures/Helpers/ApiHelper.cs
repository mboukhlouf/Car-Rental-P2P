using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.Helpers
{
    public class ApiHelper
    {
        public static Uri BaseUrl => new Uri("https://localhost:44325");

        public static Dictionary<String, Endpoint> Endpoints = new Dictionary<string, Endpoint>
        {
            { "token", new Endpoint("Token", false) },
            { "tokenGetUser", new Endpoint("Token", true) },
            { "advertisements", new Endpoint("api/Advertisements", false) }
        };

        public static Endpoint TokenEndpoint => Endpoints["token"];
        public static Endpoint TokenGetUserEndpoint => Endpoints["token"];
        public static Endpoint AdvertisementsEndpoint => Endpoints["advertisements"];
    }

    public class Endpoint
    {
        public Uri Uri { get; }
        public bool RequiresAuthorization { get; }

        public Endpoint(String uri, bool requiresAuthorization)
        {
            Uri = new Uri(uri, UriKind.Relative);
            RequiresAuthorization = requiresAuthorization;
        }

        public Endpoint(Uri uri, bool requiresAuthorization)
        {
            Uri = uri;
            RequiresAuthorization = requiresAuthorization;
        }
    }
}
