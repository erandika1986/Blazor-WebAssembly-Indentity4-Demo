using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Pipelines.Queries.ProductCategory.GetProductCategoryById
{
    public record GetProductCategoryByIdQuery(int id) : IRequest<ProductCategoryDto>
    {
    }

    public class GetProductCategoryByIdQueryHandler : IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryDto>
    {
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
        private readonly IMapper _mapper;

        public GetProductCategoryByIdQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository, IMapper mapper)
        {
            this._productCategoryQueryRepository = productCategoryQueryRepository;
            this._mapper = mapper;
        }

        public async Task<ProductCategoryDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await _productCategoryQueryRepository.GetById(request.id, cancellationToken);

            return _mapper.Map<ProductCategoryDto>(productCategory);
        }
    }
}
