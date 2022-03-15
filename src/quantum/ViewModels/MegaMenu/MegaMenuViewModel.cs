using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using Renderer.Entities;

namespace Renderer.ViewModels
{
    public class MegaMenuViewModel
    {
        public string FirstPageId { get; set; }

        public string FirstSectionCss { get; set; }

        public IList<string> FirstSectionProportions { get; set; }

        public string SecondPageId { get; set; }

        public string SecondSectionCss { get; set; }

        public IList<string> SecondSectionProportions { get; set; }

        public string ThirdPageId { get; set; }

        public string ThirdSectionCss { get; set; }

        public IList<string> ThirdSectionProportions { get; set; }

        public bool HideSectionsInEdit { get; set; }

        public NavigationViewModel NavigationViewModel { get; set; }

        public ICompositeViewComponentContext<MegaMenuEntity> Context { get; set; }
    }
}