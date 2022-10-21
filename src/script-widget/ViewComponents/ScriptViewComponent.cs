using System;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace script_widget.ViewComponents
{
    /// <summary>
    /// Test widget with different kind of restrictions for its properties.
    /// </summary>
    [SitefinityWidget]
    public class ScriptViewComponent : ViewComponent
    {
        private IScriptRepository scriptRepository;
        public ScriptViewComponent(IScriptRepository scriptRepository)
        {
            this.scriptRepository = scriptRepository;
        }

        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<ScriptEntity> context)
        {
            if (context.Entity.Location == ScriptLocation.BodyTop || context.Entity.Location == ScriptLocation.BodyBottom)
            {
                this.scriptRepository.RegisterScript(context.Entity.Script, context.Entity.Location);
            }

            context.SetHideEmptyVisual(true);

            return this.View(context.Entity);
        }
    }
}
