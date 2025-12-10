using Microsoft.AspNetCore.Mvc;
using MiniLibraryAPI.DTOs;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;
using MiniLibraryAPI.Services;
using MiniLibraryAPI.Infrastructure.Responses;

namespace MiniLibraryAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService _service) : ControllerBase
{
    [HttpPost]
    public async Task<Response<OrderDto>> AddOrder(CreateOrderDto order)
    {
        var result = await _service.AddOrder(order);
        return result;
    }
    
    [HttpPut]
    public async Task<Response<OrderDto>> UpdateOrder(OrderDto order)
    { 
        var result = await _service.UpdateOrder(order);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<Response<string>> DeleteOrder(int id)
    {
        var result = await _service.DeleteOrder(id);
        return result;
    }


    [HttpGet]
    public async Task<Response<PagedResponse<List<OrderDto>>>> GetOrders([FromQuery]OrderFilter filter)
    {
        var result = await _service.GetOrders(filter);
        return result;
    }

    [HttpGet("{id}")]
    public async Task<Response<OrderDto>> GetOrderById(int id)
    {
        var result = await _service.GetOrderById(id);
        return result;
    }
}