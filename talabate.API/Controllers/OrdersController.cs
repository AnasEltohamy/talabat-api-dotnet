using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.API.Errors;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Services.Contract;

namespace talabat.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService , IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }


        [HttpPost]
        [ProducesResponseType(typeof(Order), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<OrderToReturnDTO>> CreateOrder(OrderDto orderDto) 
        {
            //Get Email From HttpContext of the Requst ==>  by Token
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Address = _mapper.Map<AddressDto,Address>(orderDto.shipToAddress);
            var Order= await _orderService.CreateOrderAsync(BuyerEmail, orderDto.BasketId, orderDto.DeliveryMethodId, Address , orderDto.PaymentIntentId);
            if(Order is null)
            {
                return BadRequest(new ApiResponse(400) );
            } 
            return Ok(_mapper.Map<Order,OrderToReturnDTO>(Order));

        }





        [HttpGet]
        [ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDTO>>> GetOrdersForUser()
        {
            //Get Email From HttpContext of the Requst ==>  by Token
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Orders = await _orderService.GetOrdersForUserAsync(BuyerEmail);
            if (Orders is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var MappingOpj = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDTO>>(Orders);
            return Ok(MappingOpj);
        }






        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(OrderToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<OrderToReturnDTO>> GetOrderForUser(int Id ) 
        {
            //Get Email From HttpContext of the Requst ==>  by Token
            var BuyerEmail = User.FindFirstValue(ClaimTypes.Email);

            var Order = await _orderService.GetOrderByIdForUserAsync(Id, BuyerEmail);
            if (Order is null)
            {
                return NotFound(new ApiResponse(404));
            }
            var finalOrder = _mapper.Map<Order, OrderToReturnDTO>(Order);
            return Ok(finalOrder);
        }





        //DeliveryMethods Controller

        [HttpGet("deliveryMethods")]
        [ProducesResponseType(typeof(DeliveryMethod) , StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse) , StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
        {
            return Ok(await _orderService.GetDeliveryMethodsAsync());
        }

       

    }
}

