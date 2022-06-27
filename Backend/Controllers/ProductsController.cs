using AutoMapper;
using Backend.Dtos;
using Backend.Entities;
using Backend.Errors;
using Backend.Interfaces;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using SQLitePCL;

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

    private readonly IConfiguration _config;
  
   
     public ProductsController(IProductRepository productRepo, IMapper mapper, IConfiguration config)
    {
        _productRepo = productRepo;
        _mapper = mapper;
        _config = config;   ////return _config["ApiUrl"] + source.PictureUrl;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(int brandId = 0, int typeId = 0, string sort = "name", int pageSize = 100, int page = 1, string search = "all")
    {
        var products = await _productRepo.GetProductsAsync();
        //return Ok(products);
        if(products == null) return NotFound(new ApiResponse(404));

        List<Product> filteredProducts = null;
        ////my63
        if(brandId != 0 && typeId == 0){
            filteredProducts = products.Where(x => x.ProductBrandId == brandId).ToList();
        }else if(brandId == 0 && typeId != 0){
            filteredProducts = products.Where(x => x.ProductTypeId == typeId).ToList();
        }else if( brandId != 0 && typeId != 0){
            filteredProducts = products.Where(x => x.ProductTypeId == typeId)
                .Where(x => x.ProductBrandId == brandId)
                .ToList();
        }else{
            filteredProducts = products;
        }

        List<Product> searchedProducts = filteredProducts;
        if(search != "all" && search != null && search != ""){
            searchedProducts = products
                .Where(x => x.Name.IndexOf(search, 0, StringComparison.CurrentCultureIgnoreCase) != -1)
                .ToList();
        }

        List<Product> sortedProducts = searchedProducts.OrderBy(p => p.Name).ToList();
        
        if(sort == "priceDes" || sort == "priceDesc" || sort== "priceDescending"){
            sortedProducts = filteredProducts.OrderByDescending(p => p.Price).ToList();
        }
        else if(sort == "nameDes" || sort == "nameDesc" || sort== "nameDescending"){
            sortedProducts = filteredProducts.OrderByDescending(p => p.Name).ToList();
        }
        else if(sort == "price" || sort == "priceAsc" || sort == "priceAscending"){
            sortedProducts = filteredProducts.OrderBy(p => p.Price).ToList();
        }
       

        IList<Product> pagedFilteredProducts = sortedProducts.Skip(pageSize * (page - 1)).Take(pageSize).ToList();

        ////transfer original pictureUrl to https://6272.....
        for(int i = 0; i < pagedFilteredProducts.Count(); i++){
            if(pagedFilteredProducts[i].PictureUrl != null && pagedFilteredProducts[i].PictureUrl != ""){
                var resolvedPictureUrl = _config["ApiUrl"] + pagedFilteredProducts[i].PictureUrl;
                pagedFilteredProducts[i].PictureUrl = resolvedPictureUrl;
            }
        }
        
        
        object value = new ProductReturnWithPageDto(brandId, typeId, page, pageSize, sortedProducts.Count, pagedFilteredProducts);
        var returnData = value;
        
        
        //////45. Configuring AutoMapper profile
        //return Ok(_mapper.Map<List<Product>, List<ProductToReturnDto>>(pagedFilteredProducts));
        return Ok(returnData);
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
