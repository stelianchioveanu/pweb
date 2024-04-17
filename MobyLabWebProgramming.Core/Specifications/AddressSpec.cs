using MobyLabWebProgramming.Core.Entities;
using Ardalis.Specification;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class AddressSpec : BaseSpec<AddressSpec, Address>
{
    public AddressSpec(Guid id)
    {
        Query.Where(e => e.UserId == id);
    }
}