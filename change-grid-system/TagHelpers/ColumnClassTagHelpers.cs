using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace change_grid_system.TagHelpers
{
    /// <summary>
    /// Builds the column class.
    /// </summary>
    [HtmlTargetElement(Attributes = "columnClass")]
    public class ColumnClassTagHelpers : TagHelper
    {
        /// <summary>
        /// Gets or sets the number of columns in the grid.
        /// </summary>
        public string GridSize { get; set; }

        /// <summary>
        /// Gets or sets the additional classes.
        /// </summary>
        public string AdditionalClass { get; set; }

        /// <summary>
        /// Gets or sets the number of grid columns per row column.
        /// </summary>
        public string CssColsPerRowCol  { get; set; }

        /// <summary>
        /// Processes the output.
        /// </summary>
        /// <param name="context">The view context.</param>
        /// <param name="output">The processed output.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (output != null)
            {
                output.Attributes.RemoveAll("columnClass");
                var cls = string.Concat(this.AdditionalClass, " w-", this.CssColsPerRowCol , "/", this.GridSize);
                output.Attributes.SetAttribute("class", cls);
            }
        }
    }
}
