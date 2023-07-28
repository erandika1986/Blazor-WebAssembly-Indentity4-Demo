using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.ProductCategory.SaveProductCategory
{
    public class SaveProductCategoryCommand : IRequest<ResponseDto>
    {
        public ProductCategoryDto ProductCategory { get; set; }
    }

    public class DeleteProductCategoryCommandHandler : IRequestHandler<SaveProductCategoryCommand, ResponseDto>
    {
        private readonly IProductCategoryCommandRepository _productCategoryCommandRepository;
        private readonly IProductCategoryQueryRepository _productCategoryQueryRepository;

        public DeleteProductCategoryCommandHandler(
            IProductCategoryCommandRepository productCategoryCommandRepository, 
            IProductCategoryQueryRepository productCategoryQueryRepository)
        {
            this._productCategoryCommandRepository = productCategoryCommandRepository;
            this._productCategoryQueryRepository = productCategoryQueryRepository;
        }


        public async Task<ResponseDto> Handle(SaveProductCategoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productCategory = await _productCategoryQueryRepository.GetById(request.ProductCategory.Id, cancellationToken);

                if (productCategory == null)
                {
                    productCategory = new BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.ProductCategory()
                    {
                        IsActive = true,
                        Name = request.ProductCategory.Name,
                    };

                    await _productCategoryCommandRepository.AddAsync(productCategory, cancellationToken);

                    return ResponseDto.Success("New product category added successfully.", productCategory.Id);
                }
                else
                {
                    productCategory.Name = request.ProductCategory.Name;

                    await _productCategoryCommandRepository.UpdateAsync(productCategory, cancellationToken);

                    return ResponseDto.Success("Product category has updated");
                }

            }
            catch (Exception ex)
            {
                return ResponseDto.Failure(new List<string>() { ex.Message });
            }
        }
    }
}
