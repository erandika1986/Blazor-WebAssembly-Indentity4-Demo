using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using BlazorWebAssemblyIndentityDemo.ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorWebAssemblyIndentityDemo.ClientApp.Pages
{
    public partial class Users
    {
        [Inject]
        public IUserStoreService UserStoreService { get; set; }

        private UserFilterParams _userParameters = new UserFilterParams()
        {
            RoleId = string.Empty,
            CurrentPage = 1,
            OrderBy = string.Empty,
            PageSize = 25,
            PositionId = (int)Position.CEO,
            SearchTerm = string.Empty,
        };

        protected override async Task OnInitializedAsync()
        {
            var response = await UserStoreService.GetUsers(_userParameters);
        }
    }
}
