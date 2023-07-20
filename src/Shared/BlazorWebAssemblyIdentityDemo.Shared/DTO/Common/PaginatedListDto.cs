
namespace BlazorWebAssemblyIdentityDemo.Shared.DTO.Common
{
    public class PaginatedListDto<T>
    {
        public PaginatedListDto()
        {
            
        }
        public PaginatedListDto(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;

            MetaData = new MetaData
            {
                TotalCount = count,
                PageSize = pageSize,
                CurrentPage = pageNumber,
                TotalPages = (int)Math.Ceiling(count / (double)pageSize)
            };

        }

        public IEnumerable<T> Items { get; set; }

        public MetaData MetaData { get; set; }

        public static PaginatedListDto<T> ToPageList(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count =  source.Count();

            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedListDto<T>(items, count, pageNumber, pageSize);
        }
    }
}
