
namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Common
{
    public class PaginatedListDto<T>
    {
        public PaginatedListDto()
        {
            
        }
        public PaginatedListDto(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            Items = items;
        }

        public IEnumerable<T> Items { get; set; }
        public int PageNumber { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPreviousPage => PageNumber > 1;

        public bool HasNextPage => PageNumber < TotalPages;

        public static PaginatedListDto<T> ToPageList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedListDto<T>(items, count, pageNumber, pageSize);
        }
    }
}
