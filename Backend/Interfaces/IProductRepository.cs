using Backend.Entities;

namespace Backend.Interfaces
{
    //23. Adding a repository and interface
    public interface IProductRepository
    {
        //23.
        Task<Product> GetProductByIdAsync(int id);
       

        Task<List<Product>> GetProductsAsync();

         ////30. Adding the code to get the product brands and types
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();

        Task<int> UpdateProductAsync(Product p);
        Task<int> DeleteProductAsync(int id);
        
    }
}