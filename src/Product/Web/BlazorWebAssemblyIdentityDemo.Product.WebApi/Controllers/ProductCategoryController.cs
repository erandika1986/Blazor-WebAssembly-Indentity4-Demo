using BlazorWebAssemblyIdentityDemo.Product.Application.Contracts;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.ProductCategory.DeleteProductCategory;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Commands.ProductCategory.SaveProductCategory;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.ProductCategory.GetAllProductCategories;
using BlazorWebAssemblyIdentityDemo.Product.Application.Pipelines.Queries.ProductCategory.GetProductCategoryById;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.ProductCategory;
using BlazorWebAssemblyIdentityDemo.Shared.DTO.User;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlazorWebAssemblyIdentityDemo.Product.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductCategoryController : ControllerBase
    {
        private IMediator _mediator;
        private ICurrentUserService _currentUserService;

        public ProductCategoryController(IMediator mediator, ICurrentUserService currentUserService)
        {
            this._mediator = mediator;
            this._currentUserService = currentUserService;
        }

        [HttpPost("saveProductCategory")]
        public async Task<IActionResult> SaveProductCategory([FromBody] SaveProductCategoryCommand saveCommand)
        {
            var response = await _mediator.Send(saveCommand);

            return Ok(response);
        }


        [HttpDelete("deleteProductCategory/{id:number}")]
        public async Task<IActionResult> DeleteProductCategory(int id)
        {
            var response = await _mediator.Send(new DeleteProductCategoryCommand() { Id = id });

            return Ok(response);
        }


        [HttpGet("getProductCategoryById/{id:int}")]
        public async Task<IActionResult> GetProductCategoryById(int id)
        {
            var response = await _mediator.Send(new GetProductCategoryByIdQuery(id));

            return Ok(response);
        }


        [HttpGet("getAllProductCategories")]
        public async Task<IActionResult> GetAllProductCategories([FromQuery] ProductCategoryFilterParam filterParams)
        {
            var response = await _mediator.Send(new GetAllProductCategoriesQuery(filterParams));

            return Ok(response);
        }


        [HttpGet("Privacy")]
        [Authorize(Roles = "Administrator")]
        public IEnumerable<string> Privacy()
        {
            var id = _currentUserService.UserId;
            var claims = User.Claims.Select(c => $"{c.Type}: {c.Value}").ToList();
            return claims;
        }
    }
}
