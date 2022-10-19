using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using Progress.Sitefinity.Renderer.Entities;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace color_palettes.ViewComponents.Colors
{
    public class ColorsEntity
    {     
        
        [ColorPalette("Default")]
        public string DefaultColor { get; set; }

        [ColorPalette("Custom")]
        public string CustomColor { get; set; }
    }
}
