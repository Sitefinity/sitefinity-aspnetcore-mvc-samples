using System.ComponentModel;
using Progress.Sitefinity.AspNetCore;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;

namespace form_hidden_input.Entities
{
    public class FormHiddenInputEntity
    {
        /// <summary>
        /// Label only used for edit mode and to be shown when managing form entries.
        /// </summary>
        [ContentSection(Constants.ContentSectionTitles.LabelsAndContent, 1)]

        [DefaultValue("Untitled")]
        public string Label { get; set; }

        public string Value { get; set; } = default!;

        /// <summary>
        /// Used to store the type of persistence that is required for this field.
        /// </summary>
        [Browsable(false)]
        public string SfFieldType { get; set; }

        /// <summary>
        /// Used to store the name of the field.
        /// </summary>
        [Browsable(false)]
        public string SfFieldName { get; set; }
    }
}
