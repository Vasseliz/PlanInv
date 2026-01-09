using Microsoft.EntityFrameworkCore;
using PlanInv.Api.Extensions;
using PlanInv.Api.Middlewares;
using PlanInv.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();



builder.Services.AddSwaggerConfiguration();
builder.Services.AddCorsConfiguration();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();



var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

    try
    {
        logger.LogInformation(" Verificando migrations...");
        if (app.Environment.IsDevelopment())
        {
            context.Database.Migrate();
        }
        logger.LogInformation("Database atualizado com sucesso!");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, " Erro ao aplicar migrations");
        throw;
    }
}


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Investimentos API v1");
    });
}
app.UseMiddleware<ExceptionHandlingMiddleware>(); 
app.UseHttpsRedirection();
app.UseCors("AllowAll");  
app.MapControllers();

app.Run();