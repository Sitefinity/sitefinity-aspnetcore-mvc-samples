using Progress.Sitefinity.Renderer;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System;

namespace Renderer.Entities.NativeChat
{
    [Flags]
    public enum ChatPickers
    {
        [Description("File picker")]
        FilePicker = 0x01,
        [Description("Location picker")]
        LocationPicker = 0x02
    }
}
