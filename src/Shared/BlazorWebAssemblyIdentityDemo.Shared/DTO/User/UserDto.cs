using BlazorWebAssemblyIdentityDemo.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.User
{
    public class UserDto
    {
        public UserDto()
        {
            AssignedRoleIds = new List<string>();    
        }

        public string Id { get; set; }

        [Required(ErrorMessage = "First Name is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

      

        public List<string> AssignedRoleIds { get; set; }


        [Required(ErrorMessage = "Position Id is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public string? ImageUrl { get; set; }
    }
}
