using BlazorWebAssemblyIdentityDemo.Product.Application.Contracts;
using System.Security.Claims;

namespace BlazorWebAssemblyIdentityDemo.Product.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }
        public string? UserId
        {
            get
            {
                if (_httpContextAccessor.HttpContext == null)
                    return (string?)null;

                try
                {
                    return _httpContextAccessor.HttpContext.User?.FindFirstValue("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");
                }
                catch (Exception ex)
                {
                    return (string?)null;
                }


            }
        }
    }
}
