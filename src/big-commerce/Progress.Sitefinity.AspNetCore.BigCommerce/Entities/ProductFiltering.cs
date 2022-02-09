using System.Collections.Generic;
using System.ComponentModel;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Entities
{
    /// <summary>
    /// The entity for the Product list widget.
    /// </summary>
    public enum ProductFilter
    {
        MostPopular,

        New,

        SelectedCategory
    }
}
