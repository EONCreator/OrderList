using MongoDB.Bson;
using MongoDB.Driver;

namespace OrderList.Infrastructure.Database.Filters;

public class SortBuilder
{
    public SortDefinition<BsonDocument>? BuildSort(SortDefinition? sortDefinition)
    {
        if (sortDefinition == null)
            return null;

        return sortDefinition.Type == SortType.Ascending
            ? Builders<BsonDocument>.Sort.Ascending(sortDefinition.Column)
            : Builders<BsonDocument>.Sort.Descending(sortDefinition.Column);
    }
}