using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

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
        public IJSRuntime Js { get; set; }

        private void RedirectToUpdate(string id)
        {
            var url = Path.Combine("/updateUser/", id);
            NavigationManager.NavigateTo(url);
        }

        private async Task Delete(string id)
        {
            var user =Users.FirstOrDefault(p => p.Id.Equals(id));

            var confirmed = await Js.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {user.FirstName} user?");
            if (confirmed)
            {
                await OnDeleted.InvokeAsync(id);
            }
        }
    }
}
