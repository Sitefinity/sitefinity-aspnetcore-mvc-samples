using System;
using Progress.Sitefinity.RestSdk.Dto;

namespace migrate_data_to_cms.Samples.Choices
{
    [MappedSitefinityType("Telerik.Sitefinity.DynamicTypes.Model.Testimonials.Testimonial")]
    public class TestimonialDto : SdkItem
    {
        
        public SingleChoice ChoicesSingle { get; set; }

        public MultipleChoice ChociesMultiple { get; set; }
    }
}