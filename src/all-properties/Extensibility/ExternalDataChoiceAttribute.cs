using Progress.Sitefinity.Renderer.Designers.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace all_properties.Extensibility
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [DataContract(Name = "Choice")]
    public class ExternalDataChoiceAttribute : ChoiceAttribute
    {
        public ExternalDataChoiceAttribute()
            : base(null)
        {
        }
    }
}