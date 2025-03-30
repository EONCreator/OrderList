using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderList.Infrastructure.Database;
using OrderList.Infrastructure.Database.Filters;
using OrderList.Infrastructure.Results;

namespace OrderList.Services.Orders;

public class OrderRepository(
    AppDbContext context, 
    FilterBuilder filterBuilder,
    SortBuilder sortBuilder) : IOrderRepository
{
    public async Task<OperationResult> CreateOrderAsync(JObject record)
    {
        try
        {
            var jsonString = record.ToString(Formatting.None);
            var bsonDocument = BsonSerializer.Deserialize<BsonDocument>(jsonString);
            await context.Records.InsertOneAsync(bsonDocument);
        }
        catch (MongoWriteException ex)
        {
            return OperationResult.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            return OperationResult.Failure(ex.Message);
        }

        return OperationResult.Succeeded;
    }

    public async Task<DataResult<object>> GetOrdersAsync(
        List<Filter> filters, 
        SortDefinition? sortDefinitions,
        int pageNumber, 
        int pageSize)
    {
        var filter = filterBuilder.BuildFilter(filters);
    
        var totalCount = await context.Records.CountDocumentsAsync(filter);
        var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

        var query = context.Records.Find(filter);

        var sort = sortBuilder.BuildSort(sortDefinitions);
        if (sort != null)
        {
            query = query.Sort(sort);
        }

        var orders = await query
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize) 
            .ToListAsync();

        return new DataResult<object>
        {
            Items = orders.Select(BsonTypeMapper.MapToDotNetValue).ToList(),
            TotalPages = totalPages
        };
    }
}