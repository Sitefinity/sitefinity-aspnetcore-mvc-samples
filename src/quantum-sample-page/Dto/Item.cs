using System;

namespace Renderer.Dto
{
    public class Item
    {
        public Guid Id { get; set; }

        public DateTime LastModified { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime PublicationDate { get; set; }

        public string UrlName { get; set; }

        public string OpenGraphDescription { get; set; }

        public string OpenGraphTitle { get; set; }

        public string MetaTitle { get; set; }

        public string MetaDescription { get; set; }

        public string Title { get; set; }

        public string LongText { get; set; }

        public string ShortText { get; set; }

        public SingleChoice ChoicesSingle { get; set; }

        public MultipleChoice ChociesMultiple { get; set; }

        public decimal Number { get; set; }

        public bool YesNo { get; set; }

        public Guid GUIDField { get; set; }

        public Guid[] ArrayOfGuids { get; set; }

        public DateTime DateAndTime { get; set; }

        public Guid[] Tags { get; set; }

        public Image[] RelatedMediaSingle { get; set; }
    }

    public enum SingleChoice
    {
        FirstChoice,
        SecondChoice,
        ThirdChoice
    }

    [Flags]
    public enum MultipleChoice
    {
        FirstChoice = 0x01,
        SecondChoice = 0x02,
        ThirdChoice = 0x04
    }


    /// <summary>
    /// The image view model.
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Gets or sets the thumbnail url.
        /// </summary>
        public string ThumbnailUrl { get; set; }
    }
}
