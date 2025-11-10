using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Application.Exceptions;

namespace WebApi_HU3.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // 🔹 Obtener todos los productos
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var products = await _productService.GetAllAsync();
            return Ok(products);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Obtener producto por ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(product);
        }
        catch (NotFoundException nfEx)
        {
            return NotFound(nfEx.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Crear un nuevo producto
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ProductCreateDto createDto)
    {
        try
        {
            var newProduct = await _productService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Actualizar producto existente
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ProductUpdateDto updateDto)
    {
        try
        {
            await _productService.UpdateAsync(id, updateDto);
            return Ok("Producto actualizado correctamente.");
        }
        catch (NotFoundException nfEx)
        {
            return NotFound(nfEx.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Eliminar producto
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _productService.DeleteAsync(id);
            return Ok("Producto eliminado correctamente.");
        }
        catch (NotFoundException nfEx)
        {
            return NotFound(nfEx.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
