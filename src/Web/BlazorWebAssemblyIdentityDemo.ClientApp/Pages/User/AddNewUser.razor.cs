using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using BlazorWebAssemblyIdentityDemo.ClientApp.Shared;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;
using Radzen;
using System.Text.Json;
using System;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages.User
{
    public partial class AddNewUser
    {
        private UserForRegistrationDto _user = new UserForRegistrationDto() { Id = string.Empty };

        private UserMasterDto _masterData = new UserMasterDto();

        private SuccessNotification _notification;
        public string DefaultValue = "1";

        [Inject]
        public IUserStoreService UserStoreService { get; set; }

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _masterData = await UserStoreService.GetUserMasterAsync();
        }

        private async Task AddUser()
        {
            await UserStoreService.RegisterUser(_user);
            _notification.Show();
        }

        void OnInvalidSubmit(FormInvalidSubmitEventArgs args)
        {
            //console.Log($"InvalidSubmit: {JsonSerializer.Serialize(args, new JsonSerializerOptions() { WriteIndented = true })}");
        }
    }
}
