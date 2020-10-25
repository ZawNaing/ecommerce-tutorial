using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductBrandRepository : IProductBrandRepository
    {
        private readonly StoreContext _context;
        public ProductBrandRepository(StoreContext context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }
        public ProductBrand GetProductBrandByIdAsync(Guid id)
        {
            return _context.ProductBrands.FirstOrDefault(pb => pb.Id == id);
        }
        public void CreateProductBrandAsync(ProductBrand productBrand)
        {
            if(productBrand == null)
            {
                throw new ArgumentNullException(nameof(productBrand));
            }
            _context.ProductBrands.Add(productBrand);
        }
        public void UpdateProductBrandAsync(ProductBrand productBrand)
        {
            if(productBrand == null) 
            {
              throw new ArgumentNullException(nameof(productBrand));  
            }
            
        }
        public void DeleteProductBrandAsync(ProductBrand productBrand)
        {
            if(productBrand == null) 
            {
              throw new ArgumentNullException(nameof(productBrand));  
            }
            _context.ProductBrands.Remove(productBrand);
        }

        public bool SaveChanges()
        {
           return (_context.SaveChanges() >= 0);
        }


    }
}