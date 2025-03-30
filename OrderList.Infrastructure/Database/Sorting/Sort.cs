namespace OrderList.Infrastructure.Database.Filters;

public enum SortType
{
    Ascending,
    Descending
}

public class SortDefinition
{
    public string Column { get; set; }
    public SortType Type { get; set; }
}