using PlanInv.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

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
                // Define onde ficam as Migrations
                sqlServerOptions.MigrationsAssembly("PlanInv.Infrastructure");

                // Retry automático em caso de falha
                sqlServerOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(5),
                    errorNumbersToAdd: null
                );
                sqlServerOptions.CommandTimeout(30);
            }));

        return services;
    }
    /// <summary>
    /// Registra os Repositories (será preenchido conforme criarmos)
    /// </summary>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        // Exemplo:
        // services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        // services.AddScoped<IAtivoRepository, AtivoRepository>();

        return services;
    }

    /// <summary>
    /// Registra os Services da camada Application (será preenchido conforme criarmos)
    /// </summary>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        // Exemplo:
        // services.AddScoped<IUsuarioService, UsuarioService>();
        // services.AddScoped<IAtivoService, AtivoService>();

        return services;
    }

    /// <summary>
    /// Configura CORS para Angular
    /// </summary>
    public static IServiceCollection AddCorsConfiguration(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            // Política para desenvolvimento (permite tudo temporariamente)
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

    /// <summary>
    /// Configura Swagger com documentação melhorada
    /// </summary>
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