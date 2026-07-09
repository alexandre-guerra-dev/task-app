using Api.Src.Api.Dtos.Auth;
using Api.Src.Infraestructure.Identity;
using Api.Src.Infraestructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Api.Src.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, JwtTokenService jwtTokenService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtTokenService = jwtTokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
    {
        var user = new AppUser()
        {
            UserName = registerDto.Email,
            Email = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok(new { TokenJwt = _jwtTokenService.GenerateToken(user!) });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
            return Unauthorized("Email or Password incorrect.");

        var result = await _signInManager.CheckPasswordSignInAsync(
            user,
            loginDto.Password,
            lockoutOnFailure: false
        );

        if (!result.Succeeded)
            return Unauthorized("Email or Password incorrect.");

        return Ok(new { TokenJwt = _jwtTokenService.GenerateToken(user) });
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return NoContent();
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetMe()
    {
        Console.WriteLine("User");
        
        var user = await _userManager.GetUserAsync(HttpContext.User);

        return Ok(user);
    }
}
