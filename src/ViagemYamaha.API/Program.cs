using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ViagemYamaha.API.Configuration;
using ViagemYamaha.Core.Settings;

namespace ViagemYamaha.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var fileSettings = builder.Configuration.GetSection("FileSettings");

            builder.Services.Configure<FileSettings>(fileSettings);

            builder.Services.AddDependecyInjection();

            builder.Services.AddCors(o =>
            {
                o.AddPolicy("Full",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors("Full");

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}