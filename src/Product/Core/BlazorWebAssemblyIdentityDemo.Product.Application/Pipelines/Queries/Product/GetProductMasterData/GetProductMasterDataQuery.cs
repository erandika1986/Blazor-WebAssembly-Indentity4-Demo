using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetProductMasterData
{
    public record GetProductMasterDataQuery : IRequest<ProductMasterDataDto>
    {
    }

    public class GetProductMasterDataQueryHandler : IRequestHandler<GetProductMasterDataQuery, ProductMasterDataDto>
    {
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository; 

        public GetProductMasterDataQueryHandler(IProductCategoryQueryRepository productCategoryQueryRepository)
        {
            this._productCategoryQueryRepository = productCategoryQueryRepository;
        }


        public async Task<ProductMasterDataDto> Handle(GetProductMasterDataQuery request, CancellationToken cancellationToken)
        {
            var response = new ProductMasterDataDto();

            var productCategory = await _productCategoryQueryRepository.GetAll(cancellationToken);

             response.ProductCategories.AddRange(productCategory.Select(p => new DropDownDto() { Id = p.Id.ToString(), Name = p.Name }).ToList());

            return response;
        }
    }
}
