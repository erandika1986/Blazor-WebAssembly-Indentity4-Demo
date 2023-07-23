using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.ClientApp.Shared;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.User
{
    public partial class EditUser
    {
        private UserDto _user = new UserDto();

        private UserMasterDto _masterData = new UserMasterDto();

        private SuccessNotification _notification;
        public string DefaultValue  = "1";

        [Inject]
        public IUserStoreService UserStoreService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _masterData = await UserStoreService.GetUserMasterAsync();
            _user = await UserStoreService.GetUserById(Id);
        }

        private async Task Update()
        {
            await UserStoreService.UpdateUser(_user);
            _notification.Show();
        }
    }
}
