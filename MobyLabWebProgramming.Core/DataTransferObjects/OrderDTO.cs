namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class OrderDTO
{
    public ProductDTO Product { get; set; } = default!;
    public UserDTO User { get; set; } = default!;
    public Guid Id { get; set; }
}
