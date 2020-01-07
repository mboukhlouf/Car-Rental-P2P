using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LocationDeVoitures.Helpers
{
    public class ApiHelper
    {
        public static String BaseUrl => "https://localhost:44325";

        public static Uri AdvertisementsUrl = new Uri($"{BaseUrl}/api/Advertisements");
    }
}
