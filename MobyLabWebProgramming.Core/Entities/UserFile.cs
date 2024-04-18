namespace MobyLabWebProgramming.Core.Entities;

/// <summary>
/// This is an example for another entity to store files and an example for a One-To-Many relation.
/// </summary>
public class UserFile : BaseEntity
{
    public string Path { get; set; } = default!;
    public string Name { get; set; } = default!;
    public string? Description { get; set; }

    public Product Product { get; set; } = default!;
    public Guid ProductId { get; set; }
}
