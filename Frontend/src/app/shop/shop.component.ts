import { IPagination } from './../shared/models/pagination';
import { ShopService } from './shop.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { IProduct } from '../shared/models/product';
import { ShopParams } from '../shared/models/shopParams';
import { of } from 'rxjs';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('search', {static: false}) searchTerm?: ElementRef<any>;

  products: IProduct[] = [];
  product?: IProduct;

  page = 1;
  pageSize = 6;
  count = 0;
  brandId = 0;
  typeId = 0;
  sort = 'name';
  search = 'all';

  brands: any[] = [];
  types: any[] = [];

  shopParams?: ShopParams;

  //////97. Adding the sorting functionality
  sortOptions = [
    {name: 'Name', value: 'name'},
    {name: 'Name Desc', value: 'nameDesc'},
    {name: 'Price: Low to High', value: 'priceAsc'},
    {name: 'Price: High to Low', value: 'priceDesc'},
  ]

  constructor(
    private shopService: ShopService,

  ) { }

  async ngOnInit(): Promise<void> {
    await this.getProducts();
    this.getBrands();
    this.getTypes();
  }


  onSearch(){
    this.search = this.searchTerm?.nativeElement.value;
    console.log(this.search);
    this.getProducts();
  }

  onReset(){
    if(this.searchTerm != null || this.searchTerm != undefined){
      this.searchTerm.nativeElement.value = '';
      this.search = 'all';
    }
  }

  onBrandIdSelected(id: number) {
    console.log(id);
    this.brandId = id;
    this.getProducts();
    // const params = this.shopService.getShopParams();
    // params.brandId = brandId;
    // params.pageNumber = 1;  //return to page 1 if already in other page under other search
    // this.shopService.setShopParams(params);
    // this.getProducts();
  }

  onTypeIdSelected(id: number) {
    console.log(id);
    this.typeId = id;
    this.getProducts();
    // const params = this.shopService.getShopParams();
    // params.typeId = typeId;
    // params.pageNumber = 1;
    // this.shopService.setShopParams(params);
    // this.getProducts();
  }

  onSortSelected(event: any) {
    console.log(event.target.value);
    this.sort = event.target.value;
    this.getProducts();
    // const params = this.shopService.getShopParams();
    // params.sort = sort;
    // this.shopService.setShopParams(params);
    // this.getProducts();
  }

  onPageChanged(event: any) {
    console.log(event);
    this.page = event;
    this.getProducts();
    // const params = this.shopService.getShopParams();
    // if (params.pageNumber !== event) {  //avoid double call
    //   params.pageNumber = event;
    //   this.shopService.setShopParams(params);
    //   this.getProducts(true);
    // }
  }

  async getProducts(){
    this.shopService.getProductsService(this.brandId, this.typeId, this.sort, this.pageSize, this.page, this.search).subscribe((resp: any) => {
      console.log(resp);
      if(resp){
        this.products = resp.products;
        this.page = resp.page;
        this.pageSize = resp.pageSize;
        this.count = resp.count;
      }
      // console.log(this.products);
      // console.log(this.page);
      // console.log(this.pageSize);
      // console.log(this.count);
      // console.log(this.brandId);
      // console.log(this.typeId);
      // console.log(this.search);
    });
  }

  getBrands(){
    this.shopService.getBrands().subscribe(resp => {
      //add the first brand is all to resp
      this.brands = [{id: 0, name: 'All'}, ...resp];
      console.log(this.brands);
    });
  }

  getTypes(){
    this.shopService.getProductTypes().subscribe(resp => {
      //add the first brand is all to resp
      this.types = [{id: 0, name: 'All'}, ...resp];
      console.log(this.types);
    });
  }

  getProductById(id: number){
    const product = this.products.find(p => p.id === id);
    if(product){
      //return of(product);
      this.product = product;
    }else{
      this.shopService.getProductByIdService(id).subscribe(resp => {
        this.product = resp;
      });
    }
  }

}
