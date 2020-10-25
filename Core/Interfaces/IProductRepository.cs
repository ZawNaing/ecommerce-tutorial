using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        bool SaveChanges();
        Product GetProductByIdAsync(Guid id);
        Task<IReadOnlyList<Product>> GetProductsAsync();
        void CreateProductAsync(Product product);
        void UpdateProductAsync(Product product);
        void DeleteProductAsync(Product product);
    }
}