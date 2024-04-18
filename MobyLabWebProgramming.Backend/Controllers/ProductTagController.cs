using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;


[ApiController]
[Route("api/[controller]/[action]")]
public class ProductTagController : AuthorizedController
{
    private readonly IProductTagService _productTagService;
    public ProductTagController(IUserService userService, IProductTagService productTagService) : base(userService)
    {
        _productTagService = productTagService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddProductTag([FromBody] ProductTagAddDTO tag)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productTagService.AddProductTag(tag, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RequestResponse>> DeleteProductTag([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productTagService.DeleteProductTag(id, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<ProductTagDTO>>>> GetTags([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _productTagService.GetProductTags(pagination)) :
            this.ErrorMessageResult<PagedResponse<ProductTagDTO>>(currentUser.Error);
    }
}
