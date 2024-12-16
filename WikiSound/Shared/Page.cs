namespace WikiSound.Shared
{
    public class Page<T> where T : class
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public Page()
        {
            Items = new List<T>();
        }

        public Page(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            PageNumber = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
