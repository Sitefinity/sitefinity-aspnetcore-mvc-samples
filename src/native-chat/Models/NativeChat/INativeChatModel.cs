using native_chat.Entities.NativeChat;
using native_chat.ViewModels;
using System.Threading.Tasks;

namespace native_chat.Models
{
    public interface INativeChatModel
    {
        Task<NativeChatViewModel> GetViewModel(NativeChatEntity entity);
    }
}