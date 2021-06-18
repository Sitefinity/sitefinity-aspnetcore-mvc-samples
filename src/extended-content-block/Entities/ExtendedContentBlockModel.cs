using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.AspNetCore.SitefinityApi;
using Progress.Sitefinity.AspNetCore.Widgets.Models.ContentBlock;

namespace extended_content_block.Entities
{
    /// <summary>
    /// Extended entity class for the ContentBlock view component.
    /// </summary>
    public class ExtendedContentBlockEntity : ContentBlockEntity
	{
        /// <summary>
        /// Gets or sets the custom prop.
        /// </summary>
		public string CustomProp { get; set; }
	}
}
