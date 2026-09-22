
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs.User
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
