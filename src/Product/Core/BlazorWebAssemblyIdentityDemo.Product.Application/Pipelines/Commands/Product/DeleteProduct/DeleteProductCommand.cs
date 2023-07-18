using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.DeleteProduct
{
    public record DeleteProductCommand(int id) : IRequest<ResponseDto>
    {
    }

    public class DeleteProductCommandHandle : IRequestHandler<DeleteProductCommand, ResponseDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;

        public DeleteProductCommandHandle(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository)
        {
            this._productQueryRepository = productQueryRepository;
            this._productCommandRepository = productCommandRepository;
        }

        public async Task<ResponseDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productQueryRepository.GetById(request.id, cancellationToken);

            if (product == null)
            {
                product.IsActive = false;

                await _productCommandRepository.UpdateAsync(product, cancellationToken);

                return ResponseDto.Success("Product has been deleted successfully.");
            }
            else
            {
                return ResponseDto.Failure(new List<string>() { "Operation failed. Unable to find any matching product."});
            }
        }
    }
}
