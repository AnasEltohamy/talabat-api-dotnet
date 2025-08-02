using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entites;
using Talabat.Core.IRepo;
using Talabat.Core.Specifications;

namespace Talabat.APIs.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IGinaricRepository<Product> pro;
        private readonly IMapper mapper;

        public ProductsController(IGinaricRepository<Product> pro , IMapper mapper)
        {

            this.pro = pro;
            this.mapper = mapper;
        }
        [HttpGet("GetProducts_WithSpecifications")]
        [ProducesResponseType(typeof(Pagination<ProductToReturnDTOs>) , 200)]
        [ProducesResponseType(typeof(ApiResponse), 404 )]
        public async Task<ActionResult<Pagination<ProductToReturnDTOs>>> GetProducts([FromQuery] ProductSpecParams Params )
        {
            var spec = new ProductWithTypeAndBrandSpecification(Params);
            var products = await pro.GetAllWithSpecAsync(spec);  
            var afterMapping = mapper.Map<IReadOnlyList<Product> , IReadOnlyList<ProductToReturnDTOs>>(products);
            
            var CountOfProduct= await pro.GetCountWithSpecAsync(new ProductWithFiltrationForCountAsync(Params));
            var returnProducts = new Pagination<ProductToReturnDTOs>(Params.PageSize, Params.pageIndex, afterMapping , CountOfProduct);
            return Ok(returnProducts);
        
        }

        [HttpGet("GetProductById_WithSpecifications/id")]
        [ProducesResponseType(typeof(ProductToReturnDTOs), 200)]
        [ProducesResponseType(typeof(ApiResponse) , 404)]
        public async Task<ActionResult<ProductToReturnDTOs>> GetProductById(int id)
        {

            var spec = new ProductWithTypeAndBrandSpecification(id);
            var item = await pro.GetByIdWithSpecAsync(spec);
            if (item is null) 
            {
                return NotFound(new ApiResponse(404));
            }
            var afterMapping = mapper.Map<Product, ProductToReturnDTOs>(item);
            return Ok(afterMapping);
        }





    }
}
