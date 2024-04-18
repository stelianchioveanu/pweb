using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductTagService
{
    public Task<ServiceResponse> AddProductTag(ProductTagAddDTO tag, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteProductTag(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<ProductTagDTO>>> GetProductTags(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
