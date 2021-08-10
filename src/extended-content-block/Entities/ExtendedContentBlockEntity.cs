using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Configuration;
using Progress.Sitefinity.RestSdk;
using Progress.Sitefinity.Renderer.Designers;
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
        [Category(PropertyCategory.Advanced)]
        [DisplayName("Text to append")]
		public string TextToAppend { get; set; }
	}
}
