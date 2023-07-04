using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Common.Mapper
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}
