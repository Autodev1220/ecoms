using System;
using System.ComponentModel;
using Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly StoreContext context;
    public ProductsController(StoreContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await context.Products.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null) return NotFound();

        return product;
    }


    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        context.Products.Add(product);
        await context.SaveChangesAsync();

        //can change to response
        return product;
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateProduct(int id, Product product)
    {
        if (product.Id != id || !ProductExist(id))
        {
            return BadRequest("Cannot update this product");
        }
        // It marks the product as Modified in EF Core’s change tracker, so EF knows to generate an SQL UPDATE when SaveChangesAsync() is called.
        context.Entry(product).State = EntityState.Modified;
        await context.SaveChangesAsync();

        //return na no content or a blank response
        return NoContent();
    }


    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteProduct(int id)
    {
        var product = await context.Products.FindAsync(id);

        if (product == null) return NotFound();

        context.Products.Remove(product);

        await context.SaveChangesAsync();

        return NoContent();
    }
    
     //check product if exist via Id
    private bool ProductExist(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }
}
