using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Application.DTOs.Auth;
using TaskManagement.Application.DTOs.User;

namespace TaskManagement.Application.Interfaces
{
    public interface IAuthService
    {
        /// <summary>
        /// Registers a new user with Role forced to User (never Admin) regardless of input.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResultService<AuthResponse>> RegisterAsync(RegisterRequest request);
            

        /// <summary>
        /// Validates email/password against a stored user and, on success, returns a signed JWT.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResultService<AuthResponse>> LoginAsync(LoginRequest request);

        /// <summary>
        /// this the logout's endpoint will call to revoke the token
        /// </summary>
        /// <param name="userId"></param>
        /// <returns> boolean</returns>
        Task<ResultService<bool>> RevokeTokenAsync(Guid userId);

        /// <summary>
        /// get a new Access token when it get expired using the refreshtoken
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<ResultService<AuthResponse>> RefreshTokenAsync(string request);

    }
}
