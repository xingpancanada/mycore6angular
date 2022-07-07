namespace Backend.Entities
{
   ///////137. Setting up the basket class
    public class BasketItem
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        public Product Product { get; set; }
          
        //public string ProductName { get; set; }
        
        //public decimal Price { get; set; }
        
        public int Quantity { get; set; }
        
        // public string PictureUrl { get; set; }
        
        // public string Brand { get; set; }
        
        // public string Type { get; set; }
    }
}