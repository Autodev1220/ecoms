using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder){
        /* specify the exact column type of a certain property, 
           in this case we specify the column type of price in the database when mapped */
        builder.Property(x=>x.Price).HasColumnType("decimal(18,2)");
    }
}
