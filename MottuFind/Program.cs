using System.Text.Json.Serialization;
using MottuFind_C_.Application.Services;
using MottuFind_C_.Domain.Repositories;
using MottuFind_C_.Infrastructure.Context;
using MottuFind_C_.Infrastructure.Repositories;
using Sprint1_C_.Application.Services;
using Sprint1_C_.Mappings;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Sprint1_C_.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MottuFind.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Sprint1_C_
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            // Repositories
            builder.Services.AddScoped<IFilialRepository, FilialRepository>();
            builder.Services.AddScoped<IMotoRepository, MotoRepository>();
            builder.Services.AddScoped<IPatioRepository, PatioRepository>();
            builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            builder.Services.AddScoped<ILeitorRfidRepository, LeitorRfidRepository>();
            builder.Services.AddScoped<ILeituraRfidRepository, LeituraRfidRepository>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(o =>
                o.EnableAnnotations()
            );

            // MongoDB setup
            var mongoSettings = builder.Configuration.GetSection("MongoDb").Get<MongoDbSettings>();
            if (mongoSettings == null)
                throw new Exception("MongoDb settings not configured in appsettings.json");

            builder.Services.AddSingleton(mongoSettings);
            builder.Services.AddSingleton<MongoDbContext>();

            // Services
            builder.Services.AddScoped<MotoService>();
            builder.Services.AddScoped<FilialService>();
            builder.Services.AddScoped<PatioService>();
            builder.Services.AddScoped<UsuarioService>();
            builder.Services.AddScoped<LeitorRfidService>();
            builder.Services.AddScoped<LeituraRfidService>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddControllers()
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            // ?? HEALTH CHECKS SIMPLIFICADO ??
            builder.Services.AddHealthChecks()
                .AddUrlGroup(
                    new Uri("https://www.google.com"),
                    name: "Google API",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "external", "api" }
                )
                .AddCheck<ApplicationHealthCheck>(
                    "Application",
                    failureStatus: HealthStatus.Degraded,
                    tags: new[] { "application", "internal" }
                );

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            // ?? ENDPOINTS DE HEALTH CHECK ??
            app.MapHealthChecks("/health", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse,
                Predicate = check => check.Tags.Contains("application") ||
                                   check.Tags.Contains("database") ||
                                   check.Tags.Contains("external")
            });

            app.MapHealthChecks("/health/ready", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse,
                Predicate = check => check.Tags.Contains("database") ||
                                   check.Tags.Contains("external")
            });

            app.MapHealthChecks("/health/live", new HealthCheckOptions()
            {
                ResponseWriter = HealthCheckExtensions.WriteResponse,
                Predicate = check => check.Tags.Contains("application")
            });

            app.Run();
        }
    }
}