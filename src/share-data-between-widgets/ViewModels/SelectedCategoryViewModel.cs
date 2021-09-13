using System.Collections.Generic;

namespace ViewModels
{
    public class CategorySelectorViewModel
    {
        public IDictionary<string, string> Categories { get; set; }

        public string SelectedCategory { get; set; }
    }
}
