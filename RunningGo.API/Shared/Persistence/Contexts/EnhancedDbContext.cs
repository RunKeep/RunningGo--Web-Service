using Microsoft.EntityFrameworkCore;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Shared.Persistence.Contexts;

public class EnhancedDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Food> Foods { get; set; }

    public EnhancedDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>().ToTable("users");
        builder.Entity<User>().HasKey(p => p.Id);
        builder.Entity<User>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<User>().Property(p => p.Name).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.LastName).IsRequired().HasMaxLength(30);
        builder.Entity<User>().Property(p => p.Age).IsRequired();
        builder.Entity<User>().Property(p => p.Height).IsRequired();
        builder.Entity<User>().Property(p => p.Weight).IsRequired();

        builder.Entity<Food>().ToTable("foods");
        builder.Entity<Food>().HasKey(p => p.Id);
        builder.Entity<Food>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Food>().Property(p => p.Name).IsRequired().HasMaxLength(50);
        builder.Entity<Food>().Property(p => p.Calories).IsRequired();
        builder.Entity<Food>().Property(p => p.Vitamins).IsRequired();
        builder.Entity<Food>().Property(p => p.Quantity).IsRequired();
        
        builder.UseSnakeCaseNamingConvention();
    }
}