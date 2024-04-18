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
    public ProductController(IUserService userService, IProductService productService) : base(userService)
    {
        _productService = productService;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddProduct([FromBody] ProductAddDTO product)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productService.AddProduct(product, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
