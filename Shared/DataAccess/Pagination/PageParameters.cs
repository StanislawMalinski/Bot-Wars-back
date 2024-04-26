namespace Shared.DataAccess.Pagination;

public class PageParameters
{
    private const int maxPageSize = 10;
    public int PageNumber { get; set; } = 0;

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}