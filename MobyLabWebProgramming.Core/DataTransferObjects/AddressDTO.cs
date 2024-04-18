namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddressDTO
{
    public Guid Id { get; set; }
    public string StreetName { get; set; } = default!;
    public string City { get; set; } = default!;
    public int Number { get; set; } = default!;
    public string PhoneNumber { get; set; } = default!;
}
