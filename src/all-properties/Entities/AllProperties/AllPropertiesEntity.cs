using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using Progress.Sitefinity.RestSdk;

namespace all_properties.Entities.AllProperties
{
    public class AllPropertiesEntity
    {
        [Browsable(false)]
        public string NotBrowsable { get; set; }

        [Category(PropertyCategory.Advanced)]
        [Url(ErrorMessage = "Enter valid URL.")]
        [DisplayName("URL field")]
        public string UrlField { get; set; }

        [EmailAddress(ErrorMessage = "Enter valid email.")]
        [DisplayName("Email field")]
        public string EmailField { get; set; }

        [Placeholder("This is the placeholder")]
        public string PlaceHolderProp { get; set; }

        [DisplayName("Plain description")]
        [Category(PropertyCategory.Advanced)]
        [ContentSection("Main")]
        [Description("This is the description")]
        public string DescriptionProp { get; set; }

        [DisplayName("Description")]
        [DescriptionExtended("This is the extended description", InlineDescription = "Inline description", InstructionalNotes = "Instructional notes")]
        public string DescriptionExtendedProp { get; set; }

        [Copy(Exclude = true)]
        public int ExcludedFromCopyProp { get; set; }

        [Range(1, 20, ErrorMessage = "Стойност между {1} и {2}.")]
        public double DoubleValidation { get; set; }

        [DataType(customDataType: KnownFieldTypes.Html)]
        [Placeholder("This is the placeholder")]
        public string HtmlField { get; set; }

        [DefaultValue("#AABBCC")]
        [DataType(customDataType: KnownFieldTypes.Color)]
        public string ColorField { get; set; }

        #region views

        [ViewSelector("AllProperties")]
        public string ViewName { get; set; }

        [DefaultValue("Second")]
        [ViewSelector("AllProperties")]
        public string ViewNameDefaultValue { get; set; }

        [DefaultValue("invalid")]
        [ViewSelector("AllProperties")]
        public string ViewNameDefaultValueInvalid { get; set; }

        #endregion

        #region links

        public LinkModel Link { get; set; }

        [Required]
        public LinkModel LinkRequired { get; set; }

        #endregion

        #region strings

        [StringLength(6, ErrorMessage = "Value should be between {1} and {2} symbols.", MinimumLength = 2)]
        [DisplayName("String length validation")]
        public string StringLengthValidation { get; set; }

        [MinLength(2, ErrorMessage = "The message length must be at least {1} symbols.")]
        [MaxLength(10, ErrorMessage = "The message must be less than {1} symbols.")]
        public string StringValidation { get; set; }

        [Required(ErrorMessage = "This is required string {link}.")]
        public string StringFieldRequired { get; set; }

        [DefaultValue("custom")]
        public string DefaultValueString { get; set; }

        public string PlainString { get; set; }

        [Mirror(nameof(AllPropertiesEntity.PlainString))]
        public string MirroredField { get; set; }

        #endregion

        #region dates

        [Required(ErrorMessage = "This is required date.")]
        public DateTime DateFieldRequired { get; set; }

        [DefaultValue("2020-09-11T06:38:11.170Z")]
        public DateTime? NullableDateWithDefault { get; set; }
        public DateTime? NullableDate { get; set; }

        [DefaultValue("2020-09-11T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeFromString { get; set; }

        [DefaultValue(typeof(DateTime), "2020-09-11T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeFromDate { get; set; }

        [DefaultValue("2020-13-09T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeInvalid { get; set; }

        #endregion

        #region int

        [Required(ErrorMessage = "This is required int.")]
        public int IntFieldRequired { get; set; }

        [Range(1, 20, ErrorMessage = "Value should be between {1} and {2}.")]
        public int IntegerValidation { get; set; }

        [DefaultValue(42)]
        public int IntDefaultValue { get; set; }

        public int PlainInt { get; set; }

        public int? NullableInt { get; set; }

        [DefaultValue(42)]
        public int? NullableIntWithDefault { get; set; }

        #endregion

        #region bool

        [Required(ErrorMessage = "This is required bool.")]
        public bool BoolFieldRequired { get; set; }

        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BoolFieldRequired\",\"operator\":\"Equals\",\"value\":\"true\"}]}")]
        public bool ConditionalProp { get; set; }

        [DefaultValue(true)]
        public bool BoolDefaultValue { get; set; }

        public bool PlainBool { get; set; }

        public bool? NullableBool { get; set; }

        [DefaultValue(true)]
        public bool? NullableBoolWithDefault { get; set; }

        [DisplayName("Checkbox field")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool CheckboxField { get; set; }

        #endregion

        #region enums

        [DefaultValue(EnumSingle.Value2)]
        public EnumSingle EnumDefaultValue { get; set; }

        [DefaultValue("Vaal")]
        public EnumSingle EnumDefaultValueInvalid { get; set; }

        [DefaultValue("Vall")]
        public EnumMultiple EnumDefaultValueMultipleInvalid { get; set; }

        [DefaultValue(EnumMultiple.Value1 | EnumMultiple.Value2)]
        public EnumMultiple EnumDefaultValueMultiple { get; set; }

        public EnumSingle EnumNoDefaultValue { get; set; }

        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public EnumSingle EnumChipChoice { get; set; }

        #endregion

        #region complex

        public ComplexObject Complex { get; set; }

        [TableView("TableTitle")]
        public ComplexObject ComplexTable { get; set; }

        public MultiLevelComplexObject MultiLevelComplexObject { get; set; }

        #endregion

        #region collections

        public IList<string> List { get; set; }

        public IList<ComplexObject> ListComplexObject { get; set; }

        public IList<ComplexObjectNoDefaults> ListComplexObjectNoDefaults { get; set; }

        #endregion

        #region selectors

        [Content(Type = KnownContentTypes.Images, AllowMultipleItemsSelection = true)]
        public MixedContentContext Images { get; set; }

        [Content(Type = KnownContentTypes.Pages)]
        public MixedContentContext Pages { get; set; }

        [Content(Type = KnownContentTypes.News)]
        public MixedContentContext News { get; set; }

        [Content(Type = KnownContentTypes.News, LiveData = true)]
        public MixedContentContext NewsLive { get; set; }

        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false)]
        public MixedContentContext Page { get; set; }

        [Content(Type = "Telerik.Sitefinity.DynamicTypes.Model.Pressreleases.PressRelease")]
        public MixedContentContext PressReleases { get; set; }

        [TaxonomyContent(Type = KnownContentTypes.Tags)]
        public MixedContentContext Tags { get; set; }

        [TaxonomyContent(Type = KnownContentTypes.Categories)]
        public MixedContentContext Categories { get; set; }

        [TaxonomyContent(Type = "geographical-regions")]
        public MixedContentContext CustomTaxonomy { get; set; }

        #endregion
    }

    public class ComplexObject
    {
        [DisplayName("String prop")]
        [DefaultValue("test")]
        public string ChildString { get; set; }

        [DisplayName("Int prop")]
        [DefaultValue(42)]
        public int ChildInt { get; set; }
    }

    public class ComplexObjectNoDefaults
    {
        [DisplayName("Boolean prop")]
        public bool BoolProp { get; set; }

        [DisplayName("Int prop")]
        public int ChildInt { get; set; }
    }

    public class MultiLevelComplexObject
    {
        [DisplayName("String prop")]
        [DefaultValue("testouter")]
        public string ChildString { get; set; }

        [DisplayName("Child complex prop")]
        public ComplexObject ChildComplexObject { get; set; }
    }

    public enum EnumSingle
    {
        Value1,

        Value2,

        Value3
    }

    [Flags]
    public enum EnumMultiple
    {

        Value1 = 0x01,
        Value2 = 0x02,
        Value3 = 0x04
    }
}
