using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;
using EShop.Domain.Models;

namespace EShop.Domain.Seeders
{
    public class EShopSeeder(DataContext context) : IEShopSeeder 
    {
        public async Task Seed()
        {
            if (!context.Products.Any())
            {
                var students = new List<Product>
                {
                    new Product { Name = "Mario", Ean = "69" },
                    new Product { Name = "Luigi", Ean = "2137" },
                    new Product { Name = "Leonardo", Ean = "735" }
                };

                context.Products.AddRange(students);
                context.SaveChanges();
            }
        }
    }
}
