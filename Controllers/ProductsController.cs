using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSTask.Contracts;
using NSTask.Models;

namespace NSTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [HttpGet]
        public IEnumerable<Product> GetProducts()
        {

            return _productRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var product = await _productRepository.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            await _productRepository.Update(product);
            return NoContent();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _productRepository.Add(product);


            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await _productRepository.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            await _productRepository.Remove(id);

            return Ok(product);
        }

     
    }

}
