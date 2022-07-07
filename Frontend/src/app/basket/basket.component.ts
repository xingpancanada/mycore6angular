import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { IBasket, IBasketTotals } from '../shared/models/basket';
import { BasketService } from './basket.service';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent implements OnInit {
  basket$?: Observable<IBasket>;
  basketTotal$?: Observable<IBasketTotals>;

  basket = {} as any;

  constructor(private basketService: BasketService) { }

  async ngOnInit() {
      //////153. Displaying the basket item count in the nav bar
      this.basket$ = this.basketService.basket$;

      this.basketTotal$ = this.basketService.basketTotal$;

      await this.basket$.subscribe(resp => {
        this.basket = resp;
        console.log(resp);

        this.basket.items.forEach(async (item: any) => {
          console.log(item);
          await this.basketService.getProductById(item.productId).subscribe(resp => {
            console.log(resp);
            item.product = resp;
          });
        });
      });
  }

  //////158. Adding the basket compnent functions
  // removeBasketItem(item: IBasketItem) {
  //   this.basketService.removeItemFromBasket(item);
  // }

  // incrementItemQuantity(item: IBasketItem) {
  //   this.basketService.incrementItemQuantity(item);
  // }

  // decrementItemQuantity(item: IBasketItem) {
  //   this.basketService.decrementItemQuantity(item);
  // }

}
