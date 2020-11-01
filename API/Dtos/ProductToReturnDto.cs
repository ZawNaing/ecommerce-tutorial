using System;

namespace API.Dtos
{
    public class ProductToReturnDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string ProductCode { get; set; }
        public string ProductColor { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal WholeSaleDiscount { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public Guid ProductTypeId { get; set; }
        public string ProductBrand { get; set; }
        public Guid ProductBrandId { get; set; }
        public Boolean IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}