namespace OrderList.Infrastructure.Database.Filters;

public enum FilterType
{
    String,
    Number,
    Date
}

public class Filter
{
    public string Column { get; set; }
    public FilterType Type { get; set; }
    public object Value { get; set; }
}