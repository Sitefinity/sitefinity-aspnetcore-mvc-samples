using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.ViewComponents;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Navigation;
using Progress.Sitefinity.AspNetCore.Widgets.Models.Section;
using mega_menu.Entities;

namespace mega_menu.ViewModels
{
    public class MegaMenuViewModel
    {
        public string FirstPageId { get; set; }

        public string SecondPageId { get; set; }

        public string ThirdPageId { get; set; }

        public SectionViewModel SectionViewModel { get; set; }

        public SectionViewModel SecondSectionViewModel { get; set; }

        public SectionViewModel ThirdSectionViewModel { get; set; }

        public NavigationViewModel NavigationViewModel { get; set; }

        public ICompositeViewComponentContext<MegaMenuEntity> Context { get; set; }
    }
}
