using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
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
    public async Task<ActionResult<RequestResponse>> AddProduct([FromBody] ProductAddDTO product)
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductDTO>>>> GetProducts([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.GetProducts(pagination)) :
            this.ErrorMessageResult<PagedResponse<ProductDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductDTO>>>> GetMyProducts([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.GetMyProducts(pagination, currentUser.Result)) :
            this.ErrorMessageResult<PagedResponse<ProductDTO>>(currentUser.Error);
    }
}
