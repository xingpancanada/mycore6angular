import { ShopService } from './../shop.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: IProduct;
  id?: number;
  quantity: number = 0;

  constructor(
    private activatedRoute: ActivatedRoute,
    private shopService: ShopService
  ) { }

  async ngOnInit(): Promise<void> {
    var paramId = this.activatedRoute.snapshot.paramMap.get('id');
    if(paramId){
      this.id = +paramId;
    }else{
      return;
    }
    this.loadProduct(this.id);
  }

    ///////159. Hooking up the product detail component to the basket
    addItemToBasket(){
      //this.basketService.addItemToBasket(this.product, this.quantity);
    }

    incrementQuantity(){
      this.quantity++;
    }

    decrementQuantity(){
      if(this.quantity > 1){
        this.quantity--;
      }
    }

  loadProduct(id: number){
    this.shopService.getProductByIdService(id).subscribe(resp => {
      this.product = resp;
      console.log(this.product);
    })
  }

}
