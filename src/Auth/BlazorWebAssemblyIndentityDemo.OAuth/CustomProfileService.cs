using BlazorWebAssemblyIdentityDemo.OAuth.Configuration;
using Dynami.IdentityServer4.Extensions;
using Dynami.IdentityServer4.Models;
using Dynami.IdentityServer4.Services;

namespace BlazorWebAssemblyIdentityDemo.OAuth
{
    public class CustomProfileService : IProfileService
    {
        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = InMemoryConfig.GetUsers()
                .Find(u => u.SubjectId.Equals(sub));

            context.IssuedClaims.AddRange(user.Claims);
            return Task.CompletedTask;
        }

        public Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = InMemoryConfig.GetUsers()
                .Find(u => u.SubjectId.Equals(sub));

            context.IsActive = user != null;
            return Task.CompletedTask;
        }
    }
}
