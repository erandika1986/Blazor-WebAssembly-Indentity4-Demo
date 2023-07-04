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
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne<User>(a => a.CreatedByUser)
                .WithMany(at => at.CreatedProducts)
                .HasForeignKey(a => a.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<User>(a => a.UpdatedByUser)
                .WithMany(at => at.UpdatedProducts)
                .HasForeignKey(a => a.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<ProductCategory>(a => a.ProductCategory)
                .WithMany(at => at.Products)
                .HasForeignKey(a => a.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
