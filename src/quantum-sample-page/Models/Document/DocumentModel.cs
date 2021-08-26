using Progress.Sitefinity.AspNetCore.RestSdk;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.RestSdk.Dto;
using Renderer.Entities.Document;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Renderer.Models.Document
{
    /// <summary>
    /// The model class.
    /// </summary>
    public class DocumentModel : IDocumentModel
    {
        private readonly IRestClient service;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestimonialViewComponent"/> class.
        /// </summary>
        /// <param name="service">The rest service.</param>
        public DocumentModel(IRestClient service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <param name="entity">The entity object.</param>
        /// <returns>The generated view models.</returns>
        public async Task<IList<DocumentDto>> GetViewModels(DocumentEntity entity)
        {
            var response = await this.service.GetItems<DocumentDto>(entity.Documents, new GetAllArgs() { Fields = new List<string>() { "Image", "Title", "Url", "TotalSize", "Extension" } }).ConfigureAwait(true);
            return response.Items.Select(x => this.GetItemViewModel(x)).ToArray();
        }

        private DocumentDto GetItemViewModel(DocumentDto item)
        {
            return item;
        }
    }
}