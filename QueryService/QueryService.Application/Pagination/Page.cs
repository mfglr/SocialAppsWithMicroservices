namespace QueryService.Application.Pagination
{
    public record Page<T>(T? OffSet, int RecordsPerPage, bool IsDescending) where T : IComparable;
}
