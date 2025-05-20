using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedAsync(StoreContext context)
    {
        var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");
        var products = JsonSerializer.Deserialize<List<Product>>(productsData);
        if (products == null) return;
        if (!context.Products.Any())
        {
            context.Products.AddRange(products);
        }
        await context.SaveChangesAsync();
    }
}
