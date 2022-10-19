using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Progress.Sitefinity.Renderer.Entities.Content;
using Progress.Sitefinity.AspNetCore.ViewComponents.AttributeConfigurator.Attributes;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Models;
using Progress.Sitefinity.Renderer.Entities;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.AspNetCore.Models;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace all_properties.Entities.AllProperties
{
    public class AllPropertiesEntity
    {
        /// <summary>
        /// Gets or sets a property that is not browsable.
        /// </summary>
        [ReadOnly(true)]
        public string Readonly { get; set; }

        /// <summary>
        /// Gets or sets a property that is not browsable.
        /// </summary>
        [Browsable(false)]
        public string NotBrowsable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a boolean property is true or false.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [Url(ErrorMessage = "Enter valid URL.")]
        [DisplayName("URL field")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:URI-like properties should not be strings", Justification = "Entity")]
        public string UrlField { get; set; }

        /// <summary>
        /// Gets or sets a value in the email property.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value in the double field property.
        /// </summary>
        [Range(1, 20, ErrorMessage = "Стойност между {1} и {2}.")]
        public double DoubleValidation { get; set; }

        [DataType(customDataType: KnownFieldTypes.Html)]
        [Placeholder("This is the placeholder")]
        public string HtmlField { get; set; }

        [DataType(customDataType: KnownFieldTypes.TextArea)]
        [DefaultValue("This is some text.")]
        public string TextAreaField { get; set; }

        [DataType(customDataType: KnownFieldTypes.Password)]
        public string Password { get; set; }

        [ColorPalette("Default")]
        public string ColorField { get; set; }

        [DataType(customDataType: KnownFieldTypes.Range)]
        [Suffix("MB")]
        public NumericRange Range { get; set; }

        [DataType(customDataType: KnownFieldTypes.FileTypes)]
        [DisplayName("File types")]
        public FileTypes FileTypes { get; set; }

        #region views

        /// <summary>
        /// Gets or sets the view name.
        /// </summary>
        [ViewSelector]
        public string ViewName { get; set; }

        [DefaultValue("Second")]
        [ViewSelector("AllProperties")]
        public string ViewNameDefaultValue { get; set; }

        [DefaultValue("invalid")]
        [ViewSelector]
        public string ViewNameDefaultValueInvalid { get; set; }

        #endregion

        #region links

        [DynamicLinksContainer]
        public ComplexObjectWithLinks ObjectWithLinks { get;set;}

        [DynamicLinksContainer]
        public IList<ComplexObjectWithLinks> ArrayOfObjectWithLinks { get;set;}

        public LinkModel Link { get; set; }

        [Required]
        public LinkModel LinkRequired { get; set; }

        #endregion

        #region strings

        /// <summary>
        /// Gets or sets a value in the detail css class property.
        /// </summary>
        [StringLength(6, ErrorMessage = "Value should be between {1} and {2} symbols.", MinimumLength = 2)]
        [DisplayName("String length validation")]
        public string StringLengthValidation { get; set; }

        /// <summary>
        /// Gets or sets a value in the test message property.
        /// </summary>
        [MinLength(2, ErrorMessage = "The message length must be at least {1} symbols.")]
        [MaxLength(10, ErrorMessage = "The message must be less than {1} symbols.")]
        public string StringValidation { get; set; }

        /// <summary>
        /// Gets or sets a value in the detail css class property.
        /// </summary>
        [Required(ErrorMessage = "This is required string {link}.")]
        public string StringFieldRequired { get; set; }

        /// <summary>
        /// Gets or sets the string default value.
        /// </summary>
        [DefaultValue("custom")]
        [ContentSection(FirstBasicSection, 0)]
        public string DefaultValueString { get; set; }

        /// <summary>
        /// Gets or sets the string default value.
        /// </summary>
        [DefaultValue("custom")]
        [FallbackToDefaultValueWhenEmpty]
        [ContentSection(FirstBasicSection, 1)]
        public string DefaultValueStringWithFallback { get; set; }

        [ContentSection(FirstBasicSection, 0)]
        public string PlainString { get; set; }

        [Mirror(nameof(AllPropertiesEntity.PlainString))]
        [ContentSection(SecondBasicSection, 2)]
        public string MirroredField { get; set; }

        #endregion

        #region dates

        /// <summary>
        /// Gets or sets a value in the date property.
        /// </summary>
        [Required(ErrorMessage = "This is required date.")]
        public DateTime DateFieldRequired { get; set; }

        [DefaultValue("2020-09-11T06:38:11.170Z")]
        [ContentSection(SecondBasicSection, 3)]
        public DateTime? NullableDateWithDefault { get; set; }
        public DateTime? NullableDate { get; set; }

        /// <summary>
        /// Default value is specified as ISO8601 format
        /// </summary>
        [DefaultValue("2020-09-11T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeFromString { get; set; }

        /// <summary>
        /// Default value is specified as ISO8601 format
        /// </summary>
        [DefaultValue(typeof(DateTime), "2020-09-11T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeFromDate { get; set; }

        /// <summary>
        /// Default value is specified as ISO8601 format
        /// </summary>
        [DefaultValue("2020-13-09T06:38:11.170Z")]
        public DateTime DefaultValueDateTimeInvalid { get; set; }

        #endregion

        #region int

        /// <summary>
        /// Gets or sets a value in the int property.
        /// </summary>
        [Required(ErrorMessage = "This is required int.")]
        public int IntFieldRequired { get; set; }

        /// <summary>
        /// Gets or sets a value in the integer field property.
        /// </summary>
        [Range(1, 20, ErrorMessage = "Value should be between {1} and {2}.")]
        public int IntegerValidation { get; set; }

        /// <summary>
        /// Gets or sets the int default value.
        /// </summary>
        [DefaultValue(42)]
        public int IntDefaultValue { get; set; }

        public int PlainInt { get; set; }

        public int? NullableInt { get; set; }

        [DefaultValue(42)]
        public int? NullableIntWithDefault { get; set; }

        #endregion

        #region bool

        /// <summary>
        /// Gets or sets a value in the int property.
        /// </summary>
        [Required(ErrorMessage = "This is required bool.")]
        public bool BoolFieldRequired { get; set; }

        [ConditionalVisibility("{\"conditions\":[{\"fieldName\":\"BoolFieldRequired\",\"operator\":\"Equals\",\"value\":\"true\"}]}")]
        public bool ConditionalProp { get; set; }

        /// <summary>
        /// Gets or sets the bool default value.
        /// </summary>
        [DefaultValue(true)]
        public bool BoolDefaultValue { get; set; }

        public bool PlainBool { get; set; }

        public bool? NullableBool { get; set; }

        [DefaultValue(true)]
        public bool? NullableBoolWithDefault { get; set; }

        [Group("Options")]
        [DisplayName("Checkbox field")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool CheckboxField { get; set; }

        [Group("Options")]
        [DisplayName("Checkbox field")]
        [DefaultValue(true)]
        [DataType(customDataType: KnownFieldTypes.CheckBox)]
        public bool CheckboxField2 { get; set; }

        #endregion

        #region enums

        [DefaultValue(EnumSingle.Value2)]
        public EnumSingle EnumDefaultValue { get; set; }

        [DefaultValue(EnumSingle.Value2)]
        public EnumSingle? NullableEnumDefaultValue { get; set; }

        [DefaultValue("Vaal")]
        public EnumSingle EnumDefaultValueInvalid { get; set; }

        [DefaultValue("Vall")]
        public EnumMultiple EnumDefaultValueMultipleInvalid { get; set; }

        [DefaultValue(EnumMultiple.Value1 | EnumMultiple.Value2)]
        public EnumMultiple EnumDefaultValueMultiple { get; set; }

        [DefaultValue(EnumMultiple.Value1 | EnumMultiple.Value2)]
        public EnumMultiple? NullableEnumDefaultValueMultiple { get; set; }

        public EnumSingle EnumNoDefaultValue { get; set; }

        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        public EnumSingle EnumChipChoice { get; set; }

        #endregion

        #region complex

        public ComplexObject Complex { get; set; }

        [TableView("TableTitle")]
        public ComplexObject ComplexTable { get; set; }

        public MultiLevelComplexObject MultiLevelComplexObject { get; set; }

        [TableView("BigTableTitle")]
        public BigComplexObject BigComplexTable { get; set; }


        #endregion

        #region collections

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IDictionary<string, ComplexObject> DictionaryWithInitializer { get; set; } = new Dictionary<string, ComplexObject>();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IDictionary<string, ComplexObject> Dictionary { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IDictionary<ComplexObject, ComplexObject> DictionaryComplexKey { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IList<string> List { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IEnumerable<bool> ListBool { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IList<ComplexObject> ListComplexObject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        [TableView(Reorderable = true, Selectable = true, MultipleSelect = true)]
        public IList<ComplexObject> ListTableView { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IList<ComplexObjectNoDefaults> ListComplexObjectNoDefaults { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Entity")]
        public IList<MultiLevelComplexObject> ListMultiLevelComplexObject { get; set; }

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

        [Content(Type = KnownContentTypes.Pages, AllowMultipleItemsSelection = false, DisableInteraction = true, ShowSiteSelector = true)]
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

        [DisplayName("Enum as radio choice field")]
        [DataType(customDataType: KnownFieldTypes.RadioChoice)]
        public EnumSingle EnumRadioChoice { get; set; }

        [DefaultValue(1)]
        [DisplayName("Integer as choice field")]
        [DataType(customDataType: KnownFieldTypes.ChipChoice)]
        [Choice("[{\"Title\":\"1 level\",\"Name\":\"1\",\"Value\":1,\"Icon\":null},{\"Title\":\"2 levels\",\"Name\":\"2\",\"Value\":2,\"Icon\":null},{\"Title\":\"3 levels\",\"Name\":\"3\",\"Value\":3,\"Icon\":null}]")]
        public int? IntAsChoice { get; set; }

        [DefaultValue(1)]
        [DisplayName("Integer as multiple choice field")]
        [DataType(customDataType: KnownFieldTypes.Choices)]
        [Choice("[{\"Title\":\"1 level\",\"Name\":\"1\",\"Value\":1,\"Icon\":null},{\"Title\":\"2 levels\",\"Name\":\"2\",\"Value\":2,\"Icon\":null},{\"Title\":\"3 levels\",\"Name\":\"3\",\"Value\":3,\"Icon\":null}]")]
        public int? IntAsDropdownChoice { get; set; }

        [Content]
        public MixedContentContext AllTypesSelector { get; set; }


        [Category(PropertyCategory.Advanced)]
        [ContentSection(FirstAdvancedSection, 3)]
        public string AFirstAdvancedProp { get; set; }

        [Category(PropertyCategory.Advanced)]
        [ContentSection(FirstAdvancedSection, 0)]
        public string BFirstAdvancedProp { get; set; }

        [Category(PropertyCategory.Advanced)]
        [ContentSection(SecondAdvancedSection, 0)]
        public string ASecondAdvancedProp { get; set; }

        [Category(PropertyCategory.Advanced)]
        [ContentSection(SecondAdvancedSection, 2)]
        public string BSecondAdvancedProp { get; set; }

        /// <summary>
        /// Gets or sets the attributes for the content list.
        /// </summary>
        [Category(PropertyCategory.Advanced)]
        [ContentSection(Constants.ContentSectionTitles.Attributes, 2)]
        [DisplayName("Attributes for...")]
        [DataType(customDataType: KnownFieldTypes.Attributes)]
        [LengthDependsOn(null, "", "", ExtraRecords = "[{\"Name\": \"AllPropertiesWidget\", \"Title\": \"AllPropertiesWidget\"}]")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2227:Collection properties should be read only", Justification = "Must be able to set in property editor.")]
        public IDictionary<string, IList<AttributeModel>> Attributes { get; set; }

        private const string FirstBasicSection = "First basic section";
        private const string SecondBasicSection = "Second basic section";
        private const string FirstAdvancedSection = "First advanced section";
        private const string SecondAdvancedSection = "Second advanced section";
    }

    public class ComplexObjectWithLinks
    {
        public LinkModel Link { get; set; }
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
        public bool? BoolProp { get; set; }

        [DisplayName("Int prop")]
        public int? ChildInt { get; set; }
    }

    public class BigComplexObject
    {
        [DisplayName("String prop1")]
        [DefaultValue("test1")]
        public string ChildString1 { get; set; }

        [DisplayName("String prop2")]
        [DefaultValue("test2")]
        public string ChildString2 { get; set; }

        [DisplayName("String prop3")]
        [DefaultValue("test3")]
        public string ChildString3 { get; set; }

        [DisplayName("String prop4")]
        [DefaultValue("test4")]
        public string ChildString4 { get; set; }

        [DisplayName("String prop5")]
        [DefaultValue("test5")]
        public string ChildString5 { get; set; }

        [DisplayName("String prop6")]
        [DefaultValue("test6")]
        public string ChildString6 { get; set; }
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
        Value3 = 0x04,
        [Browsable(false)]
        Value4 = 0x08,
    }
}
