using System.Collections.Generic;
using System.Threading.Tasks;
using Progress.Sitefinity.RestSdk.Dto;
using Renderer.Entities.Document;

namespace Renderer.Models.Document
{
    /// <summary>
    /// The model interface.
    /// </summary>
    public interface IDocumentModel
    {
        /// <summary>
        /// Gets the view models.
        /// </summary>
        /// <returns>The generated view models.</returns>
        Task<IList<DocumentDto>> GetViewModels(DocumentEntity entity);
    }
}