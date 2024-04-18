using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class ProductController : AuthorizedController
{
    private readonly IProductService _productService;
    private readonly IUserFileService _userFileService;
    public ProductController(IUserService userService, IProductService productService, IUserFileService userFileService) : base(userService)
    {
        _productService = productService;
        _userFileService= userFileService;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddProduct([FromForm] ProductAddDTO product)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.AddProduct(product, _userFileService, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RequestResponse<ProductDTO>>> GetProduct([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.GetProduct(id, _userFileService, currentUser.Result)) :
            this.ErrorMessageResult<ProductDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteProduct([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.DeleteProduct(id, _userFileService, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
