using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;

public class User : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Password { get; set; } = default!;
    public UserRoleEnum Role { get; set; } = default!;

    public ICollection<Product> UserProducts { get; set; } = default!;

    public Address Address { get; set; } = default!;

    public ICollection<Feedback> ReceivedFeedbacks { get; set; } = default!;
    public ICollection<Feedback> OfferedFeedbacks { get; set; } = default!;
}
