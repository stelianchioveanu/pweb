using Microsoft.AspNetCore.Http;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class UserFileService : IUserFileService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;
    private readonly IFileRepository _fileRepository;

    /// <summary>
    /// This static method creates the path for a user to where it has to store the files, each user should have an own folder.
    /// </summary>
    private static string GetFileDirectory(Guid userId) => Path.Join(userId.ToString(), IUserFileService.UserFilesDirectory);

    /// <summary>
    /// Inject the required services through the constructor.
    /// </summary>
    public UserFileService(IRepository<WebAppDatabaseContext> repository, IFileRepository fileRepository)
    {
        _repository = repository;
        _fileRepository = fileRepository;
    }

    public async Task<ServiceResponse> SaveFile(IFormFile file, Product currProduct, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        var fileName = _fileRepository.SaveFile(file, GetFileDirectory(requestingUser.Id));

        if (fileName.Result == null)
        {
            return fileName.ToResponse();
        }

        await _repository.AddAsync(new UserFile
        {
            Name = file.FileName,
            Path = fileName.Result,
            ProductId = currProduct.Id
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public ServiceResponse DeleteFile(UserFile file, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        return _fileRepository.DeleteFile(Path.Join(GetFileDirectory(requestingUser.Id), file.Name));
    }
}
