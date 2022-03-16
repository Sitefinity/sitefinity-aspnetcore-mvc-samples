using migrate_data_to_cms.Samples;
using migrate_data_to_cms.Samples.Relations;

namespace migrate_data_to_cms
{
    public class Program
    {
        public static void Main()
        {
            var restClient = RestClientFactory.GetRestClient().Result;

            var samples = new ISample[] { new BasicSample(), new RelateMedia() };
            foreach (var sample in samples)
            {
                sample.Run(restClient).Wait();
            }
        }
    }
}
