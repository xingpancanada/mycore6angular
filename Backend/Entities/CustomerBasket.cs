namespace Backend.Entities
{
    //////137. Setting up the basket class
    public class CustomerBasket
    {
        public CustomerBasket()
        {
        }

        public CustomerBasket(string id)
        {
            Id = id;
        }

        // public CustomerBasket(string uid){
        //     Uid = uid;
        // }

        public string Id { get; set; }

        //public string Uid { get; set; }
        public List<BasketItem> Items { get; set; } = new List<BasketItem>();
        
        public int? DeliveryMethodId { get; set; }

        public string ClientSecret { get; set; }
        public string PaymentIntentId { get; set; }

        public decimal? ShippingPrice { get; set; }
    }

}