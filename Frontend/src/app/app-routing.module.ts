import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path:'', component: HomeComponent},
  {path:'test-error', component: TestErrorComponent},
  {path:'server-error', component: ServerErrorComponent},
  {path:'not-found', component: NotFoundComponent},
  {path:'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule)}, //lazy loading
  {path:'basket', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule)}, //lazy loading
  {path:'account', loadChildren: () => import('./account/account.module').then(mod => mod.AccountModule)}, //lazy loading
  {path:'**', redirectTo: 'not-found', pathMatch: 'full'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
