
using AeroGuard.Data;
using AeroGuard.Repositories.Implementation;
using AeroGuard.Repositories.Interface;
using AeroGuard.Services.Implementation;
using Microsoft.EntityFrameworkCore;

namespace AeroGuard
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

            // MSSQL Database
            builder.Services.AddDbContext<AeroGuardDbContext>(
                options => options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "AeroGuardDB")));

            // Repository Pattern
            builder.Services.AddScoped<
                ISurveyRepository, SurveyRepository>();

            builder.Services.AddScoped<
                IImageRepository, ImageRepository>();

            // Services
            builder.Services.AddScoped<ImageStorageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint(
                        "/openapi/v1.json",
                        "AeroGuard v1"
                    );
                });
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
