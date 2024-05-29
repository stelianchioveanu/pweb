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
            return ServiceResponse.FromError(CommonErrors.WrongInputs);
        }

        if (feedback.Stars < 0 || feedback.Stars > 5)
        {
            return ServiceResponse.FromError(CommonErrors.WrongInputs);
        }

        await _repository.AddAsync(new Feedback
        {
            Description = feedback.Description,
            Stars = feedback.Stars,
            FromUserId = requestingUser.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<FeedbackDTO>>> GetFeedbacks(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {

        var result = await _repository.PageAsync(pagination, new FeedbackProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<FeedbackDTO>>.ForSuccess(result);
    }
}
