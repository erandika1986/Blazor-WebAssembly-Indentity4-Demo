using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Product.Application.Extensions;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetAllProductByProductCategoryId
{
    public record GetAllProductsQuery(ProductFilterParam filterParam) : IRequest<PaginatedListDto<ProductDto>>
    {
    }

    public class GetAllProductByProductCategoryIdQueryHandler : IRequestHandler<GetAllProductsQuery, PaginatedListDto<ProductDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetAllProductByProductCategoryIdQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            this._productQueryRepository = productQueryRepository;
            this._mapper = mapper;
        }


        public async Task<PaginatedListDto<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = _productQueryRepository
                .GetAllQueryableAsync()
                .Search(request.filterParam.SearchTerm)
                .SearchByProductCategoryId(request.filterParam.SelectedProductCategory);

            var recordCount = await products.CountAsync();

            var paginatedProducts = await products
                .Skip(request.filterParam.CurrentPage - 1)
                .Take(request.filterParam.PageSize)
                .ToListAsync();

            var response = new PaginatedListDto<ProductDto>
                  (
                  _mapper.Map<List<ProductDto>>(paginatedProducts),
                  recordCount,
                  request.filterParam.CurrentPage,
                  request.filterParam.PageSize);

            return response;

        }
    }
}
