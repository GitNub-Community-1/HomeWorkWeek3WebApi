using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public interface IOrderService
{
    Task<Order> AddOrder(Order order);
    Task<Order> UpdateOrder(Order order);
    Task<int> DeleteOrder(int id);
    Task<List<Order>> GetOrders(OrderFilter filter);
    Task<Order> GetOrderById(int id);
}