using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly StoreContext _context;
        public ProductTypeRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }

        public ProductType GetProductTypeByIdAsync(Guid id)
        {
            return _context.ProductTypes.FirstOrDefault(pt => pt.Id == id);
        }

        public void CreateProductTypeAsync(ProductType productType)
        {
            if(productType == null)
            {
                throw new ArgumentNullException(nameof(productType));
            }
            _context.ProductTypes.Add(productType);
        }

        public void UpdateProductTypeAsync(ProductType productType)
        {
            if(productType == null)
            {
                throw new ArgumentNullException(nameof(productType));
            }
        }

        public void DeleteProductTypeAsync(ProductType productType)
        {
            if(productType == null)
            {
                throw new ArgumentNullException(nameof(productType));
            }
            _context.ProductTypes.Remove(productType);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}