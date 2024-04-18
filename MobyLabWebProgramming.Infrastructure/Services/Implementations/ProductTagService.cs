using System.Net;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using Serilog;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProductTagService : IProductTagService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProductTagService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddProductTag(ProductTagAddDTO tag, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add product tags!", ErrorCodes.CannotAdd));
        }

        if (tag == null || tag.Tag.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "The tag should have at least 1 character!", ErrorCodes.WrongTag));
        }

        var result = await _repository.GetAsync(new ProductTagSpec(tag.Tag), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "Tag already exists!", ErrorCodes.TagAlreadyExists));
        }

        await _repository.AddAsync(new ProductTag
        {
            Tag = tag.Tag
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProductTag(Guid id, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete product tags!", ErrorCodes.CannotAdd));
        }

        await _repository.DeleteAsync<ProductTag>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<ProductTagDTO>>> GetProductTags(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductTagProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductTagDTO>>.ForSuccess(result);
    }
}
