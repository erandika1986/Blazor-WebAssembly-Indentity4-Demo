using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public interface IProductService
    {
        Task<PaginatedListDto<ProductDto>> GetProducts(ProductFilterParam filterParam);
        Task<ResponseDto> SaveProduct(ProductDto product);
        Task<ResponseDto> DeleteProduct(int id);
        Task<ProductDto> GetProductById(int id);
    }
}
