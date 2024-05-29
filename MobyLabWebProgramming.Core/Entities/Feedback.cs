namespace MobyLabWebProgramming.Core.Entities;

public class Feedback : BaseEntity
{
    public string Description { get; set; } = default!;
    public int Stars { get; set; } = default!;
    public Guid FromUserId { get; set; }
    public User FromUser { get; set; } = default!;
}
