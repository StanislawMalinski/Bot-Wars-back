namespace Shared.DataAccess.DTO.Responses;

public class PageResponse<T>
{
    public PageResponse(List<T> page, int pageSize, int allRecords)
    {
        Page = page;
        AllRecords = allRecords;
        if (allRecords <= 0) return;
        AmountOfPages = AllRecords / pageSize;
        if (allRecords % pageSize > 0) AmountOfPages++;
    }

    public List<T> Page { get; set; }
    public int AllRecords { get; set; }
    public int AmountOfPages { get; set; }
}