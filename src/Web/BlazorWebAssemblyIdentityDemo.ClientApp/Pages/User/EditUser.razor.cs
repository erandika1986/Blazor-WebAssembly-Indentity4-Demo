using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.ClientApp.Shared;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.User
{
    public partial class EditUser
    {
        private UserDto _user;

        private UserMasterDto MasterData { get; set; }

        private SuccessNotification _notification;
        public string DefaultValue  = "1";

        [Inject]
        public IUserStoreService UserStoreService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            MasterData = await UserStoreService.GetUserMasterAsync();
            _user = await UserStoreService.GetUserById(Id);
        }

        private async Task Update()
        {
            _notification.Show();
        }
    }
}
