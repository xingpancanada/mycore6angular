import { IProduct } from './../shared/models/product';
import { IPagination } from './../shared/models/pagination';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7262/api/';

  products: IProduct[] = [];
  brands: any[]= [];
  types: any[] = [];

  constructor(private http: HttpClient){}

  p? = 1;
  pz? = 100;
  s? = 'name';
  b?: number = 0;
  t?: number = 0;
  sc? = 'all';


  ////public async Task<ActionResult<List<Product>>> GetProducts(int? brandId, int? typeId, string sort = "name", int pageSize = 10, int page = 1){}

  getProductsService(brandId?:number, typeId?:number, sort?:string, pageSize?: number, page?: number, search?: string){
    if(page !== null && page !== undefined){
      this.p = page;
    }
    if(pageSize !== null && pageSize !== undefined){
      this.pz = pageSize;
    }
    if(sort !== null && sort !== undefined){
      this.s = sort;
    }
    if(brandId !== null && brandId !== undefined){
      this.b = brandId;
    }
    if(typeId !== null && typeId !== undefined){
      this.t = typeId;
    }
    if(search !== null && search !== undefined){
      this.sc = search;
    }

    return this.http.get<any[]>(this.baseUrl + `products?brandid=${this.b}&typeid=${this.t}&sort=${this.s}&pageSize=${this.pz}&page=${this.p}&search=${this.sc}`);
  }

  getProductByIdService(id: number){
    return this.http.get<IProduct>(this.baseUrl + 'products/' + id);
  }


  getBrands(){
    if(this.brands.length > 0){
      return of(this.brands);
    }
    // return this.http.get<IBrand[]>(this.baseUrl + 'products/brands');
    return this.http.get<any[]>(this.baseUrl + 'products/brands').pipe(
      map(response => {
        this.brands = response;
        console.log('brand response:', response);
        //console.log('brand response data:', response);  //wrong!!! response is already the data!!!!!!
        return response;
      })
    )
  }

  getProductTypes(){
    if(this.types.length > 0){
      return of(this.types);
    }
    // return this.http.get<IProductType[]>(this.baseUrl + 'products/types');
    return this.http.get<any[]>(this.baseUrl + 'products/types').pipe(
      map(response => {
        this.types = response;
        return response;
      })
    )
  }

}
