using Microsoft.EntityFrameworkCore;   
using MiniLibraryAPI.Data;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using AutoMapper;
using MiniLibraryAPI.Infrastructure.Responses;
using System.Net;

namespace MiniLibraryAPI.Services;

public class OrderService(ApplicationDbContext context,  IMapper mapper) : IOrderService
{
    public async Task<Response<OrderDto>> AddOrder(CreateOrderDto orderdto)
    {
        try
        {
            var order = mapper.Map<Order>(orderdto);
            context.Orders.Add(order);
            await context.SaveChangesAsync();
            var result = mapper.Map<OrderDto>(order);
            return new Response<OrderDto>(HttpStatusCode.Created, "Order created successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<OrderDto>> UpdateOrder(OrderDto order_)
    {
        try
        {
            var check = await context.Orders.FindAsync(order_.Id);
            if (check == null)
                return new Response<OrderDto>(HttpStatusCode.NotFound, "Order not found");
            
            check.Name = order_.Name;
            check.Phone = order_.Phone;
            check.BookId = order_.BookId;
            context.Orders.Update(check);
            await context.SaveChangesAsync();
            var result = mapper.Map<OrderDto>(check);
            return new Response<OrderDto>(HttpStatusCode.OK, "Order updated successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<string>> DeleteOrder(int id)
    {
        try
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
                return new Response<string>(HttpStatusCode.NotFound, "Order not found");
            
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return new Response<string>(HttpStatusCode.OK, "Order deleted successfully!");
        }
        catch (Exception ex)
        {
            return new Response<string>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<PagedResponse<List<OrderDto>>>> GetOrders(OrderFilter filter)
    {
        try
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
            
            var totalRecords = await query.CountAsync();
            
            var page = filter.Page > 0 ? filter.Page : 1;
            var size = filter.Size > 0 ? filter.Size : 20;

            var orders = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync();
            
            var result = mapper.Map<List<OrderDto>>(orders);

            var pagedResponse = new PagedResponse<List<OrderDto>>
            {
                Data = result,
                Page = page,
                Size = size,
                TotalRecords = totalRecords
            };

            return new Response<PagedResponse<List<OrderDto>>>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Message = "Orders retrieved successfully!",
                Data = pagedResponse
            };
        }
        catch (Exception ex)
        {
            return new Response<PagedResponse<List<OrderDto>>>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }

    public async Task<Response<OrderDto>> GetOrderById(int id)
    {
        try
        {
            var order = await context.Orders.FirstOrDefaultAsync(a => a.Id == id);
            if (order == null)
                return new Response<OrderDto>(HttpStatusCode.NotFound, "Order not found");
            
            var result = mapper.Map<OrderDto>(order);
            return new Response<OrderDto>(HttpStatusCode.OK, "Order retrieved successfully!", result);
        }
        catch (Exception ex)
        {
            return new Response<OrderDto>(HttpStatusCode.BadRequest, $"Error: {ex.Message}");
        }
    }
}