using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Product
{
    public class ProductMasterDataDto
    {
        public ProductMasterDataDto()
        {
            ProductCategories = new List<DropDownDto>();
        }

        public List<DropDownDto> ProductCategories { get; set; }
    }
}
