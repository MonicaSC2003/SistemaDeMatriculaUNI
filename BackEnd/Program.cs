using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using DAL.Interfaces;
using DAL.Interfaces.InterfacesDeEntidades;
using DAL.Implementaciones;
using DAL.Implementaciones.ImplementacionesDeEntidades;
using BackEnd.Servicios.Interfaces;
using BackEnd.Servicios.Implementaciones;
using BackEnd.Helpers.Implementaciones;
using BackEnd.Helpers.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region ServicesYDBContext
builder.Services.AddDbContext<SistemaCursosContext>(
                                options =>
                                options.UseSqlServer(
                                    builder
                                    .Configuration
                                    .GetConnectionString("DefaultConnection")
                                 ));

// Registro de DAL
builder.Services.AddScoped<ICursoDAL, DALCursoImpl>();
builder.Services.AddScoped<IEvaluacioneDAL, DALEvaluacioneImpl>();
builder.Services.AddScoped<IHorarioDAL, DALHorarioImpl>();
builder.Services.AddScoped<IInscripcioneDAL, DALInscripcioneImpl>();
builder.Services.AddScoped<INotaDAL, DALNotaImpl>();
builder.Services.AddScoped<ISeccioneDAL, DALSeccioneImpl>();
builder.Services.AddScoped<ITipoEvaluacioneDAL, DALTipoEvaluacioneImpl>();
builder.Services.AddScoped<IUsuarioDAL, DALUsuarioImpl>();
builder.Services.AddScoped<IMailHelper, MailHelper>();

// Registro de Unidad de Trabajo
builder.Services.AddScoped<IUnidadDeTrabajo, UnidadDeTrabajo>();
// Registro de Servicios
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITipoEvaluacionService, TipoEvaluacionService>();
builder.Services.AddScoped<IEvaluacionService, EvaluacionService>();
builder.Services.AddScoped<IHorarioService, HorarioService>();
builder.Services.AddScoped<ICursoService, CursoService>();

var app = builder.Build();
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();