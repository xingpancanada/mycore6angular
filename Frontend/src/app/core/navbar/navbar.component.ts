import { AccountService } from './../../account/account.service';
import { BasketService } from './../../basket/basket.service';
import { Observable, ReplaySubject, Subscription } from 'rxjs';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { IBasket } from 'src/app/shared/models/basket';
import { IUser } from 'src/app/shared/models/user';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit, OnDestroy {
  basket$?: Observable<IBasket>;
  basketSub?: Subscription;
  basket?: IBasket;
  user?:IUser;
  userSub?:Subscription;
  itemNumber?: number = 0;

  constructor(
    private basketService: BasketService,
    private accountService: AccountService
  ) {

  }


  ngOnInit() {
    this.userSub = this.accountService.loadCurrentUser().subscribe((resp: any) => {
      console.log(resp);
      this.user = resp;
    });

    //this.currentUser$ = this.accountService.currentUser$;



    this.basketSub = this.basketService.basket$.subscribe(resp => {
      if(resp){
        this.basket = resp;
        if(resp.items){
          this.itemNumber = resp.items.length;
        }else{
          this.itemNumber = 0;
        }
      }
    });
  }

  logout(){
    this.accountService.logout();
  }

  ngOnDestroy(): void {
    if(this.basketSub){
      this.basketSub.unsubscribe();
    }
    if(this.userSub){
      this.userSub.unsubscribe();
    }
  }
}
