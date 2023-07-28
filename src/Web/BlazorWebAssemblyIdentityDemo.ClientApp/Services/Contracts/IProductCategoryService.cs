using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public interface IProductCategoryService
    {
        Task SaveProductCategory(ProductCategoryDto productCategory);

        Task DeleteProductCategory(int productCategoryId);

        Task<ProductCategoryDto> GetProductCategoryById(int id);

        Task<List<ProductCategoryDto>> GetAllProductCategories();
    }
}
