using System.Security.Claims;

namespace BlazorWebAssemblyIdentityDemo.ManageUserApi.Extensions
{
    public static class ClaimExtensions
    {
        public static bool HasClaim(this IList<Claim> userClaims, Claim claim)
        {
            return userClaims.Any(x => x.Type == claim.Type && x.Value == claim.Value);
        }

        public static bool HasClaimType(this IList<Claim> userClaims, Claim claim)
        {
            return userClaims.Any(x => x.Type == claim.Type && x.Value == claim.Value);
        }
    }
}
