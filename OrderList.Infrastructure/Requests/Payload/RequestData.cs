using OrderList.Infrastructure.Database.Filters;

namespace OrderList.Infrastructure.Requests.Payload;

public class RequestData
{
    public List<Filter> Filters { get; set; }
    public SortDefinition? SortDefinition { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}