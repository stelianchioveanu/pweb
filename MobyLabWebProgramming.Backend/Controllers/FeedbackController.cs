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
public class FeedbackController : AuthorizedController
{
    private readonly IFeedbackService _feedbackService;
    public FeedbackController(IUserService userService, IFeedbackService feedbackService) : base(userService)
    {
        _feedbackService = feedbackService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> AddFeedback([FromBody] FeedbackAddDTO feedback)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _feedbackService.AddFeedback(feedback, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<RequestResponse<PagedResponse<FeedbackDTO>>>> GetFeedbacks([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _feedbackService.GetFeedbacks(pagination)) :
            this.ErrorMessageResult<PagedResponse<FeedbackDTO>>(currentUser.Error);
    }
}
