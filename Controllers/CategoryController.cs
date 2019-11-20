using EfCore3.Data;
using EfCore3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EfCore3.Controllers
{
    [ApiController]
    [Route("v1/categories")]
    public class CategoryController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Category>>> Get([FromServices] DataContext context)
        {
            var categories = await context.Categories.ToListAsync();

            return categories;
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<Category>> Post([FromServices] DataContext context, [FromBody] Category model)
        {
            if (ModelState.IsValid)
            {
                context.Categories.Add(model);
                await context.SaveChangesAsync();

                return model;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<Category>> Put([FromServices] DataContext context, Category model, int id)
        {
            if (ModelState.IsValid)
            {
                var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

                if (category != null)
                {
                    category.Title = model.Title;
                    await context.SaveChangesAsync();
                }

                return model;
            }
            else
                return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<List<Category>>> Delete([FromServices] DataContext context, int id)
        {
            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category != null)
            {
                context.Remove(category);
                await context.SaveChangesAsync();
            }

            var categories = await context.Categories.ToListAsync();

            return categories;
        }
    }
}
