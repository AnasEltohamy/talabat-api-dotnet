using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using talabat.API.Errors;

namespace Talabat.APIs.Controllers
{
    [Route("errors/{code}")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi =true)]
    public class ErrorController : ControllerBase
    {

        public ActionResult Errors(int code)
        {
            //return NotFound(new ApiResponse(code));
            return new ObjectResult(new ApiResponse(code)) { StatusCode = code  };

        }
    }
}
