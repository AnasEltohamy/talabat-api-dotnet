using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepo;

namespace Talabat.APIs.Controllers
{
    
    public class BrandsController : ApiBaseController
    {
        private readonly IGinaricRepository<ProductBrand> _productBrand;

        public BrandsController(IGinaricRepository<ProductBrand> ProductBrand)
        {
            _productBrand = ProductBrand;
        }


        [HttpGet("GetBrands")]
       
         public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var preandes = await _productBrand.getAllAsync();
            return Ok(preandes);
        } 

    }
}
