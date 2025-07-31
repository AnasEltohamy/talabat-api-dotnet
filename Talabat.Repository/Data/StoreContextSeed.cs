using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entites;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {

        public static async Task SeedAsync(StoreContext dbcontext)
        {
            if (!dbcontext.ProductBrands.Any())
            {
                var ProductBrand = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrand);
                if (Brands?.Count > 0)
                {
                    foreach (var Brand in Brands) 
                    {
                        await dbcontext.Set<ProductBrand>().AddAsync(Brand);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }


            if (!dbcontext.productTypes.Any())
            {
                var ProductType= File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Types= JsonSerializer.Deserialize<List<ProductType>>(ProductType);
                if (Types?.Count>0)
                {
                    foreach (var Type in Types)
                    {
                       await dbcontext.Set<ProductType>().AddAsync(Type);
                    }
                    await dbcontext.SaveChangesAsync();
                }
            }

            if (!dbcontext.Products.Any())
            {
                var Product = File.ReadAllText("../Talabat.Repository/Data/DataSeed/Products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(Product);
                if (Products?.Count>0)
                {
                    foreach (var item in Products)
                    {
                        await dbcontext.Set<Product>().AddAsync(item);
                    }
                    await dbcontext.SaveChangesAsync();
                }

            }
            
        }
    }
}
