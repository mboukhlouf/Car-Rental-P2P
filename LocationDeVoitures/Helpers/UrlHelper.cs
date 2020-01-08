using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace LocationDeVoitures.Helpers
{
    public class UrlHelper
    {
        public static Uri AddQueryParamter(Uri uri, String name, String value)
        {
            var uriBuilder = new UriBuilder(uri);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[name] = value;
            uriBuilder.Query = query.ToString();
            return uriBuilder.Uri;
        }
    }
}
