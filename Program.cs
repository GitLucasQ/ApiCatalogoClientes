using ApiCatalogoClientes.Common;
using ApiCatalogoClientes.Domain;
using ApiCatalogoClientes.Interfaces;
using ApiCatalogoClientes.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
});

// Configure RepositoryWrapper
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

// Configure DB Connection
var connectionString = builder.Configuration.GetSection("AppSettings")["conexion"];
builder.Services.AddDbContext<RepositoryContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// configure AppSettings
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configure Automapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.UseRouting();

app.UseCors("CorsPolicy");

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
