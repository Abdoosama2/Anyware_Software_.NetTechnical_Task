using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

namespace TaskManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService _authService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.IsSuccess)
            {
                return Conflict(result);
            }

            SetRefreshToken(result.Data!.RefreshToken!);
            return CreatedAtAction(nameof(Register), result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _authService.LoginAsync(request);
            if (!result.IsSuccess)
            {
                return Unauthorized(result);
            }
               
            SetRefreshToken(result.Data!.RefreshToken!);
            return Ok(result);
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _authService.RevokeTokenAsync(userId);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            
            Response.Cookies.Delete("RefreshToken");
            return NoContent();
        }

       
        [HttpPost("refresh-token")]
        [Authorize]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await _authService.RefreshTokenAsync(refreshToken);
            if (!result.IsSuccess)
                return Unauthorized(result);

            return Ok(result);
        }
        private void SetRefreshToken(string refershToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("RefreshToken", refershToken, cookieOptions);
        }
    }
}
