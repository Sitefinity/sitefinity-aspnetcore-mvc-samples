using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Web;
using Renderer.Entities;
using Renderer.ViewModels;

namespace Renderer.Models
{
    public interface IMegaMenuModel
    {
        Task<MegaMenuViewModel> InitializeViewModel(MegaMenuEntity entity, IRenderContext renderContext);
    }
}