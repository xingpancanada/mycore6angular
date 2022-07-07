using System.Runtime.CompilerServices;
using Backend.Data;
using Backend.Entities;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.ObjectPool;
using SQLitePCL;

namespace Backend.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly StoreDBContext _storeDBContext;

        public BasketRepository(StoreDBContext storeDBContext)
        {
            _storeDBContext = storeDBContext;
           
        }

        public async Task<List<CustomerBasket>> GetBasketsAsync(){
            return await _storeDBContext.CustomerBasket
                .Include(b => b.Items)
                .ToListAsync();
        }

       

        public async Task<CustomerBasket> GetBasketAsync(string id)
        {
            var cb = await _storeDBContext.CustomerBasket
                .Include(b => b.Items)
                .FirstOrDefaultAsync(b => b.Id == id);

            // for(int i = 0;  i < cb.Items.Count; i ++){
            //     var p = await _productRepo.GetProductByIdAsync(cb.Items[i].ProductId);
            //     cb.Items[i].Product = p;
            // }

            return cb;
        }

         public async Task<CustomerBasket> AddBasketAsync(CustomerBasket cb)
        {
          
            _storeDBContext.CustomerBasket.Add(cb);
            var result = await _storeDBContext.SaveChangesAsync();
            if(result == 1){
                return cb;
            }else{
                Console.WriteLine("Failed to add basket!");
                return null;
            }
        }

        public async Task<int> UpdateBasketAsync(CustomerBasket cb)
        {
            // var basket = await _storeDBContext.CustomerBasket
            //     .Include(b => b.Items)
            //     .FirstOrDefaultAsync(b => b.Id == cb.Id);
            //var items = cb.Items;
            
            _storeDBContext.CustomerBasket.UpdateRange(cb);
            //_storeDBContext.BasketItem.UpdateRange(items);
            //_storeDBContext.Entry(cb).State = EntityState.Modified;
            //var result = await _storeDBContext.SaveChangesAsync();
            return await _storeDBContext.SaveChangesAsync();
        }
                
        public async Task<int> DeleteBasketAsync(string id)
        {
            var cb = await this.GetBasketAsync(id);
            _storeDBContext.CustomerBasket.Remove(cb);
            var result = await _storeDBContext.SaveChangesAsync();
            return result;
        }
    }
}