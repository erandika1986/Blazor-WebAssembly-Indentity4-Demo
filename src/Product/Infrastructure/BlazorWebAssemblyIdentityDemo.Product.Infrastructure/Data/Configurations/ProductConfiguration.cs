using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Infrastructure.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.Id);

            builder.Property(p => p.Id).ValueGeneratedOnAdd();

            builder.Property(p => p.IsActive).HasDefaultValue(true);

            builder.HasOne<ProductCategory>(a => a.ProductCategory)
                .WithMany(at => at.Products)
                .HasForeignKey(a => a.ProductCategoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
