using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;
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
            try
            {
                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.DeleteAsync("Product/deleteProduct/" + id);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var responseDto = JsonSerializer.Deserialize<ResponseDto>(content, _options);

                return responseDto;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unknown error occurred.");
            }
        }

        public async Task<ProductDto> GetProductById(int id)
        {
            try
            {
                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.GetAsync("Product/getProductById/" + id.ToString());
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var responseDto = JsonSerializer.Deserialize<ProductDto>(content, _options);

                return responseDto;
            }
            catch (Exception ex)
            {
                return new ProductDto();
            }
        }

        public async Task<PaginatedListDto<ProductDto>> GetProducts(ProductFilterParam filterParams)
        {
            try
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["currentPage"] = filterParams.CurrentPage.ToString(),
                    ["pageSize"] = "10",
                    ["searchTerm"] = filterParams.SearchTerm == null ? "" : filterParams.SearchTerm,
                    ["orderBy"] = filterParams.OrderBy,
                    ["selectedProductCategoryId"] = filterParams.SelectedProductCategoryId.ToString()
                };

                var httpClient = _clientFactory.CreateClient("productApi");
                var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("Product/getAllProducts", queryStringParam));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var pagingResponse = JsonSerializer.Deserialize<PaginatedListDto<ProductDto>>(content, _options);

                return pagingResponse;
            }
            catch (Exception ex)
            {
                return new PaginatedListDto<ProductDto>();
            }
        }

        public async Task<ResponseDto> SaveProduct(ProductDto product)
        {
            try
            {
                var content = JsonSerializer.Serialize(product);

                var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

                var httpClient = _clientFactory.CreateClient("productApi");

                var putResult = await httpClient.PostAsync("Product/saveProduct", bodyContent);

                var putContent = await putResult.Content.ReadAsStringAsync();

                if (!putResult.IsSuccessStatusCode)
                {
                    throw new ApplicationException(putContent);
                }

                var pagingResponse = JsonSerializer.Deserialize<ResponseDto>(content, _options);

                return pagingResponse;
            }
            catch (Exception ex)
            {
                return ResponseDto.Failure(new List<string> { "An exception has been occurred." });
            }

        }

        public async Task<string> UploadProductImage(int id, MultipartFormDataContent content)
        {
            var httpClient = _clientFactory.CreateClient("productApi");
            var postResult = await httpClient.PostAsync("Product/uploadProductImage/"+id.ToString(), content);
            var postContent = await postResult.Content.ReadAsStringAsync();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(postContent);
            }
            else
            {
                var imgUrl = Path.Combine("https://localhost:5011/", postContent);
                return imgUrl;
            }
        }
    }
}
