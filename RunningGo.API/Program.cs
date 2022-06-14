using Microsoft.EntityFrameworkCore;
using RunningGo.API.Dietas.Domain.Repositories;
using RunningGo.API.Dietas.Domain.Services;
using RunningGo.API.Dietas.Persistence.Repositories;
using RunningGo.API.Dietas.Services;
using RunningGo.API.Rutinas.Domain.Repositories;
using RunningGo.API.Rutinas.Domain.Services;
using RunningGo.API.Rutinas.Persistence.Repositories;
using RunningGo.API.Rutinas.Services;
using RunningGo.API.Shared.Domain.Repositories;
using RunningGo.API.Shared.Domain.Services;
using RunningGo.API.Shared.Mapping;
using RunningGo.API.Shared.Persistence.Contexts;
using RunningGo.API.Shared.Persistence.Repositories;
using RunningGo.API.Shared.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<EnhancedDbContext>(options => options.UseMySQL(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information).EnableServiceProviderCaching()
    .EnableDetailedErrors());

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IFoodRepository, FoodRepository>();
builder.Services.AddScoped<IFoodService, FoodService>();
builder.Services.AddScoped<IDietRepository, DietRepository>();
builder.Services.AddScoped<IDietService, DietService>();

builder.Services.AddScoped<IHabitRepository, HabitRepository>();
builder.Services.AddScoped<IHabitService, HabitService>();
builder.Services.AddScoped<IRoutineRepository, RoutineRepository>();
builder.Services.AddScoped<IRoutineService, RoutineService>();

builder.Services.AddAutoMapper(typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<EnhancedDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();