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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne<User>(a => a.CreatedByUser)
                .WithMany(at => at.CreatedUsers)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);

            builder.HasOne<User>(a => a.UpdatedByUser)
                .WithMany(at => at.UpdatedUsers)
                .HasForeignKey(a => a.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false);
        }
    }
}
