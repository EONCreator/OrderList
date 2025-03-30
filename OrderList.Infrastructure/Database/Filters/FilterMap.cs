namespace OrderList.Infrastructure.Database.Filters;

using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

public class FilterMap
{
    private readonly Dictionary<FilterType, Func<Filter, FilterDefinition<BsonDocument>>> _filterMap;

    public FilterMap()
    {
        _filterMap = new Dictionary<FilterType, Func<Filter, FilterDefinition<BsonDocument>>>
        {
            { FilterType.String, f => Builders<BsonDocument>.Filter.Regex(f.Column, new BsonRegularExpression(f.Value.ToString(), "i")) },
            { FilterType.Number, f => Builders<BsonDocument>.Filter.Eq(f.Column, Convert.ToInt32(f.Value)) },
            { FilterType.Date, f => Builders<BsonDocument>.Filter.Lte(f.Column, f.Value) }
        };
    }

    public FilterDefinition<BsonDocument> GetFilter(Filter filter)
    {
        if (_filterMap.TryGetValue(filter.Type, out var filterFunc))
        {
            return filterFunc(filter);
        }

        throw new ArgumentException($"Unsupported filter type: {filter.Type}");
    }
}