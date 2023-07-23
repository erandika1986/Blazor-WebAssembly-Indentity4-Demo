using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Radzen;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Components.Users
{
    public partial class UserTable
    {
        [Parameter]
        public List<BasicUserDto>  Users { get; set; }

        [Parameter]
        public EventCallback<string> OnDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public DialogService DialogService { get; set; }

        [Inject]
        public IJSRuntime Js { get; set; }

        private void RedirectToUpdate(string id)
        {
            var url = Path.Combine("/editUser/", id);
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(string id)
        {
            var user = Users.FirstOrDefault(p => p.Id.Equals(id));

            var response = await DialogService.Confirm($"Are you sure you want to delete {user.FirstName} user?", "Confirm", 
                new ConfirmOptions() 
                { 
                    OkButtonText = "Yes", 
                    CancelButtonText = "No" 
                });

            if(response.HasValue && response.Value)
            {
                await OnDeleted.InvokeAsync(id);
            }

        }
    }
}
