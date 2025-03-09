
using Inteli.Mantainability.Factories;

namespace Inteli.Mantainability
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Registrando todas as factories como serviços injetáveis
            builder.Services.AddTransient<IFreteServiceFactory, TransportadoraAFactory>();
            builder.Services.AddTransient<IFreteServiceFactory, TransportadoraBFactory>();
            builder.Services.AddTransient<IFreteServiceFactory, CorreiosFactory>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
