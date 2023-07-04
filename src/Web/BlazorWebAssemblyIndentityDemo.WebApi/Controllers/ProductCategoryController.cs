using BlazorWebAssemblyIdentityDemo.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebAssemblyIdentityDemo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductCategoryController : ControllerBase
    {
        private IMediator _mediator;

        public ProductCategoryController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("getAllProductCategories")]
        public async Task<IActionResult> GetAllProductCategories()
        {
            var response = await _mediator.Send(new GetAllProductCategoriesQuery());

            return Ok(response);
        }
    }
}
