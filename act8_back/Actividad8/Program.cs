using Actividad8.Negocio.Data;
using Actividad8.Negocio.Data.Repositories;
using Actividad8.Negocio.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<Turnos_context>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IRepositoryServicio, RepositoryServicio>();

builder.Services.AddScoped<IServiceServicio, ServiceServicio>();

builder.Services.AddScoped<IRepositoryTurno, RepositoryTurno>();

builder.Services.AddScoped<IServiceTurno, ServiceTurno>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
