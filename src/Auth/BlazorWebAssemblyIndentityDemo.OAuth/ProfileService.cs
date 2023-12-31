﻿using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using Dynami.IdentityServer4.Extensions;
using Dynami.IdentityServer4.Models;
using Dynami.IdentityServer4.Services;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlazorWebAssemblyIdentityDemo.OAuth
{
    public sealed class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
        private readonly UserManager<User> _userMgr;
        private readonly RoleManager<Role> _roleMgr;

        public ProfileService(
            UserManager<User> userMgr,
            RoleManager<Role> roleMgr,
            IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
            _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _userMgr.FindByIdAsync(sub);
            ClaimsPrincipal userClaims = await _userClaimsPrincipalFactory.CreateAsync(user);

            List<Claim> claims = userClaims.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();

            if (_userMgr.SupportsUserRole)
            {
                IList<string> roles = await _userMgr.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(JwtClaimTypes.Role, roleName));
                    if (_roleMgr.SupportsRoleClaims)
                    {
                        Role role = await _roleMgr.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleMgr.GetClaimsAsync(role));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            string sub = context.Subject.GetSubjectId();
            User user = await _userMgr.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}
