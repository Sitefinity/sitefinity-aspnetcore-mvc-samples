using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Client;

using System.Net.Http;
using System.Net.Http.Headers;

namespace migrate_data_to_cms
{
    public class SitefinityConfig
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string Url { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }
}
