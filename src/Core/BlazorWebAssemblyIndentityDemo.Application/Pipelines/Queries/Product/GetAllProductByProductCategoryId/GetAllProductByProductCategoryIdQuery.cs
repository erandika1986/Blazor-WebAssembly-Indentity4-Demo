using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Application.Pipelines.Queries.Product.GetAllProductByProductCategoryId
{
    public record GetAllProductByProductCategoryIdQuery(int productCategoryId) : IRequest<List<ProductDto>>
    {
    }

    public class GetAllProductByProductCategoryIdQueryHandler : IRequestHandler<GetAllProductByProductCategoryIdQuery, List<ProductDto>>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetAllProductByProductCategoryIdQueryHandler(IProductQueryRepository productQueryRepository, IMapper mapper)
        {
            this._productQueryRepository = productQueryRepository;
            this._mapper = mapper;
        }


        public async Task<List<ProductDto>> Handle(GetAllProductByProductCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var products = await _productQueryRepository.GetProductByProductCategoryId(request.productCategoryId, cancellationToken);

            return _mapper.Map<List<ProductDto>>(products);
        }
    }
}
