using Backend.Entities;

namespace Backend.Dtos
{
    public class ProductToReturnDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string PictureUrl { get; set; }
        
        public decimal Price { get; set; }
        
        public int ProductTypeId { get; set; }
        
        public int ProductBrandId { get; set; }

        public ProductType ProductType { get; set; }
        public ProductBrand ProductBrand { get; set; }
        
        
    }
}