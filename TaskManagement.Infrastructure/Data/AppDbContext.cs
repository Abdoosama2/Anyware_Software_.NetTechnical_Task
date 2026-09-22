using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(x => x.Email).IsUnique();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(100);
                entity.Property(x => x.Email).IsRequired().HasMaxLength(256);
                entity.Property(x => x.Role).IsRequired().HasConversion<string>().HasMaxLength(20);
            });

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
          
            base.OnModelCreating(modelBuilder);
        }
    }
}
