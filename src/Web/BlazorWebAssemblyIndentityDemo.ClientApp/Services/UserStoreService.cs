using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlazorWebAssemblyIndentityDemo.ClientApp.Services
{
    public class UserStoreService : IUserStoreService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly JsonSerializerOptions _options;
        public UserStoreService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }
        public async Task<PaginatedListDto<BasicUserDto>> GetUsers(UserFilterParams filterParams)
        {
            try
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["currentPage"] = filterParams.CurrentPage.ToString(),
                    ["pageSize"] = "10",
                    ["searchTerm"] = filterParams.SearchTerm == null ? "" : filterParams.SearchTerm,
                    ["orderBy"] = filterParams.OrderBy,
                    ["RoleId"] = filterParams.RoleId,
                    ["PositionId"] = filterParams.PositionId.ToString()
                };

                var httpClient = _clientFactory.CreateClient("usersAPI");
                var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("Account/getUsers", queryStringParam));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var pagingResponse = JsonSerializer.Deserialize<PaginatedListDto<BasicUserDto>>(content, _options);

                return pagingResponse;
            }
            catch (Exception ex)
            {
                return new PaginatedListDto<BasicUserDto>();
            }
        }
    }
}
