namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public class GetProductArgs
    {
        public int[] CategoryFilter { get; set; }

        public string Sort { get; set; }

        public string SortDirection { get; set; }

        public int? Limit { get; set; }
    }
}