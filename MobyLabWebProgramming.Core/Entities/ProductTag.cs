using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.Entities;
public class ProductTag : BaseEntity
{
    public ProductTagEnum Tag { get; set; } = default!;
    public ICollection<Product>? Products { get; set; }
}
