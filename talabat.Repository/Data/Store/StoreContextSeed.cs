using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using talabat.Core.Entites.Order_Aggregate;
using talabat.Core.Entites.Products;

namespace talabat.Repository.Data.Store
{
    public static class StoreContextSeed
    {
        public async static Task Seeding(StoreContext storeContext)
        {
            if (storeContext.productBrands.Count() == 0)
            {
                var ProductBrandData = File.ReadAllText("../talabat.Repository/Data/Store/DataSeed/brands.json");
                var ProductBrand = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                if (ProductBrand?.Count() > 0)
                {
                    foreach (var item in ProductBrand)
                    {
                        storeContext.Set<ProductBrand>().Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }

            }

            if (storeContext.productCategories.Count() == 0)
            {
                var ProductCategoryData = File.ReadAllText("../talabat.Repository/Data/Store/DataSeed/categories.json");
                var ProductCategory = JsonSerializer.Deserialize<List<ProductCategory>>(ProductCategoryData);
                if (ProductCategory?.Count() > 0)
                {
                    foreach (var item in ProductCategory)
                    {
                        storeContext.Set<ProductCategory>().Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }


            }

            if (!storeContext.products.Any())
            {
                var productsData = File.ReadAllText("../talabat.Repository/Data/Store/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products?.Count() > 0)
                {
                    foreach (var item in products)
                    {
                        storeContext.Set<Product>().Add(item);
                    }
                    await storeContext.SaveChangesAsync();
                }

            }

            if (!storeContext.deliveryMethods.Any())
            {
                var GetDeliveryJson = File.ReadAllText("../talabat.Repository/Data/Store/DataSeed/delivery.json");
                var Delivery = JsonSerializer.Deserialize<List<DeliveryMethod>> (GetDeliveryJson);
                if (Delivery?.Count() > 0)
                {
                     foreach (var item in Delivery)
                    {
                        storeContext.Set<DeliveryMethod>().Add(item);
                    }
                     await storeContext.SaveChangesAsync();
                }

               




            }
            
        }




       
    }
}
