using WebApi_HU3.Application.DTOs;

namespace WebApi_HU3.Application.Interfaces
{
    public interface IProductService
    {
        Task<ProductDto> CreateAsync(ProductCreateDto createDto);
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto> GetByIdAsync(int id);
        Task UpdateAsync(int id, ProductUpdateDto updateDto);
        Task DeleteAsync(int id);
    }
}