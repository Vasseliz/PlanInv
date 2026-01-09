using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PlanInv.Application.Interfaces;
using PlanInv.Application.Services;
using PlanInv.Domain.Interfaces;
using PlanInv.Infrastructure.Data;
using PlanInv.Infrastructure.Repositories;

namespace PlanInv.Api.Extensions;

public static class ServiceCollectionExtensions
{

    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' não encontrada!");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, sqlServerOptions =>
            {
                sqlServerOptions.MigrationsAssembly("PlanInv.Infrastructure");

                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );
                sqlServerOptions.CommandTimeout(30);
            }));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {

        services.AddScoped<IUsuarioRepository, UsuarioRepository>();


        return services;
    }

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {


        services.AddScoped<IUsuarioService, UsuarioService>();


        return services;
    }


    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {

            options.AddPolicy("AllowAll", policy =>
            {
                policy
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });

        });

        return services;
    }

    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PlanInv API",
                Version = "v1",
                Description = "API para Controle de Carteira de Investimentos",
            });

        });

        return services;
    }
}