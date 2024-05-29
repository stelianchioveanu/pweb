using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class OrderProjectionSpec : BaseSpec<OrderProjectionSpec, Order, OrderDTO>
{
    protected override Expression<Func<Order, OrderDTO>> Spec => e => new()
    {
        User = new UserDTO
        {
            Name = e.User.Name,
            Email = e.User.Email,
            Id = e.User.Id,
            Role = e.User.Role,
        },
        Product = new ProductDTO
        {
            Price = e.Product.Price,
            Description = e.Product.Description,
            Id = e.Product.Id,
            Name = e.Product.Name,
            Tags = e.Product.ProductTags.Select(x => x.Tag).ToList(),
            User = new UserDTO
            {
                Email = e.Product.User.Email,
                Id = e.Product.User.Id,
                Role = e.Product.User.Role,
                Name = e.Product.User.Name,
            }
        },
        Id = e.Id,
    };

    public OrderProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public OrderProjectionSpec(string? search, Guid id, bool type)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (type)
        {
            if (search == null)
            {
                Query.Where(e => e.UserId == id);
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Product.Name, searchExpr) && e.UserId == id);

        } else
        {
            if (search == null)
            {
                Query.Where(e => e.Product.User.Id == id);
                return;
            }

            var searchExpr = $"%{search.Replace(" ", "%")}%";

            Query.Where(e => EF.Functions.ILike(e.Product.Name, searchExpr) && e.Product.User.Id == id);
        }
    }
}
