using System.Net;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

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
            return ServiceResponse.FromError(CommonErrors.WrongInputsProduct);
        }

        string[] allowedExt = { ".jpeg", ".jpg", ".png" };

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

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<ProductDTO>> GetProduct(Guid id, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<ProductDTO>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.GetAsync(new ProductProjectionSpec(id), cancellationToken);

        if (result == null)
        {
            return ServiceResponse<ProductDTO>.FromError(CommonErrors.ProductNotFound);
        }

        return ServiceResponse<ProductDTO>.ForSuccess(result);
    }

    public async Task<ServiceResponse> DeleteProduct(Guid id, IUserFileService _userFileService, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<ProductDTO>.FromError(CommonErrors.UserNotFound);
        }

        var product = await _repository.GetAsync(new ProductSpec(id), cancellationToken);

        if (product == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProductNotFound);
        }

        if (requestingUser.Id != product.UserId && requestingUser.Role != Core.Enums.UserRoleEnum.Admin && requestingUser.Role != Core.Enums.UserRoleEnum.Personnel)
        {
            return ServiceResponse.FromError(CommonErrors.CannotDeleteProducts);
        }

        await _repository.DeleteAsync(new ProductSpec(id), cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetProducts(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<ProductDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<PagedResponse<ProductDTO>>> GetMyProducts(PaginationSearchQueryParams pagination, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<PagedResponse<ProductDTO>>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.PageAsync(pagination, new ProductProjectionSpec(pagination.Search, requestingUser.Id), cancellationToken);

        return ServiceResponse<PagedResponse<ProductDTO>>.ForSuccess(result);
    }
}
