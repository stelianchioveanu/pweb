using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IProductService
{
    public Task<ServiceResponse> AddProduct(ProductAddDTO product, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteProduct(Guid id, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    //public Task<ServiceResponse> DeleteProduct(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    //public Task<ServiceResponse<PagedResponse<ProductTagDTO>>> GetProductTags(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
}
