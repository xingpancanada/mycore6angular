
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBasket, IBasketTotals } from '../shared/models/basket';
import { IProduct } from '../shared/models/product';
import {v4 as uuidv4} from 'uuid';

@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  basket = {} as IBasket;

  private basketBS = new BehaviorSubject<IBasket | any>(null);
  basket$ = this.basketBS.asObservable();

   //////155. Adding the basket totals to the services
   private basketTotalSource = new BehaviorSubject<IBasketTotals | any>(null);
   basketTotal$ = this.basketTotalSource.asObservable();

   shipping = 0;

  constructor(
    private http: HttpClient
  ) { }

  //////150. Adding a item to the basket service method
  async addItemToBasket(p: IProduct, q = 1) {
    var bs : IBasket = await this.addOrGetBasket();
    console.log(bs);

    if(bs){
      if(bs.items?.length > 0){
        const index = await bs.items.findIndex(x => x.productId == p.id);
        console.log(index);
        if(index !== -1){
          await (bs.items[index].quantity += q);
          console.log(bs.items[index]);
        }else{
          bs.items.push({
            productId: p.id,
            product: p,
            quantity: q,
            //id: bs.items.length
          });
        }
      }else{
        bs.items = [];
        bs.items.push({
          productId: p.id,
          product: p,
          quantity: q,
          //id: 0
        });
      }
      console.log('after added item, basket: ', bs);
      this.basketBS.next(bs);
    }else{
      console.log('cannot get the basket!')
    }

    await this.http.put(this.baseUrl + 'Baskets/update', bs).subscribe((resp: any) => {
      console.log('update put basket resp:', resp);
    });

    //await this.getBasketByLocalId();
  }

  async addOrGetBasket():Promise<any> {
    return new Promise(async (resolve, reject) => {
      var localId  = await localStorage.getItem('basket_id');
      if(localId){
        await this.http.get(this.baseUrl + 'Baskets/' + localId).subscribe(async (resp: any) => {
          console.log('get basket by uid resp:', resp);
          if(resp){
            await this.basketBS.next(resp);
            await (this.basket = resp);
            console.log('this.basket: ', this.basket);

            if(this.basket){
              console.log(this.basket);
              await resolve(this.basket);
            }else{
              reject('cannot set this basket');
            }

            //return this.basket;
          }else{
            console.log('Your local basket uid is not using.');
            await localStorage.removeItem('basket_id');
          }
        });

      }else{
        var b = {} as IBasket;
        b.id = uuidv4();
        this.http.post(this.baseUrl + 'Baskets', b).subscribe(async (resp: any) => {
          console.log('add basket resp:', resp);
          if(resp){
            await this.basketBS.next(resp);
            await (b = resp);
            await localStorage.setItem('basket_id', b.id);
            console.log('b', b);
            await (this.basket = b);
            console.log('this.basket: ', this.basket);

            if(this.basket){
              console.log(this.basket);
              await resolve(this.basket);
            }else{
              reject('cannot set this basket');
            }

            //return this.basket;
          }else{
            console.log("Failed to create basket!");
          }
        });
      }

    })
  }

  async addBasket(){
    var localId  = localStorage.getItem('basket_id');
    if(localId){
      this.http.get(this.baseUrl + 'Baskets/' + localId).subscribe((resp: any) => {
        console.log('get basket by uid resp:', resp);
        if(resp){
          this.basketBS.next(resp);
          this.basket = resp;
          console.log('this.basket: ', this.basket);
        }else{
          console.log('Your local basket uid is not using.');
          localStorage.removeItem('basket_id');
        }
      });
    }else{
      var b = {} as IBasket;
      b.id = uuidv4();
      this.http.post(this.baseUrl + 'Baskets', b).subscribe((resp: any) => {
        console.log('add basket resp:', resp);
        if(resp){
          this.basketBS.next(resp);
          b = resp;
          localStorage.setItem('basket_id', b.id);
          console.log('b', b);
          this.basket = b;
          console.log('this.basket: ', this.basket);
        }else{
          console.log("Failed to create basket!");
        }
      });
    }
  }

  async getBasketByLocalId(){
    var localId  = localStorage.getItem('basket_id');

    if(localId){
      this.http.get(this.baseUrl + 'Baskets/' + localId).subscribe((resp: any) => {
        console.log('get basket resp:', resp);
        this.basketBS.next(resp);
      })
    }else{
      return;
    }
  }

  getProductById(id: number){
    return this.http.get(this.baseUrl + 'Products/' + id);
  }

   //////150. Adding a item to the basket service method
  // private createBasket(): IBasket {
  //   const newbasket = new Basket();
  //   this.http.post(this.baseUrl + 'CustomerBasket', newbasket).subscribe((resp: any) => {
  //     console.log('create basket resp:', resp);
  //     if(resp){
  //       //this.basketBS.next(basket);
  //       localStorage.setItem('basket_uid', newbasket.uid);
  //     }else{
  //       console.log("Something was wrong when added basket!");
  //     }
  //   });

  //   this.http.get(this.baseUrl + 'CustomerBasket/' + newbasket.uid).subscribe((resp: any) => {
  //     if(resp){
  //       this.basket = resp;
  //       this.basketBS.next(this.basket);
  //       console.log('this.basket after re-get: ', this.basket);
  //     }
  //   })
  //   return this.basket;
  // }

  // async updateBasket(basket: IBasket){
  //   console.log('for update basket:', basket);
  //   var uid = await localStorage.getItem('basket_uid');
  //   return this.http.put(this.baseUrl + 'CustomerBasket/' + uid, basket).subscribe((resp: any) => {
  //     if(resp){
  //       this.basketBS.next(basket);
  //       //localStorage.setItem('basket_uid', basket.uid);
  //     }else{
  //       console.log("Something was wrong when updated basket!");
  //     }
  //   })
  // }

  // async getBasket(){
  //   var uid = await localStorage.getItem('basket_uid');

  //   this.http.get(this.baseUrl + 'CustomerBasket/' + uid).subscribe(async(resp: any) => {
  //     console.log('get basket resp:', resp);
  //     if(resp){
  //       this.basketBS.next(resp);
  //       //b = resp;
  //       this.basket = resp;
  //       console.log(this.basket);
  //     }else{
  //       console.log("Something was wrong when queried basket!");
  //     }
  //   })
  //   // console.log(b);
  //   return this.basket;
  // }

  // deleteBasket(){
  //   var uid = localStorage.getItem('basket_uid');
  //   return this.http.delete(this.baseUrl + 'CustomerBasket?uid='+ uid).subscribe((resp: any) => {
  //     if(resp){
  //       this.basketBS.next(null);
  //       localStorage.removeItem('basket_uid');
  //     }else{
  //       console.log("Something was wrong when deleted basket!");
  //     }
  //   })
  // }

  //   //////157. Adding the increment and decrement functionality
  //   async incrementItemQuantity(item: IBasketItem) {
  //     //const b = await this.getBasket();

  //     if(this.basket?.items){
  //       const foundItemIndex = this.basket?.items.findIndex((x: any) => x.id === item.id);
  //       this.basket.items[foundItemIndex].quantity++;
  //       this.updateBasket(this.basket);
  //     }
  //   }

  //   //////157. Adding the increment and decrement functionality
  //   async decrementItemQuantity(item: IBasketItem) {
  //     const b = await this.getBasket();
  //     if(this.basket?.items){
  //       const foundItemIndex = this.basket?.items.findIndex((x: any) => x.id === item.id);
  //       if (this.basket?.items[foundItemIndex].quantity > 1) {
  //         this.basket.items[foundItemIndex].quantity--;
  //       } else {
  //         this.removeItemFromBasket(item);
  //       }
  //       this.updateBasket(this.basket);
  //     }
  //   }

  //   //////157. Adding the increment and decrement functionality
  //   async removeItemFromBasket(item: IBasketItem) {
  //     //const basket = await this.getBasket();
  //     if (this.basket?.items.some((x: any) => x.id === item.id)) {
  //       this.basket.items = this.basket.items.filter((i: any) => i.id !== item.id);
  //       if (this.basket?.items.length > 0) {
  //         this.updateBasket(this.basket);
  //       } else {
  //         this.deleteBasket();
  //       }
  //     }
  //   }


  //     //////150. Adding a item to the basket service method
  // async addItemToBasket(item: IProduct, quantity = 1) {
  //   console.log('add item to basket...');
  //   const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(item, quantity);
  //   var uid = await localStorage.getItem('basket_uid');
  //   console.log('uid:', uid);
  //   //var basket: any;
  //   //setTimeout(async() => {
  //     if(uid){
  //       this.basket = await this.getBasket();
  //       console.log(this.basket);
  //       if(this.basket.items?.length > 0){
  //         const index = this.basket.items.findIndex((i: any) => i.id == itemToAdd.id);
  //         if (index === -1) {     //if(!index) -> new item
  //           itemToAdd.quantity = quantity;
  //           this.basket.items.push(itemToAdd);
  //         } else {
  //           this.basket.items[index].quantity += quantity;
  //         }
  //       }else{
  //         this.basket.items = [];
  //         this.basket.items.push(itemToAdd);
  //       }
  //     }
  //     else{
  //       this.basket = await this.createBasket();
  //       console.log(this.basket);
  //       this.basket.items.push(itemToAdd);
  //     }

  //     //basket.items = await this.addOrUpdateItem(basket.items, itemToAdd, quantity);
  //     console.log('basket.items:', this.basket.items);
  //     await this.updateBasket(this.basket);
  //   //}, 1000);

  // }

  // //////150. Adding a item to the basket service method
  // private addOrUpdateItem(items: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {

  //   const index = items.findIndex(i => i.id === itemToAdd.id);
  //   if (index === -1) {     //if(!index) -> new item
  //     itemToAdd.quantity = quantity;
  //     items.push(itemToAdd);
  //   } else {
  //     items[index].quantity += quantity;
  //   }

  //   return items;
  // }


  // //////150. Adding a item to the basket service method
  // private mapProductItemToBasketItem(item: IProduct, quantity: number): IBasketItem {
  //   return {
  //     id: item.id,
  //     productName: item.name,
  //     price: item.price,
  //     pictureUrl: item.pictureUrl,
  //     quantity,
  //     brand: item.productBrand,
  //     type: item.productType
  //   }
  // }
}
