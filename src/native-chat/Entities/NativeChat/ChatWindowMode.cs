using System.ComponentModel;

namespace native_chat.Entities.NativeChat
{
    public enum ChatWindowMode
    {
        [Description("Display modal")]
        modal,
        [Description("Display inline")]
        inline
    }
}
