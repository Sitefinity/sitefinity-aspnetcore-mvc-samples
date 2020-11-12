using System;
using System.Collections.Generic;
using System.Text;

namespace Renderer.Entities.StaticSection
{
    public class StaticSectionEntity
    {
        public ViewType ViewType { get; set; }
    }

    public enum ViewType
    {
        Container,
        ContainerFluid
    }
}