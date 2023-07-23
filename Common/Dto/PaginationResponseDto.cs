namespace Common.Dto
{
    public class PaginationResponseDto<T>
    {
        public List<T>? DataList { get; set; }
        public int TotalCount { get; set; }
    }
}
