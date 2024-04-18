using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

/// <summary>
/// This service is a simple service to demonstrate how to work with files.
/// </summary>
public interface IUserFileService
{
    public const string UserFilesDirectory = "UserFiles";

    public Task<ServiceResponse> SaveFile(IFormFile file, Product currProduct, UserDTO requestingUser, CancellationToken cancellationToken = default);

    public ServiceResponse DeleteFile(UserFile file, UserDTO requestingUser, CancellationToken cancellationToken = default);
}
