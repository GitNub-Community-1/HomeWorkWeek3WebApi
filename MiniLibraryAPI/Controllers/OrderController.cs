using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Services;

namespace MiniLibraryAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Order> AddOrder(Order order)
    {
        var CreatedOrder = await _service.AddOrder(order);
        return CreatedOrder;
    }
    
    [HttpPut]
    public async Task<Order> UpdateOrder( Order order)
    { 
        var updateOrder = await _service.UpdateOrder(order);
        return updateOrder;
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteOrder(int id)
    {
        var result = await _service.DeleteOrder(id);
        if(result>0)
        {
            return "Delete Succefully!";
        }

        return "Deleted not succefully!";
    }


    [HttpGet]
    public async Task<List<Order>> GetOrders([FromQuery]OrderFilter filter)
    {
        var orders = await _service.GetOrders(filter);
        return orders;
    }

    [HttpGet("{id}")]
    public async Task<Order> GetOrderById(int id)
    {
        var order = await _service.GetOrderById(id);
        return order;
    }
}