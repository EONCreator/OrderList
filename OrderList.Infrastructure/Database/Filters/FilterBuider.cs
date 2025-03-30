using MongoDB.Bson;
using MongoDB.Driver;

namespace OrderList.Infrastructure.Database.Filters;

public class FilterBuilder(FilterMap filterMap)
{
    public FilterDefinition<BsonDocument> BuildFilter(List<Filter> filters)
    {
        var filterBuilder = Builders<BsonDocument>.Filter;
        var filterList = new List<FilterDefinition<BsonDocument>>();

        foreach (var f in filters)
        {
            var mongoFilter = filterMap.GetFilter(f);
            filterList.Add(mongoFilter);
        }

        return filterList.Count > 0 ? filterBuilder.And(filterList) : filterBuilder.Empty;
    }
}