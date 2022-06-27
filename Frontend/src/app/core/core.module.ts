import { RouterModule } from '@angular/router';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { TestErrorComponent } from './test-error/test-error.component';
import { ServerErrorComponent } from './server-error/server-error.component';
import { NotFoundComponent } from './not-found/not-found.component';

@NgModule({
  declarations: [
    NavbarComponent,
    TestErrorComponent,
    ServerErrorComponent,
    NotFoundComponent
  ],
  imports: [
    CommonModule,
    RouterModule,   ///////110. Setting up the nav links
  ],
  exports: [
    NavbarComponent
  ],
})
export class CoreModule { }
