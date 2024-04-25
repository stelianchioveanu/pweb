namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class OrderDTO
{
    public Guid ProductId { get; set; }
    public Guid UserId { get; set; }

    public Guid Id { get; set; }
}
