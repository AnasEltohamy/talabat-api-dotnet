namespace Talabat.APIs.Errors
{
    public class ApiVaIidationErrorResponse:ApiResponse
    {

        public IEnumerable<string> Errors { get; set; }
        public ApiVaIidationErrorResponse():base(400)
        {
            Errors = new List<string>();
        }


    }
}
