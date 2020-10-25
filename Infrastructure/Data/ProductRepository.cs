using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public Product GetProductByIdAsync(Guid id)
        {
            return _context.Products
                     .Include(p => p.ProductType)
           .Include(p => p.ProductBrand)
            .FirstOrDefault(p => p.Id == id);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync()
        {
           return await _context.Products
           .Include(p => p.ProductType)
           .Include(p => p.ProductBrand)
           .ToListAsync();
        }

        public void CreateProductAsync(Product product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            _context.Products.Add(product);
        }

        public void UpdateProductAsync(Product product)
        {
            if(product == null) 
            {
              throw new ArgumentNullException(nameof(product));  
            }
        }
        public void DeleteProductAsync(Product product)
        {
            if(product == null) 
            {
              throw new ArgumentNullException(nameof(product));  
            }
             _context.Products.Remove(product);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}