using System.Threading.Tasks;
using Progress.Sitefinity.AspNetCore.BigCommerce.Entities;
using Progress.Sitefinity.AspNetCore.BigCommerce.RestClient;
using Progress.Sitefinity.AspNetCore.BigCommerce.ViewModels;
using Progress.Sitefinity.AspNetCore.ViewComponents;

namespace Progress.Sitefinity.AspNetCore.BigCommerce.Models
{
    /// <summary>
    /// Defines model for the Button (CTA) widget.
    /// </summary>
    public interface IProductListModel
    {
        Task<ProductListViewModel> InitializeViewModel(ProductListEntity entity);

        Task<ProductListViewModel> InitializeViewModel(ProductListEntity entity, Category category);
    }
}
