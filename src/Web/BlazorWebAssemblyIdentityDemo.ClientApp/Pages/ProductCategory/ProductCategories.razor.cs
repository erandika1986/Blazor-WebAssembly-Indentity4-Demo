using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.ProductCategory
{
    public partial class ProductCategories : IDisposable
    {
        private List<ProductCategoryDto> Categories { get; set; } = new List<ProductCategoryDto>();
        private MetaData MetaData { get; set; } = new MetaData();

        private ProductCategoryFilterParam _productCategoryParameters = new ProductCategoryFilterParam()
        {
            CurrentPage = 1,
            OrderBy = string.Empty,
            PageSize = 25,
            SearchTerm = string.Empty,
        };

        [Inject]
        public IProductCategoryService ProductCategoryService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await GetProductCategory();
        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _productCategoryParameters.CurrentPage = 1;
            _productCategoryParameters.SearchTerm = searchTerm;
            await GetProductCategory();
        }

        private void RedirectToUpdate()
        {
            var url = Path.Combine("/addProductCategory");
            NavigationManager.NavigateTo(url);
        }


        private async Task GetProductCategory()
        {
            var productCategoryResponse = await ProductCategoryService.GetAllProductCategories(_productCategoryParameters);
            Categories = productCategoryResponse.Items.ToList();
            MetaData = productCategoryResponse.MetaData;
        }

        private async Task SelectedPage(int page)
        {
            _productCategoryParameters.CurrentPage = page;
            await GetProductCategory();
        }

        private async Task SortChanged(string orderBy)
        {
            _productCategoryParameters.OrderBy = orderBy;
            _productCategoryParameters.CurrentPage = 1;
            await GetProductCategory();
        }


        private async Task DeleteProductCategory(int id)
        {
            await ProductCategoryService.DeleteProductCategory(id);
            _productCategoryParameters.CurrentPage = 1;
            await GetProductCategory();
        }


        public void Dispose()
        {
     
        }
    }
}
