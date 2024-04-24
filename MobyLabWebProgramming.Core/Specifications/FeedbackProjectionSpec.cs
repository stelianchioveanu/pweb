using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class FeedbackProjectionSpec : BaseSpec<FeedbackProjectionSpec, Feedback, FeedbackDTO>
{
    protected override Expression<Func<Feedback, FeedbackDTO>> Spec => e => new()
    {
        Id = e.Id,
        Stars = e.Stars,
        Description = e.Description
    };

    public FeedbackProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public FeedbackProjectionSpec(string? search, Guid id)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            Query.Where(e => e.ToUserId == id);
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Description, searchExpr) && e.ToUserId == id);
    }
}
