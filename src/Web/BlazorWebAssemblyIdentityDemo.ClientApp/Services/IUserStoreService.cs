using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public interface IUserStoreService
    {
        Task<UserMasterDto> GetUserMasterAsync();
        Task<PaginatedListDto<BasicUserDto>> GetUsers(UserFilterParams filterParams);
    }
}
