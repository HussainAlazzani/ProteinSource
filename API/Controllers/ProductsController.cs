using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Core.Interfaces;
using Core.Specifications;
using API.Dtos;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using API.Errors;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Category> _categoryRepository;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Brand> brandRepository,
            IGenericRepository<Category> categoryRepository)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification();

            var products = await _productRepository.GetAllWithSpecAsync(spec);

            var productsDto = new List<ProductDto>();
            foreach (var product in products)
            {
                productsDto.Add(product.AsDto());
            }

            return Ok(productsDto);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductWithCategoriesAndBrandSpecification(id);
            var product = await _productRepository.GetWithSpecAsync(spec);

            return Ok(product.AsDto());
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<Brand>>> GetBrands()
        {
            return Ok(await _brandRepository.GetAllAsync());
        }

        [HttpGet("categories")]
        public async Task<ActionResult<IReadOnlyList<CategoryDto>>> GetCategories()
        {
            var spec = new CategoriesWithProductsSpecification();

            var categories = await _categoryRepository.GetAllWithSpecAsync(spec);
            var categoriesDto = new List<CategoryDto>();

            foreach (var category in categories)
            {
                categoriesDto.Add(category.AsDto());
            }

            return Ok(categoriesDto);
        }

        [HttpGet("categories/{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(int id)
        {
            var spec = new CategoryWithProductsSpecification(id);
            var category = await _categoryRepository.GetWithSpecAsync(spec);

            return Ok(category.AsDto());
        }
    }
}