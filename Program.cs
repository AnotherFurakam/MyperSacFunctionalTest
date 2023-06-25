using Microsoft.EntityFrameworkCore;
using MyperSacFunctionalTest.Models;
using MyperSacFunctionalTest.Services.Departamento;
using MyperSacFunctionalTest.Services.Provincia;
using MyperSacFunctionalTest.Services.Trabajador;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuraci�n de la conexi�n a la base de datos
builder.Services.AddDbContext<TrabajadoresPruebaContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
    );


// Configuraci�n del AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Registro de servicios en esta parte
builder.Services.AddScoped<ITrabajadorService, TrabajadorServiceImpl>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoServiceImpl>();
builder.Services.AddScoped<IProvinciaService, ProvinciaServiceImpl>();


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
