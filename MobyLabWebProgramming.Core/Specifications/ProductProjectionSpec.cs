﻿using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class ProductProjectionSpec : BaseSpec<ProductProjectionSpec, Product, ProductDTO>
{
    /// <summary>
    /// This is the projection/mapping expression to be used by the base class to get UserDTO object from the database.
    /// </summary>
    protected override Expression<Func<Product, ProductDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Price = e.Price,
        Description = e.Description,
        User = new UserDTO
        {
            Email = e.User.Email,
            Id = e.User.Id,
            Name = e.User.Name,
            Role = e.User.Role,
        },
        Tags = e.ProductTags.Select(x => x.Tag).ToList(),
    };

    public ProductProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public ProductProjectionSpec(Guid id) : base(id)
    {
    }

    public ProductProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) || EF.Functions.ILike(e.Description, searchExpr));
    }

    public ProductProjectionSpec(string? search, Guid id)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            Query.Where(e => e.UserId == id);
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => (EF.Functions.ILike(e.Name, searchExpr) || EF.Functions.ILike(e.Description, searchExpr)) && e.UserId == id);
    }
}
