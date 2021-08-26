using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.RestSdk;

namespace Renderer.Entities.Document
{
    public class DocumentEntity
    {
        [Content(Type = KnownContentTypes.Documents)]
        public MixedContentContext Documents { get; set; }
    }
}
