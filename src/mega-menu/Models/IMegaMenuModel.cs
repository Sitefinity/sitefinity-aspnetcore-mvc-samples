using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.Web;
using mega_menu.Entities;
using mega_menu.ViewModels;

namespace mega_menu.Models
{
    public interface IMegaMenuModel
    {
        Task<MegaMenuViewModel> InitializeViewModel(MegaMenuEntity entity, IRenderContext renderContext);
    }
}
