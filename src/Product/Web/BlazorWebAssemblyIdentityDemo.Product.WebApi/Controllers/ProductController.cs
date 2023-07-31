using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.DeleteProduct;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.Product.SaveProduct;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetAllProductByProductCategoryId;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetProductById;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.Product.GetProductMasterData;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories;

using BlazorWebAssemblyIdentityDemo.Shared.DTO.Product;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

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

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProductsByProducts([FromQuery] ProductFilterParam filterParam)
        { 
            var response = await _mediator.Send(new GetAllProductsQuery(filterParam));

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

        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _mediator.Send(new GetProductByIdQuery(id));

            return Ok(response);
        }

        [HttpGet("getProductMasterData")]
        public async Task<IActionResult> GetProductMasterData()
        {
            var response = await _mediator.Send(new GetProductMasterDataQuery());

            return Ok(response);
        }

        [HttpPost("uploadProductImage/{id:int}")]
        public IActionResult Upload(int id)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("StaticFiles", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(dbPath);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }
    }
}
