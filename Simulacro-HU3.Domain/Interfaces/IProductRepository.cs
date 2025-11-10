using WebApi_HU3.Domain.Entities;

namespace WebApi_HU3.Domain.Interfaces;

public interface IProductRepository
{
    public Task<IEnumerable<Product>> GetAllProducts(); // Get All Products
    public Task<Product> GetProductById(int id); // Get Product By ID
    public Task<Product> AddProduct(Product product); // Add Product
    public Task<Product> UpdateProduct(Product product); // Update Product
    public Task<Product> DeleteProduct(int id); // Delete Product
}