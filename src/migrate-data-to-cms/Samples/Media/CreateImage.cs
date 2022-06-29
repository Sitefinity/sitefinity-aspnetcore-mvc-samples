using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples.Media
{
    /// <summary>
    /// Basic sample that demonstrates how to create and upload an image.
    /// </summary>
    internal class CreateImage : ISample
    {
        public async Task Run(IRestClient restClient)
        {
            // default image library id
            var libraryId = "4BA7AD46-F29B-4e65-BE17-9BF7CE5BA1FB";

            var headers = GetUploadHeaders("image.jpg", libraryId);

            var item = await restClient.CreateItem<ImageDto>(new CreateArgs()
            {
                Data = GetBase64Content(TestFileHelper.ImageFile1),
                ContentType = "image/jpeg",
                Encoding = "base64",
                AdditionalHeaders = headers,
            });

            Console.WriteLine($"Created image item with Id - {item.Id}");
        }

        /// <summary>
        /// Gets the initial upload headers.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="parentId">The parent id.</param>
        /// <returns></returns>
        private static Dictionary<string, string> GetUploadHeaders(string fileName, string parentId)
        {
            var additionalHeaders = new Dictionary<string, string>();
            var headersJson = JObject.FromObject(new
            {
                Title = Guid.NewGuid().ToString(),
                AlternativeText = Guid.NewGuid().ToString(),
                ParentId = parentId,
                DirectUpload = true,
            }).ToString(Formatting.None);
            additionalHeaders.Add("X-Sf-Properties", headersJson);
            additionalHeaders["X-File-Name"] = fileName;

            return additionalHeaders;
        }

        /// <summary>
        /// Gets the cotents of the file as a string.
        /// </summary>
        /// <param name="path">The path to the embedded resource.</param>
        /// <returns></returns>
        private static string GetBase64Content(string path)
        {
            System.IO.Stream stream = TestFileHelper.GetEmbeddedFile(path);

            byte[] byteArray;
            using (BinaryReader br = new BinaryReader(stream))
            {
                byteArray = br.ReadBytes((int)stream.Length);
            }

            return Convert.ToBase64String(byteArray);
        }
    }
}
