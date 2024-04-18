using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;


public interface IAddressService
{
    public Task<ServiceResponse> UpdateAddress(AddressUpdateDTO address, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
