using BlazorWebAssemblyIndentityDemo.ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorWebAssemblyIndentityDemo.ClientApp.Pages
{
    public partial class Users
    {
        [Inject]
        public IHttpClientFactory ClientFactory { get; set; }

        private List<string> _claims = new List<string>();
        protected override async Task OnInitializedAsync()
        {
            var httpClient = ClientFactory.CreateClient("usersAPI");
            _claims = await httpClient.GetFromJsonAsync<List<string>>("account/privacy");
        }
    }
}
