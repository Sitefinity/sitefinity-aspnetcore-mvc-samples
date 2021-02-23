using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using sitefinity_data.Dto;

namespace sitefinity_data.ViewModels.SitefinityData
{
    public class ItemGroup
    {
        public string Name { get; set; }

        public IList<ItemViewModel> Items { get; set; }
    }
}
