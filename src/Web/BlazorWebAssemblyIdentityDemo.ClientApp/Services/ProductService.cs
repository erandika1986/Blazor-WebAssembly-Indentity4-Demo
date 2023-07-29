using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using System.Text.Json;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public ProductService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ResponseDto> DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<PaginatedListDto<ProductDto>> GetProducts(ProductFilterParam filterParam)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto> SaveProduct(ProductDto product)
        {
            throw new NotImplementedException();
        }
    }
}
