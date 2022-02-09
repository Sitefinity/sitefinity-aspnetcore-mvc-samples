﻿using form_hidden_input.Entities;
using Microsoft.AspNetCore.Mvc;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace form_hidden_input.ViewComponents
{
    [SitefinityFormWidget("Hidden", Title = "Hidden input", EmptyIconText = "Custom empty text", EmptyIcon = "eye-slash")]
    public class FormHiddenInputViewComponent : ViewComponent
    {
        /// <summary>
        /// Invokes the view.
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IViewComponentResult Invoke(IViewComponentContext<FormHiddenInputEntity> context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            return this.View(context.Entity);
        }
    }
}