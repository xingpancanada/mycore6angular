using AutoMapper;
using Backend.Data;
using Backend.Dtos;
using Backend.Entities;
using Backend.Errors;
using Backend.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Controllers;

// [ApiController]
// [Route("api/[controller]")]
public class  ProductsController : BaseApiController
{
    // ////connect to db
    // private readonly StoreDBContext _storeDBContext;
    // public ProductsController(StoreDBContext storeDBContext)
    // {
    //     _storeDBContext = storeDBContext;
    // }

    private readonly IProductRepository _productRepo;

    /////44. Adding AutoMapper
    private readonly IMapper _mapper;
   
     public ProductsController(IProductRepository productRepo, IMapper mapper)
    {
        _productRepo = productRepo;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(int? brandId, int? typeId)
    {
        var products = await _productRepo.GetProductsAsync();
        //return Ok(products);
        if(products == null) return NotFound(new ApiResponse(404));

        List<Product> filteredProducts = null;
        ////my63
        if(brandId != null && typeId == null){
            filteredProducts = products.Where(x => x.ProductBrandId == brandId).ToList();
        }else if(typeId != null && brandId == null){
            filteredProducts = products.Where(x => x.ProductTypeId == typeId).ToList();
        }else if(typeId != null && brandId != null){
            filteredProducts = products.Where(x => x.ProductTypeId == typeId)
                .Where(x => x.ProductBrandId == brandId)
                .ToList();
        }else{
            filteredProducts = products;
        }
        
        //////45. Configuring AutoMapper profile
        return Ok(_mapper.Map<List<Product>, IReadOnlyList<ProductToReturnDto>>(filteredProducts));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id)
    {
        var product = await _productRepo.GetProductByIdAsync(id);
        //return Ok(product);

        if(product == null) return NotFound(new ApiResponse(404));

        //////44. Adding AutoMapper to the API project
        // using autoMapping
        return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
    }

    [HttpGet("brands")]
    public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
    {
        var productBrands = await _productRepo.GetProductBrandsAsync();
        if(productBrands == null) return NotFound(new ApiResponse(404));
        return Ok(productBrands);
    }

    [HttpGet("types")]
    public async Task<ActionResult<List<ProductType>>> GetProductTypes()
    {
        var productTypes = await _productRepo.GetProductTypesAsync();
        if(productTypes == null) return NotFound(new ApiResponse(404));
        return Ok(productTypes);
    }
}
