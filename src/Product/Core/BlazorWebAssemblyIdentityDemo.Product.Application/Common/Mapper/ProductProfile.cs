using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Common.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product, ProductDto>().ReverseMap();
        }
    }
}
