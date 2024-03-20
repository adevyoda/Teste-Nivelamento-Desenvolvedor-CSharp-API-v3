using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Questao5.Infrastructure.Sqlite;
using Questao5.Application.Commands;
using Questao5.Application.Queries;
using Questao5.Application.Services;
using Questao5.Domain.Entities;
using Questao5.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// sqlite
builder.Services.AddSingleton(new DatabaseConfig { Name = builder.Configuration.GetValue<string>("DatabaseName", "Data Source=database.sqlite") });
builder.Services.AddSingleton<IDatabaseBootstrap, DatabaseBootstrap>();

// Registre o servi�o IDbContext no cont�iner de servi�os
builder.Services.AddSingleton<IDbContext, DbContext>();

// Adicione os servi�os IAccountMovementService e IAccountBalanceService
builder.Services.AddScoped<IAccountMovementService, AccountMovementService>();
builder.Services.AddScoped<IAccountBalanceService, AccountBalanceService>();



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

// sqlite
//app.Services.GetService<IDatabaseBootstrap>().Setup();


// Obtenha o servi�o IDatabaseBootstrap do cont�iner de servi�os
var databaseBootstrap = app.Services.GetService<IDatabaseBootstrap>();

// Verifique se o servi�o retornado n�o � nulo antes de chamar o m�todo Setup
if (databaseBootstrap != null)
{
    try
    {
        // Tente chamar o m�todo Setup
        databaseBootstrap.Setup();
    }
    catch (Exception ex)
    {
        // Em caso de erro, registre o erro ou tome alguma outra a��o apropriada
        Console.WriteLine($"Erro ao configurar o banco de dados: {ex.Message}");
        // Voc� tamb�m pode registrar o erro em um log ou notificar o administrador do sistema, dependendo da necessidade
    }
}



app.Run();
