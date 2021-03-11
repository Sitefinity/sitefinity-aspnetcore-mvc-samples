using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;

namespace custom_section.Entities.StaticSection
{
    public class StaticSectionEntity
    {
        [ViewSelector("StaticSection")]
        public string ViewType { get; set; }
    }
}
