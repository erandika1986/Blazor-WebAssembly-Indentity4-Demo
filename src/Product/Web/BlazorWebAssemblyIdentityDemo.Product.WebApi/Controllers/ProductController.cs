using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.DeleteProduct;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.SaveProduct;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetAllProductByProductCategoryId;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories;

using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyIdentityDemo.Product.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getAllProductByProductCategoryId/{productCategoryId}")]
        public async Task<IActionResult> GetAllProductByProductCategoryId(int productCategoryId)
        {
            var response = await _mediator.Send(new GetAllProductByProductCategoryIdQuery(productCategoryId));

            return Ok(response);
        }

        [HttpPost("saveProduct")]
        public async Task<IActionResult> SaveProduct([FromBody] ProductDto product)
        {
            var response = await _mediator.Send(new SaveProductCommand() { Product = product });

            return Ok(response);
        }

        [HttpDelete("deleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await _mediator.Send(new DeleteProductCommand(id));

            return Ok(response);
        }
    }
}
