using Backend.Entities;

namespace Backend.Dtos
{
    public class BasketItemDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        //public Product Product { get; set; }
          
        //public string ProductName { get; set; }
        
        //public decimal Price { get; set; }
        
        public int Quantity { get; set; }
    }
}