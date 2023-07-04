using BlazorWebAssemblyIdentityDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Entities
{
    public class Product : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public int ProductCategoryId { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
    }
}
