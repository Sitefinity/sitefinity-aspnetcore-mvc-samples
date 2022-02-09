namespace Progress.Sitefinity.AspNetCore.BigCommerce.RestClient
{
    public class ResponseWrapper<T>
    {
        public T[] Data { get; set; }

        public ResponseMeta Meta { get; set; }
    }
}