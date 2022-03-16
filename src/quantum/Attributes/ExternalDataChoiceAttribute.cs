using Progress.Sitefinity.Renderer.Designers.Attributes;
using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Renderer.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DataContract(Name = "Choice")]
    public class ExternalDataChoiceAttribute : ChoiceAttribute
    {
        public ExternalDataChoiceAttribute()
            : base(null)
        {

        }
    }
}