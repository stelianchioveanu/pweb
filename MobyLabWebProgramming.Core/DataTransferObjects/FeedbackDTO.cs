namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class FeedbackDTO
{
    public Guid Id { get; set; }
    public string Description { get; set; } = default!;
    public int Stars { get; set; } = default!;
}