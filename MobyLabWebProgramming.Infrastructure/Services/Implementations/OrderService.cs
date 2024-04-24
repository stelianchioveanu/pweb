using System.Net;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class FeedbackService : IFeedbackService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public FeedbackService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddFeedback(FeedbackAddDTO feedback, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (feedback == null || feedback.Description.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Every input should have at least 1 character!", ErrorCodes.WrongInputs));
        }

        if (feedback.Stars < 0 || feedback.Stars > 5)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Stars should have a value between 0 and 5!", ErrorCodes.WrongInputs));
        }

        var entity = await _repository.GetAsync(new UserSpec(feedback.ToUserId), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.NotFound, "User not found!", ErrorCodes.EntityNotFound));
        }

        if (entity.Id == requestingUser.Id)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "A user cannot provide feedback on his own!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Feedback
        {
            Description = feedback.Description,
            Stars = feedback.Stars,
            FromUserId = requestingUser.Id,
            ToUserId = entity.Id,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, Guid ToUserId, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new UserSpec(ToUserId), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse<PagedResponse<FeedbackDTO>>.FromError(new(HttpStatusCode.NotFound, "User not found!", ErrorCodes.EntityNotFound));
        }

        var result = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(pagination.Search, ToUserId), cancellationToken);

        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(result);
    }
}
