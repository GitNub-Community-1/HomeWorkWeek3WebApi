using Microsoft.EntityFrameworkCore;
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;

namespace MiniLibraryAPI.Services;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    public async Task<Order> AddOrder(Order order)
    {
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return order;    }

    public async Task<Order> UpdateOrder(Order order)
    {
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        return order;    }

    public async Task<int> DeleteOrder(int id)
    {
        var order = await context.Orders.FindAsync(id);
        context.Orders.Remove(order);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<Order>> GetOrders(OrderFilter filter)
    {
        var query = context.Orders
            .AsQueryable();
        
        if (filter.Id.HasValue)
        {
            query = query.Where(x => x.Id == filter.Id.Value);
        }
        if (!string.IsNullOrEmpty(filter.Name))
        {
            query = query.Where(x => x.Name.Contains(filter.Name));
        }
        if (filter.OrderDate.HasValue)
        {
            query = query.Where(x => x.OrderDate == filter.OrderDate.Value);
        }
        if (!string.IsNullOrEmpty(filter.Phone))
        {
            query = query.Where(x => x.Phone.Contains(filter.Phone));
        }
        
        return await query.ToListAsync();
    }

    public async Task<Order> GetOrderById(int id)
    {
        return await context.Orders.FirstOrDefaultAsync(a => a.Id == id);
    }
}