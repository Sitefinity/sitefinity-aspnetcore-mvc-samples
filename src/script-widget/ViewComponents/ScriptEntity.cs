using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace script_widget
{
    public class ScriptEntity
    {
        /// <summary>
        /// Gets or sets the location
        /// </summary>
        public ScriptLocation Location { get; set; }
        
        /// <summary>
        /// Gets or sets the script content
        /// </summary>
        [DisplayName("Script")]
        [Description("Put the script with its wrapping tag -> e.g. <script>javascript code</script>")]
        [DataType(customDataType: KnownFieldTypes.TextArea)]
        public string Script { get; set; }
    }

    public enum ScriptLocation
    {
        Inline,
        BodyTop,
        BodyBottom
    }
}
