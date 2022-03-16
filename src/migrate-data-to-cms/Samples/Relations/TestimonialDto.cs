using Progress.Sitefinity.RestSdk.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace migrate_data_to_cms.Samples.Relations
{
    [MappedSitefinityType("Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial")]
    public class TestimonialDto : SdkItem
    {
        public string Title { get; set; }

        public ImageDto[] Photo { get; set; }
    }
}
