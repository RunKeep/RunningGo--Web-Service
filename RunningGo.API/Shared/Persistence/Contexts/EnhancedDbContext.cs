using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RunningGo.API.Dietas.Domain.Models;
using RunningGo.API.Rutinas.Domain.Models;
using RunningGo.API.Shared.Domain.Models;
using RunningGo.API.Shared.Extensions;

namespace RunningGo.API.Shared.Persistence.Contexts;

public class EnhancedDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    
    public DbSet<Food> Foods { get; set; }
    public DbSet<Diet> Diets { set; get; }
    
    public DbSet<Habit> Habits { set; get; }

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

        builder.Entity<Diet>().ToTable("diets");
        builder.Entity<Diet>().HasKey(p => p.Id);
        builder.Entity<Diet>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Diet>().Property(p => p.Description).IsRequired().HasMaxLength(200);
        builder.Entity<Diet>().Property(p => p.Specs).IsRequired().HasMaxLength(200);
        builder.Entity<Diet>().Property(p => p.Duration).IsRequired();
        builder.Entity<Diet>().Property(p => p.Quantity).IsRequired();

        builder.Entity<Habit>().ToTable("habits");
        builder.Entity<Habit>().HasKey(p => p.Id);
        builder.Entity<Habit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Habit>().Property(p => p.Description).IsRequired().HasMaxLength(250);

        //Relationships
        
        builder.Entity<Food>().HasMany(p => p.Diets)
            .WithOne(p => p.Food)
            .HasForeignKey(p => p.FoodId);

        builder.Entity<User>().HasMany(p => p.Diets)
            .WithOne(p => p.User)
            .HasForeignKey(p => p.UserId);
        
        

        builder.UseSnakeCaseNamingConvention();
    }
}