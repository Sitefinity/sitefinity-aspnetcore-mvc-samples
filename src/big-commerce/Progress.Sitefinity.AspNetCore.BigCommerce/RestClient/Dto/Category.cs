using Newtonsoft.Json;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonProperty("custom_url")]
        public CustomUrl CustomUrl { get; set; }
    }

    public class CustomUrl
    {
        public string Url { get; set; }
    }
}