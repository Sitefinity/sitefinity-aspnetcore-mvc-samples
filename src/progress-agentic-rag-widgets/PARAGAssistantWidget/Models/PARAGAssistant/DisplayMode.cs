using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace Progress.Sitefinity.AspNetCore.Widgets.Models.SitefinityAssistant
{
    /// <summary>
    /// Chat window DisplayMode enum.
    /// </summary>
    public enum DisplayMode
    {
        /// <summary>
        /// Modal.
        /// </summary>
        [EnumDisplayName("Display overlay")]
        [Description("Display overlay")]
        Modal,

        /// <summary>
        /// Inline.
        /// </summary>
        [EnumDisplayName("Display inline")]
        [Description("Display inline")]
        Inline
    }
}
