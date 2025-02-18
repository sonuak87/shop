using System;
using Core.Entities;
namespace Core.Interface;

public interface ICartService
{
    Task<ShoppingCart?> GetCartAsync(string key);
    Task<ShoppingCart?> SetCartAsync(ShoppingCart shoppingCart);
    Task<bool> DeleteCartAsync(string key);
}
