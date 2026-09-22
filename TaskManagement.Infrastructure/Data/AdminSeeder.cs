using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Infrastructure.Data
{
    public class AdminSeeder
    {
        private const string AdminEmail = "admin@example.com";
        private const string AdminPassword = "Admin@123";
        private const string AdminName = "System Administrator";

        public static async Task SeedAsync(AppDbContext context)
        {
           
            await context.Database.MigrateAsync();

            var adminExists = await context.Users.AnyAsync(u => u.Email == AdminEmail);
            if (adminExists)
                return;

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Name = AdminName,
                Email = AdminEmail,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(AdminPassword),
                Role = UserRole.Admin,
                CreatedAt = DateTime.UtcNow
            };

            await context.Users.AddAsync(admin);
            await context.SaveChangesAsync();
        }
    }
}

