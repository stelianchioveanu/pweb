namespace MobyLabWebProgramming.Core.DataTransferObjects;


public class ProductDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
    public Guid UserId { get; set; }
}
