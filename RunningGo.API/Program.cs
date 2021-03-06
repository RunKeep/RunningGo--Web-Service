using Microsoft.EntityFrameworkCore;
using RunningGo.API.Checkeos.Domain.Repositories;
using RunningGo.API.Checkeos.Domain.Services;
using RunningGo.API.Checkeos.Persistence.Repositories;
using RunningGo.API.Checkeos.Services;
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
using RunningGo.API.SistemaDeMetas.Domain.Repositories;
using RunningGo.API.SistemaDeMetas.Domain.Services;
using RunningGo.API.SistemaDeMetas.Persistence.Repositories;
using RunningGo.API.SistemaDeMetas.Services;

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

builder.Services.AddScoped<IGoalRepository, GoalRepository>();
builder.Services.AddScoped<IGoalService, GoalService>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<IProcessService, ProcessService>();

builder.Services.AddScoped<ISpecialistRepository, SpecialistRepository>();
builder.Services.AddScoped<ISpecialistService, SpecialistService>();
builder.Services.AddScoped<ICheckupRepository, CheckupRepository>();
builder.Services.AddScoped<ICheckupService, CheckupService>();
builder.Services.AddScoped<IArrangeRepository, ArrangeRepository>();
builder.Services.AddScoped<IArrangeService, ArrangeService>();

builder.Services.AddAutoMapper(typeof(ModelToResourceProfile),
    typeof(ResourceToModelProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<EnhancedDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
/*if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}*/

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();