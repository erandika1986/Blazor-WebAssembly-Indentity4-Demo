using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.Product
{
    public partial class Products
    {
        [Parameter]
        public string CategoryId { get; set; }
    }
}
