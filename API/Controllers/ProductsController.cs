using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo, IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productTypeRepo = productTypeRepo;
            _productBrandRepo = productBrandRepo;
            _productsRepo = productsRepo;

        }

        // [Cached(600)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts(
            [FromQuery] ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec = new ProductWithFiltersForCountSpecification(productParams);
            var totalItems = await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data = _mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(productParams.PageIndex,
            productParams.PageSize, totalItems, data));
        }

        // [HttpGet]
        // public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetAllProducts()
        // {
        //     var products = await _productsRepo.ListAllAsync();
        //     var data = _mapper
        //     .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

        //     return Ok(data);
        // }

        // [Cached(600)]
        [HttpGet("{id}", Name = "GetProduct")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(Guid id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecification(id);
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        // [Cached(600)]
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandRepo.ListAllAsync();
            return Ok(brands);
        }

        // [Cached(600)]
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var types = await _productTypeRepo.ListAllAsync();
            return Ok(types);
        }

        [HttpPost]
        public ActionResult<Product> CreateProduct(Product product)
        {
               var destinationFilePath = "";
            if(product.PictureUrl != null) {
              
                CommonFunctions.checkFileExistAndMove(product.PictureUrl, out destinationFilePath);
            }
            product.PictureUrl = destinationFilePath;
            _productRepository.CreateProductAsync(product);
            _productRepository.SaveChanges();

            return CreatedAtRoute(nameof(GetProduct), new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateProduct(Guid id, ProductUpdateDto product)
        {
            var p = _productRepository.GetProductByIdAsync(id);
            var destinationFilePath = "";
            if (p == null)
            {
                return NotFound();
            }

            if(product.PictureUrl != null && product.PictureUrl.Contains("/temp")) {
                CommonFunctions.checkFileExistAndMove(product.PictureUrl,out destinationFilePath);
                 product.PictureUrl = destinationFilePath;
            }
           
            var data = _mapper.Map(product, p);
            _productRepository.UpdateProductAsync(data);
            _productRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(Guid id)
        {
            var p = _productRepository.GetProductByIdAsync(id);
            if (p == null)
            {
                return NotFound();
            }
            
            _productRepository.DeleteProductAsync(p);
            _productRepository.SaveChanges();

            return NoContent();
        }
    }
}