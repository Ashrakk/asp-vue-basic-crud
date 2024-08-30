namespace CorsoASP.Core.Views;

public class PaginatedDataView<T>
{
    public IEnumerable<T> Data { get; set; }
    public int TotalCount { get; set; }

    public PaginatedDataView(IEnumerable<T> data, int totalCount)
    {
        Data = data;
        TotalCount = totalCount;
    }
}