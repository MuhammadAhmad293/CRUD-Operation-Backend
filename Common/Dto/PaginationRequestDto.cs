using Common.Enums;

namespace Common.Dto
{
    public class PaginationRequestDto
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? SortingExpression { get; set; }
        public SortDirection SortingDirection { get; set; }
    }
}
