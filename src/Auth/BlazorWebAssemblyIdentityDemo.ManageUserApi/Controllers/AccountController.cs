using BlazorWebAssemblyIdentityDemo.OAuth.Data;
using BlazorWebAssemblyIdentityDemo.OAuth.Data.Extensions;
using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using BlazorWebAssemblyIdentityDemo.Shared.DTO;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using BlazorWebAssemblyIdentityDemo.Shared.Helper;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace BlazorWebAssemblyIdentityDemo.ManageUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly AuthIdentityContext _context;

        public AccountController(
            UserManager<User> userManager, 
            RoleManager<Role> roleManager, AuthIdentityContext context)
        {

            this._userManager = userManager;
            this._roleManager = roleManager;
            this._context = context;
        }

        [HttpPost("Registration")]
        [Authorize]
        public async Task<IActionResult> RegisterUser([FromBody] UserForRegistrationDto userForRegistration)
        {
            if (userForRegistration == null || !ModelState.IsValid)
                return BadRequest();

            var user = new User
            {
                FirstName = userForRegistration.FirstName,
                LastName = userForRegistration.LastName,
                UserName = userForRegistration.Email,
                Email = userForRegistration.Email,
                Position = Enum.Parse<Position>(userForRegistration.Position),
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, userForRegistration.Password);

            var claims = new List<Claim>()
            {
                new Claim(JwtClaimTypes.Name, $"{userForRegistration.FirstName} {userForRegistration.LastName}"),
                new Claim(JwtClaimTypes.GivenName, userForRegistration.FirstName),
                new Claim(JwtClaimTypes.FamilyName, userForRegistration.LastName),
                new Claim(JwtClaimTypes.WebSite, "http://demo.com"),
                new Claim("position", EnumHelper.GetEnumDescription(Enum.Parse<Position>(userForRegistration.Position))),
                new Claim("country", userForRegistration.Country),
                new Claim("subject", user.Id)
            };

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(ResponseDto.Failure(errors));
            }

            foreach (var roleId in userForRegistration.AssignedRoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);

                claims.Add(new Claim(JwtClaimTypes.Role, role.Name));

                await _userManager.AddToRoleAsync(user, role.Name);
            }

            result = _userManager.AddClaimsAsync(user, claims).Result;

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            return StatusCode(201);
        }

        [HttpGet("getUsers")]
        public async Task<IActionResult> GetUsers([FromQuery] UserFilterParams userParams)
        {
            var users = _context.Users
                .AsQueryable()
                .Search(userParams.SearchTerm)
                .SearchByRoleId(userParams.RoleId)
                .SearchByPositionId(userParams.PositionId)
                .Sort(userParams.OrderBy);

            var recordCount = await users.CountAsync();

            var paginatedUser = await users
                .Skip(userParams.CurrentPage - 1)
                .Take(userParams.PageSize)
                .ToListAsync();

            users = users.Include(x => x.UserRoles).ThenInclude(r => r.Role);

            var basicUserDtos = new List<BasicUserDto>();

            foreach (var user in users)
            {

                basicUserDtos.Add(new BasicUserDto()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Position = EnumHelper.GetEnumDescription(user.Position),
                    Roles = string.Join(",", user.UserRoles.Select(x => x.Role.Name).ToList()),
                    ImageUrl = "https://icon-library.com/images/default-user-icon/default-user-icon-3.jpg"
                });
            }

            var response = new PaginatedListDto<BasicUserDto>
                (basicUserDtos, recordCount, userParams.CurrentPage, userParams.PageSize);

            return Ok(response);
        }

        [HttpGet("getUserById")]
        public async Task<IActionResult> GetUserById([FromQuery] string id)
        {
            var response = new UserForRegistrationDto();

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
      
            response.FirstName = user.FirstName;
            response.LastName = user.LastName;
            response.Email = user.Email;
            response.Position = ((int)user.Position.Value).ToString();
            response.Country = "United State";
            response.ImageUrl = "https://icon-library.com/images/default-user-icon/default-user-icon-3.jpg";

            foreach (var role in user.UserRoles)
            {
                response.AssignedRoleIds.Add(role.RoleId);
            }

            return Ok(response);
        }

        [HttpGet("getUserMasterData")]
        public async Task<IActionResult> GetUserMasterData()
        {
            var masterData = new UserMasterDto();

            var roles = await _context
                .Roles
                .Select(x => 
                    new DropDownDto() 
                    { 
                        Id = x.Id,
                        Name = x.Name 
                    }).ToListAsync();

            masterData.Roles.AddRange(roles);

            foreach (Position position in (Position[])Enum.GetValues(typeof(Position)))
            {
                masterData
                    .Positions
                    .Add(
                            new DropDownDto() 
                            { 
                                Id = ((int)position).ToString(), 
                                Name = EnumHelper.GetEnumDescription(position) 
                            });
            }

            masterData.SortBy.Add(new DropDownDto() { Id = "firstName", Name = "First Name" });
            masterData.SortBy.Add(new DropDownDto() { Id = "lastName", Name = "Last Name" });
            masterData.SortBy.Add(new DropDownDto() { Id = "position", Name = "Position" });

            return Ok(masterData);
        }


        [HttpGet("unauthorized")]
        public IActionResult UnauthorizedTestAction() =>
            Ok(new { Message = "Access is allowed for unauthorized users" });


        [HttpGet("Privacy")]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<string> Privacy()
        {
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            return claims;
        }
    }
}
