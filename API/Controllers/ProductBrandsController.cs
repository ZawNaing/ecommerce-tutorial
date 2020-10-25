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
    public class ProductBrandsController : BaseApiController
    {
        private readonly IProductBrandRepository _productBrandRepo;
        private readonly IMapper _mapper;
        public ProductBrandsController(IProductBrandRepository productBrandRepo, IMapper mapper)
        {
            _mapper = mapper;
            _productBrandRepo = productBrandRepo;
        }

        [HttpGet()]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandRepo.GetProductBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("{id}", Name = "GetProductBrandsById")]
        public ActionResult<IReadOnlyList<ProductBrand>> GetProductBrandsById(Guid id)
        {
            var brand = _productBrandRepo.GetProductBrandByIdAsync(id);
            return Ok(brand);
        }

        [HttpPost]
        public ActionResult<ProductBrand> CreateProductBrand(ProductBrand productBrand)
        {
            _productBrandRepo.CreateProductBrandAsync(productBrand);
            _productBrandRepo.SaveChanges();

            return CreatedAtRoute(nameof(GetProductBrandsById), new {id = productBrand.Id}, productBrand);
            // return Ok(productBrand);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProductBrand(Guid id, ProductBrandUpdateDto productBrand)
        {
            var pb = _productBrandRepo.GetProductBrandByIdAsync(id);
            if(pb == null)
            {
                return NotFound();
            }
            // pb = productBrand;
            var data = _mapper.Map(productBrand, pb);
            _productBrandRepo.UpdateProductBrandAsync(data);
            _productBrandRepo.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProductBrand(Guid id)
        {
            var pb = _productBrandRepo.GetProductBrandByIdAsync(id);
            if(pb == null)
            {
                return NotFound();
            }
            _productBrandRepo.DeleteProductBrandAsync(pb);
            _productBrandRepo.SaveChanges();
            
            return NoContent(); 
        }
    }
}