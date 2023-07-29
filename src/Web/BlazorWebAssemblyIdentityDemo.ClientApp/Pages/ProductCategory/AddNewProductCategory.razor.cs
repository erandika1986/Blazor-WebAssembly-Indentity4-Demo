using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.ClientApp.Shared;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.ProductCategory
{
    public partial class AddNewProductCategory
    {
        private ProductCategoryDto  _productCategory = new ProductCategoryDto() { Name = string.Empty };

        private SuccessNotification _notification;

        [Inject]
        public IProductCategoryService ProductCategoryService { get; set; }


        private async Task AddProductCategory()
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
