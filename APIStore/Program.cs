using Data.Entities;
using Data.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;

namespace Data
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ProductService>();
            builder.Services.AddScoped<ISaleService, SaleService>();
            builder.Services.AddScoped<DevolutionService>();
            builder.Services.AddScoped<ExchangeService>();

            //Pra usar filters e Middlewares
            builder.Services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            //Consifguração dos Filters
            builder.Services.AddControllers(options =>
            {
                //aqui eu vou adicionar os filters que eu criar
                
                //options.Filters.Add<>;
            });

            //Configuração CORS
            builder.Services.AddCors(corsOptions =>
            {
                corsOptions.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:5500")
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });

            builder.Services.AddEndpointsApiExplorer();
            //Instalar Nugget Swashbuckle.AspNetCore
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors("AllowSpecificOrigin");
            }

            app.UseAuthorization();
            //Será que eu tenho que colcoar meus middlewares aqui???
            //Acho que é aqui sim
            //app.UseMiddleware<nomedaclassemiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
