import { AccountService } from './account/account.service';
import { BasketService } from './basket/basket.service';
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{

  title = 'Frontend';

  products: any[] = [];

  constructor(
    private http: HttpClient,
    private basketService: BasketService,
    private accountService: AccountService
    ){
      //this.basketService.addBasket();
    }

  // ngOnInit(): void {
  //   // this.http.get(`https://localhost:7262/api/products?pageSize=10&sort=price`).subscribe((resp: any) => {
  //   //   console.log(resp);
  //   //   this.products = resp;
  //   //   console.log(this.products);
  //   // });
  // }

  ngOnInit(): void {
    this.loadBasket();
    this.loadCurrentUser();
  }

  //////195. Persisting the login
  loadCurrentUser(){
    this.accountService.loadCurrentUser();
    // const token = localStorage.getItem('token');
    // if(token){
    //   this.accountService.loadCurrentUser(token).then(resp => {
    //     console.log(resp);
    //   });
    // }else{
    //   console.log('no token');
    // }
  }

  ///////152. Persisting the basket on startup
  loadBasket(){
    // const basketId = localStorage.getItem('basket_id');

    // if(basketId){
      this.basketService.getBasketByLocalId();
    //}
  }
}
