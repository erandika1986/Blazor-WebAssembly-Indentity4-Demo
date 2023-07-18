using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public string? CreatedById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public string? UpdatedById { get; set; }
        public bool IsActive { get; set; }


    }
}
