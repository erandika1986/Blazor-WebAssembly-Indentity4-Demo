using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.ClientApp.Shared;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.ProductCategory
{
    public partial class EditProductCategory
    {
        private ProductCategoryDto _productCategory = new ProductCategoryDto();

        private SuccessNotification _notification;

        [Inject]
        public IProductCategoryService ProductCategoryService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _productCategory = await ProductCategoryService.GetProductCategoryById(int.Parse(Id));
        }

        private async Task Update()
        {
            await ProductCategoryService.SaveProductCategory(_productCategory);
            _notification.Show();
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
            //console.Log($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
        }
    }
}
