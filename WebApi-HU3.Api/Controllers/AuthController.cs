using Microsoft.AspNetCore.Mvc;
using WebApi_HU3.Application.DTOs;
using WebApi_HU3.Application.Interfaces;

namespace WebApi_HU3.Api.Controllers;

[ApiController]
[Route("api/auth/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    /// <summary>
    /// Login: Autentica al usuario y devuelve un token JWT
    /// </summary>
    /// <param name="loginDto">DTO con Email y Password</param>
    /// <returns>JWT token o error</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(typeof(object), 401)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid data", errors = ModelState });

        try
        {
            var token = await _authService.Authenticate(loginDto.Email, loginDto.Password);

            if (token == null)
                return Unauthorized(new { message = "Email or Password is incorrect" });

            return Ok(new { token });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        }
    }

    /// <summary>
    /// Register: Registra un nuevo usuario
    /// </summary>
    /// <param name="registerDto">DTO con Username, Email y Password</param>
    /// <returns>Mensaje de éxito o error</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(object), 200)]
    [ProducesResponseType(typeof(object), 400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Register([FromBody] UserRegisterDto registerDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { message = "Invalid data", errors = ModelState });

        try
        {
            await _userService.RegisterAsync(registerDto);
            return Ok(new { message = "User registered successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Internal server error", detail = ex.Message });
        }
    }
}
