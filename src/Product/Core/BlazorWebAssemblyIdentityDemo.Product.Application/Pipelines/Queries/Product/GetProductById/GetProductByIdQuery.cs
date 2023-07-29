using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetProductById
{
    public record GetProductByIdQuery(ProductFilterParam filterParam) : IRequest<ProductDto>
    {
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(
            IProductQueryRepository productQueryRepository, 
            IMapper mapper)
        {
            this._productQueryRepository = productQueryRepository;
            this._mapper = mapper;
        }


        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetById(request.id, cancellationToken);

            return _mapper.Map<ProductDto>(product);
        }
    }
}
