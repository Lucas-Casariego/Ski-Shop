using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<StoreContext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        app.UseSwagger();

        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ski-Shop API V1"));

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
        try
        {
            context.Database.Migrate();
            DbInitializer.Initialize(context);
        }
        catch (Exception ex)
        {
            throw;
        }


        app.Run();
    }
}