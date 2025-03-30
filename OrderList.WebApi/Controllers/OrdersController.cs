using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrderList.Application.Interfaces;
using OrderList.Infrastructure.Requests.Payload;

namespace OrderList.WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController(IOrderService orderService) : Controller
{
    [HttpPost("[action]")]
    public async Task<IActionResult> GetOrders([FromBody] RequestData requestData)
        => Ok(await orderService.GetOrders(requestData));

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateOrder([FromBody] JObject order)
    {
        Console.WriteLine($"Received Order: {order}");
        var result = await orderService.CreateOrder(order);
        if (!result.Success)
            return StatusCode(500, result.Message);
        
        return Ok(result);
    }
}