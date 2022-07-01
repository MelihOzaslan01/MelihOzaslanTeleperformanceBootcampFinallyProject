using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Interfaces;
using Shopping.Application.ProductCQRS.Commands.CreateProduct;
using Shopping.Application.ProductCQRS.Commands.DeleteProduct;
using Shopping.Application.ProductCQRS.Commands.UpdateProduct;
using Shopping.Application.ProductCQRS.Queries.GetProductById;
using Shopping.Application.ProductCQRS.Queries.GetProducts;
using Shopping.Application.ProductCQRS.Queries.GetProductsByShoppingListId;

namespace Shopping.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        [HttpPost("AddProduct")]
        public async Task<IActionResult> Create(CreateProductCommandRequest command)
        {
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> Update(UpdateProductCommandRequest command)
        {
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> Delete(DeleteProductCommandRequest command)
        {
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetProducts()
        {
            var command = new GetProductsQueryRequest();
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
        [HttpGet("GetProductById/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var command = new GetProductByIdQueryRequest {Id = id};
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest("Not Found!");
            }

            return Ok(result);
        }
        [HttpGet("GetProductByShoppingListId/{id:int}")]
        public async Task<IActionResult> GetProductsByShoppingListId(int id)
        {
            var command = new GetProductsByShoppingListIdQueryRequest{ShoppingListId = id};
            var result = await Mediator.Send(command);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
