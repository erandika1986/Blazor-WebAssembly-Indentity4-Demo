using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
using System.Text.Json;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;

        public ProductCategoryService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task DeleteProductCategory(int productCategoryId)
        {
            try
            {
                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.DeleteAsync("ProductCategory/deleteProductCategory/" + productCategoryId);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unknown error occurred.");
            }
        }

        public async Task<List<ProductCategoryDto>> GetAllProductCategories()
        {
            try
            {
                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.GetAsync("ProductCategory/getAllProductCategories");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var pagingResponse = JsonSerializer.Deserialize<List<ProductCategoryDto>>(content, _options);

                return pagingResponse;
            }
            catch (Exception ex)
            {
                return new List<ProductCategoryDto> ();
            }
        }

        public async Task<ProductCategoryDto> GetProductCategoryById(int id)
        {
            try
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["id"] = id.ToString(),
                };

                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("ProductCategory/getProductCategoryById", queryStringParam));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var responseDto = JsonSerializer.Deserialize<ProductCategoryDto>(content, _options);

                return responseDto;
            }
            catch (Exception ex)
            {
                return new ProductCategoryDto();
            }
        }

        public async Task SaveProductCategory(ProductCategoryDto productCategory)
        {
            var content = JsonSerializer.Serialize(productCategory);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpClient = _clientFactory.CreateClient("productApi");

            var putResult = await httpClient.PostAsync("ProductCategory/saveProductCategory", bodyContent);

            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
