using API.RequestHelper;
using Core.Entities;
using Core.Interface;
using Core.Specification;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController(IGenericRepository<Product> repo) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts([FromQuery] ProductSpecParam specParam)
        {
            var spec = new ProductSpecification(specParam);
            return await CreatPagedResult(repo, spec, specParam.PageIndex, specParam.PageSize);
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPost]
        public async Task<ActionResult<Product>> CreateProduct(Product product)
        {
            repo.Add(product);
            if (await repo.SaveChangesAsync())
            {
                return CreatedAtAction("GetProduct", product);
            }
            return BadRequest("Problem with creating new Product");
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, Product product)
        {
            if (product.Id != id || !ProductExists(id)) return BadRequest("Cannot update this product");
            repo.Update(product);
            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem with Updating Product");
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await repo.GetByIdAsync(id);
            if (product == null) return NotFound();
            repo.Delete(product);
            if (await repo.SaveChangesAsync())
            {
                return NoContent();
            }
            return BadRequest("Problem with deleting Product");
        }

        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetBrands()
        {
            var spec = new BrandSpecification();
            return Ok(await repo.ListAsync(spec));
        }
        [HttpGet("type")]
        public async Task<ActionResult<IReadOnlyList<string>>> GetTypes()
        {
            var spec = new TypeSpecification();
            return Ok(await repo.ListAsync(spec));
        }

        private bool ProductExists(int id)
        {
            return repo.Exists(id);
        }
    }
}
