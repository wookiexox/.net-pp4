using EShop.Application.Service;
using EShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using EShop.Domain.Seeders;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Threading.Tasks;

namespace EShopService;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDb"), ServiceLifetime.Transient);
        builder.Services.AddScoped<IRepository, Repository>();

        // Add services to the container.
        builder.Services.AddScoped<ICreditCardService, CreditCardService>();
        builder.Services.AddScoped<IProductService, IProductService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddScoped<IEShopSeeder, EShopSeeder>();

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

        var scope = app.Services.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<IEShopSeeder>();
        await seeder.Seed();

        app.Run();
    }
}
