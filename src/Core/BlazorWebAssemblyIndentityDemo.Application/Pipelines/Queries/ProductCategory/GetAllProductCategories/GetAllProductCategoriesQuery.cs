using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories
{
    public class GetAllProductCategoriesQuery : IRequest<List<ProductCategoryDto>>
    {
    }

    public class GetAllProductCategoriesQueryHandler : IRequestHandler<GetAllProductCategoriesQuery, List<ProductCategoryDto>>
    {
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
        private readonly IMapper _mapper;

        public GetAllProductCategoriesQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository, IMapper mapper)
        {
            this._productCategoryQueryRepository = productCategoryQueryRepository;
            this._mapper = mapper;
        }

        public async Task<List<ProductCategoryDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var productCategories = await _productCategoryQueryRepository.GetAll(cancellationToken);

            return _mapper.Map<List<ProductCategoryDto>>(productCategories);
        }
    }
}
