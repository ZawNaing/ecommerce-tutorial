using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductTypeController : BaseApiController
    {
        private readonly IProductTypeRepository _productTypeRepo;
        private readonly IMapper _mapper;
        public ProductTypeController(IProductTypeRepository productTypeRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _productTypeRepo.GetProductTypesAsync();
            return Ok(types);
        }

        [HttpGet("{id}", Name = "GetProductTypeById")]
        public ActionResult<IReadOnlyList<ProductType>> GetProductTypeById(Guid id)
        {
            var type = _productTypeRepo.GetProductTypeByIdAsync(id);
            return Ok(type);
        }

        [HttpPost]
        public ActionResult<ProductType> CreateProductType(ProductType productType)
        {
            _productTypeRepo.CreateProductTypeAsync(productType);
            _productTypeRepo.SaveChanges();

            return CreatedAtRoute(nameof(GetProductTypeById), new {id = productType.Id}, productType);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductType(Guid id, ProductTypeUpdateDto productTypeUpdateDto)
        {
            var pt = _productTypeRepo.GetProductTypeByIdAsync(id);
            if(pt == null)
            {
                return NotFound();
            }
            var data = _mapper.Map(productTypeUpdateDto, pt);
            _productTypeRepo.UpdateProductTypeAsync(data);
            _productTypeRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductType(Guid id)
        {
            var pb = _productTypeRepo.GetProductTypeByIdAsync(id);
            if(pb == null)
            {
                return NotFound();
            }
            _productTypeRepo.DeleteProductTypeAsync(pb);
            _productTypeRepo.SaveChanges();
            
            return NoContent(); 
        }
    }
}