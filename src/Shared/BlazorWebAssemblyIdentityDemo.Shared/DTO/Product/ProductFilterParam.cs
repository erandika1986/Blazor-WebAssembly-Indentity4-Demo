using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Product
{
    public class ProductFilterParam : BaseFilterParams
    {
        public int SelectedProductCategory { get; set; }
    }
}
