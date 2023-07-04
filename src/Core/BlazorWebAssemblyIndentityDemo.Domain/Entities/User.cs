using BlazorWebAssemblyIdentityDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public User()
        {
            UserRoles = new HashSet<UserRole>();

            CreatedUsers = new HashSet<User>();
            UpdatedUsers = new HashSet<User>(); 

            CreatedProductCategory = new HashSet<ProductCategory>();
            UpdatedProductCategory = new HashSet<ProductCategory>();

            CreatedProducts = new HashSet<Product>();
            UpdatedProducts = new HashSet<Product>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<User> CreatedUsers { get; set; }
        public virtual ICollection<User> UpdatedUsers { get; set; }

        public virtual ICollection<ProductCategory> CreatedProductCategory { get; set; }
        public virtual ICollection<ProductCategory> UpdatedProductCategory { get; set; }

        public virtual ICollection<Product> CreatedProducts { get; set; }
        public virtual ICollection<Product> UpdatedProducts { get; set; }
    }
}
