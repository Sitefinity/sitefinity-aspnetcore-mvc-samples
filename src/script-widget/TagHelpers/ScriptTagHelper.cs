using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
namespace script_widget
{

    /// <summary>
    /// The helpers for row tags.
    /// </summary>
    [HtmlTargetElement("body")]
    public class ScriptTagHelper : TagHelper
    {
        private IScriptRepository scriptRepository;

        public ScriptTagHelper(IScriptRepository scriptRepository)
        {
            this.scriptRepository = scriptRepository;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (this.scriptRepository.Scripts.TryGetValue(ScriptLocation.BodyTop, out IList<string> scripts))
            {
                foreach (var script in scripts)
                {
                    output.PreContent.AppendHtml(script);
                }
            }

            if (this.scriptRepository.Scripts.TryGetValue(ScriptLocation.BodyBottom, out scripts))
            {
                foreach (var script in scripts)
                {
                    output.Content.AppendHtml(script);
                }
            }
        }
    }
}