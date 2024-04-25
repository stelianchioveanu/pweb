using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IOrderService
{
    public Task<ServiceResponse> AddOrder(OrderAddDTO order, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<OrderDTO>>> GetMyOrders(PaginationSearchQueryParams pagination, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
