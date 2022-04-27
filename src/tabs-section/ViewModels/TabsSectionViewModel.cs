using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using tabs_section.ViewComponents;

namespace tabs_section.ViewModels
{
    public class TabsSectionViewModel
    {
        public string FirstTabLabel { get; set; }

        public string FirstTabSectionCss { get; set; }

        public IList<string> FirstTabSectionProportions { get; set; }

        public string SecondTabLabel { get; set; }

        public string SecondTabSectionCss { get; set; }

        public IList<string> SecondTabSectionProportions { get; set; }

        public ICompositeViewComponentContext<TabsSectionEntity> Context { get; set; }
    }
}
