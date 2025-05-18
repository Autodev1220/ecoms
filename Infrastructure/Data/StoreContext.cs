using System;
using Core.Entities;
using Infrastructure.Config;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class StoreContext(DbContextOptions<StoreContext> options) : DbContext(options)
{
    public DbSet<Product> Products {get;set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //This ensures that any base class logic for model creation is also executed.
        base.OnModelCreating(modelBuilder); 
        
        //This automatically applies all configurations (like your ProductConfiguration) found in a specific assembly.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProductConfiguration).Assembly); 
    }

}
