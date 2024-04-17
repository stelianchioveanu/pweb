using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;


public sealed class ProductTagSpec : BaseSpec<ProductTagSpec, ProductTag>
{
    public ProductTagSpec(Guid id) : base(id)
    {
    }

    public ProductTagSpec(string tag)
    {
        Query.Where(e => e.Tag == tag);
    }
}