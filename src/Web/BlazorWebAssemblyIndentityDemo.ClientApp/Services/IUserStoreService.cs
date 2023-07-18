using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;

namespace BlazorWebAssemblyIndentityDemo.ClientApp.Services
{
    public interface IUserStoreService
    {
        Task<PaginatedListDto<BasicUserDto>> GetUsers(UserFilterParams filterParams);
    }
}
