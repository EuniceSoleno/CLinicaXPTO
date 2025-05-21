using CLinicaXPTO.DAL.Repositories;
using ClinicaXPTO.DAL;
using CLinicaXPTO.Interface.Repositories_Interface;
using CLinicaXPTO.Interface.Services_Interfaces;
using CLinicaXPTO.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CLinicaXPTODBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUtenteRepository, UtenteRepository>();
builder.Services.AddScoped<IUtenteServiceInterface, UtenteService>();

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
