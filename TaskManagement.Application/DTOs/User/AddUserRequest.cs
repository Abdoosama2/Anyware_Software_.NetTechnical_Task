using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.User
{
    public class AddUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;

        [Required]
        public UserRole Role { get; set; }
    }
}
