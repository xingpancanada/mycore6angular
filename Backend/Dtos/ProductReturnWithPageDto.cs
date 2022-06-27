using System.Runtime.InteropServices;
using Backend.Entities;

namespace Backend.Dtos
{
    public class ProductReturnWithPageDto
    {
        public ProductReturnWithPageDto(int brandId, int typeId, int page, int pageSize, int count, IEnumerable<Product> products)
        {
            BrandId = brandId;
            TypeId = typeId;
            Page = page;
            PageSize = pageSize;
            Count = count;
            Products = products;
        }

        public int BrandId { get; set; }
        public int TypeId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        
        public int Count { get; set; }
        
        public IEnumerable<Product> Products { get; set; }
    }
}