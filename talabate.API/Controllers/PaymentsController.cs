using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.API.Errors;
using talabat.Core.Entites.Products;
using talabat.Core.Services.Contract;

namespace talabat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        


        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpPost("{basketId}")]
        public async Task<ActionResult<CustomerBasketDTO>> CreateOrUpdatePaymentIntent(string basketId)
        {
            var basket = await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId);
            if (basket is null)
            {
                return BadRequest(new ApiResponse(400, "Basket not found :<( "));
            }
            else 
            {
                return Ok(basket);
            }




            

        } 
    }
}
