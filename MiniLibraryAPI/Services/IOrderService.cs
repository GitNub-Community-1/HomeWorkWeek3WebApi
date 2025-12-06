using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.Services;

public interface IOrderService
{
    Task<OrderDto> AddOrder(CreateOrderDto order);
    Task<Order> UpdateOrder(OrderDto order);
    Task<int> DeleteOrder(int id);
    Task<List<OrderDto>> GetOrders(OrderFilter filter);
    Task<OrderDto> GetOrderById(int id);
}