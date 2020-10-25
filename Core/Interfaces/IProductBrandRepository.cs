using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductBrandRepository
    {
        bool SaveChanges();
        ProductBrand GetProductBrandByIdAsync(Guid id);
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        void CreateProductBrandAsync(ProductBrand productBrand);
        void UpdateProductBrandAsync(ProductBrand productBrand);
        void DeleteProductBrandAsync(ProductBrand productBrand);
    }
}