using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Interfaces
{
    public interface IJwtService
    {
    

        /// <summary>
        /// Generates a signed JWT access token containing the user's Id, Name, Email, and Role claims.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public string GenerateAccessToken(User user);

        /// <summary>
        /// Generates a cryptographically random refresh token string, unrelated to the JWT itself.
        /// </summary>
        /// <returns></returns>
       public string GenerateRefreshToken();
    }
}
