using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{

    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext _storeContext;

        public BuggyController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var products = _storeContext.Products.Find(100);
            if (products is null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(products);
        }


        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var products = _storeContext.Products.Find(100);
            var productToReturn = products.ToString(); //Error [null will be convert to ToString ]
            // Will Throw Exception [Null Reference Exception]
            return Ok(productToReturn);

        }


        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet("BadRequest/id")]

        public ActionResult GetBadRequest(int id)
        {
            return Ok();

        }

}
}
