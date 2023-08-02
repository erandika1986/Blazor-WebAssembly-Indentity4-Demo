using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Components.Products
{
    public partial class ProductTable
    {
        [Parameter]
        public List<ProductDto> Products { get; set; }

        [Parameter]
        public EventCallback<int> OnDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        private void RedirectToUpdate(int id)
        {
            var url = Path.Combine("/editProduct/", id.ToString());
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id.Equals(id));

            var response = await DialogService.Confirm($"Are you sure you want to delete {product.Name} Product?", "Confirm",
                new ConfirmOptions()
                {
                    OkButtonText = "Yes",
                    CancelButtonText = "No"
                });

            if (response.HasValue && response.Value)
            {
                await OnDeleted.InvokeAsync(id);
            }

        }
    }
}
