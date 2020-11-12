using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Renderer.Dto;

namespace Renderer.ViewModels.Testimonial
{
    public class ItemGroup
    {
        public string Name { get; set; }

        public IList<Item> Items { get; set; }
    }
}