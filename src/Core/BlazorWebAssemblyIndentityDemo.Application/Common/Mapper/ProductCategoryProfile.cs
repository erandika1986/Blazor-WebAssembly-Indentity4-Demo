using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Common.Mapper
{
    public class ProductCategoryProfile : Profile
    {
        public ProductCategoryProfile()
        {
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
        }
    }
}
