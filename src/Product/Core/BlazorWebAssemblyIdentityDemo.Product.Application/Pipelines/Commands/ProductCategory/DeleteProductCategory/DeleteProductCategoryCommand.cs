using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.ProductCategory.DeleteProductCategory
{
    public class DeleteProductCategoryCommand : IRequest<ResponseDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCategoryCommandHandler : IRequestHandler<DeleteProductCategoryCommand, ResponseDto>
    {
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;
        private readonly IProductCategoryCommandRepository _productCategoryCommandRepository;

        public DeleteProductCategoryCommandHandler(
            IProductCategoryQueryRepository productCategoryQueryRepository, 
            IProductCategoryCommandRepository productCategoryCommandRepository)
        {
            this._productCategoryQueryRepository = productCategoryQueryRepository;
            this._productCategoryCommandRepository = productCategoryCommandRepository;    
        }

        public async Task<ResponseDto> Handle(DeleteProductCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _productCategoryQueryRepository.GetById(request.Id, cancellationToken);
                if (productCategory == null)
                {
                    return ResponseDto.Failure(new List<string> { "Unable to find matching product category." });
                }

                await _productCategoryCommandRepository.DeleteAsync(productCategory, cancellationToken);

                return ResponseDto.Success("Selected record has been deleted successfully.");
            }
            catch (Exception ex)
            {
                return ResponseDto.Failure(new List<string> { "An error has been occurred while deleting a record.",ex.ToString() });
            }
        }
    }
}
