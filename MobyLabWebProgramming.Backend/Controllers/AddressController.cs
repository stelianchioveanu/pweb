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
public class AddressController : AuthorizedController
{
    private readonly IAddressService _addressService;
    public AddressController(IUserService userService, IAddressService addressService) : base(userService)
    {
        _addressService = addressService;
    }


    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> UpdateAddress([FromBody] AddressUpdateDTO address)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _addressService.UpdateAddress(address, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
