using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Infrastructure.Data.Configurations
{
    internal class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRole");

            builder.HasKey(x => new { x.UserId, x.RoleId});

            builder.HasOne<User>(a => a.User)
                .WithMany(at => at.UserRoles)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<Role>(a => a.Role)
                .WithMany(at => at.UserRoles)
                .HasForeignKey(a => a.RoleId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
