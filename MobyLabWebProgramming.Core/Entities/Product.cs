namespace MobyLabWebProgramming.Core.Entities;

public class Product  : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;

    public ICollection<ProductTag> ProductTags { get; set; } = default!;
    public ICollection<UserFile>? UserFiles { get; set; } = default!;
    public ICollection<Order> Orders { get; set; } = default!;
}
