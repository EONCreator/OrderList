namespace OrderList.Infrastructure.Results;

public class DataResult<T>
{
    public List<T> Items { get; set; }
    public int TotalPages { get; set; }
}