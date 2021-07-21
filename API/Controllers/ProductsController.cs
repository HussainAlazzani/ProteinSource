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
using API.Helpers;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<Brand> _brandRepository;
        private readonly IGenericRepository<Category> _categoryRepository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            IGenericRepository<Product> productRepository,
            IGenericRepository<Brand> brandRepository,
            IGenericRepository<Category> categoryRepository,
            ILogger<ProductsController> logger)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetProducts()
        {
            var spec = new ProductsWithCategoriesAndBrandsSpecification();

            var products = await _productRepository.GetAllWithSpecAsync(spec);

            // Convert to DTO
            var productsDto = new List<ProductDto>();
            foreach (var product in products)
            {
                productsDto.Add(product.AsDto());
            }

            return Ok(productsDto);
        }

        [HttpGet("Filtered")]
        public async Task<ActionResult<PaginationForList<ProductDto>>> GetFilteredProducts(
            [FromQuery] ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithCategoryAndBrandSpecification(productSpecParams);
            var countSpec = new ProductsWithFiltersForCountSpecification(productSpecParams);

            var totalProducts = await _productRepository.CountAsync(countSpec);
            var products = await _productRepository.GetAllWithSpecAsync(spec);

            // Convert to DTO
            var productsDto = new List<ProductDto>();
            foreach (var product in products)
            {
                productsDto.Add(product.AsDto());
            }

            return Ok(new PaginationForList<ProductDto>(productSpecParams.PageIndex, productSpecParams.PageSize, totalProducts, productsDto));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var spec = new ProductWithCategoriesAndBrandSpecification(id);
            var product = await _productRepository.GetWithSpecAsync(spec);

            if (product == null)
            {
                return NotFound(new ApiErrorResponse(404));
            }

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

        /// <summary>
        /// This method requires 3 rounds of database requests:
        /// The first requests the category by its id without including the products.
        /// The second requests the products without filtering to get the total number of products in that category.
        /// The third requests the filtered products.
        /// This method can be made more efficient by including products when requesting the category.
        /// </summary>
        [HttpGet("categories/filtered/")]
        public async Task<ActionResult<Pagination<CategoryDto>>> GetFilteredCategory([FromQuery] CategorySpecParams categorySpecParams)
        {
            if (categorySpecParams.CategoryId <= 0) categorySpecParams.CategoryId = 1;

            var categorySpec = new CategoryWithNoProductsSpecification(categorySpecParams.CategoryId);
            var category = await _categoryRepository.GetWithSpecAsync(categorySpec);

            // Get the products that fall under the category selected
            var productSpecParams = new ProductSpecParams
            {
                CategoryId = categorySpecParams.CategoryId,
                PageSize = categorySpecParams.PageSize,
                PageIndex = categorySpecParams.PageIndex,
                Sort = categorySpecParams.Sort,
            };

            // Request the products that fall under the specified category. 
            // The products will not include the navigation properties, ie. brand ids and categories.
            var countSpec = new ProductsWithFiltersForCountSpecification(productSpecParams);
            var totalProducts = await _productRepository.CountAsync(countSpec);

            // Request the filtered producst that fall under the specified category. Include navigation properties.
            var productSpec = new ProductsWithCategoryAndBrandSpecification(productSpecParams);
            var products = await _productRepository.GetAllWithSpecAsync(productSpec);

            category.Products = products.ToList();

            return Ok(new Pagination<CategoryDto>(categorySpecParams.PageIndex, categorySpecParams.PageSize, totalProducts, category.AsDto()));
        }
    }
}