using Newtonsoft.Json;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public class PaginationMeta
    {
        public int Total { get; set; }

        public int Count { get; set; }

        [JsonProperty("per_page")]
        public int PerPage { get; set; }

        [JsonProperty("current_page")]
        public int CurrentPage { get; set; }

        [JsonProperty("total_pages")]
        public int TotalPages { get; set; }
    }
}