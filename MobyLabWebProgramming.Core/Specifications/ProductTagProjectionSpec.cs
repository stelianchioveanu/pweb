using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class ProductTagProjectionSpec : BaseSpec<ProductTagProjectionSpec, ProductTag, ProductTagDTO>
{
    protected override Expression<Func<ProductTag, ProductTagDTO>> Spec => e => new()
    {
        Id = e.Id,
        Tag = e.Tag
    };

    public ProductTagProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProductTagProjectionSpec(Guid id) : base(id)
    {
    }

    public ProductTagProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Tag, searchExpr));
    }
}
