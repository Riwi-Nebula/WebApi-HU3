using Microsoft.EntityFrameworkCore;
using WebApi_HU3.Domain.Entities;
using WebApi_HU3.Domain.Interfaces;
using WebApi_HU3.Infraestructure.Data;

namespace WebApi_HU3.Infraestructure.Repositories;

//inyeccion de contexto base de datos 'DbContext'
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    
    //se obtiene lista completa de los estudiantes registrados
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    //se obtiene lista completa de los estudiantes registrados
    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        return await _context.Products.ToListAsync();
    }
    
    //se obtiene un estudiante por id
    public async Task<Product?> GetProductById(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    //se agregar un nuevo estudiante
    public async Task<Product> AddProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    //actualiza informacion de un estudiante existente
    public async Task<Product> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product;
    }

    //elimina un estudiante por id
    public async Task<Product> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
        return product;
    }
}