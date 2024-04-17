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

public class UserService : IUserService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly ILoginService _loginService;
    private readonly IMailService _mailService;

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public UserService(IRepository<WebAppDatabaseContext> repository, ILoginService loginService, IMailService mailService)
    {
        _repository = repository;
        _loginService = loginService;
        _mailService = mailService;
    }

    public async Task<ServiceResponse<UserDTO>> GetUser(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserProjectionSpec(id), cancellationToken); // Get a user using a specification on the repository.

        return result != null ? 
            ServiceResponse<UserDTO>.ForSuccess(result) : 
            ServiceResponse<UserDTO>.FromError(CommonErrors.UserNotFound); // Pack the result or error into a ServiceResponse.
    }

    public async Task<ServiceResponse<PagedResponse<UserDTO>>> GetUsers(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new UserProjectionSpec(pagination.Search), cancellationToken); // Use the specification and pagination API to get only some entities from the database.

        return ServiceResponse<PagedResponse<UserDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<LoginResponseDTO>> Login(LoginDTO login, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserSpec(login.Email), cancellationToken);

        if (result == null) // Verify if the user is found in the database.
        {
            return ServiceResponse<LoginResponseDTO>.FromError(CommonErrors.UserNotFound); // Pack the proper error as the response.
        }

        if (result.Password != login.Password) // Verify if the password hash of the request is the same as the one in the database.
        {
            return ServiceResponse<LoginResponseDTO>.FromError(new(HttpStatusCode.BadRequest, "Wrong password!", ErrorCodes.WrongPassword));
        }

        var user = new UserDTO
        {
            Id = result.Id,
            Email = result.Email,
            Name = result.Name,
            Role = result.Role
        };

        return ServiceResponse<LoginResponseDTO>.ForSuccess(new()
        {
            User = user,
            Token = _loginService.GetToken(user, DateTime.UtcNow, new(7, 0, 0, 0)) // Get a JWT for the user issued now and that expires in 7 days.
        });
    }

    public async Task<ServiceResponse<int>> GetUserCount(CancellationToken cancellationToken = default) => 
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<User>(cancellationToken)); // Get the count of all user entities in the database.

    public async Task<ServiceResponse> AddUser(UserAddDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add users!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new UserSpec(user.Email), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The user already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new User
        {
            Email = user.Email,
            Name = user.Name,
            Role = user.Role,
            Password = user.Password
        }, cancellationToken); // A new entity is created and persisted in the database.

        await _mailService.SendMail(user.Email, "Welcome!", MailTemplates.UserAddTemplate(user.Name), true, "My App", cancellationToken); // You can send a notification on the user email. Change the email if you want.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateUser(UserUpdateDTO user, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != user.Id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can update the user!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new UserSpec(user.Id), cancellationToken);

        if (entity != null) // Verify if the user is not found, you cannot update an non-existing entity.
        {
            entity.Name = user.Name ?? entity.Name;
            entity.Password = user.Password ?? entity.Password;

            await _repository.UpdateAsync(entity, cancellationToken); // Update the entity and persist the changes.
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteUser(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Id != id) // Verify who can add the user, you can change this however you se fit.
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or the own user can delete the user!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<User>(id, cancellationToken); // Delete the entity.

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> Register(RegisterDTO register, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new UserSpec(register.Email), cancellationToken);

        if (register == null || register.Email.IsNullOrEmpty() || register.Name.IsNullOrEmpty() || register.Password.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Every input should have at least 1 character!", ErrorCodes.WrongInputs));
        }

        if (result != null)
        {
            return ServiceResponse.FromError(CommonErrors.UserAlreadyExists);
        }

        await _repository.AddAsync(new User
        {
            Email = register.Email,
            Name = register.Name,
            Role = UserRoleEnum.Client,
            Password = PasswordUtils.HashPassword(register.Password)
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> AddProductTag(ProductTagAddDTO tag, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can add product tags!", ErrorCodes.CannotAdd));
        }

        if (tag == null || tag.Tag.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "The tag should have at least 1 character!", ErrorCodes.WrongTag));
        }

        var result = await _repository.GetAsync(new ProductTagSpec(tag.Tag), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "Tag already exists!", ErrorCodes.TagAlreadyExists));
        }

        await _repository.AddAsync(new ProductTag
        {
            Tag = tag.Tag
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProductTag(Guid id, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin && requestingUser.Role != UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin or personnel can delete product tags!", ErrorCodes.CannotAdd));
        }

        await _repository.DeleteAsync<ProductTag>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<ProductTagDTO>>> GetProductTags(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductTagProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductTagDTO>>.ForSuccess(result);
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
        } else
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
}
