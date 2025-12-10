using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Services;

public interface IOrderService
{
    Task<Response<OrderDto>> AddOrder(CreateOrderDto order);
    Task<Response<OrderDto>> UpdateOrder(OrderDto order);
    Task<Response<string>> DeleteOrder(int id);
    Task<Response<PagedResponse<List<OrderDto>>>> GetOrders(OrderFilter filter);
    Task<Response<OrderDto>> GetOrderById(int id);
}