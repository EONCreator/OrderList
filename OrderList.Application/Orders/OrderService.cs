using System.Text.Json;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using OrderList.Application.Interfaces;
using OrderList.Infrastructure.Database.Filters;
using OrderList.Infrastructure.Requests.Payload;
using OrderList.Infrastructure.Results;
using OrderList.Services.Orders;

namespace OrderList.Application.Orders;

public class OrderService(IOrderRepository repository) : IOrderService
{
    public async Task<OperationResult> CreateOrder(JObject order)
        => await repository.CreateOrderAsync(order);

    public async Task<DataResult<object>> GetOrders(RequestData requestData)
        => await repository.GetOrdersAsync(
            requestData.Filters, 
            requestData.SortDefinition, 
            requestData.PageNumber, 
            requestData.PageSize);
}