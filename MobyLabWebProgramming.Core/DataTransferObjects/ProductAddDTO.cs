namespace MobyLabWebProgramming.Core.DataTransferObjects;


public class ProductAddDTO
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public int Price { get; set; } = default!;
}