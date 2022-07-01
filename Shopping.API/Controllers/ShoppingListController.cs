using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.API.Controllers.Interfaces;
using Shopping.Application.ShoppingListCQRS.Commands.CompleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.CreateShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.DeleteShoppingList;
using Shopping.Application.ShoppingListCQRS.Commands.UpdateShoppingList;
using Shopping.Application.ShoppingListCQRS.Queries.AdminGetShoppingLists;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCategory;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleted;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCompleteDate;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByCreateDate;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByDescription;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListById;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingListByTitle;
using Shopping.Application.ShoppingListCQRS.Queries.GetShoppingLists;

namespace Shopping.API.Controllers;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "User")]
[Route("api/[controller]")]
[ApiController]
public class ShoppingListController:BaseController
{
    [HttpPost("AddShoppingList")]
    public async Task<IActionResult> Create(CreateShoppingListCommandRequest command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpPut("UpdateShoppingList")]
    public async Task<IActionResult> Update(UpdateShoppingListCommandRequest command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpDelete("DeleteShoppingList")]
    public async Task<IActionResult> Delete(DeleteShoppingListCommandRequest command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
    [HttpGet("GetShoppingLists")]
    public async Task<IActionResult> GetShoppingLists()
    {
        var command = new GetShoppingListsQueryRequest();
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Roles = "Admin")]
    [HttpGet("AdminGetShoppingLists")]
    public async Task<IActionResult> AdminGetShoppingLists()
    {
        var command = new AdminGetShoppingListsQueryRequest();
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByTitle/{title}")]
    public async Task<IActionResult> GetShoppingListByTitle(string title)
    {
        var command = new GetShoppingListByTitleQueryRequest{Title = title};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListById/{id:int}")]
    public async Task<IActionResult> GetShoppingListById(int id)
    {
        var command = new GetShoppingListByIdQueryRequest{Id = id};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByDescription/{desc}")]
    public async Task<IActionResult> GetShoppingListByDescription(string desc)
    {
        var command = new GetShoppingListByDescriptionQueryRequest{Description = desc};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByCategory/{category}")]
    public async Task<IActionResult> GetShoppingListByCategory(string category)
    {
        var command = new GetShoppingListByCategoryQueryRequest{Category =category};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByCompleted/{completed:bool}")]
    public async Task<IActionResult> GetShoppingListByCompleted(bool completed)
    {
        var command = new GetShoppingListByCompletedQueryRequest{IsCompleted =completed};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByCreateDate/{date:datetime}")]
    public async Task<IActionResult> GetShoppingListByCreateDate(DateTime date)
    {
        var command = new GetShoppingListByCreateDateQueryRequest(){CreateDate = date};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpGet("GetShoppingListByCompleteDate/{date:datetime}")]
    public async Task<IActionResult> GetShoppingListByCompleteDate(DateTime date)
    {
        var command = new GetShoppingListByCompleteDateQueryRequest(){CompleteDate = date};
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [HttpPost("CompleteShoppingList")]
    public async Task<IActionResult> CompleteShoppingList(CompleteShoppingListCommandRequest command)
    {
        var result = await Mediator.Send(command);
        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}