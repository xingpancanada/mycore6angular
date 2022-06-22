using System.Runtime.CompilerServices;
using System.Text.Json;
using Backend.Entities;

namespace Backend.Data
{
    public class StoreDBContextSeed
    {
        public static async Task SeedAsync(StoreDBContext context){
            try{
                if(!context.ProductBrands.Any()){
                    ////29. Adding Seed Data
                    var brandsData = File.ReadAllText("../Backend/Data/SeedData/brands.json");

                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach(var item in brands){
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.ProductTypes.Any()){
                    ////29. Adding Seed Data
                    var typesData = File.ReadAllText("../Backend/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach(var item in types){
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if(!context.Products.Any()){
                    ////29. Adding Seed Data from JSON file
                    var productsData = File.ReadAllText("../Backend/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach(var item in products){
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }catch(Exception ex){
                throw(ex);
            }
        }
    }
}