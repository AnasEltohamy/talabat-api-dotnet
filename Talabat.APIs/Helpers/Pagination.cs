using Talabat.APIs.DTOs;

namespace Talabat.APIs.Helpers
{
    public class Pagination<T>
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Data { get; set; }

        public Pagination(int pageSize, int pageIndex, IReadOnlyList<T> data ,int CountOfProduct)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Data = data;
            Count = CountOfProduct;
        }

        

    }
}
