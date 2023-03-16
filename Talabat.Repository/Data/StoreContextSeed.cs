using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;
using Talabat.Domine.Entites.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                if (!context.productBrands.Any())
                {
                    var brandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                    var brandsJson = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                    foreach (var brand in brandsJson)
                        context.Set<ProductBrand>().Add(brand);
                }
                if (!context.productTypes.Any())
                {
                    var typesData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                    var typesJson = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                    foreach (var type in typesJson)
                        context.Set<ProductType>().Add(type);
                }
                if (!context.products.Any())
                {
                    var productsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                    var protucts = JsonSerializer.Deserialize<List<Product>>(productsData);
                    foreach (var protuct in protucts)
                        context.Set<Product>().Add(protuct);
                }
                if (!context.DelivreyMethods.Any())
                {
                    var DelivreyData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                    var Delivrey = JsonSerializer.Deserialize<List<DelivreyMethod>>(DelivreyData);
                    foreach (var Deliv in Delivrey)
                        context.Set<DelivreyMethod>().Add(Deliv);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContextSeed>();
                logger.LogError(ex, ex.Message);
            }
        }
    }
}
