using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
{
    public interface IUserStoreService
    {
        Task<UserMasterDto> GetUserMasterAsync();
        Task<PaginatedListDto<BasicUserDto>> GetUsers(UserFilterParams filterParams);
        Task<UserDto> GetUserById(string id);
        Task UpdateUser(UserDto user);
        Task RegisterUser(UserForRegistrationDto userForRegistrationDto);
        Task DeleteUser(string id);
    }
}
