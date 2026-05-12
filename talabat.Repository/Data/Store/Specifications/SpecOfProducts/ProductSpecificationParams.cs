using System.Drawing.Printing;

namespace talabat.Repository.Data.Store.Specifications.SpecOfProducts
{
    public class ProductSpecificationParams
    {
        //string? Sort ,int? PrandId , int? CategoryId
        public string? Sort {  get; set; }
        public int? PreandId { get; set; }
        public int ? CategoryId { get; set; }

        private int PageSize = 5 ;
        public int pageSize
        {
            get { return PageSize; }
            set { PageSize = value > 10 ? 10 : value; } 
        }

        public int PageIndex { get; set; } = 1;


        public string? Search {  get; set; }



    }
}
