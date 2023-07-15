using BlazorWebAssemblyIdentityDemo.OAuth.Data.Configuration;
using BlazorWebAssemblyIdentityDemo.OAuth.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.OAuth.Data
{
    public class AuthIdentityContext : IdentityDbContext<User>
    {
        public AuthIdentityContext(DbContextOptions<AuthIdentityContext> options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
