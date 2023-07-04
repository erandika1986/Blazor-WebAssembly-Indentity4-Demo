using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Common
{
    public class BaseAuditableEntity : BaseEntity
    {
        public DateTime CreatedDate { get; set; }
        public int? CreatedById { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? UpdatedById { get; set; }
        public bool IsActive { get; set; }

        public virtual User CreatedByUser { get; set; }
        public virtual User UpdatedByUser { get; set; }
    }
}
