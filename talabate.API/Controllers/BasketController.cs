using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.API.Errors;
using talabat.Core.Entites.Products;
using talabat.Core.Repositores.Contract;
using talabat.Repository;

namespace talabat.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _basketRepository;

        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketRepository  , IMapper mapper )
        {
            _basketRepository = basketRepository;
            _mapper = mapper;
        }




        //Get Basket By Id
        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string id)
        
        {
           
            var basket =  await _basketRepository.GetBasketAsync(id);

            
            if (basket == null) 
            {
                return new CustomerBasket(id);
            }
            var mappingToCustomerBasket = _mapper.Map<CustomerBasket, CustomerBasketDTO >(basket);

            return Ok(mappingToCustomerBasket);

        }

        //public async Task<ActionResult<CustomerBasket>> GetBasket(string id)

        //{
        //    var GetCustomerBasket = await _basketRepository.GetBasketAsync(id);
        //    if (GetCustomerBasket == null)
        //    {
        //        return new CustomerBasket(id);
        //    }


        //    return Ok(GetCustomerBasket);

        //}



        // Cerate Or Update Basket
        [HttpPost]
        [ProducesResponseType(typeof(CustomerBasketDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerBasketDTO>> UpdateBasket(CustomerBasketDTO basketDto) 
        {


            var maapperBasket = _mapper.Map<CustomerBasketDTO ,CustomerBasket>(basketDto);
            var UpdateOrCreateBasket =await _basketRepository.UpdateBasketAsync(maapperBasket);
            var finalBasket = _mapper.Map<CustomerBasket ,CustomerBasketDTO>(UpdateOrCreateBasket); //PictureUrl = "http://localhost:4200/images/products/sb-ang1.png"
            return finalBasket is null ? BadRequest(new ApiResponse(400)): Ok(finalBasket);
        }







        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            return await _basketRepository.DeleteBasketAsync(id);
        }
    }
}
