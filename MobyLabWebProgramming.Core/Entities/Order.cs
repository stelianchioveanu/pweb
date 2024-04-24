namespace MobyLabWebProgramming.Core.Entities;

public class Order : BaseEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = default!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = default!;
}
