using Backend.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces
{
    public interface IBasketRepository
    {
        Task<CustomerBasket> GetBasketAsync(string id);
        //Task<CustomerBasket> GetBasketByUidAsync(string uid);
        Task<List<CustomerBasket>> GetBasketsAsync();
        Task<int> UpdateBasketAsync(CustomerBasket basket);
        Task<int> DeleteBasketAsync(string id);
        Task<CustomerBasket> AddBasketAsync(CustomerBasket basket);
    }
}