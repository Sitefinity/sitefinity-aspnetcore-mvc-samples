using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace content_selectors.Entities.SelectorDemoUsage
{
	/// <summary>
	/// Describes the external URLs entity.
	/// </summary>
	public class ExternalUrlEntity
	{
		/// <summary>
		/// Gets or sets the title.
		/// </summary>
		[DisplayName("Title")]
		public string Title { get; set; }

		/// <summary>
		/// Gets or sets the URL.
		/// </summary>
		[DisplayName("Url")]
		public string Url { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether to open link in new window.
		/// </summary>
		[DisplayName("Open link in a new window")]
		[DataType(customDataType: KnownFieldTypes.ChipChoice)]
		[DefaultValue(false)]
		[Choice("[{\"Title\":\"Yes\",\"Name\":\"Yes\",\"Value\":\"True\",\"Icon\":null},{\"Title\":\"No\",\"Name\":\"No\",\"Value\":\"False\",\"Icon\":null}]")]
		public bool OpenInNewWindow { get; set; }
	}
	}