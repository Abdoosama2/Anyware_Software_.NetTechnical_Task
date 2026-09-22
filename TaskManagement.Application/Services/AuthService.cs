
using Mapster;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.DTOs.User;
using TaskManagement.Application.Interfaces;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Services
{
    public class AuthService(IUserRepository _userRepository,IJwtService _jwtService) : IAuthService
    {
        public async Task<ResultService<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
         
            var existingUser= await _userRepository.GetByEmailAsync(request.Email);
             if (existingUser is not null)
                return ResultService<AuthResponse>.Failure("A user with this email is already exists!!");
            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.User,
                CreatedAt = DateTime.UtcNow
            };

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            await _userRepository.AddAsync(user);
            var savingResult = await _userRepository.SaveChangesAsync();
            if (!savingResult)
            {
                return ResultService<AuthResponse>.Failure("Failed to save the new user");
            }
            var response = new AuthResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserResponse = user.Adapt<UserResponse>()
            };

            return ResultService<AuthResponse>.Success(response);

        }
        public async Task<ResultService<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user is null)
                return ResultService<AuthResponse>.Failure("Invalid email or password!");

            var passwordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!passwordValid)
            {
                return ResultService<AuthResponse>.Failure("Invalid email or password!");
            }

            var accessToken = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _userRepository.SaveChangesAsync();

            var response = new AuthResponse
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                UserResponse = user.Adapt<UserResponse>()
            };

            return ResultService<AuthResponse>.Success(response);
        }

        public async Task<ResultService<bool>> RevokeTokenAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user is null)
                return ResultService<bool>.Failure("User not found.");

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            var saved = await _userRepository.SaveChangesAsync();
            if (!saved)
                return ResultService<bool>.Failure("Failed to revoke token.");

            return ResultService<bool>.Success(true);
        }

        public async Task<ResultService<AuthResponse>> RefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetByRefreshTokenAsync(refreshToken);
            if (user is null)
                return ResultService<AuthResponse>.Failure("Invalid refresh token.");

            if (user.RefreshTokenExpiryTime is null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return ResultService<AuthResponse>.Failure("Refresh token has expired. Please log in again.");

            var newAccessToken = _jwtService.GenerateAccessToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

            var saved = await _userRepository.SaveChangesAsync();
            if (!saved)
                return ResultService<AuthResponse>.Failure("Failed to refresh token.");

            var response = new AuthResponse
            {
                Token = newAccessToken,
                RefreshToken = newRefreshToken,
                UserResponse = user.Adapt<UserResponse>()
            };

            return ResultService<AuthResponse>.Success(response);
        }
    }
}
