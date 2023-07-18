using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.User
{
    public class UserFilterParams : BaseFilterParams
    {
        public string? RoleId { get; set; }
        public int? PositionId { get; set; }
    }
}
