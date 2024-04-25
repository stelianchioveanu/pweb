using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;


public sealed class OrderSpec : BaseSpec<OrderSpec, Order>
{
    public OrderSpec(Guid id) : base(id)
    {
    }

    public OrderSpec(Guid productId, Guid userId)
    {
        Query.Where(e => e.UserId == userId && e.ProductId == productId);
    }
}