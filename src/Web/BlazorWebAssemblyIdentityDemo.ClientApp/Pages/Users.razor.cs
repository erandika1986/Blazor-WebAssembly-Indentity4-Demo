using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using BlazorWebAssemblyIdentityDemo.ClientApp.Services;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Pages
{
    public partial class Users : IDisposable
    {
        private List<BasicUserDto> UserList { get; set; } = new List<BasicUserDto>();
        private MetaData MetaData { get; set; } = new MetaData();
        private UserMasterDto MasterData { get; set; } = new UserMasterDto();


        private UserFilterParams _userParameters = new UserFilterParams()
        {
            RoleId = string.Empty,
            CurrentPage = 1,
            OrderBy = string.Empty,
            PageSize = 25,
            PositionId = (int)Position.CEO,
            SearchTerm = string.Empty,
        };

        [Inject]
        public IUserStoreService UserStoreService { get; set; }


        protected async override Task OnInitializedAsync()
        {

            MasterData = await UserStoreService.GetUserMasterAsync();

        }

        private async Task SearchChanged(string searchTerm)
        {
            Console.WriteLine(searchTerm);
            _userParameters.CurrentPage = 1;
            _userParameters.SearchTerm = searchTerm;
            await GetUsers();
        }

        private async Task RoleChanged(string roleId)
        {
            _userParameters.RoleId = roleId;
            await GetUsers();
        }

        private async Task PositionChanged(string positionId)
        {
            _userParameters.PositionId = int.Parse(positionId);
            await GetUsers();
        }

        private async Task SortChanged(string orderBy)
        {
            _userParameters.OrderBy = orderBy;
            await GetUsers();
        }


        private async Task DeleteUser(string id)
        {
            //await UserStoreService.DeleteUser(id);
            _userParameters.CurrentPage = 1;
            await GetUsers();
        }

        private async Task GetUsers()
        {
            var userResponse = await UserStoreService.GetUsers(_userParameters);
            UserList = userResponse.Items.ToList();

        }

        public void Dispose()
        {

        }
    }
}
