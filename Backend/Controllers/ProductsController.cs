using Backend.Data;
using Backend.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class  ProductsController : ControllerBase
{
    ////connect to db
    private readonly StoreDBContext _storeDBContext;
    public ProductsController(StoreDBContext storeDBContext)
    {
        _storeDBContext = storeDBContext;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts()
    {
        var products = await _storeDBContext.Products.ToListAsync();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _storeDBContext.Products.FindAsync(id);
        return Ok(product);
    }
}
