using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    public static async Task SeedDataAsync(StoreContext context)
    {
        if (!context.Products.Any())
        {
            var productData = await File.ReadAllTextAsync("D:\\sangram\\shop\\Infrastructure\\Data\\SeedData\\products.json");
            var product = JsonSerializer.Deserialize<List<Product>>(productData);
            if (product == null) return;
            context.AddRange(product);
            await context.SaveChangesAsync();
        }

    }

}
