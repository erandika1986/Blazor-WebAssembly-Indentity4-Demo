using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.Product
{
    public partial class Products
    {
        private List<ProductDto> _products = new List<ProductDto>();

        private MetaData _metaData  = new MetaData();

        private ProductMasterDataDto _masterData = new ProductMasterDataDto();

        private ProductFilterParam _filterParam = new ProductFilterParam()
        {
            CurrentPage = 1,
            OrderBy = string.Empty,
            PageSize = 25,
            SelectedProductCategoryId = "0",
            SearchTerm = string.Empty,
            
        };

        [Parameter]
        public string CategoryId { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _masterData = await ProductService.GetProductMasterData();

            await GetProducts();
        }

        private async Task SelectedPage(int page)
        {
            _filterParam.CurrentPage = page;
            await GetProducts();
        }

        private async Task SearchChanged(string searchTerm)
        {

            _filterParam.CurrentPage = 1;
            _filterParam.SearchTerm = searchTerm;
            await GetProducts();
        }

        private async Task SortChanged(string orderBy)
        {
            _filterParam.OrderBy = orderBy;
            _filterParam.CurrentPage = 1;
            await GetProducts();
        }

        private async Task ProductCategoryChange(string productCategoryId)
        {
            _filterParam.SelectedProductCategoryId = productCategoryId;
            _filterParam.CurrentPage = 1;
            await GetProducts();
        }


        private async Task DeleteProduct(int id)
        {
            await ProductService.DeleteProduct(id);
            _filterParam.CurrentPage = 1;
            await GetProducts();
        }

        private async Task GetProducts()
        {
            var response = await ProductService.GetProducts(_filterParam);
            _products = response.Items.ToList();
            _metaData = response.MetaData;
        }
    }
}
