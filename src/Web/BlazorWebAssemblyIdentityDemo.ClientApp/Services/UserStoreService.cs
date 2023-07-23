using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BlazorWebAssemblyIdentityDemo.ClientApp.Services
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

        public async Task DeleteUser(string id)
        {
            try
            {
                var httpClient = _clientFactory.CreateClient("usersAPI");
                var response = await httpClient.DeleteAsync("Account/DeleteUser/"+ id);
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unknown error occurred.");
            }
        }

        public async Task<UserDto> GetUserById(string id)
        {
            try
            {
                var queryStringParam = new Dictionary<string, string>
                {
                    ["id"] =id
                };

                var httpClient = _clientFactory.CreateClient("usersAPI");
                var response = await httpClient.GetAsync(QueryHelpers.AddQueryString("Account/getUserById", queryStringParam));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var responseDto = JsonSerializer.Deserialize<UserDto>(content, _options);

                return responseDto;
            }
            catch (Exception ex)
            {
                return new UserDto();
            }
        }

        public async Task<UserMasterDto> GetUserMasterAsync()
        {
            try
            {

                var httpClient = _clientFactory.CreateClient("usersAPI");
                var response = await httpClient.GetAsync("Account/getUserMasterData");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    throw new ApplicationException(content);
                }

                var masterData = JsonSerializer.Deserialize<UserMasterDto>(content, _options);

                return masterData;
            }
            catch (Exception ex)
            {
                return new UserMasterDto();
            }
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

        public async Task RegisterUser(UserForRegistrationDto userForRegistrationDto)
        {
            
            var content = JsonSerializer.Serialize(userForRegistrationDto);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpClient = _clientFactory.CreateClient("usersAPI");

            var putResult = await httpClient.PostAsync("Account/RegisterUser", bodyContent);

            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }

        public async Task UpdateUser(UserDto user)
        {
            var content = JsonSerializer.Serialize(user);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var httpClient = _clientFactory.CreateClient("usersAPI");

            var putResult = await httpClient.PutAsync("Account/UpdateUser", bodyContent);

            var putContent = await putResult.Content.ReadAsStringAsync();

            if (!putResult.IsSuccessStatusCode)
            {
                throw new ApplicationException(putContent);
            }
        }
    }
}
