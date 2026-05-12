using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using talabat.API.DTOs.BasketAndOrderDtos;
using talabat.API.Errors;
using talabat.API.Helpers;
using talabat.Core.Entites.Products;
using talabat.Core.Services.Contract;

using talabat.Repository.Data.Store.Specifications.SpecOfProducts;

namespace talabat.API.Controllers
{

    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ProductsController : ControllerBase
    {


        
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService,IMapper mapper)
        {
           
            _productService = productService;
            _mapper = mapper;
        }


       
        [HttpGet]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Pagination<ProductToReturnDTO>>> GetProducts([FromQuery] ProductSpecificationParams ProductSpecificationParams)
        {
            var Spec = new ProductWithCategoryAndBrandWithSpecification(ProductSpecificationParams);
            var AllProduct = await _productService.GetProductsWithSpecAsync(Spec);
            var Mapping = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDTO>>(AllProduct);
            var GetCountOfProducts = new CountOfProductWithFiltrationSpecification(ProductSpecificationParams);
            var ApplyCountOfProducts = await _productService.GetCountWithFiltrationAsync(GetCountOfProducts);
            var FinelOpject = new Pagination<ProductToReturnDTO>(ProductSpecificationParams.PageIndex, ProductSpecificationParams.pageSize, ApplyCountOfProducts, Mapping);
            return Ok(FinelOpject);

        }


        [HttpGet("{Id}")]
        [ProducesResponseType(typeof(ProductToReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDTO>> GetProduct(int Id)
        {
            var Spec = new ProductWithCategoryAndBrandWithSpecification(Id);
            var Product = await _productService.GetProductWithSpecById(Spec);
            if (Product is null) {
                return NotFound(new ApiResponse(404));
            
            }
            var MappingOpj = _mapper.Map<Product, ProductToReturnDTO>(Product);
            return Ok(MappingOpj);
        }






        [HttpGet("brands")]
        [ProducesResponseType(typeof(ProductBrand), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var Brands = await _productService.GetAllBrandAsync();
            return Ok(Brands);
        }



        [HttpGet("categories")]
        [ProducesResponseType(typeof(ProductCategory), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var Categorys = await _productService.GetAllCategoryAsync();
            return Ok(Categorys);

        }



    }
}
