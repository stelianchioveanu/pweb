﻿using System.Net;
using Microsoft.IdentityModel.Tokens;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public OrderService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddOrder(OrderAddDTO order, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse.FromError(CommonErrors.UserNotFound);
        }

        if (order == null)
        {
            return ServiceResponse.FromError(CommonErrors.WrongInputs);
        }

        var aux = await _repository.GetAsync(new OrderSpec(order.ProductId, requestingUser.Id), cancellationToken);

        if (aux != null)
        {
            return ServiceResponse.FromError(CommonErrors.OrderAlreadyExists);
        }

        var entity = await _repository.GetAsync(new ProductSpec(order.ProductId), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(CommonErrors.ProductNotFound);
        }

        if (entity.UserId == requestingUser.Id)
        {
            return ServiceResponse.FromError(CommonErrors.CannotAddOrder);
        }

        await _repository.AddAsync(new Order
        {
            UserId = requestingUser.Id,
            ProductId = order.ProductId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetOrders(PaginationSearchQueryParams pagination, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<PagedResponse<OrderDTO>>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(pagination.Search, requestingUser.Id, false), cancellationToken);

        return ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<PagedResponse<OrderDTO>>> GetMyOrders(PaginationSearchQueryParams pagination, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser == null)
        {
            return ServiceResponse<PagedResponse<OrderDTO>>.FromError(CommonErrors.UserNotFound);
        }

        var result = await _repository.PageAsync(pagination, new OrderProjectionSpec(pagination.Search, requestingUser.Id, true), cancellationToken);

        return ServiceResponse<PagedResponse<OrderDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse> DeleteOrder(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetAsync(new OrderSpec(id), cancellationToken);

        if (entity == null)
        {
            return ServiceResponse.FromError(CommonErrors.OrderNotFound);
        }

        var entity2 = await _repository.GetAsync(new ProductSpec(entity.ProductId), cancellationToken);

        if (entity2 == null)
        {
            return ServiceResponse.FromError(CommonErrors.OrderNotFound);
        }

        if (requestingUser != null && requestingUser.Id != entity.UserId && requestingUser.Id != entity2.UserId)
        {
            return ServiceResponse.FromError(CommonErrors.CannotDeleteOrder);
        }

        await _repository.DeleteAsync<Order>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}