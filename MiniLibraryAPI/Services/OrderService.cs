using Microsoft.EntityFrameworkCore;   
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using AutoMapper;

namespace MiniLibraryAPI.Services;

public class OrderService(ApplicationDbContext context,  IMapper mapper) : IOrderService
{
    public async Task<OrderDto> AddOrder(CreateOrderDto orderdto)
    {
        var order = mapper.Map<Order>(orderdto);
        context.Orders.Add(order);
        await context.SaveChangesAsync();
        return mapper.Map<OrderDto>(order);;    
    }

    public async Task<Order> UpdateOrder(OrderDto order_)
    {
        var check = await context.Orders.FindAsync(order_.Id);
        check.Name = order_.Name;
        check.Phone = order_.Phone;
        check.BookId = order_.BookId;
        context.Orders.Update(check);
        await context.SaveChangesAsync();
        return mapper.Map<Order>(order_);    
    }

    public async Task<int> DeleteOrder(int id)
    {
        var order = await context.Orders.FindAsync(id);
        context.Orders.Remove(order);
        var i =  await context.SaveChangesAsync();
        return i;
    }

    public async Task<List<OrderDto>> GetOrders(OrderFilter filter)
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
        
        var orders = await query.ToListAsync();
        var translate = mapper.Map<List<OrderDto>>(orders);
        return translate;
    }

    public async Task<OrderDto> GetOrderById(int id)
    {
        var order = await context.Orders.FirstOrDefaultAsync(a => a.Id == id);
        var translate = mapper.Map<OrderDto>(order);
        return translate;
    }
}