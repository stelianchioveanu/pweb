using System.Linq.Expressions;
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MobyLabWebProgramming.Core.Specifications;

/// <summary>
/// This is a specification to filter the user entities and map it to and UserDTO object via the constructors.
/// Note how the constructors call the base class's constructors. Also, this is a sealed class, meaning it cannot be further derived.
/// </summary>
public sealed class AddressProjectionSpec : BaseSpec<AddressProjectionSpec, Address, AddressDTO>
{
    protected override Expression<Func<Address, AddressDTO>> Spec => e => new()
    {
        Id = e.Id,
        StreetName = e.StreetName,
        City = e.City,
        Number = e.Number,
        PhoneNumber = e.PhoneNumber,
    };

    public AddressProjectionSpec(bool orderByCreatedAt = true) : base(orderByCreatedAt)
    {
    }

    public AddressProjectionSpec(Guid id)
    {
        Query.Where(e => e.UserId == id);
    }
}
