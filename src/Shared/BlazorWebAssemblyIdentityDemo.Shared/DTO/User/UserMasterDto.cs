using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.User
{
    public class UserMasterDto
    {
        public UserMasterDto()
        {
            Roles = new List<DropDownDto>();
            Positions = new List<DropDownDto>();
            SortBy = new List<DropDownDto>();
        }

        public List<DropDownDto> Roles { get; set; }
        public List<DropDownDto>  Positions { get; set; }
        public List<DropDownDto> SortBy { get; set; }
    }
}
