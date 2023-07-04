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
    internal class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategory");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne<User>(a => a.CreatedByUser)
                .WithMany(at => at.CreatedProductCategory)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(a => a.UpdatedByUser)
                .WithMany(at => at.UpdatedProductCategory)
                .HasForeignKey(a => a.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
