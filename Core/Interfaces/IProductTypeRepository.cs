using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductTypeRepository
    {
        bool SaveChanges();
        ProductType GetProductTypeByIdAsync(Guid id);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        void CreateProductTypeAsync(ProductType productType);
        void UpdateProductTypeAsync(ProductType productType);
        void DeleteProductTypeAsync(ProductType productType);  
    }
}