using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using TaskManagement.Application.DTOs.User;

namespace TaskManagement.Application.DTOs.Auth
{
    public class AuthResponse
    {
        public string? Token { get; set; }

        [JsonIgnore]
        public string? RefreshToken { get; set; }

        public UserResponse? UserResponse { get; set; }
    }
}
