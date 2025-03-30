using System.Text.Json;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;
using OrderList.Infrastructure.Database.Filters;
using OrderList.Infrastructure.Results;

namespace OrderList.Services.Orders;

public interface IOrderRepository
{
    Task<OperationResult> CreateOrderAsync(JObject record);
    Task<DataResult<object>> GetOrdersAsync(
        List<Filter> filter, 
        SortDefinition? sortDefinitions,
        int pageNumber,
        int pageSize);
}