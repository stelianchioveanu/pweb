namespace MobyLabWebProgramming.Core.Entities;
public class Address : BaseEntity
{
    public string StreetName { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string City { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
}
