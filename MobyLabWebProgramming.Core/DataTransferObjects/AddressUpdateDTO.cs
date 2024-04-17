using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class AddressUpdateDTO
{
    public string StreetName { get; set; } = default!;
    public string City { get; set; } = default!;
    public int Number { get; set; } = default!;
    public UserRoleEnum Role { get; set; } = default!;
}
