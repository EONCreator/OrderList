using System.Text.Json;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;
using OrderList.Infrastructure.Database.Filters;
using OrderList.Infrastructure.Requests.Payload;
using OrderList.Infrastructure.Results;

namespace OrderList.Application.Interfaces;

public interface IOrderService
{
    Task<OperationResult> CreateOrder(JObject order);
    Task<DataResult<object>> GetOrders(RequestData requestData);
}