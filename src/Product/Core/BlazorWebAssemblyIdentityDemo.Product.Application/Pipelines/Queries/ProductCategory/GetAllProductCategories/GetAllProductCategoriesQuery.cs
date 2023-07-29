using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Product.Application.Extensions;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories
{
    public record GetAllProductCategoriesQuery(ProductCategoryFilterParam FilterParam) : IRequest<PaginatedListDto<ProductCategoryDto>>
    {

    }

    public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, PaginatedListDto<ProductCategoryDto>>
    {
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
        private readonly IMapper _mapper;

        public GetAllProductCategoriesQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository, IMapper mapper)
        {
            this._productCategoryQueryRepository = productCategoryQueryRepository;
            this._mapper = mapper;
        }

        public async Task<PaginatedListDto<ProductCategoryDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var productCategories = _productCategoryQueryRepository.GetAllQueryableAsync()
                .Search(request.FilterParam.SearchTerm)
                .Sort(request.FilterParam.OrderBy);

            var recordCount = await productCategories.CountAsync();

            var paginatedPageCategory = await productCategories
                .Skip(request.FilterParam.CurrentPage - 1)
                .Take(request.FilterParam.PageSize)
                .ToListAsync();

          var response = new PaginatedListDto<ProductCategoryDto>
                (
                _mapper.Map<List<ProductCategoryDto>>(paginatedPageCategory),
                recordCount, 
                request.FilterParam.CurrentPage, 
                request.FilterParam.PageSize);

            return response;
        }
    }
}
