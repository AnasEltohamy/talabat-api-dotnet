using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Core.IRepo;

namespace Talabat.APIs.Controllers
{
   
    public class TypesController : ApiBaseController
    {
        private readonly IGinaricRepository<ProductType> _productTypes;

        public TypesController(IGinaricRepository<ProductType> productTypes) 
        {
            _productTypes = productTypes;
        }
        [HttpGet("GetTypes")]
        public async Task<ActionResult<IReadOnlyList<Type>>> GetTypes() 
        {
            var types = await _productTypes.getAllAsync();
            return Ok(types);
        }

    }
}
