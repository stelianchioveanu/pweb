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
using Org.BouncyCastle.Utilities.Net;
using Serilog;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ProductService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddProduct(ProductAddDTO product, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (product == null || product.Price == 0 || product.Name.IsNullOrEmpty() || product.Description.IsNullOrEmpty())
        {
            return ServiceResponse.FromError(new(HttpStatusCode.BadRequest, "Every input should have at least 1 character and the price should be higher then 0!", ErrorCodes.WrongInputs));
        }

        var newProduct = await _repository.AddAsync(new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            UserId = requestingUser.Id,
        }, cancellationToken);

        if (product.Tags != null && product.Tags.Count != 0)
        {
            foreach (var tag in product.Tags)
            {
                var entity = await _repository.GetAsync(new ProductTagSpec(tag), cancellationToken);
                if (entity != null)
                {
                    if (entity.Products == null)
                    {
                        entity.Products = new HashSet<Product>();
                    }
                    entity.Products.Add(newProduct);
                    await _repository.UpdateAsync(entity, cancellationToken);
                }
            }
        }

        if (product.Files != null)
        {
            foreach (var file in product.Files)
            {
                await _userFileService.SaveFile(file, newProduct, requestingUser, cancellationToken);
            }
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<ProductDTO>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken);

        if (result != null)
        {
            result.FilePaths = new List<string>();
            var files = await _repository.ListAsync(new UserFileProjectionSpec(id), cancellationToken);
            foreach (var file in files)
            {
                result.FilePaths.Add(file.Path);
            }
        }

        return ServiceResponse<ProductDTO>.ForSuccess(result);
    }
}
