using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using Backend.Dtos;
using Backend.Entities;
using Backend.Errors;
using Backend.Interfaces;
using Backend.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    public class BasketsController : BaseApiController
    {
        private readonly IBasketRepository _basketRepo;

        private readonly IMapper _mapper;

        private readonly IConfiguration _config;
    
    
        public BasketsController(IBasketRepository basketRepo, IMapper mapper, IConfiguration config)
        {
            _basketRepo = basketRepo;
            _mapper = mapper;
            _config = config;   ////return _config["ApiUrl"] + source.PictureUrl;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerBasket>>> GetBaskets()
        {
            var baskets = await _basketRepo.GetBasketsAsync();
            return Ok(baskets);
        }

        // [HttpGet("byuid")]
        // public async Task<ActionResult<CustomerBasket>> GetBasketByUid(string uid)
        // {
        //     var basket = await _basketRepo.GetBasketByUidAsync(uid);

        //     if(basket == null) return NotFound(new ApiResponse(404));

        //     return Ok(basket);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _basketRepo.GetBasketAsync(id);

            if(basket == null) return NotFound(new ApiResponse(404));

            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> AddBasket(CustomerBasket cb)
        {
            var customerBasket = await _basketRepo.AddBasketAsync(cb);
            return Ok(customerBasket);
        }

        [HttpPut("update")]
        public async Task UpdateBasket(BasketUpdateDto cb)
        {
            var b = _mapper.Map<BasketUpdateDto, CustomerBasket>(cb);
            await _basketRepo.UpdateBasketAsync(b);
        }

        [HttpDelete]
        public async Task DeleteBasket(string id)
        {
            try{
                var result = await _basketRepo.DeleteBasketAsync(id);
                if(result != 1){
                    Console.WriteLine("Failed to Update Basket!");
                }
            }catch(Exception e){
                Console.WriteLine(e);
            }
        }
    }
}