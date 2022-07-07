import { Component, Input, OnInit } from '@angular/core';
import { BasketService } from 'src/app/basket/basket.service';
import { IProduct } from 'src/app/shared/models/product';

@Component({
  selector: 'app-product-item',
  templateUrl: './product-item.component.html',
  styleUrls: ['./product-item.component.scss']
})
export class ProductItemComponent implements OnInit {
  @Input() product?: any;

  constructor(private basketService: BasketService) { }

  ngOnInit(): void {
    //console.log(this.product);
  }

  addItemToBasket(){
    this.basketService.addItemToBasket(this.product);
    //this.basketService.addBasket();
    console.log(this.product);
  }
}
