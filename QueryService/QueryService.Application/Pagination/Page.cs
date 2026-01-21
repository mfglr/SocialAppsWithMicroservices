namespace QueryService.Application.Pagination
{
    public record Page<T>(T Cursor, int RecordsPerPage, bool IsDescending);
}
