using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Common
{
    public class BaseFilterParams
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string? SearchTerm { get; set; }
        public string? OrderBy { get; set; }
    }
}
