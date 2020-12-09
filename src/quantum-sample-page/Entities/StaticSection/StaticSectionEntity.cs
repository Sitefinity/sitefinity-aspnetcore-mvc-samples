namespace Renderer.Entities.StaticSection
{
    public class StaticSectionEntity
    {
        public ViewType ViewType { get; set; }
    }

    public enum ViewType
    {
        Container,
        ContainerFluid,
        TwoMixed,
        ThreeAutoLayout
    }
}