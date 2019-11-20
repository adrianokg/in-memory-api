using EfCore3.Data;
using EfCore3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCore3.Controllers
{
    [ApiController]
    [Route("v1/products")]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Product>>> Get([FromServices] DataContext context)
        {
            var products = await context.Products.Include(x => x.Category)
                .AsNoTracking()
                .ToListAsync();

            return products;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> GetById([FromServices] DataContext context, int id)
        {
            var product = await context.Products.Include(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Product>> Post([FromServices] DataContext context, [FromBody] Product model)
        {
            if (ModelState.IsValid)
            {
                context.Products.Add(model);
                await context.SaveChangesAsync();

                return model;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Product>> Put([FromServices] DataContext context, Product model, int id)
        {
            if (ModelState.IsValid)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

                if (product != null)
                {
                    product.Title = model.Title;
                    product.Description = model.Description;
                    product.Price = model.Price;
                    product.CategoryId = model.CategoryId;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Product>>> Delete([FromServices] DataContext context, int id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (product != null)
            {
                context.Remove(product);
                await context.SaveChangesAsync();
            }

            var products = await context.Products.ToListAsync();

            return products;
        }

        [HttpGet]
        [Route("categories/{id:int}")]
        public async Task<ActionResult<List<Product>>> GetByCategory([FromServices] DataContext context, int id)
        {
            var products = await context.Products.Include(x => x.Category)
                .AsNoTracking()
                .Where(x => x.CategoryId == id)
                .ToListAsync();

            return products;
        }
    }
}
