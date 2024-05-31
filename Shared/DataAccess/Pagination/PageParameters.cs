namespace Shared.DataAccess.Pagination;

public class PageParameters
{
    private const int maxPageSize = 10;

    private int _pageNumber = 0;

    public int PageNumber
    {
        get => _pageNumber < 0 ? 0 : _pageNumber;
        set => _pageNumber = value;
    }

    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize < 0 ? 1 : _pageSize;
        set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
    }
}