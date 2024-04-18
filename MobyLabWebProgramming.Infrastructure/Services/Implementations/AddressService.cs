using System.Net;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using Serilog;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class AddressService : IAddressService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public AddressService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> UpdateAddress(AddressUpdateDTO address, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (address == null || address.Number == 0 || address.StreetName.IsNullOrEmpty() || address.StreetName.IsNullOrEmpty() || address.PhoneNumber.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Every input should have at least 1 character!", ErrorCodes.WrongInputs));
        }

        var oldAddress = await _repository.GetAsync(new AddressSpec(requestingUser.Id), cancellationToken);

        if (oldAddress != null)
        {
            oldAddress.StreetName = address.StreetName;
            oldAddress.City = address.City;
            oldAddress.Number = address.Number;
            oldAddress.PhoneNumber = address.PhoneNumber;

            await _repository.UpdateAsync(oldAddress, cancellationToken);
        }
        else
        {
            await _repository.AddAsync(new Address
            {
                StreetName = address.StreetName,
                City = address.City,
                Number = address.Number,
                PhoneNumber = address.PhoneNumber,
                UserId = requestingUser.Id,
            }, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<AddressDTO>> GetAddress(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<AddressDTO>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.GetAsync(new AddressProjectionSpec(id), cancellationToken);


        return ServiceResponse<AddressDTO>.ForSuccess(result);
    }
}
