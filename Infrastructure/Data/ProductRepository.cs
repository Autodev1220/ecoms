using System;
using System.Security.Cryptography.X509Certificates;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProductRepository(StoreContext context) : IProductRepository
{
    /* remove this due to the implementation of primary constructor
    
    private readonly StoreContext context;
    
    public ProductRepository(StoreContext context)
    {
        this.context = context;
    }
    
    */

    public void AddProduct(Product product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Product product)
    {
        context.Products.Remove(product);
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
        
    }

    public async Task<IReadOnlyList<Product>> GetProductsAsync()
    {
        return await context.Products.ToListAsync();
    }

    public bool ProductExist(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Product product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
