using Renderer.Entities.NativeChat;
using Renderer.ViewModels.NativeChat;
using System.Threading.Tasks;

namespace Renderer.Models.NativeChat
{
    public interface INativeChatModel
    {
        Task<NativeChatViewModel> GetViewModel(NativeChatEntity entity);
    }
}