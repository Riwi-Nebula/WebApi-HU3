// Ruta: Simulacro-HU3.Application/Services/StudentService.cs
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Application.Exceptions; // Crearemos esta excepción
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;

namespace WebApi_HU3.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto createDto)
        {
            // 1. Mapear DTO a Entidad
            var product = new Product
            {
                Name = createDto.Name,
                Description = createDto.Description,
                Price = createDto.Price
            };

            // 2. Usar repositorio
            var newProduct = await _productRepository.AddProduct(product);

            // 3. Mapear Entidad a DTO para respuesta
            return MapProductToDto(newProduct);
        }

        public async Task<ProductDto> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetProductById(id);
            
            if (product == null)
            {
                throw new NotFoundException("Estudiante no encontrado");
            }

            return MapProductToDto(product);
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _productRepository.GetAllProducts();
            return products.Select(MapProductToDto).ToList();
        }

        public async Task UpdateAsync(int id, ProductUpdateDto updateDto)
        {
            var product = await _productRepository.GetProductById(id);
            if (product == null)
            {
                throw new NotFoundException("Estudiante no encontrado");
            }

            // Actualizar propiedades
            product.Name = updateDto.Name;
            product.Description = updateDto.Description;
            product.Price = updateDto.Price;

            await _productRepository.UpdateProduct(product);
        }

        public async Task DeleteAsync(int id)
        {
            var deletedProduct = await _productRepository.DeleteProduct(id);
            if (deletedProduct == null) 
            {
                throw new NotFoundException("Estudiante no encontrado para eliminar");
            }
        }

        // --- Helper de Mapeo ---
        private ProductDto MapProductToDto(Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}