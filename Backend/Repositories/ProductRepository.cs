using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreDBContext _storeDBContext;
        public ProductRepository(StoreDBContext storeDBContext)
        {
            _storeDBContext = storeDBContext;
        }

    
        public async Task<Product> GetProductByIdAsync(int id)
        {
            /////31. Eager loading of navigation properties
            return await _storeDBContext.Products
                .Include(p=>p.ProductBrand)
                .Include(p=>p.ProductType)
                .FirstOrDefaultAsync(p=>p.Id == id);
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            /////31. Eager loading of navigation properties
            return await _storeDBContext.Products
                .Include(p=>p.ProductBrand)
                .Include(p=>p.ProductType)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            ////30. Adding the code to get the product brands and types
            return await _storeDBContext.ProductTypes.ToListAsync();
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            ////30. Adding the code to get the product brands and types
            return await _storeDBContext.ProductBrands.ToListAsync();
        }

        public async Task<int> UpdateProductAsync(Product product)
        {
            _storeDBContext.Products.Attach(product);
            _storeDBContext.Entry(product).State = EntityState.Modified;

            var result = await _storeDBContext.SaveChangesAsync();
            return result;
        }

        public async Task<int> DeleteProductAsync(int id)
        {
            var p = await this.GetProductByIdAsync(id);
            _storeDBContext.Products.Remove(p);
            var result = await _storeDBContext.SaveChangesAsync();
            return result;
        }
    }
}