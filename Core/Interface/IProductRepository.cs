using System;
using Core.Entities;

namespace Core.Interface;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetProductsAsync(string? brand, string? type, string? sort);
    Task<Product?> GetProductsByIdAsync(int id);
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);
    bool ProductExists(int id);
    Task<IReadOnlyList<string>> GetBrandAsync();
    Task<IReadOnlyList<string>> GetTypeAsync();

    Task<bool> SaveChangesAsync();
}
