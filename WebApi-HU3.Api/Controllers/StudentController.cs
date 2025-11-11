using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;
using WebApi_HU3.Application.Exceptions;

namespace WebApi_HU3.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    // 🔹 Obtener todos los estudiantes
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        try
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Obtener estudiante por ID
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var student = await _studentService.GetByIdAsync(id);
            return Ok(student);
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

    // 🔹 Crear un nuevo estudiante
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] StudentCreateDto createDto)
    {
        try
        {
            var newStudent = await _studentService.CreateAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = newStudent.Id }, newStudent);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    // 🔹 Actualizar estudiante existente
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] StudentUpdateDto updateDto)
    {
        try
        {
            await _studentService.UpdateAsync(id, updateDto);
            return Ok("Estudiante actualizado correctamente.");
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

    // 🔹 Eliminar estudiante
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _studentService.DeleteAsync(id);
            return Ok("Estudiante eliminado correctamente.");
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
