using AutoMapper;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Entities;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Queries;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.Common;
using BlazorWebAssemblyIdentityDemo.Product.Domain.Repositories.Commands;

namespace BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.SaveProduct
{
    public class SaveProductCommand : IRequest<ResponseDto>
    {
        public ProductDto Product { get; set; }
    }

    public class SaveProductCommandHandler : IRequestHandler<SaveProductCommand, ResponseDto>
    {
        private readonly IProductQueryRepository _productQueryRepository;
        private readonly IProductCommandRepository _productCommandRepository;
        private readonly IMapper _mapper;

        public SaveProductCommandHandler(IProductQueryRepository productQueryRepository, IProductCommandRepository productCommandRepository, IMapper mapper)
        {
            this._productQueryRepository = productQueryRepository;
            this._productCommandRepository = productCommandRepository;
            this._mapper = mapper;
        }

        public async Task<ResponseDto> Handle(SaveProductCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var productEntity = await _productQueryRepository.GetById(request.Product.Id, cancellationToken);

                if (productEntity == null)
                {
                    productEntity = _mapper.Map<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>(request.Product);

                    if (productEntity == null)
                    {
                        throw new ApplicationException("There is a problem in mapper.");
                    }

                    await _productCommandRepository.AddAsync(productEntity, cancellationToken);

                    return ResponseDto.Success("A new product has been added successfully.");
                }
                else
                {
                    productEntity = _mapper.Map<BlazorWebAssemblyIdentityDemo.Product.Domain.Entities.Product>(request.Product);

                    await _productCommandRepository.UpdateAsync(productEntity, cancellationToken);

                    return ResponseDto.Success("A new product has been updated successfully.");
                }

            }
            catch (Exception ex)
            {
                return ResponseDto.Failure(new List<string>() { "Operation Failed. An error has been occurred."});
            }


        }
    }
}
