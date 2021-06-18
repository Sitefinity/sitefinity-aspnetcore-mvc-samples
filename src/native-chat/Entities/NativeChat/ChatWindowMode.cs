using Progress.Sitefinity.Renderer;
using Progress.Sitefinity.Renderer.Designers;
using Progress.Sitefinity.Renderer.Designers.Attributes;
using Progress.Sitefinity.Renderer.Entities.Content;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace native_chat.Entities.NativeChat
{
    public enum ChatWindowMode
    {
        [Description("Display modal")]
        Modal,
        [Description("Display inline")]
        Inline
    }
}
