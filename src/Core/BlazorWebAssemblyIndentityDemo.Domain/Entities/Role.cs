using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Entities
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        
        public string Name { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
